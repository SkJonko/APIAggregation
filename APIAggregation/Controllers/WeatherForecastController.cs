using APIAggregation.Services.Weather;
using APIAggregation.WebServiceRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace APIAggregation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        #region Privates

        private readonly IEnumerable<IWeatherServices> _weatherServices;

        #endregion


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="weatherServices"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherForecastController(IEnumerable<IWeatherServices> weatherServices)
        {
            _weatherServices = weatherServices ?? throw new ArgumentNullException(nameof(weatherServices));
        }


        /// <summary>
        /// The Weather that has when you search for city
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Application Exception of Endpoint</exception>
        [HttpGet]
        [Route("CityWeather")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCityWeatherForecastResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
        public async Task<GetCityWeatherForecastResponse> GetCityWeather([FromQuery, Required] GetWeatherForecastLatitudeLongitudeRequest request, CancellationToken cancellationToken)
        {
            var weatherService = _weatherServices.FirstOrDefault(x => x.WeatherServiceId == request.Service) ?? throw new Exception("Invalid Weather ID");

            return await weatherService.RetrieveWeather(request.City, request.Unit, cancellationToken);
        }

        /// <summary>
        /// The Weather that has when you search for latitude and longitude
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Application Exception of Endpoint</exception>
        [HttpGet]
        [Route("LatitudeLongitudeWeather")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCityWeatherForecastResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
        public async Task<GetCityWeatherForecastResponse> GetWeatherForecastLatitudeLongitude([FromQuery] WeatherForecastLatitudeLongtitudeRequest request, CancellationToken cancellationToken)
        {
            var weatherService = _weatherServices.FirstOrDefault(x => x.WeatherServiceId == request.Service) ?? throw new Exception("Invalid Weather ID");

            return await weatherService.RetrieveWeather(request.Latitude, request.Longitude, request.Unit, cancellationToken);
        }
    }
}
