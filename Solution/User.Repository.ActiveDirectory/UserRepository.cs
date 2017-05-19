namespace ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using ActiveDirectoryBlogpost.Foundation.Attributes;
    using ActiveDirectoryBlogpost.Foundation.Connection;
    using ActiveDirectoryBlogpost.Foundation.Errors;

    using ActiveDirectoryBlogPost.UserMicroservice.Repository.Contracts;

    using AutoMapper;

    /// <summary>
    /// The User repository.
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private string[] SampleGroups => new string[] { "AllAccess", "Management", "SomethingFunky" };

        /// <summary>
        /// Gets all users for the given customer number.
        /// </summary>
        /// <param name="customerName">The customers name.</param>
        /// <param name="expression">The expression by which to filter the users. Empty String = Get all users.</param>
        /// <returns>A list of all retrieved users.</returns>
        public IEnumerable<UserFromRepository> GetUsers(string customerName, string expression)
        {
            // Get the DirectorySearcher from the Foundation
            using (var activeDirectorySearcher = ActiveDirectoryConnector.GetDirectorySearcher(customerName))
            {
                activeDirectorySearcher.Filter = this.GetSearchFilter(expression);

                // Get all the users for the customer
                var results = activeDirectorySearcher.FindAll();

                // Convert the search result into a viable dataset.
                var userList = (from SearchResult entry in results
                        select new UserFromActiveDirectory
                        {
                            DistinguishedName = entry.Properties["distinguishedName"][0].ToString(),
                            IsLocked = (bool)entry.GetDirectoryEntry().InvokeGet("IsAccountLocked"),
                            Attributes = entry.Properties
                        }).ToList();

                // Map the Users into UserFromRepository Objects to make them actually readable.
                var usersFromRepository = Mapper.Map<List<UserFromActiveDirectory>, List<UserFromRepository>>(userList);
                return usersFromRepository;
            }
        }

        /// <summary>
        /// Gets a user by his samAccountName.
        /// </summary>
        /// <param name="customerName"> The customer name. </param>
        /// <param name="samAccountName"> The samAccountName. </param>
        /// <returns>
        /// The retrieved user.
        /// </returns>
        public UserFromRepository GetBySamAccountName(string customerName, string samAccountName)
        {
            using (var activeDirectorySearcher = ActiveDirectoryConnector.GetDirectorySearcher(customerName))
            {
                activeDirectorySearcher.Filter = $"(&(sAMAccountName={samAccountName}))";

                var result = activeDirectorySearcher.FindOne();

                if (result == null)
                {
                    throw new UserNotFoundException($"The user with the sAMAccountName {samAccountName} could not be found.");
                }

                var userFromActiveDirectory = new UserFromActiveDirectory
                {
                    DistinguishedName = result.Properties["distinguishedName"][0].ToString(),
                    IsLocked = (bool)result.GetDirectoryEntry().InvokeGet("IsAccountLocked"),
                    Attributes = result.Properties
                };

                var userFromRepository = Mapper.Map<UserFromActiveDirectory, UserFromRepository>(userFromActiveDirectory);

                return userFromRepository;
            }
        }

        /// <summary>
        /// Creates a user with the given profile data.
        /// </summary>
        /// <param name="customerName"> The requesting users customer name. </param>
        /// <param name="userData">The data for the new account.</param>
        public void Create(string customerName, UserFromRepository userData)
        {
            var connectionString = "LDAP://WIN-3IQC3CKVBOO.BluePrint.local/OU=" + customerName + ",OU=BlogPost,DC=BluePrint,DC=local";

            var directoryRootEntry = new DirectoryEntry(connectionString);
            var domainName = this.GetDomainNameFromLdapPath(connectionString);

            userData.Name = $"{userData.ForeName} {userData.SurName}";
            userData.Cn = userData.SamAccountName;

            try
            {
                if (this.DirectoryEntryContainsUser(directoryRootEntry, userData.Cn))
                {
                    throw new UserAlreadyExistsException($"A user with the CN '{userData.Cn}' already exists.");
                }

                if (this.DirectoryEntryContainsAccount(directoryRootEntry, userData.SamAccountName))
                {
                    throw new UserAlreadyExistsException($"A user with the sAMAccountName '{userData.SamAccountName}' already exists.");
                }

                var newUser = directoryRootEntry.Children.Add($"CN={userData.Cn}", "user");

                var userPrincipalName = $"{userData.SamAccountName}@{domainName}";
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.UserPrincipalName, userPrincipalName);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.SamAccountName, userData.SamAccountName);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.AccountExpires, this.ParseDateToFileSystemTimeOrDefault(userData.ExpirationDate));
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.Name, userData.Name);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.FirstName, userData.ForeName);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.LastName, userData.SurName);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.Mail, userData.Email);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.Description, userData.Description);
                this.SetUserProperty(newUser, ActiveDirectoryAttributeNames.DisplayName, userData.DisplayName);

                // Password and Group Memberships cannot be set during an initial registration.
                // Both can only be set on already existing users, therefore the user must be saved, before we can proceed.
                try
                {
                    newUser.CommitChanges();
                }
                catch (Exception ex)
                {
                    newUser.Close();
                    throw new UserNotCreatedException("Failed to create user!", ex);
                }

                // Set the new users password to the given value.
                this.SetUserPassword(newUser, userData.Password);

                // Add the User to some groups.
                this.AddUserToSampleGroups(directoryRootEntry, newUser, customerName);
            }
            finally
            {
                directoryRootEntry.Close();
            }
        }

        /// <summary>
        /// Updates a single user by his samAccountName.
        /// </summary>
        /// <param name="customerNo"> The customer No. </param>
        /// <param name="samAccountName">The samAccountName of the user to update.</param>
        /// <param name="userData">The updated user data.</param>
        public void UpdateBySamAccountName(string customerNo, string samAccountName, UserFromRepository userData)
        {
            using (var activeDirectorySearcher = ActiveDirectoryConnector.GetDirectorySearcher(customerNo))
            {
                activeDirectorySearcher.Filter = $"(&(sAMAccountName={samAccountName}))";

                var result = activeDirectorySearcher.FindOne();

                if (result == null)
                {
                    throw new UserNotFoundException($"The user with the sAMAccountName {samAccountName} could not be found.");
                }

                var userEntry = result.GetDirectoryEntry();

                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.AccountExpires, this.ParseDateToFileSystemTimeOrDefault(userData.ExpirationDate));
                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.FirstName, userData.ForeName);
                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.LastName, userData.SurName);
                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.Mail, userData.Email);
                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.Description, userData.Description);
                this.SetUserProperty(userEntry, ActiveDirectoryAttributeNames.DisplayName, userData.DisplayName);

                userEntry.CommitChanges();
                userEntry.Close();
            }
        }

        /// <summary>
        /// Creates a filter for the DirectorySearcher, based on the Settings' AccountFilter object and the passed expression.
        /// </summary>
        /// <param name="expression">The expression by which to filter the users.</param>
        /// <returns>The created filter.</returns>
        private string GetSearchFilter(string expression)
        {
            // For the search to be executed properly, trailing whitespaces must be removed.
            expression = expression.Trim();

            // Basic Filter, that makes sure only users are queried.
            const string BaseFilter = "(&(objectCategory=User)(objectClass=person))";

            // If the passed expression is empty, just return the base filter. This will result in a list of all users to be returned.
            if (string.IsNullOrEmpty(expression))
            {
                return BaseFilter;
            }

            // Apply expression filter to all properties that should match the search expression.
            // In this example, this will inculde the Properties "cn", "sn", "sAMAccountname" and "displayName".
            // You can add any and all properties here you like.
            var expressionFilter = $"(|(cn=*{expression}*)(sn=*{expression}*)(sAMAccountName=*{expression}*)(displayName=*{expression}*))";

            var finalFilter = $"(&{BaseFilter}{expressionFilter})";

            return finalFilter;
        }

        /// <summary>
        /// Checks if the given directory entry already contains a user with the given CN.
        /// -----
        /// NOTE: 
        /// Searching a DirectoryEntry for a non-existing object will ALWAYS cause a DirectoryServicesCOMException.
        /// See https://msdn.microsoft.com/en-us/library/39zxbb5w(v=vs.110).aspx
        /// ----
        /// </summary>
        /// <param name="de">The directory entry to search.</param>
        /// <param name="cn">The cn to look for.</param>
        /// <returns> <c>true</c>, if the entry exists, otherwise <c>false</c>.</returns>
        private bool DirectoryEntryContainsUser(DirectoryEntry de, string cn)
        {
            try
            {
                de.Children.Find($"CN={cn}", "user");
                return true;
            }
            catch (DirectoryServicesCOMException)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the given directory entry already contains a user with the given sAMAccountName.
        /// -----
        /// NOTE: 
        /// Searching a DirectoryEntry for a non-existing object will ALWAYS cause a DirectoryServicesCOMException.
        /// See https://msdn.microsoft.com/en-us/library/39zxbb5w(v=vs.110).aspx
        /// ----
        /// </summary>
        /// <param name="de">The directory entry to search.</param>
        /// <param name="samAccountName">The samAccountName to look for.</param>
        /// <returns> <c>true</c>, if the entry exists, otherwise <c>false</c>.</returns>
        private bool DirectoryEntryContainsAccount(DirectoryEntry de, string samAccountName)
        {
            try
            {
                de.Children.Find($"sAMAccountName={samAccountName}", "user");
                return true;
            }
            catch (DirectoryServicesCOMException)
            {
                return false;
            }
        }

        /// <summary>
        /// Parses a given date into a FileSystemTime format.
        /// </summary>
        /// <param name="dateToParse">The date to parse.</param>
        /// <returns>The parsed date.</returns>
        private string ParseDateToFileSystemTimeOrDefault(DateTime? dateToParse)
        {
            return dateToParse?.ToFileTime().ToString() ?? "0";
        }

        /// <summary>
        /// Gets the domain name from the given LDAP path.
        /// </summary>
        /// <param name="path">The LDAP path from which to get the domain name.</param>
        /// <returns>The domain name.</returns>
        private string GetDomainNameFromLdapPath(string path)
        {
            var sb = new StringBuilder();

            var matches = Regex.Matches(path, @"DC=(?<element>[\S]+)");
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(".");
                    }

                    var element = match.Groups["element"].Value.Trim(' ', ',');
                    sb.Append(element);
                }
            }

            var domainName = sb.ToString();

            return domainName;
        }

        /// <summary>
        /// Checks if the referenced user object contains a specific ActiveDirectory property.
        /// If the property exists, the value will be updated, otherwise, the value will be added.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="propertyName">Name of the ActiveDirectory property to look for.</param>
        /// <param name="value">The value to assign to the property.</param>
        private void SetUserProperty(DirectoryEntry user, string propertyName, string value)
        {
            // The user does not contain the given property, but the property "value" is set:
            // Add the property to the user.
            if (!user.Properties.Contains(propertyName) && !string.IsNullOrEmpty(value))
            {
                user.Properties[propertyName].Add(value);
                return;
            }

            if (!string.IsNullOrEmpty(value))
            {
                // The user does contain the given property and the paremter "value" is set: Update the users property.
                user.Properties[propertyName].Value = value;
            }
            else
            {
                // The user does contain the given property and the paremter "value" is not set: Remove the users property.
                user.Properties[propertyName].Clear();
            }
        }

        /// <summary>
        /// Sets the password of the given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="InvalidPasswordException">The user was created, but setting the password failed.</exception>
        private void SetUserPassword(DirectoryEntry user, string password)
        {
            try
            {
                user.Invoke("SetPassword", new object[] { password });
                user.CommitChanges();
            }
            catch (Exception ex)
            {
                user.Close();
                throw new InvalidPasswordException("The user was created, but setting the password failed.", ex);
            }
        }

        /// <summary>
        /// Adds the given user to the sample groups defined at the top of this class.
        /// </summary>
        /// <param name="groupContainer">The directory entry that contains the groups.</param>
        /// <param name="user">The user to assign to the groups.</param>
        /// <param name="customerName">The name of the customer that owns the group.</param>
        /// <exception cref="GroupNotFoundException">The group does not exist.</exception>
        private void AddUserToSampleGroups(DirectoryEntry groupContainer, DirectoryEntry user, string customerName)
        {
            var userDistinguishedName = user.Properties[ActiveDirectoryAttributeNames.DistinguishedName].Value.ToString();

            foreach (var groupName in this.SampleGroups)
            {
                var fullGroupname = $"{customerName} - {groupName}";

                try
                {
                    var groupEntry = groupContainer.Children.Find($"CN={fullGroupname}", "group");

                    groupEntry.Properties["member"].Add(userDistinguishedName);
                    groupEntry.CommitChanges();
                    groupEntry.Close();
                }
                catch (Exception ex)
                {
                    user.Close();
                    throw new GroupNotFoundException($"The group '{fullGroupname}' does not exist.", ex);
                }
            }
        }
    }
}