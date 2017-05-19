namespace ActiveDirectoryBlogPost.DisableUserMicroservice.Service.Contracts
{
    /// <summary>
    /// The main interface for interacting with DisableUser repository.
    /// </summary>
    public interface IDisableUserService
    {
        /// <summary>
        /// Disables a user with a specific ID.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The id of the user to disable.</param>
        void DisableBySamAccountName(string customerName, string samAccountName);
    }
}
