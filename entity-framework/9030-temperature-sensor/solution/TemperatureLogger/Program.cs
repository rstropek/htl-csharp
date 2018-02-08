using System;
using System.Threading;

namespace TemperatureLogger
{
    class Program
    {
        static void Main()
        {
            var sensor = new TemperatureSensor();
            using (var db = new TemperatureContext())
            {
                // Clean database before importing data (optional, just to make testing easier)
                db.CleanupTargetDatbaseAsync().Wait();

                while (!Console.KeyAvailable)
                {
                    var temperature = sensor.GetCurrentTemperatureAsync().Result;
                    db.StoreTemperatureAsync(temperature).Wait();
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
