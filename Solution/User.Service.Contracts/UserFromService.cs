namespace ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts
{
    using System;

    /// <summary>
    /// The model which represents a user.
    /// </summary>
    public class UserFromService
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user's CN.
        /// </summary>
        /// <value>
        /// The user's CN.
        /// </value>
        public string Cn { get; set; }

        /// <summary>
        /// Gets or sets the user's Name.
        /// </summary>
        /// <value>
        /// The user's Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the users distinguished name.
        /// </summary>
        /// <value>
        /// The users distinguished name.
        /// </value>
        public string DistinguishedName { get; set; }

        /// <summary>
        /// Gets or sets the users principal name.
        /// </summary>
        /// <value>
        /// The users principal name.
        /// </value>
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// Gets or sets the users SAM account name.
        /// </summary>
        /// <value>
        /// The users SAM account name.
        /// </value>
        public string SamAccountName { get; set; }

        /// <summary>
        /// Gets or sets the forename.
        /// </summary>
        /// <value>
        /// The forename.
        /// </value>
        public string ForeName { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public string SurName { get; set; }

        /// <summary>
        /// Gets the full name from forename and surname.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => $"{this.ForeName} {this.SurName}";

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        /// <value>
        /// The DisplayName.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hashed password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the User has been disabled.
        /// </summary>
        /// <value>
        /// The Disabled Flag.
        /// </value>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the Users account has been locked.
        /// </summary>
        /// <value>
        /// The Locked Flag.
        /// </value>
        public bool Locked { get; set; }

        /// <summary>
        /// Gets or sets the date the account will expire at.
        /// </summary>
        /// <value>
        /// The date the account will expire at.
        /// </value>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether the users account is expired.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user is expired; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpired => this.ExpirationDate < DateTime.Now;

        /// <summary>
        /// Gets or sets the groups that the user belongs to.
        /// </summary>
        /// <value>
        /// The groups that the user belongs to.
        /// </value>
        public string[] UserGroups { get; set; }

        /// <summary>
        ///  Gets or sets the users main phone number.
        /// </summary>
        /// <value>
        /// The users main phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the users main homepage.
        /// </summary>
        /// <value>
        /// The users main homepage.
        /// </value>
        public string HomePage { get; set; }

        /// <summary>
        /// Gets or sets the users street address.
        /// </summary>
        /// <value>
        /// The users street address.
        /// </value>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the users Post Office Box.
        /// </summary>
        /// <value>
        /// The users Post Office Box.
        /// </value>
        public string PostOfficeBox { get; set; }

        /// <summary>
        /// Gets or sets the users City.
        /// </summary>
        /// <value>
        /// The users City.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the users State/Province.
        /// </summary>
        /// <value>
        /// The users State/Province.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the users postal code.
        /// </summary>
        /// <value>
        /// The users postal code.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the users country code.
        /// </summary>
        /// <value>
        /// The users country code.
        /// </value>
        public string CountryCode { get; set; }
    }
}
