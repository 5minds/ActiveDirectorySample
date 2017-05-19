namespace ActiveDirectoryBlogpost.WebApp
{
    using System.Web.Http;

    using ActiveDirectoryBlogpost.WebApp.Extensions;
    using ActiveDirectoryBlogpost.WebApp.Infrastructure;

    using Autofac;
    using Autofac.Integration.WebApi;
    
    using Microsoft.Owin.Logging;

    using Owin;

    /// <summary>
    /// Startup point for our web application.
    /// </summary>
    public class Startup
    {
        private IContainer AutofacContainer { get; set; }

        /// <summary>
        /// Configure the application.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            var infrastructureProvider = new InfrastructureProvider();
            this.AutofacContainer = infrastructureProvider.Container;

            this.RegisterWebApi(appBuilder);
        }

        /// <summary>
        /// Configures the web API.
        /// </summary>
        /// <returns>The ready-to-use HttpConfiguration.</returns>
        private HttpConfiguration ConfigureWebApi()
        {
            var httpConfig = new HttpConfiguration();

            // Set the dependency resolver for WebAPI to be Autofac
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(this.AutofacContainer);
            
            // Configure formatter
            httpConfig.RemoveXmlFormatter();
            httpConfig.AddJsonFormatter();

            // Web-API-Routen
            httpConfig.MapHttpAttributeRoutes();

            // route to catch all 404 errors
            httpConfig.Routes.MapHttpRoute(
                name: "ErrorRouteNotFound",
                routeTemplate: "{*url}",
                defaults: new { controller = "Error", action = "HandleRouteNotFound" });

            return httpConfig;
        }

        /// <summary>
        /// Registers the Web API.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        private void RegisterWebApi(IAppBuilder appBuilder)
        {
            // enable autofac in middlware
            appBuilder.UseAutofacLifetimeScopeInjector(this.AutofacContainer);

            appBuilder.SetLoggerFactory(new DiagnosticsLoggerFactory());
            
            // configure the web api
            var httpConfig = this.ConfigureWebApi();
            
            appBuilder.UseWebApi(httpConfig);
        }
    }
}
