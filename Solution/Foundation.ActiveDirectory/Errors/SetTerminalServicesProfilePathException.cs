namespace ActiveDirectoryBlogpost.Foundation.Errors
{
    using System;

    /// <summary>
    /// Error while setting the terminal services profile path.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SetTerminalServicesProfilePathException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetTerminalServicesProfilePathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public SetTerminalServicesProfilePathException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTerminalServicesProfilePathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public SetTerminalServicesProfilePathException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
