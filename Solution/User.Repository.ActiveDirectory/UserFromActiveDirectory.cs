namespace ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.Linq;
    using System.Security.Principal;

    using ActiveDirectoryBlogpost.Foundation.Attributes;
    using ActiveDirectoryBlogpost.Foundation.Helpers;

    /// <summary>
    /// Encapsulates the name and attributes of a single ActiveDirectory user, as it was retrievd from the active directory,
    /// and contains a number of conversion methods which will allow you to display the Active Directory properties in a human-readable format.
    /// </summary>
    public class UserFromActiveDirectory
    {
        #region "Active Directory Properties"

        /// <summary>
        /// Gets or sets the distinguished name of the ActiveDirectory User.
        /// </summary>
        /// <value>
        /// The distinguished name of the ActiveDirectory User.
        /// </value>
        public string DistinguishedName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the users account has been locked out.
        /// This usually happens, when the user has entered the wrong password one too many times or some other security regulation has been violated.
        /// ----
        /// NOTE: "Locked" is NOT the same as "Disabled" (most notably in that, unlike "disabled", a "locked" state cannot be set manually)!
        /// See: http://www.windows-active-directory.com/difference-between-disabled-expired-and-locked-account.html
        /// ----
        /// </summary>
        /// <value>
        /// <c>true</c> if the account is locked out; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets the list of attributes of the Active Directory user, as they are actually received from the Active Directory.
        /// </summary>
        /// <value>
        /// The ActiveDirectory entries attributes.
        /// </value>
        public ResultPropertyCollection Attributes { get; set; }

        #endregion

        #region "Attribute value retrieval and conversion"

        // ------------------------------
        // These methods can be used for most of the Active Directory Attributes.
        // ------------------------------

        /// <summary>
        /// Gets the value of an attribute and converts it into a string.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute to get.
        /// </param>
        /// <returns>
        /// The retrieved property.
        /// </returns>
        public string GetAttribute(string attributeName)
        {
            var attribute = this.Attributes.Contains(attributeName)
                             ? this.Attributes[attributeName][0].ToString()
                             : string.Empty;

            return attribute;
        }

        /// <summary>
        /// Gets the value of an attribute and converts it into a DateTime.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute to get.
        /// </param>
        /// <returns>
        /// The retrieved property.
        /// </returns>
        public DateTime? GetAttributeAsDate(string attributeName)
        {
            var attribute = this.Attributes.Contains(attributeName)
                             ? this.Attributes[attributeName][0].ToString()
                             : string.Empty;

            if (string.IsNullOrEmpty(attribute))
            {
                return DateTime.Parse(attribute);
            }

            return null;
        }

        #endregion

        #region "Special Attributes Conversion Methods"

        // ------------------------------
        // Some Active Directory Properties require a very specific type of conversion for them to become human-readable.
        // A users expiration-date is a perfect example for such a property.
        // ------------------------------
        // Thus, each of the following conversion methods are used for a single specific property respectively.
        // ------------------------------

        /// <summary>
        /// Gets the expiration date for this user, if any.
        /// ----
        /// BACKGROUND: ActiveDirectory stores a users expiration date as a long-value (called "FileTime"), which is not something you'd like to display to the outside user.
        /// ----
        /// </summary>
        /// <returns>Either the expiration date, or null, if no date has been set.</returns>
        public DateTime? GetExpirationDate()
        {
            var expirationDate = this.Attributes.Contains(ActiveDirectoryAttributeNames.AccountExpires)
                             ? (long)this.Attributes[ActiveDirectoryAttributeNames.AccountExpires][0]
                             : -1;

            // Active Directory stores the ExpiredAt Date in three different states:
            // 0 or greater DateTime.MaxValue.Ticks for "never", or a valid number for an expiration date.
            if (expirationDate <= DateTime.MinValue.Ticks || expirationDate > DateTime.MaxValue.Ticks)
            {
                return null;
            }

            return DateTime.FromFileTime(expirationDate);
        }

        /// <summary>
        /// Determines whether the users account is disabled or not.
        /// ----
        /// BACKGROUND: This requires a direct analysis of "userAccountControl", since the disabled-flag is not stored as an individual property.
        /// See: http://www.selfadsi.de/ads-attributes/user-userAccountControl.htm#UF_ACCOUNT_DISABLE
        /// ----
        /// </summary>
        /// <returns>
        ///   <c>true</c>, if user is disabled; otherwise, <c>false</c>.
        /// </returns>
        public bool GetIsUserDisabled()
        {
            var userAccountControl = this.Attributes.Contains(ActiveDirectoryAttributeNames.UserAccountControl)
                             ? (int)this.Attributes[ActiveDirectoryAttributeNames.UserAccountControl][0]
                             : -1;

            if (userAccountControl == -1)
            {
                return false;
            }

            var isDisabled = userAccountControl & (int)UserAccountControl.UF_ACCOUNT_DISABLE;
            return isDisabled == (int)UserAccountControl.UF_ACCOUNT_DISABLE;
        }

        /// <summary>
        /// Gets the users security id.
        /// ----
        /// BACKGROUND: Unlike many other User ID properties (such as sAMAccountName or distinguishedName),
        /// the security ID is not stored as a string, but as a byte-array.
        /// ----
        /// </summary>
        /// <returns>
        /// The user security id.
        /// </returns>
        public string GetUserSecurityId()
        {
            var sidBytes = this.Attributes.Contains(ActiveDirectoryAttributeNames.UserSecurityId)
                             ? (byte[])this.Attributes[ActiveDirectoryAttributeNames.UserSecurityId][0]
                             : null;

            if (sidBytes == null)
            {
                return string.Empty;
            }

            var sid = new SecurityIdentifier(sidBytes, 0);
            return sid.ToString();
        }

        /// <summary>
        /// Gets the country code (de, gb, us, etc.), based on the numerical country code.
        /// ----
        /// BACKGROUND: Active Directory doesn't store the alphanumerical country codes, but only the numerical codes.
        /// This is not something you (usually) want to pass to the end-user, therefore the country code is converted here to it's alphanumerical counterpart.
        /// You can see the Helper-Class "Iso3166Parser" which lies also in the Foundation-Project to see how the conversion is achieved.
        /// ----
        /// </summary>
        /// <returns>The country code.</returns>
        public string GetCountryCode()
        {
            var countryCode = this.Attributes.Contains(ActiveDirectoryAttributeNames.CountryCode)
                             ? (int)this.Attributes[ActiveDirectoryAttributeNames.CountryCode][0]
                             : 0;
            
            return Iso3166Parser.ParseFromNumerical(countryCode);
        }

        /// <summary>
        /// Gets the users groups.
        /// </summary>
        /// <returns>The users groups.</returns>
        public List<string> GetGroups()
        {
            var parsedGroups = new List<string>();

            var groups = this.Attributes.Contains(ActiveDirectoryAttributeNames.UserGroups)
                             ? this.Attributes[ActiveDirectoryAttributeNames.UserGroups]
                             : null;

            if (groups == null)
            {
                return parsedGroups;
            }

            parsedGroups.AddRange(from object @group in groups select @group.ToString());

            return parsedGroups;
        }

        #endregion
    }
}