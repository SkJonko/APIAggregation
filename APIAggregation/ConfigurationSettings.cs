using APIAggregation.Models;

namespace APIAggregation
{
    /// <summary>
    /// The configuration file.
    /// </summary>
    public class ConfigurationSettings
    {

        #region PRIVATE

        /// <summary>
        /// The Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        #endregion PRIVATE


        /// <summary>
        /// The Weather Services
        /// </summary>
        public readonly ServicesModel weatherServicesModel = new();

        /// <summary>
        /// The Joke Services
        /// </summary>
        public readonly ServicesModel jokeServicesModel = new();


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _configuration.Bind("WeatherServices", weatherServicesModel);
            _configuration.Bind("JokeServices", jokeServicesModel);
        }
    }
}