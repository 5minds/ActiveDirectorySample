namespace ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// The main interface for interacting with user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a a list of users for the requesting customer user.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="expression">The expression by which to filter the users. Empty String = Get all users.</param>
        /// <returns>The retrieved users.</returns>
        IEnumerable<UserFromService> GetUsers(string customerName, string expression);

        /// <summary>
        /// Gets a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <returns>The retrieved user.</returns>
        UserFromService GetBySamAccountName(string customerName, string samAccountName);

        /// <summary>
        /// Creates a user with the given profile data.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="createUserFromService">The data for the new account.</param>
        void Create(string customerName, UserFromService createUserFromService);

        /// <summary>
        /// Updates a single users profile by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer for which to get the users.</param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <param name="userFromService">The updated user data.</param>
        void UpdateBySamAccountName(string customerName, string samAccountName, UserFromService userFromService);
    }
}
