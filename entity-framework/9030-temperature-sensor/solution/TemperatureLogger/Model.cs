using System;

namespace TemperatureLogger
{
    // Required exercise: Create model classes for Entity Framework
    public class TemperatureReading
    {
        public int ID { get; set; }

        public DateTime MeasureDateTime { get; set; }

        public double Temperature { get; set; }
    }

    public class Alert
    {
        public int ID { get; set; }

        public string Message { get; set; }

        public TemperatureReading TemperatureReading { get; set; }
    }
}
