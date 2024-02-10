namespace APIAggregation.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ServicesModel
    {

        /// <summary>
        /// 
        /// </summary>
        public ServicesModel()
        {
            AppEndpoints = [];
        }

        /// <summary>
        /// AppEndpoints
        /// </summary>
        public List<AppEndpoint> AppEndpoints { get; set; }
    }
    

    /// <summary>
    /// 
    /// </summary>
    public sealed class AppEndpoint
    {
        /// <summary>
        /// The code of Service
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// The ID of Service
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Base Address of Service
        /// </summary>
        public required string BaseAddress { get; set; }

        /// <summary>
        /// The app key if contains
        /// </summary>
        public required string AppKey { get; set; }

        /// <summary>
        /// The cache timne if has
        /// </summary>
        public int Cache { get; set; }
    }
}