namespace ActiveDirectoryBlogPost.WebApiExtensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// The API controller extension.
    /// </summary>
    public static class ApiControllerExtension
    {
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="entryPoint">The entry point.</param>
        /// <returns>The location to the given entry point.</returns>
        public static string GetLocation(this ApiController controller, string entryPoint)
        {
            return $"{controller.Request.RequestUri.GetLeftPart(UriPartial.Authority)}{entryPoint}";
        }

        /// <summary>
        /// Returns an error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="userMessage">The human readable user message.</param>
        /// <param name="internalMessage">The internal message.</param>
        /// <param name="info">Useful information.</param>
        /// <returns>
        /// The action result.
        /// </returns>
        public static IHttpActionResult GetErrorResult(this ApiController controller, string userMessage, string internalMessage, string info = "")
        {
            var errorResult = new ErrorResult(userMessage, internalMessage, info);
            return errorResult;
        }

        /// <summary>
        /// Returns an error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="userMessage">The human readable user message.</param>
        /// <param name="internalMessage">The internal message.</param>
        /// <param name="info">Useful information.</param>
        /// <returns>
        /// The action result.
        /// </returns>
        public static IHttpActionResult GetErrorResult(this ApiController controller, HttpStatusCode statusCode, string userMessage, string internalMessage, string info = "")
        {
            var errorResult = new ErrorResult(statusCode, userMessage, internalMessage, info);
            return errorResult;
        }

        /// <summary>
        /// Gets the correlation identifier for the current request.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>The correlation identifier for the current request.</returns>
        public static string GetCorrelationId(this ApiController controller)
        {
            const string OwinRequestIdKey = "owin.RequestId";
            object id;

            var owinContext = controller.ActionContext.Request.GetOwinContext();
            var owinEnvironment = owinContext.Environment;
            var isIdResolved = owinEnvironment.TryGetValue(OwinRequestIdKey, out id);

            return isIdResolved ? id.ToString() : string.Empty;
        }
    }
}