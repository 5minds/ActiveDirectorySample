namespace ActiveDirectoryBlogpost.WebApp
{
    /// <summary>
    /// This class defines the package version and is only referenced in the local assembly info.
    /// </summary>
    internal static class NuGetVersionControl
    {
        /// <summary>
        /// NuGet version.
        /// </summary>
        public const string Version = "0.2.1";

        /// <summary>
        /// NuGet pre version.
        /// </summary>
        public const string PreVersion = Version + PreFlag;

        /// <summary>
        /// NuGet pre version flag.
        /// Must begin with "-" if used.
        /// </summary>
        private const string PreFlag = "";
    }
}
