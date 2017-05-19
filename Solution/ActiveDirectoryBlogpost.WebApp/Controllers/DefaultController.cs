namespace ActiveDirectoryBlogpost.WebApp.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// A controller to view initially.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("")]
    public class DefaultController : ApiController
    {
        /// <summary>
        /// Echoes a default welcome string.
        /// </summary>
        /// <returns>An action result.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route]
        public IHttpActionResult Welcome()
        {
            return this.Json("This is the API for the Active Directory Blogpost project.");
        }
    }
}
