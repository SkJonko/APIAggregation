using APIAggregation.WebServiceRequests;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static APIAggregation.Helpers.Enum;

namespace APIAggregation.Helpers
{
    /// <summary>
    /// The Middleware
    /// </summary>
    public class HandlingMiddleware
    {

        #region PARAMETERS

        /// <summary>
        /// 
        /// </summary>
        public RequestDelegate requestDelegate;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<HandlingMiddleware> logger;

        #endregion PARAMETERS


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDelegate"></param>
        /// <param name="logger"></param>
        public HandlingMiddleware(RequestDelegate requestDelegate, ILogger<HandlingMiddleware> logger)
        {
            this.requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await HandleEnterLog(context.Request);

                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        /// <summary>
        /// Method to Handle the Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleException(HttpContext context, Exception ex)
        {
            logger.LogError(ex, "ERROR API Middleware Exception {ExceptionMessage}", ex.Message);

            var errorMessageObject = new ApiBaseResponse();

            errorMessageObject.SignalFailure(ErrorCodes.SystemError, ex.Message, context?.TraceIdentifier);

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            return context.Response.WriteAsync(errorMessage);
        }

        /// <summary>
        /// Method to Handle the Enter of Each Call
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task HandleEnterLog(HttpRequest request)
        {
            var logMessage = await GetRequestBodyAsync(request);

            logger.LogInformation("ENTER API {endpointRequest}", logMessage);
        }

        /// <summary>
        /// Retrieve the Body Request to log it
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<Dictionary<string, string>> GetRequestBodyAsync(HttpRequest request)
        {
            Dictionary<string, string> parameters = new();

            // enable reading the body stream multiple times
            request.EnableBuffering();

            using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);

            // read the request body
            var body = await reader.ReadToEndAsync();

            // reset the stream position to the beginning
            request.Body.Position = 0;

            parameters.Add("Schema", request.Scheme);
            parameters.Add("Host", request.Host.ToString());
            parameters.Add("Path", request.Path);
            parameters.Add("QueryString", request.QueryString.ToString());
            parameters.Add("Body", body);

            return parameters;
        }
    }
}
