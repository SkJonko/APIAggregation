using Newtonsoft.Json;

namespace APIAggregation.WebServiceRequests.Joke
{
    /// <summary>
    /// Joke Response
    /// </summary>
    public class JokeApi
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Setup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Delivery { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Flags? Flags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Safe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Lang { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Flags
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Nsfw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Religious { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Political { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Racist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Sexist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Explicit { get; set; }
    }
}