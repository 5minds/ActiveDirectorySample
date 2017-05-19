namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Encapsulates an exception, for when a user performed a request without sufficient access rights.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AccessDeniedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AccessDeniedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
