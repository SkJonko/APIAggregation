using System.ComponentModel.DataAnnotations;

namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// 
    /// </summary>
    public class WeatherForecastLatitudeLongtitudeRequest : WeatherForecastRequest
    {
        /// <summary>
        /// The latitude that you want to search
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude that you want to search
        /// </summary>
        [Required]
        public double Longitude { get; set; }
    }
}
