namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts
{
    /// <summary>
    /// The main interface for interacting with EnableUser service.
    /// </summary>
    public interface IEnableUserService
    {
        /// <summary>
        /// Enables a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The samAccountName of the user to unblock.</param>
        void EnableBySamAccountName(string customerName, string samAccountName);
    }
}
