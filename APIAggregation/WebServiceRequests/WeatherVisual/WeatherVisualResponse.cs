namespace APIAggregation.WebServiceRequests.Sera
{
    /// <summary>
    /// The reponse of Weather Visual
    /// </summary>
    public class WeatherVisualResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public float latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Currentconditions currentConditions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Day[] days { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Currentconditions
    {
        /// <summary>
        /// 
        /// </summary>
        public float temp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float feelslike { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Day
    {
        /// <summary>
        /// 
        /// </summary>
        public float tempmax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float tempmin { get; set; }
    }
}