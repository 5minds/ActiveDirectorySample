namespace ActiveDirectoryBlogPost.UserMicroservice.Repository.ActiveDirectory
{
    using ActiveDirectoryBlogpost.Foundation.Attributes;

    using ActiveDirectoryBlogPost.UserMicroservice.Repository.Contracts;

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
            this.CreateMap<UserFromActiveDirectory, UserFromRepository>()
                .ForMember(dest => dest.Id, src => src.MapFrom(result => result.GetUserSecurityId()))
                .ForMember(dest => dest.Cn, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Cn)))
                .ForMember(dest => dest.Name, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Name)))
                .ForMember(dest => dest.DistinguishedName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.DistinguishedName)))
                .ForMember(dest => dest.UserPrincipalName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.UserPrincipalName)))
                .ForMember(dest => dest.SamAccountName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.SamAccountName)))
                .ForMember(dest => dest.ForeName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.FirstName)))
                .ForMember(dest => dest.SurName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.LastName)))
                .ForMember(dest => dest.Password, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Password)))
                .ForMember(dest => dest.Email, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Mail)))
                .ForMember(dest => dest.UserGroups, src => src.MapFrom(result => result.GetGroups()))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.PhoneNumber)))
                .ForMember(dest => dest.HomePage, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.HomePage)))
                .ForMember(dest => dest.Street, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Street)))
                .ForMember(dest => dest.City, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.City)))
                .ForMember(dest => dest.State, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.State)))
                .ForMember(dest => dest.Description, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.Description)))
                .ForMember(dest => dest.DisplayName, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.DisplayName)))
                .ForMember(dest => dest.CountryCode, src => src.MapFrom(result => result.GetCountryCode()))
                .ForMember(dest => dest.PostalCode, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.PostalCode)))
                .ForMember(dest => dest.PostOfficeBox, src => src.MapFrom(result => result.GetAttribute(ActiveDirectoryAttributeNames.PostOfficeBox)))
                .ForMember(dest => dest.Disabled, src => src.MapFrom(result => result.GetIsUserDisabled()))
                .ForMember(dest => dest.Locked, src => src.MapFrom(result => result.IsLocked))
                .ForMember(dest => dest.ExpirationDate, src => src.MapFrom(result => result.GetExpirationDate()));
        }
    }
}
