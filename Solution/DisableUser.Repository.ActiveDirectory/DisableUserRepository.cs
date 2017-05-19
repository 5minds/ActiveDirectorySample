namespace ActiveDirectoryBlogPost.DisableUserMicroservice.Repository.ActiveDirectory
{
    using ActiveDirectoryBlogpost.Foundation.Attributes;
    using ActiveDirectoryBlogpost.Foundation.Connection;
    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.DisableUserMicroservice.Repository.Contracts;

    /// <summary>
    /// The DisableUser repository.
    /// </summary>
    /// <seealso cref="IDisableUserRepository" />
    public class DisableUserRepository : IDisableUserRepository
    {
        /// <summary>
        /// Disables a user with a specific ID.
        /// </summary>
        /// <param name="customerName">The name of the customer the user belongs to.</param>
        /// <param name="samAccountName">The id of the user to disable.</param>
        public void DisableBySamAccountName(string customerName, string samAccountName)
        {
            using (var activeDirectorySearcher = ActiveDirectoryConnector.GetDirectorySearcher(customerName))
            {
                activeDirectorySearcher.Filter = "(&(sAMAccountName=" + samAccountName + "))";
                activeDirectorySearcher.PropertiesToLoad.Add("userAccountControl");

                var result = activeDirectorySearcher.FindOne();

                if (result == null)
                {
                    throw new UserNotFoundException("The user with the sAMAccountName " + samAccountName + " could not be found.");
                }

                var entryToUpdate = result.GetDirectoryEntry();

                // Perform the Block
                var val = (int)entryToUpdate.Properties["userAccountControl"].Value;
                entryToUpdate.Properties["userAccountControl"].Value = val | (int)UserAccountControl.UF_ACCOUNT_DISABLE;

                entryToUpdate.CommitChanges();
                entryToUpdate.Close();
            }
        }
    }
}