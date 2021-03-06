﻿namespace ActiveDirectoryBlogPost.UserMicroservice.Service
{
    using ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts;

    using Autofac;

    /// <summary>
    /// The user service container module for registering implementations on IOC container.
    /// </summary>
    public class ContainerModule : Module
    {
        /// <summary>
        /// The overridden load method to register implementations on IOC container.
        /// </summary>
        /// <param name="builder">
        /// The builder for registering on IOC container.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            base.Load(builder);
        }
    }
}