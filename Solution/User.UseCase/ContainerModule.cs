namespace ActiveDirectoryBlogPost.UserMicroservice.UseCase
{
    using ActiveDirectoryBlogPost.UserMicroservice.UseCase.Contracts;

    using Autofac;

    /// <summary>
    /// The user UseCase container module for registering implementations on IOC container.
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
            builder.RegisterType<UserUseCase>().As<IUserUseCase>();
            base.Load(builder);
        }
    }
}