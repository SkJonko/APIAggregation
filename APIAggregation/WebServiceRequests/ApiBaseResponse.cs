using static APIAggregation.Helpers.Enum;

namespace APIAggregation.WebServiceRequests
{
    /// <summary>
    /// The API Base Response Model
    /// <inheritdoc/>
    /// </summary>
    public class ApiBaseResponse
    {
        /// <summary>
        /// Denotes the error message returned by the web service method.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Denotes the Trace Id that you get the error.
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Denotes the error code returned by the web service method.
        /// </summary>
        public int ErrorCode { get; set; }


        /// <summary>
        /// setup base response on failure
        /// </summary>
        /// <param name="errorCode"></param>
        public void SignalFailure(ErrorCodes errorCode)
        {
            SignalFailure(errorCode, null, null);
        }


        /// <summary>
        /// setup base response on failure
        /// </summary>
        /// <param name="errorCode">The input <see cref="ErrorCodes"/> code</param>
        /// <param name="errorMessage">The optional message</param>
        /// <param name="traceId">The traceId of request</param>
        public void SignalFailure(ErrorCodes errorCode, string? errorMessage, string? traceId)
        {
            ErrorCode = (int)errorCode;
            ErrorMessage = (string.IsNullOrEmpty(errorMessage) || string.IsNullOrWhiteSpace(errorMessage)) ? errorCode.ToString() : errorMessage;
            TraceId = (string.IsNullOrEmpty(traceId) || string.IsNullOrWhiteSpace(traceId)) ? errorCode.ToString() : traceId;
        }
    }
}