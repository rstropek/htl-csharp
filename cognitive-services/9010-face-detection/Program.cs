using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Pens;
using SixLabors.Primitives;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FaceRecognition
{
    static class Program
    {
        const string subscriptionKey = "9f6.............................";
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Missing arguments. Please specify source and target file name.");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine($"Invalid argument. '{args[0]}' does not exist.");
                return;
            }

            var imageBytes = ReadImageAsByteArrayAsync(args[0]).Result;
            var faces = PerformFaceDetectionAsync(imageBytes).Result;
            using (var image = AddFaceRectangles(imageBytes, faces))
            {
                image.SaveAsJpeg(File.OpenWrite(args[1]));
            }
        }

        static async Task<byte[]> ReadImageAsByteArrayAsync(string imageFilePath)
        {
            using (var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        static async Task<JArray> PerformFaceDetectionAsync(byte[] imageBytes)
        {
            // Read the Web API Reference Documentation for the face detection API
            // at https://docs.microsoft.com/en-us/azure/cognitive-services/face/apireference

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                var uri = uriBase + "?returnFaceId=true";
                using (var content = new ByteArrayContent(imageBytes))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var response = await client.PostAsync(uri, content);
                    var contentString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JArray>(contentString);
                }
            }
        }

        static Image<Rgba32> AddFaceRectangles(byte[] imageBytes, JArray faces)
        {
            var image = Image.Load<Rgba32>(imageBytes);
            var pen = new Pen<Rgba32>(Rgba32.Red, 5);

            image.Mutate(ctx =>
            {
                foreach (dynamic face in faces)
                {
                    dynamic faceRect = face.faceRectangle;
                    var rect = new
                    {
                        left = (float)faceRect.left,
                        top = (float)faceRect.top,
                        right = (float)(faceRect.left + faceRect.width),
                        bottom = (float)(faceRect.top + faceRect.height)
                    };

                    ctx.DrawPolygon(pen, new[]
                    {
                            new PointF(rect.left, rect.top), new PointF(rect.right, rect.top),
                            new PointF(rect.right, rect.bottom), new PointF(rect.left, rect.bottom),
                            new PointF(rect.left, rect.top)
                    });
                }
            });

            return image;
        }
    }
}
