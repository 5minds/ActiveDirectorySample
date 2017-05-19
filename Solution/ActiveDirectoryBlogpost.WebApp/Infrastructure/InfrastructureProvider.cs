namespace ActiveDirectoryBlogpost.WebApp.Infrastructure
{
    using System;

    using Autofac;

    using AutoMapper;

    /// <summary>
    /// Provider to use the IOC. This class cannot be inherited.
    /// </summary>
    public sealed class InfrastructureProvider : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureProvider"/> class.
        /// </summary>
        public InfrastructureProvider()
        {
            this.InitializeIocContainer();
            this.InitializeAutoMapper();
        }

        /// <summary>
        /// Gets the IOC container.
        /// </summary>
        /// <value>
        /// The IOC container.
        /// </value>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Container.Dispose();
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.AddProfile<ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi.MapperProfile>();
                    cfg.AddProfile<ActiveDirectoryBlogPost.UserMicroservice.UseCase.MapperProfile>();
                    cfg.AddProfile<ActiveDirectoryBlogPost.UserMicroservice.Service.MapperProfile>();
                    cfg.AddProfile<ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory.MapperProfile>();
                });
        }

        /// <summary>
        /// Initializes the IOC container.
        /// </summary>
        private void InitializeIocContainer()
        {
            var builder = new ContainerBuilder();

            // Register User microservice layers.
            builder.RegisterModule<ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.UserMicroservice.UseCase.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.UserMicroservice.Service.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory.ContainerModule>();

            // Register DisableUser microservice layers.
            builder.RegisterModule<ActiveDirectoryBlogPost.DisableUserMicroservice.Endpoint.WebApi.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.DisableUserMicroservice.Service.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.DisableUserMicroservice.Repository.ActiveDirectory.ContainerModule>();

            // Register EnableUser microservice layers.
            builder.RegisterModule<ActiveDirectoryBlogPost.EnableUserMicroservice.Endpoint.WebApi.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.EnableUserMicroservice.Service.ContainerModule>();
            builder.RegisterModule<ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.ActiveDirectory.ContainerModule>();

            this.Container = builder.Build();
        }
    }
}
