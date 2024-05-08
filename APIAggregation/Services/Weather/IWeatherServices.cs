using APIAggregation.Helpers;
using APIAggregation.WebServiceRequests;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.Services.Weather
{
    /// <summary>
    /// Retrieve 
    /// </summary>
    public interface IWeatherServices
    {
        /// <summary>
        /// The Weather Service ID
        /// </summary>
        int WeatherServiceId { get; }

        /// <summary>
        /// Retrieve weather from City.
        /// </summary>
        /// <param name="city">The city name that you want to search</param>
        /// <param name="unit">The unit</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The model of weather that returns if the city exists. Response is cached for X minutes refer appsettings</returns>
        Task<GetCityWeatherForecastResponse?> RetrieveWeather(string city, WeatherUnit unit, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve Weather from latitude and longitude
        /// </summary>
        /// <param name="lat">The latitude that you want to search</param>
        /// <param name="lon">The longitude that you want to search</param>
        /// <param name="unit">The unit</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The model of weather that returns if the cthis long and lat exists. Response is cached for X minutes refer appsettings</returns>
        Task<GetCityWeatherForecastResponse?> RetrieveWeather(double lat, double lon, WeatherUnit unit, CancellationToken cancellationToken = default);
    }
}
