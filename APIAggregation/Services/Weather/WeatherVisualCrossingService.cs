using APIAggregation.Helpers;
using APIAggregation.Models;
using APIAggregation.WebServiceRequests;
using APIAggregation.WebServiceRequests.Sera;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.Services.Weather
{

    /// <summary>
    /// https://www.visualcrossing.com/resources/documentation/weather-api/timeline-weather-api/
    /// https://www.visualcrossing.com/weather/weather-data-services
    /// </summary>
    public class WeatherVisualCrossingService : IWeatherServices
    {

        #region PRIVATES

        /// <summary>
        /// The HTTP Client Factory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Memory cache Service
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Configuration of Project
        /// </summary>
        private readonly ConfigurationSettings _configurationSettings;

        /// <summary>
        /// The Endpoint Service Configuration
        /// </summary>
        private readonly AppEndpoint appEndpoint;

        /// <summary>
        /// The configuration Weather service ID
        /// </summary>
        public int WeatherServiceId => 2;

        #endregion


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="cache"></param>
        /// <param name="configurationSettings"></param>
        public WeatherVisualCrossingService(IHttpClientFactory httpClientFactory, IMemoryCache cache, IOptions<ConfigurationSettings> configurationSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configurationSettings = configurationSettings.Value ?? throw new ArgumentNullException(nameof(configurationSettings));


            appEndpoint = _configurationSettings.weatherServicesModel.AppEndpoints.First(x => x.Id == WeatherServiceId);
        }


        /// <summary>
        /// Retrieve weather from City.
        /// </summary>
        /// <param name="city">The city name that you want to search</param>
        /// <param name="unit">The unit</param>
        /// <param name="cancellationToken">The cancellation Token.</param>
        /// <returns>The model of weather that returns if the city exists. Response is cached for X minutes refer appsettings</returns>
        public async Task<GetCityWeatherForecastResponse?> RetrieveWeather(string city, WeatherUnit unit, CancellationToken cancellationToken = default) =>
            await _cache.GetOrCreateAsync($"{city}{unit}{appEndpoint.Code}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appEndpoint.Cache);
                return await GetWeather($"VisualCrossingWebServices/rest/services/timeline/{city}", unit, cancellationToken);
            });


        /// <summary>
        /// Retrieve Weather from latitude and longitude
        /// </summary>
        /// <param name="lat">The latitude that you want to search</param>
        /// <param name="lon">The longitude that you want to search</param>
        /// <param name="unit">The unit</param>
        /// <param name="cancellationToken">The cancellation Token.</param>
        /// <returns>The model of weather that returns if the cthis long and lat exists. Response is cached for X minutes refer appsettings</returns>
        public async Task<GetCityWeatherForecastResponse?> RetrieveWeather(double lat, double lon, WeatherUnit unit, CancellationToken cancellationToken = default) =>
            await _cache.GetOrCreateAsync($"{lat}{lon}{unit}{appEndpoint.Code}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appEndpoint.Cache);
                return await GetWeather($"VisualCrossingWebServices/rest/services/timeline/{lat},{lon}", unit, cancellationToken);
            });


        #region PRIVATE METHODS

        /// <summary>
        /// Returns the Open Weather Model.
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="unit">The unit</param>
        /// <param name="cancellationToken"></param>
        private async Task<GetCityWeatherForecastResponse?> GetWeather(string url, WeatherUnit unit, CancellationToken cancellationToken = default)
        {
            var unitMetric = unit == WeatherUnit.C ? "metric" : "us";
            return (await _httpClientFactory.ExecuteRequest<WeatherVisualResponse>(
                HttpMethod.Get, 
                $"{appEndpoint.BaseAddress}{url}?unitGroup={unitMetric}&key={appEndpoint.AppKey}&contentType=json", 
                HttpClients.WeatherVisual,
                cancellationToken: cancellationToken))!.ToWeatherResponseModel();
        }

        #endregion
    }
}
