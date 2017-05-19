namespace ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi.Requests
{
    using System;

    /// <summary>
    /// Contains parameters for an CreateUser request body. 
    /// </summary>
    public class CreateUserRequest
    {
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
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the date the account will expire at.
        /// </summary>
        /// <value>
        /// The date the account will expire at.
        /// </value>
        public DateTime? ExpirationDate { get; set; }
    }
}
