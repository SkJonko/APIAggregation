using System.ComponentModel.DataAnnotations;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// The weather forecast request Base
    /// </summary>
    public class WeatherForecastRequest
    {
        /// <summary>
        /// The UNIT that you want to search
        /// </summary>
        [Required]
        public WeatherUnit Unit { get; set; }

        /// <summary>
        /// The Service of weather to search
        /// </summary>
        [Required]
        public int Service { get; set; }
    }
}
