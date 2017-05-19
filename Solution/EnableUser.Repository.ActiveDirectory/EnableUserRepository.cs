namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.ActiveDirectory
{
    using ActiveDirectoryBlogpost.Foundation.Attributes;
    using ActiveDirectoryBlogpost.Foundation.Connection;
    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.Contracts;

    /// <summary>
    /// The EnableUser repository.
    /// </summary>
    /// <seealso cref="IEnableUserRepository" />
    public class EnableUserRepository : IEnableUserRepository
    {
        /// <summary>
        /// Enables a single user by his samAccountName.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The samAccountName of the user to unblock.</param>
        public void EnableBySamAccountName(string customerName, string samAccountName)
        {
            // Get the Directory Searcher from the Foundation
            using (var activeDirectorySearcher = ActiveDirectoryConnector.GetDirectorySearcher(customerName))
            {
                // Search for the user to activate.
                activeDirectorySearcher.Filter = "(&(sAMAccountName=" + samAccountName + "))";
                activeDirectorySearcher.PropertiesToLoad.Add("userAccountControl");

                var result = activeDirectorySearcher.FindOne();

                if (result == null)
                {
                    throw new UserNotFoundException("The user with the sAMAccountName " + samAccountName + " could not be found.");
                }

                // Get the DirectoryEntry that corresponds to the user.
                var entryToUpdate = result.GetDirectoryEntry();

                // Perform the Activation
                var val = (int)entryToUpdate.Properties["userAccountControl"].Value;
                entryToUpdate.Properties["userAccountControl"].Value = val & (int)~UserAccountControl.UF_ACCOUNT_DISABLE;

                entryToUpdate.CommitChanges();
                entryToUpdate.Close();
            }
        }
    }
}