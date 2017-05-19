namespace ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory
{
    using ActiveDirectoryBlogPost.UserMicroservice.Repository.Contracts;

    using Autofac;

    /// <summary>
    /// Contains methods for IoC Support.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class ContainerModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}