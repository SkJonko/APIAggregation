using System.ComponentModel.DataAnnotations;

namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWeatherForecastLatitudeLongitudeRequest : WeatherForecastRequest
    {
        /// <summary>
        /// The City
        /// </summary>
        [Required]
        public string City { get; set; }
    }
}
