namespace ActiveDirectoryBlogPost.UserMicroservice.UseCase
{
    using ActiveDirectoryBlogPost.UserMicroservice.Service.Contracts;
    using ActiveDirectoryBlogPost.UserMicroservice.UseCase.Contracts;

    using AutoMapper;

    /// <summary>
    /// The user service mapping profile class, defining the mappings.
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
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public void CreateMappings()
        {
            this.CreateMap<UserFromService, UserFromUseCase>().ReverseMap();
        }
    }
}