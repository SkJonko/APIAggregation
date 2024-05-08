using APIAggregation.Helpers;
using APIAggregation.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using APIAggregation.WebServiceRequests.Joke;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.Services.Joke
{
    /// <summary>
    /// https://jokeapi.dev/
    /// </summary>
    public class JokeService
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

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="cache"></param>
        /// <param name="configurationSettings"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public JokeService(IHttpClientFactory httpClientFactory, IMemoryCache cache, IOptions<ConfigurationSettings> configurationSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configurationSettings = configurationSettings.Value ?? throw new ArgumentNullException(nameof(configurationSettings));


            appEndpoint = _configurationSettings.jokeServicesModel.AppEndpoints.First(x => x.Id == 1);
        }

        /// <summary>
        /// Method to return Joke based on the language and the categories
        /// </summary>
        /// <param name="categories">The categories that you want to search for</param>
        /// <param name="language">The language that you want to retrieve the information</param>
        /// <param name="cancellationToken">The Cancellation Token</param>
        /// <returns></returns>
        public async Task<JokeApi?> GetJoke(List<string> categories, Languages language, CancellationToken cancellationToken = default) =>
            await _cache.GetOrCreateAsync($"{string.Join(",", categories)}{language}{appEndpoint.Code}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appEndpoint.Cache);
                return await _httpClientFactory.ExecuteRequest<JokeApi>(HttpMethod.Get, $"{appEndpoint.BaseAddress}{string.Join(",", categories)}?lang={language}&type=twopart", HttpClients.JokeApi, cancellationToken: cancellationToken);
            });
    }
}