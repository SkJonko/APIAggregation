using APIAggregation.Services.Joke;
using APIAggregation.WebServiceRequests;
using APIAggregation.WebServiceRequests.Joke;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace APIAggregation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JokeController : Controller
    {

        #region Privates

        private readonly JokeService _jokeService;

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="jokeService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public JokeController( JokeService jokeService)
        {
            _jokeService = jokeService ?? throw new ArgumentNullException(nameof(jokeService));
        }

        /// <summary>
        /// Returns a Joke 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetJoke")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JokeApi))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
        public async Task<JokeApi> GetJoke([FromBody, Required] GetJokeRequest request)
        {
            return await _jokeService.GetJoke(GetStringValueFromFlags(request.Categories), request.Language);
        }


        /// <summary>
        /// Retrieve the name of each Enum of flags.
        /// </summary>
        /// <typeparam name="T">The enum type that you want to retrieve the strings.</typeparam>
        /// <param name="flags">The enum Flags</param>
        /// <returns></returns>
        static List<string> GetStringValueFromFlags<T>(T flags)
            where T : Enum
        {
            List<string> results = [];

            if (Convert.ToInt32(flags) == 0)
            {
                results.Add(flags.ToString());
                return results;
            }

            // Iterate through each flag value
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if ((flags as Enum).HasFlag(value as Enum) && Convert.ToInt32(value) != 0)
                    results.Add(value.ToString());
            }

            return results;
        }
    }
}
