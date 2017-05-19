namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Endpoint.WebApi
{
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts;
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
    public class EnableUserController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnableUserController"/> class.
        /// </summary>
        /// <param name="enableUserService">The user useCase.</param>
        public EnableUserController(IEnableUserService enableUserService)
        {
            this.EnableUserService = enableUserService;
        }

        /// <summary>
        /// Gets the EnableUserService.
        /// </summary>
        /// <value>
        /// The EnableUserService.
        /// </value>
        private IEnableUserService EnableUserService { get; }

        /// <summary>
        /// Enables the user with the given identifier.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The user samAccountName.</param>
        /// <returns>The url at which to retrieve the enabled user.</returns>
        /// <example>
        /// <!-- Request: Post http://localhost:12812/api/users/{customerName}/{samAccountName}/enable -->
        /// Response: Status 200 with JSON data for given userId.
        /// </example>
        [HttpPost]
        [Route("{customerName}/{samAccountName}/enable")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult EnableUserById(string customerName, string samAccountName)
        {
            try
            {
                this.EnableUserService.EnableBySamAccountName(customerName, samAccountName);

                var location = $"{this.GetLocation("/api/users")}/{customerName}/{samAccountName}";
                return this.Ok(location);
            }
            catch (UserNotFoundException userEx)
            {
                Console.Write(userEx.Message);
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