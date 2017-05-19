namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Encapsulates an exception, for when a user could not be found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
