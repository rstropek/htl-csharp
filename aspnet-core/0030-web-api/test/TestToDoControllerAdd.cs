using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test {
    public partial class TestToDoController {
        [TestMethod]
        public async Task TestAdd()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/todo-items")
            {
                Content = new StringContent("\"Do something\"", Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(message);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.Headers.Location.PathAndQuery.Contains("/api/todo-items/"));
        }
    }
}
