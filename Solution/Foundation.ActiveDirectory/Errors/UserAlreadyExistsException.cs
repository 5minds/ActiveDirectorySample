namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Encapsulates an exception, for when a request was made to create a user with an already existing sAMAccountName.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public UserAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
