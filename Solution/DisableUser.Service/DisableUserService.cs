namespace ActiveDirectoryBlogPost.DisableUserMicroservice.Service
{
    using ActiveDirectoryBlogPost.DisableUserMicroservice.Repository.Contracts;
    using ActiveDirectoryBlogPost.DisableUserMicroservice.Service.Contracts;

    /// <summary>
    /// The main class for interacting with the DisableUser service.
    /// </summary>
    /// <seealso cref="IDisableUserService" />
    /// <seealso cref="IDisableUserService" />
    public class DisableUserService : IDisableUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisableUserService"/> class.
        /// </summary>
        /// <param name="disableUserRepository">
        /// The DisableUser repository.
        /// </param>
        public DisableUserService(IDisableUserRepository disableUserRepository)
        {
            this.DisableUserRepository = disableUserRepository;
        }

        /// <summary>
        /// Gets the DisableUser repository.
        /// </summary>
        /// <value>
        /// The DisableUser repository.
        /// </value>
        private IDisableUserRepository DisableUserRepository { get; }

        /// <summary>
        /// Disables a user with a specific ID.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The id of the user to disable.</param>
        public void DisableBySamAccountName(string customerName, string samAccountName)
        {
            this.DisableUserRepository.DisableBySamAccountName(customerName, samAccountName);
        }
    }
}