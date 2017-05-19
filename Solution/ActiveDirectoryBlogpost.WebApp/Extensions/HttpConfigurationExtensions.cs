namespace ActiveDirectoryBlogpost.WebApp.Extensions
{
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    /// <summary>
    /// Extensions for configuring general HTTP pipeline options.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// Removes the XML output formatter.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RemoveXmlFormatter(this HttpConfiguration config)
        {
            var xmlFormatter = config.Formatters.XmlFormatter;

            var xmlType = xmlFormatter.SupportedMediaTypes.FirstOrDefault(p => p.MediaType == "application/xml");
            xmlFormatter.SupportedMediaTypes.Remove(xmlType);
        }

        /// <summary>
        /// Adds the JSON output formatter.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void AddJsonFormatter(this HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.Indent = true;
            jsonFormatter.SupportedEncodings.Insert(0, Encoding.UTF8);

            // avoid circular dependencies
            jsonFormatter.MaxDepth = 5;
        }
    }
}