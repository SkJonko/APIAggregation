namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// The weather reasponse
    /// </summary>
    public class GetCityWeatherForecastResponse
    {
        /// <summary>
        /// The coord of City
        /// </summary>
        public Coord Coord { get; set; }

        /// <summary>
        /// The temperature information
        /// </summary>
        public Temeprature Temeprature { get; set; }
    }

    /// <summary>
    /// The temperature that API Returns
    /// </summary>
    public class Temeprature
    {
        /// <summary>
        /// The temperature that has.
        /// </summary>
        public float Temp { get; set; }

        /// <summary>
        /// How the temperature is feels like
        /// </summary>
        public float FeelsLike { get; set; }

        /// <summary>
        /// Min Temperature
        /// </summary>
        public float Min { get; set; }

        /// <summary>
        /// Max temperature
        /// </summary>
        public float Max { get; set; }
    }

    /// <summary>
    /// The coord
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// The longitude of the area that you are looking
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// The latitudeof the area that you are looking
        /// </summary>
        public float Latitude { get; set; }
    }
}