namespace ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi.Requests;
    using ActiveDirectoryBlogPost.UserMicroservice.UseCase.Contracts;

    using ActiveDirectoryBlogPost.WebApiExtensions;

    using AutoMapper;


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
    public class UserController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userUseCase">The user use case.</param>
        public UserController(IUserUseCase userUseCase)
        {
            this.UserUseCase = userUseCase;
        }

        /// <summary>
        /// Gets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        private IUserUseCase UserUseCase { get; }

        /// <summary>
        /// Gets all the customers users from the active directory.
        /// </summary>
        /// <param name="customerName">The Name of the customer for which to retrieve the users.</param>
        /// <param name="expression">Optional: A full-text expression by which to filter the user list.</param>
        /// <returns>List of users.</returns>
        /// <example>
        /// <!-- Request: GET http://localhost:12812/api/users/{customerName}?expression=%expr% -->
        /// Response: Status 200 with JSON array of users.
        /// </example>
        [HttpGet]
        [Route("{customerName}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetUsers(string customerName, string expression = "")
        {
            try
            {
                var users = this.UserUseCase.GetUsers(customerName, expression);
                return this.Ok(users);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return this.GetErrorResult(HttpStatusCode.InternalServerError, exception.Message, string.Empty);
            }
        }

        /// <summary>
        /// Gets the user by the given samAccountName.
        /// </summary>
        /// <param name="customerName">The Name of the customer for which to retrieve the users.</param>
        /// <param name="samAccountName">The users ActiveDirectory samAccountName.</param>
        /// <returns>The requested user.</returns>
        /// <example>
        /// <!-- Request: GET http://localhost:12812/api/users/{customerName}/{samAccountName} -->
        /// Response: Status 200 with JSON data for given userId.
        /// </example>
        [HttpGet]
        [Route("{customerName}/{samAccountName}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetBySamAccountName(string customerName, string samAccountName)
        {
            try
            {
                var user = this.UserUseCase.GetBySamAccountName(customerName, samAccountName);
                return user == null ? (IHttpActionResult)this.NotFound() : this.Ok(user);
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

        /// <summary>
        /// Creates a new user for the given customer, using the given profile data.
        /// </summary>
        /// <param name="customerName">The Name of the customer for which to retrieve the users.</param>
        /// <param name="userData">The data for the new account.</param>
        /// <returns>The url by which to retrieve the created user.</returns>
        /// <example>
        /// <!-- Request: POST http://localhost:12812/api/users/{customerName} -->
        /// Response: Status 200 with JSON data.
        /// </example>
        [HttpPost]
        [Route("{customerName}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Create(string customerName, [FromBody] CreateUserRequest userData)
        {
            try
            {
                var userFromUsecase = Mapper.Map<CreateUserRequest, UserFromUseCase>(userData);
                this.UserUseCase.Create(customerName, userFromUsecase);
                
                var location = $"{this.GetLocation("/api/users")}/{customerName}/{userData.SamAccountName}";

                var parameters = new Dictionary<string, object>
                {
                    { "samAccountName", $"{userData.SamAccountName}" },
                    { "location", location }
                };

                return this.Created(location, parameters);
            }
            catch (AccessDeniedException accEx)
            {
                Console.WriteLine(accEx.Message);
                return this.GetErrorResult(HttpStatusCode.Forbidden, accEx.Message, string.Empty);
            }
            catch (RequestValidationException reqEx)
            {
                Console.WriteLine(reqEx.Message);
                return this.GetErrorResult(HttpStatusCode.BadRequest, reqEx.Message, string.Empty);
            }
            catch (UserAlreadyExistsException userEx)
            {
                Console.WriteLine(userEx.Message);
                return this.GetErrorResult(HttpStatusCode.Conflict, userEx.Message, string.Empty);
            }
            catch (UserNotCreatedException userEx)
            {
                Console.WriteLine(userEx.Message);
                return this.GetErrorResult(HttpStatusCode.NotModified, userEx.Message, string.Empty);
            }
            catch (InvalidPasswordException passEx)
            {
                Console.WriteLine(passEx.Message);
                return this.GetErrorResult(HttpStatusCode.BadRequest, passEx.Message, string.Empty);
            }
            catch (GroupNotFoundException groupEx)
            {
                Console.WriteLine(groupEx.Message);
                return this.GetErrorResult(HttpStatusCode.NotFound, groupEx.Message, string.Empty);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return this.GetErrorResult(HttpStatusCode.InternalServerError, exception.Message, string.Empty);
            }
        }

        /// <summary>
        /// Updates the customers user with the given samAccountName.
        /// </summary>
        /// <param name="customerName">The Name of the customer for which to retrieve the users.</param>
        /// <param name="samAccountName">The user samAccountName.</param>
        /// <param name="userData">The updated user data.</param>
        /// <returns>The url by which to retrieve the updated user.</returns>
        /// <example>
        /// <!-- Request: PUT http://localhost:12812/api/users/{customerName}/{samAccountName} -->
        /// Response: Status 200 with JSON data for given userId.
        /// </example>
        [HttpPut]
        [Route("{customerName}/{samAccountName}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult UpdateBySamAccountName(string customerName, string samAccountName, [FromBody] UpdateUserRequest userData)
        {
            try
            {
                var updateDataFromUsecase = Mapper.Map<UpdateUserRequest, UserFromUseCase>(userData);
                this.UserUseCase.UpdateBySamAccountName(customerName, samAccountName, updateDataFromUsecase);

                var location = $"{this.GetLocation("/api/users")}/{customerName}/{samAccountName}";
                return this.Ok(location);
            }
            catch (RequestValidationException reqEx)
            {
                Console.WriteLine(reqEx.Message);
                return this.GetErrorResult(HttpStatusCode.BadRequest, reqEx.Message, string.Empty);
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