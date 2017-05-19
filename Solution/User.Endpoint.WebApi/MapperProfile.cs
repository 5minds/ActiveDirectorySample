namespace ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi
{
    using ActiveDirectoryBlogPost.UserMicroservice.Endpoint.WebApi.Requests;
    using ActiveDirectoryBlogPost.UserMicroservice.UseCase.Contracts;

    using AutoMapper;

    /// <summary>
    /// The mapping profile class for mapping the <see cref="UserController"/> request classes to 
    /// the corresponding Use Case model classes.
    /// </summary>
    /// <seealso cref="Profile" />
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMappings();
        }

        /// <summary>
        /// Creates the mappings for the <see cref="MapperProfile"/> class.
        /// </summary>
        public void CreateMappings()
        {
            this.CreateMap<CreateUserRequest, UserFromUseCase>();
            this.CreateMap<UpdateUserRequest, UserFromUseCase>();
        }
    }
}
