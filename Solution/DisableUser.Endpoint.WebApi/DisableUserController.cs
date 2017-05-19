namespace ActiveDirectoryBlogPost.DisableUserMicroservice.Endpoint.WebApi
{
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.DisableUserMicroservice.Service.Contracts;
    using ActiveDirectoryBlogPost.WebApiExtensions;


    // ----------------------------------------------------------
    // NOTE: Usually, error handling would be done by some sort of middleware or API extension.
    // But for the sake of keeping this demonstrational project small, exception handling is done here.
    // 
    // Also, since it is not the goal of this demonstration, we are forgoing the usage of authentication services,
    // logging and usage of external config-files, which you would usually have in a project like this.
    // ----------------------------------------------------------

    /// <summary>
    /// The user controller class holds the methods for the user specific http routes.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/users")]
    public class DisableUserController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisableUserController"/> class.
        /// </summary>
        /// <param name="disableUserService">The user service.</param>
        public DisableUserController(IDisableUserService disableUserService)
        {
            this.DisableUserService = disableUserService;
        }

        /// <summary>
        /// Gets the DisableUserService.
        /// </summary>
        /// <value>
        /// The user DisableUserService.
        /// </value>
        private IDisableUserService DisableUserService { get; }

        /// <summary>
        /// Disables the user with the given identifier.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The user samAccountName.</param>
        /// <returns>The url at which to retrieve the disabled user.</returns>
        /// <example>
        /// <!-- Request: Post http://localhost:12812/api/users/{customerName}/{samAccountName}/disable -->
        /// Response: Status 200 with JSON data for given userId.
        /// </example>
        [HttpPost]
        [Route("{customerName}/{samAccountName}/disable")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult DisableUserById(string customerName, string samAccountName)
        {
            try
            {
                this.DisableUserService.DisableBySamAccountName(customerName, samAccountName);

                var location = $"{this.GetLocation("/api/users")}/{customerName}/{samAccountName}";
                return this.Ok(location);
            }
            catch (UserNotFoundException userEx)
            {
                Console.WriteLine(userEx.Message);
                return this.GetErrorResult(HttpStatusCode.NotFound, userEx.Message, string.Empty);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return this.GetErrorResult(HttpStatusCode.InternalServerError, exception.Message, string.Empty);
            }
        }
    }
}