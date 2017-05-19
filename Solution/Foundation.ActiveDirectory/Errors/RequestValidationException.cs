namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Encapsulates an exception, for when validating a requests body resulted in failure.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class RequestValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public RequestValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public RequestValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
