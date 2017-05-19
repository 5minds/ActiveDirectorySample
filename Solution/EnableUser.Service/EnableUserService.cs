namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Service
{
    using ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.Contracts;
    using ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts;

    /// <summary>
    /// The main class for interacting with the EnableUser service.
    /// </summary>
    /// <seealso cref="IEnableUserService" />
    /// <seealso cref="IEnableUserService" />
    public class EnableUserService : IEnableUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnableUserService"/> class.
        /// </summary>
        /// <param name="enableUserRepository">
        /// The EnableUser repository.
        /// </param>
        public EnableUserService(IEnableUserRepository enableUserRepository)
        {
            this.EnableUserRepository = enableUserRepository;
        }

        /// <summary>
        /// Gets the EnableUser repository.
        /// </summary>
        /// <value>
        /// The EnableUser repository.
        /// </value>
        private IEnableUserRepository EnableUserRepository { get; }

        /// <summary>
        /// Enables a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The samAccountName of the user to unblock.</param>
        public void EnableBySamAccountName(string customerName, string samAccountName)
        {
           this.EnableUserRepository.EnableBySamAccountName(customerName, samAccountName);
        }
    }
}