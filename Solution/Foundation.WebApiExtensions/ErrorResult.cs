namespace ActiveDirectoryBlogPost.WebApiExtensions
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// Represents an Error result with information for the client. 
    /// </summary>
    /// <seealso cref="IHttpActionResult" />
    public class ErrorResult : IHttpActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// </summary>
        /// <param name="userMessage">The human readable message for the user.</param>
        /// <param name="internalMessage">The internal error message.</param>
        /// <param name="info">Additional information for the user.</param>
        public ErrorResult(string userMessage, string internalMessage, string info = "")
            : this(HttpStatusCode.InternalServerError, userMessage, internalMessage, info)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the request.</param>
        /// <param name="userMessage">The human readable message for the user.</param>
        /// <param name="internalMessage">The internal error message.</param>
        /// <param name="info">Additional information for the user.</param>
        public ErrorResult(HttpStatusCode statusCode, string userMessage, string internalMessage, string info = "")
        {
            this.StatusCode = statusCode;
            this.UserMessage = userMessage;
            this.InternalMessage = internalMessage;
            this.Info = info;
        }

        /// <summary>
        /// Gets or sets the http request.
        /// </summary>
        /// <value>
        /// The http request.
        /// </value>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        /// <value>
        /// The http status code.
        /// </value>
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

        /// <summary>
        /// Gets the human readable user message.
        /// </summary>
        /// <value>
        /// The human readable user message.
        /// </value>
        public string UserMessage { get; }

        /// <summary>
        /// Gets the internal error message.
        /// </summary>
        /// <value>
        /// The internal error message.
        /// </value>
        public string InternalMessage { get; }

        /// <summary>
        /// Gets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string Info { get; }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // create result with necessary information
            var result = new { this.StatusCode, this.UserMessage, this.InternalMessage, this.Info };

            // create response
            var response = new HttpResponseMessage(this.StatusCode)
            {
                Content = new ObjectContent(result.GetType(), result, new JsonMediaTypeFormatter()),
                RequestMessage = this.Request
            };

            return await Task.FromResult(response);
        }
    }
}
