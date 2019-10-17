using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TravelPlanner.Logic;

namespace TravelPlanner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var routes = JsonSerializer.Deserialize<Route[]>(await File.ReadAllTextAsync("travelPlan.json"));

            var finder = new ConnectionFinder(routes);
            var trip = finder.FindConnection(args[0], args[1], args[2]);
            if (trip == null)
            {
                Console.WriteLine("Sorry, no connection.");
                return;
            }

            Console.WriteLine($"Leave {trip.FromCity} at {trip.Leave}, arrive in {trip.ToCity} at {trip.Arrive}.");
        }
    }
}
