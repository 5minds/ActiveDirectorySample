namespace ActiveDirectoryBlogPost.DisableUserMicroservice.Repository.Contracts
{
    /// <summary>
    /// The main interface for interacting with the DisableUser repository.
    /// </summary>
    public interface IDisableUserRepository
    {
        /// <summary>
        /// Disables a user with a specific ID.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The id of the user to disable.</param>
        void DisableBySamAccountName(string customerName, string samAccountName);
    }
}