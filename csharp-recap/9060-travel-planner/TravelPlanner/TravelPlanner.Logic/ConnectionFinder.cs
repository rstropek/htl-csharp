using System.Collections.Generic;
using System.Linq;

namespace TravelPlanner.Logic
{
    public class ConnectionFinder
    {
        private readonly IEnumerable<Route> routes;

        public ConnectionFinder(IEnumerable<Route> routes)
        {
            this.routes = routes;
        }

        public TripWithLocations FindConnection(string from, string to, string startTime)
        {
            if (from == "Linz" || to == "Linz")
            {
                IEnumerable<Trip> trips;
                if (from == "Linz")
                {
                    trips = routes.First(r => r.City == to).FromLinz;
                }
                else
                {
                    trips = routes.First(r => r.City == from).ToLinz;
                }

                var trip = trips.FirstOrDefault(t => t.Leave.CompareTo(startTime) >= 0);
                if (trip != null)
                {
                    return new TripWithLocations
                    {
                        FromCity = from,
                        ToCity = to,
                        Leave = trip.Leave,
                        Arrive = trip.Arrive
                    };
                }

                return null;
            }
            else
            {
                var tripToLinz = FindConnection(from, "Linz", startTime);
                if (tripToLinz == null)
                {
                    return null;
                }

                var tripToDestination = FindConnection("Linz", to, tripToLinz.Arrive);
                if (tripToDestination != null)
                {
                    return new TripWithLocations
                    {
                        FromCity = from,
                        ToCity = to,
                        Leave = tripToLinz.Leave,
                        Arrive = tripToDestination.Arrive
                    };
                }

                return null;
            }
        }
    }
}
