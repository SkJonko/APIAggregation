using System.ComponentModel.DataAnnotations;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// The Joke request Web Service Contract
    /// </summary>
    public class GetJokeRequest
    {
        /// <summary>
        /// The categories ID that you want to search or filter
        /// </summary>
        [Required]
        public Categories Categories { get; set; }

        /// <summary>
        /// The Language that you want to search the Joke
        /// </summary>
        [Required]
        public Languages Language { get; set; }
    }
}
