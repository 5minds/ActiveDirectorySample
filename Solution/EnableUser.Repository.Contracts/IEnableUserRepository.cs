namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.Contracts
{
    /// <summary>
    /// The main interface for interacting with the EnableUser repository.
    /// </summary>
    public interface IEnableUserRepository
    {
        /// <summary>
        /// Enables a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The samAccountName of the user to unblock.</param>
        void EnableBySamAccountName(string customerName, string samAccountName);
    }
}