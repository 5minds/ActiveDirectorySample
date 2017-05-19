namespace ActiveDirectoryBlogPost.UserMicroservice.UseCase
{
    using System.Collections.Generic;

    using ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts;
    using ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts;
    using ActiveDirectoryBlogPost.UserMicroservice.UseCase.Contracts;

    using AutoMapper;

    /// <summary>
    /// The main class for interacting with the User service.
    /// </summary>
    /// <seealso cref="IUserUseCase" />
    /// <seealso cref="IUserUseCase" />
    public class UserUseCase : IUserUseCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserUseCase"/> class.
        /// </summary>
        /// <param name="userService">The User service.</param>
        /// <param name="enableUserService">The EnableUser service.</param>
        public UserUseCase(IUserService userService, IEnableUserService enableUserService)
        {
            this.UserService = userService;
            this.EnableUserService = enableUserService;
        }

        /// <summary>
        /// Gets the User service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        private IUserService UserService { get; }

        /// <summary>
        /// Gets the User service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        private IEnableUserService EnableUserService { get; }

        /// <summary>
        /// Gets a a list of users for the requesting customer user.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="expression">The expression by which to filter the users. Empty String = Get all users.</param>
        /// <returns>The retrieved users.</returns>
        public IEnumerable<UserFromUseCase> GetUsers(string customerName, string expression)
        {
            var usersFromService = this.UserService.GetUsers(customerName, expression);
            var usersFromUseCase = Mapper.Map<IEnumerable<UserFromService>, IEnumerable<UserFromUseCase>>(usersFromService);
            return usersFromUseCase;
        }

        /// <summary>
        /// Gets a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <returns>The retrieved user.</returns>
        public UserFromUseCase GetBySamAccountName(string customerName, string samAccountName)
        {
            var userFromService = this.UserService.GetBySamAccountName(customerName, samAccountName);
            var userFromUseCase = Mapper.Map<UserFromService, UserFromUseCase>(userFromService);

            return userFromUseCase;
        }

        /// <summary>
        /// Creates a user with the given profile data.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="createUserFromUseCase">The data for the new account.</param>
        public void Create(string customerName, UserFromUseCase createUserFromUseCase)
        {
            var createUserFromService = Mapper.Map<UserFromUseCase, UserFromService>(createUserFromUseCase);

            // Create User
            this.UserService.Create(customerName, createUserFromService);

            // Enable User
            this.EnableUserService.EnableBySamAccountName(customerName, createUserFromUseCase.SamAccountName);
        }

        /// <summary>
        /// Updates a single users profile by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <param name="updateDataFromUseCase">The updated user data.</param>
        public void UpdateBySamAccountName(string customerName, string samAccountName, UserFromUseCase updateDataFromUseCase)
        {
            var updateDataFromService = Mapper.Map<UserFromUseCase, UserFromService>(updateDataFromUseCase);
            this.UserService.UpdateBySamAccountName(customerName, samAccountName, updateDataFromService);
        }
    }
}
