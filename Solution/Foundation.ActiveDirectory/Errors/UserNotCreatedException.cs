namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Encapsulates an exception, for when a user could not be found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserNotCreatedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotCreatedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UserNotCreatedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotCreatedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public UserNotCreatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
