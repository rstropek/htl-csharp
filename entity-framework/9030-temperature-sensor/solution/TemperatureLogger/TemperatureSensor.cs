using System;
using System.Threading.Tasks;

// Note: This file is given for the exam; students do not have to code this.

namespace TemperatureLogger
{
    /// <summary>
    /// Represents a temperature sensor
    /// </summary>
    public class TemperatureSensor
    {
        private Random random = new Random();

        /// <summary>
        /// Get current temperature asynchronously
        /// </summary>
        /// <returns>
        /// Task whose result represents the current temperature
        /// </returns>
        public async Task<double> GetCurrentTemperatureAsync()
        {
            await Task.Delay(10);
            return random.NextDouble() * 60d - 20d;
        }
    }
}
