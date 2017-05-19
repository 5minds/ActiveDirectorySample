namespace ActiveDirectoryBlogpost.Foundation.Connection
{
    using System;
    using System.DirectoryServices;
    using System.Runtime.InteropServices;

    using ActiveDirectoryBlogpost.Foundation.Attributes;

    /// <summary>
    /// Contains methods for connecting to the Active Directory.
    /// </summary>
    public static class ActiveDirectoryConnector
    {
        /// <summary>
        /// Builds the directory entry upon which to run all the ActiveDirectory related queries.
        /// </summary>
        /// <param name="customerName">Additional parameter for a customers number.</param>
        /// <returns>The directory searcher.</returns>
        public static DirectoryEntry GetDirectorEntry(string customerName)
        {
            try
            {
                // Build a connection string that connects directly to the customers Organizational Unit.
                // This way, the connecting customer can only alter users and groups which belong to his own Organizational Unit
                // ------
                // REPLACE THESE SETTINGS WITH YOUR OWN ENVIRONMENT
                // ------
                var connectionString = "LDAP://WIN-3IQC3CKVBOO.BluePrint.local/OU=" + customerName + ",OU=Blogpost,DC=BluePrint,DC=local";

                // Create the DirectoryEntry object with which to connect to the Active Directory.
                var directoryRootEntry = new DirectoryEntry(connectionString);

                // Check if the connection was successful.
                VerifyDirectoryEntry(ref directoryRootEntry);

                return directoryRootEntry;
            }
            catch (COMException e)
            {
                Console.Write(e.Message);
                throw new Exception("Cannot establish Active Directory connection. Please check the settings and make sure the server is available.", e);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Builds the directory searcher upon which to run all the ActiveDirectory related queries.
        /// </summary>
        /// <param name="customerName">Additional parameter for a customers number.</param>
        /// <returns>The directory searcher.</returns>
        public static DirectorySearcher GetDirectorySearcher(string customerName)
        {
            try
            {
                // Build a connection string that connects directly to the customers Organizational Unit.
                // This way, the connecting customer can only alter users and groups which belong to his own Organizational Unit
                // ------
                // REPLACE THESE SETTINGS WITH YOUR OWN ENVIRONMENT
                // ------
                var connectionString = "LDAP://WIN-3IQC3CKVBOO.BluePrint.local/OU=" + customerName + ",OU=Blogpost,DC=BluePrint,DC=local";

                // Create the DirectoryEntry object with which to connect to the Active Directory.
                var directoryRootEntry = new DirectoryEntry(connectionString);

                // Check if the connection was successful.
                VerifyDirectoryEntry(ref directoryRootEntry);

                // Create the DirectorySearcher Object upon which to run all the User/Group Queries.
                // Also make sure, that only users are retrieved when executing a query.
                var ds = new DirectorySearcher(directoryRootEntry) { Filter = "(&(objectCategory=User)(objectClass=person))" };
                ds.PropertiesToLoad.Clear();

                // Get all the attributes that should be loaded when querying a user.
                var attributesToUse = ActiveDirectoryAttributeNames.GetAll();

                foreach (var attribute in attributesToUse)
                {
                    ds.PropertiesToLoad.Add(attribute);
                }

                return ds;
            }
            catch (COMException e)
            {
                Console.Write(e.Message);
                throw new Exception("Cannot establish Active Directory connection. Please check the settings and make sure the server is available.", e);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Verifies the directory entry by checking the GUID property.
        /// ---
        /// BACKGROUND: Creating a DirectoryEntry Object with invalid credentials will not cause an error. 
        /// Instead, the DirectoryEntry will be created with missing or invalid properties,
        /// which would cause an exception at runtime, when accessing such a property.
        /// Therefore, the object needs to be checked before using it further.
        /// ---
        /// </summary>
        /// <param name="de">The directory entry.</param>
        /// <returns>True, if the value is set.</returns>
        /// <throws>Will fail, if de.Guid is not set.</throws>
        private static bool VerifyDirectoryEntry(ref DirectoryEntry de)
        {
            return !string.IsNullOrEmpty(de.Guid.ToString());
        }
    }
}
