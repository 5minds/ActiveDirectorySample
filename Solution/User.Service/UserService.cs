namespace ActiveDirectoryBlogPost.UserMicroservice.Service
{
    using System.Collections.Generic;

    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.UserMicroservice.Repository.Contracts;
    using ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts;

    using AutoMapper;

    /// <summary>
    /// The main class for interacting with the user service.
    /// </summary>
    /// <seealso cref="IUserService" />
    /// <seealso cref="IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        private IUserRepository UserRepository { get; }

        /// <summary>
        /// Gets a a list of users for the requesting customer user.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="expression">The expression by which to filter the users. Empty String = Get all users.</param>
        /// <returns>The retrieved users.</returns>
        public IEnumerable<UserFromService> GetUsers(string customerName, string expression)
        {
            var usersFromRepository = this.UserRepository.GetUsers(customerName, expression);
            var usersFromService = Mapper.Map<IEnumerable<UserFromRepository>, IEnumerable<UserFromService>>(usersFromRepository);

            return usersFromService;
        }

        /// <summary>
        /// Gets a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <returns>The retrieved user.</returns>
        public UserFromService GetBySamAccountName(string customerName, string samAccountName)
        {
            var userFromRepository = this.UserRepository.GetBySamAccountName(customerName, samAccountName);
            return MapUser(userFromRepository);
        }

        /// <summary>
        /// Creates a user with the given profile data.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="userFromService">The data for the new account.</param>
        public void Create(string customerName, UserFromService userFromService)
        {
            var validationErrors = ValidateCreateRequest(userFromService);

            if (!string.IsNullOrEmpty(validationErrors))
            {
                throw new RequestValidationException(validationErrors);
            }

            // Apply transformations
            var userFromRepository = Mapper.Map<UserFromService, UserFromRepository>(userFromService);
            
            // Create user
            this.UserRepository.Create(customerName, userFromRepository);
        }

        /// <summary>
        /// Updates a single users profile by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <param name="userFromService">The updated user data.</param>
        public void UpdateBySamAccountName(string customerName, string samAccountName, UserFromService userFromService)
        {
            var userFromRepository = Mapper.Map<UserFromService, UserFromRepository>(userFromService);
            this.UserRepository.UpdateBySamAccountName(customerName, samAccountName, userFromRepository);
        }

        /// <summary>
        /// Validates the supplied request body for creating a user.
        /// -----
        /// NOTE: Extensive validation is usually handled by a business rule service or a validation extension of some kind.
        /// In the interest of keeping this project relatively lean, we forgoe the implementation of such a service.
        /// -----
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>True, if the request body is valid. Otherwise false.</returns>
        private static string ValidateCreateRequest(UserFromService request)
        {
            var errors = string.Empty;

            if (string.IsNullOrEmpty(request.SamAccountName) | string.IsNullOrEmpty(request.DisplayName)
                | string.IsNullOrEmpty(request.SurName) | string.IsNullOrEmpty(request.Password))
            {
                errors += "The request body must contain \"SamAccountName\", \"Password\", \"SurName\" and \"DisplayName\"! \n";
            }

            // The maximum length of a SamAccountName can only ever be 20 characters.
            // Anything longer will result in a fatal - and unbelievably non-descript - ActiveDirectory Error!
            // See: https://msdn.microsoft.com/en-us/library/ms679635(v=vs.85).aspx
            if (request.SamAccountName == null || request.SamAccountName.Length > 20)
            {
                errors += $"SamAccountName {request.SamAccountName} is too long! It must be no longer than 20 characters! \n";
            }

            return errors;
        }

        /// <summary>
        /// Maps the given UserFromRepository object into a UserFromService object.
        /// </summary>
        /// <param name="userFromRepository">The user from repository.</param>
        /// <returns>The mapped object.</returns>
        private static UserFromService MapUser(UserFromRepository userFromRepository)
        {
            if (userFromRepository == null)
            {
                return null;
            }

            var userFromService = Mapper.Map<UserFromRepository, UserFromService>(userFromRepository);

            return userFromService;
        }
    }
}