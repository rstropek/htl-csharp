namespace TravelPlanner.Logic
{
    public class Trip
    {
        public string Leave { get; set; }
        public string Arrive { get; set; }
    }

    public class Route
    {
        public string City { get; set; }
        public Trip[] ToLinz { get; set; }
        public Trip[] FromLinz { get; set; }
    }

    public class TripWithLocations : Trip
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
    }
}
