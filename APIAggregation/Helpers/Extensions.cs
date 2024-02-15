using APIAggregation.Services.Weather;
using APIAggregation.WebServiceRequests.OpenWeatherMap;
using APIAggregation.WebServiceRequests;
using Microsoft.Extensions.Options;
using APIAggregation.WebServiceRequests.Sera;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using System.Reflection;
using APIAggregation.Services.Joke;

namespace APIAggregation.Helpers
{
    /// <summary>
    /// Extensions to use in the application
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Method to create the Swagger information
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"APIAggregation ({Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} @ {Environment.MachineName} v{typeof(Program).Assembly.GetName().Version})",
                    Description = "Simple API Aggregation",
                    Version = "1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Kotis Antonis",
                        Email = "antkotis@hotmail.com"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            return services;
        }

        /// <summary>
        /// Add Services in DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesInDI(this IServiceCollection services, ConfigurationSettings configurationSettings)
        {
            services.AddSingleton(Options.Create(configurationSettings));

            services.AddScoped<IWeatherServices, OpenWeatherMapService>();
            services.AddScoped<IWeatherServices, WeatherVisualCrossingService>();

            services.AddScoped<JokeService>();

            services.AddHttpClient(HttpClients.OpenWeather);
            services.AddHttpClient(HttpClients.WeatherVisual);
            services.AddHttpClient(HttpClients.JokeApi);

            services.AddMemoryCache();

            return services;
        }


        #region HttpClientFactory

        /// <summary>
        /// Execute request and returns you string
        /// </summary>
        /// <param name="httpClient">The HTTP Client</param>
        /// <param name="httpMethod">The HTTP Method</param>
        /// <param name="uri">The URI</param>
        /// <param name="httpClientName">The HTTP Client Name</param>
        /// <param name="request">The request if exists</param>
        /// <returns></returns>
        public static async Task<string> ExecuteRequest(this IHttpClientFactory httpClient, HttpMethod httpMethod, string uri, string httpClientName, StringContent? request = null)
            => await httpClient.ExecuteRequestPrivate(httpMethod, uri, httpClientName, request);

        /// <summary>
        /// Execute request and returns you the model T
        /// </summary>
        /// <param name="httpClient">The HTTP Client</param>
        /// <param name="httpMethod">The HTTP Method</param>
        /// <param name="uri">The URI</param>
        /// <param name="httpClientName">The HTTP Client Name</param>
        /// <param name="request">The request if exists</param>
        /// <returns></returns>
        public static async Task<T?> ExecuteRequest<T>(this IHttpClientFactory httpClient, HttpMethod httpMethod, string uri, string httpClientName, StringContent? request = null)
            => JsonConvert.DeserializeObject<T?>(await httpClient.ExecuteRequestPrivate(httpMethod, uri, httpClientName, request));

        /// <summary>
        /// The private Method to do the HTTP Call.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="httpMethod"></param>
        /// <param name="uri"></param>
        /// <param name="httpClientName"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<string> ExecuteRequestPrivate(this IHttpClientFactory httpClient, HttpMethod httpMethod, string uri, string httpClientName, StringContent? request = null)
        {
            try
            {
                var req = new HttpRequestMessage()
                {
                    Content = request,
                    Method = httpMethod,
                    RequestUri = new Uri(uri)
                };

                using var client = httpClient.CreateClient(httpClientName);

                using var result = await client.SendAsync(req);

                var stringResponse = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                    return stringResponse;
                else
                    throw new Exception($"{result.StatusCode} {stringResponse}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion HttpClientFactory


        #region Model Extensions

        /// <summary>
        /// Extensions that converts the response of Open Weather to response Model
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static GetCityWeatherForecastResponse ToWeatherResponseModel(this OpenWeatherMapResponse? response)
            => new()
            {
                Temeprature = new()
                {
                    Temp = response!.Main.Temp,
                    FeelsLike = response!.Main.Feels_like,
                    Max = response!.Main.Temp_max,
                    Min = response!.Main.Temp_min
                },
                Coord = new()
                {
                    Latitude = response!.Coord.Lat,
                    Longitude = response!.Coord.Lon
                }
            };

        /// <summary>
        /// Extensions that converts the response of Weather Visaul to response Model
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static GetCityWeatherForecastResponse ToWeatherResponseModel(this WeatherVisualResponse? response)
            => new()
            {
                Temeprature = new()
                {
                    Temp = response!.currentConditions.temp,
                    FeelsLike = response!.currentConditions.feelslike,
                    Max = response!.days.FirstOrDefault()!.tempmax,
                    Min = response!.days.FirstOrDefault()!.tempmin
                },
                Coord = new()
                {
                    Latitude = response!.latitude,
                    Longitude = response!.longitude
                }
            };

        #endregion
    }
}
