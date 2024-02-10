namespace APIAggregation.WebServiceRequests.OpenWeatherMap
{
    /// <summary>
    /// The response that returns the Service of Open Weather
    /// </summary>
    public class OpenWeatherMapResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Coord Coord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Weather[] Weather { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Base { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Main Main { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Visibility { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Wind Wind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Rain Rain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Clouds? Clouds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Dt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Sys? Sys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Timezone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Cod { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// 
        /// </summary>
        public float Lon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Lat { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Main
    {
        /// <summary>
        /// 
        /// </summary>
        public float Temp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Feels_like { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Temp_min { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Temp_max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Pressure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Humidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sea_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Grnd_level { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// 
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Deg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Gust { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Rain
    {
        /// <summary>
        /// 
        /// </summary>
        public float _1h { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Clouds
    {
        /// <summary>
        /// 
        /// </summary>
        public int All { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sunrise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sunset { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Main { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Icon { get; set; }
    }
}