namespace ActiveDirectoryBlogpost.WebApp.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// Called for all not found routes.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ErrorController : ApiController
    {
        /// <summary>
        /// Handles the request for all not found routes.
        /// </summary>
        /// <returns>An ErrorResult to inform the client or at least the HttpStatusCode 404.</returns>
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public IHttpActionResult HandleRouteNotFound()
        {
            return this.NotFound();
        }
    }
}
