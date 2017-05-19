namespace ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.ActiveDirectory
{
    using ActiveDirectoryBlogPost.EnableUserMicroservice.Repository.Contracts;

    using Autofac;

    /// <summary>
    /// Contains methods for IoC functionality.
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
            builder.RegisterType<EnableUserRepository>().As<IEnableUserRepository>();
        }
    }
}