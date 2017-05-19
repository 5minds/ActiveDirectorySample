namespace ActiveDirectoryBlogpost.Foundation.Attributes
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains a dictionary for all relevant properties a user can have in ActiveDirectory.
    /// Refer to: https://msdn.microsoft.com/en-us/library/ms677980(v=vs.85).aspx .
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Contains URL.")]
    public class ActiveDirectoryAttributeNames
    {
        /// <summary>
        /// Gets the users unique identifier.
        /// </summary>
        /// <value>
        /// The user unique identifier.
        /// </value>
        public static string UserId => "objectGUID";

        /// <summary>
        /// Gets the users unique security identifier.
        /// </summary>
        /// <value>
        /// The user unique security identifier.
        /// </value>
        public static string UserSecurityId => "objectSid";

        /// <summary>
        /// Gets The users principal name.
        /// </summary>
        /// <value>
        /// The users other principal name.
        /// </value>
        public static string UserPrincipalName => "userPrincipalName";

        /// <summary>
        /// Gets the users principal name (pre Win2000).
        /// </summary>
        /// <value>
        /// The users other principal name.
        /// </value>
        public static string SamAccountName => "sAMAccountname";

        /// <summary>
        /// Gets the users password.
        /// </summary>
        /// <value>
        /// The users password.
        /// </value>
        public static string Password => "userPassword";

        /// <summary>
        /// Gets a users CN.
        /// </summary>
        /// <value>
        /// The users CN.
        /// </value>
        public static string Cn => "cn";

        /// <summary>
        /// Gets a users name.
        /// </summary>
        /// <value>
        /// The users name.
        /// </value>
        public static string Name => "name";

        /// <summary>
        /// Gets a users distinguished name.
        /// </summary>
        /// <value>
        /// The users distinguished name.
        /// </value>
        public static string DistinguishedName => "distinguishedName";

        /// <summary>
        /// Gets a users first name.
        /// </summary>
        /// <value>
        /// The users first name.
        /// </value>
        public static string FirstName => "givenName";

        /// <summary>
        /// Gets a users last name.
        /// </summary>
        /// <value>
        /// The users last name.
        /// </value>
        public static string LastName => "sn";

        /// <summary>
        /// Gets a users initials.
        /// </summary>
        /// <value>
        /// The users initials.
        /// </value>
        public static string Initials => "initials";

        /// <summary>
        /// Gets a users display name.
        /// </summary>
        /// <value>
        /// The users display name.
        /// </value>
        public static string DisplayName => "displayName";

        /// <summary>
        /// Gets a users account description.
        /// </summary>
        /// <value>
        /// The users account description.
        /// </value>
        public static string Description => "description";

        /// <summary>
        /// Gets the name of the users main office.
        /// </summary>
        /// <value>
        /// The name of the users main office.
        /// </value>
        public static string MainOffice => "physicalDeliveryOfficeName";

        /// <summary>
        /// Gets the users main phone number.
        /// </summary>
        /// <value>
        /// The users main phone number.
        /// </value>
        public static string PhoneNumber => "telephoneNumber";

        /// <summary>
        /// Gets the users additional phone numbers.
        /// </summary>
        /// <value>
        /// The users additional phone numbers.
        /// </value>
        public static string AdditionalPhoneNumbers => "otherTelephone";

        /// <summary>
        /// Gets the users main email.
        /// </summary>
        /// <value>
        /// The users main email.
        /// </value>
        public static string Mail => "mail";

        /// <summary>
        /// Gets the users main email address.
        /// </summary>
        /// <value>
        /// The users main email address.
        /// </value>
        public static string EmailAddresses => "E-mail-Addresses";

        /// <summary>
        /// Gets the users main homepage.
        /// </summary>
        /// <value>
        /// The users main homepage.
        /// </value>
        public static string HomePage => "wWWHomePage";

        /// <summary>
        /// Gets The users other homepages.
        /// </summary>
        /// <value>
        /// The users other homepages.
        /// </value>
        public static string OtherHomePages => "url";

        /// <summary>
        /// Gets the users street address.
        /// </summary>
        /// <value>
        /// The users street address.
        /// </value>
        public static string Street => "streetAddress";

        /// <summary>
        /// Gets the users Post Office Box.
        /// </summary>
        /// <value>
        /// The users Post Office Box.
        /// </value>
        public static string PostOfficeBox => "postOfficeBox";

        /// <summary>
        /// Gets the users City.
        /// </summary>
        /// <value>
        /// The users City.
        /// </value>
        public static string City => "l";

        /// <summary>
        /// Gets the users State/Province.
        /// </summary>
        /// <value>
        /// The users State/Province.
        /// </value>
        public static string State => "st";

        /// <summary>
        /// Gets the users postal code.
        /// </summary>
        /// <value>
        /// The users postal code.
        /// </value>
        public static string PostalCode => "postalCode";

        /// <summary>
        /// Gets the users country code.
        /// </summary>
        /// <value>
        /// The users country code.
        /// </value>
        public static string CountryCode => "countryCode";

        /// <summary>
        /// Gets the name of the users company.
        /// </summary>
        /// <value>
        /// The name of the users company.
        /// </value>
        public static string Company => "company";

        /// <summary>
        /// Gets the name of the company manager.
        /// </summary>
        /// <value>
        /// The name of the company manager.
        /// </value>
        public static string Manager => "manager";

        /// <summary>
        /// Gets the users title he holds in his company.
        /// </summary>
        /// <value>
        /// The users title he holds in his company.
        /// </value>
        public static string Title => "title";

        /// <summary>
        /// Gets the department the users works in.
        /// </summary>
        /// <value>
        /// The department the users works in.
        /// </value>
        public static string Department => "department";

        /// <summary>
        /// Gets the directReports property.
        /// </summary>
        /// <value>
        /// The the directReports property.
        /// </value>
        public static string DirectReports => "directReports";

        /// <summary>
        /// Gets the amount of hours the user has been logged into the system.
        /// </summary>
        /// <value>
        /// Theamount of hours the user has been logged into the system.
        /// </value>
        public static string LogonHours => "logonHours";

        /// <summary>
        /// Gets the Workstation from which the user is currently logged in (if any).
        /// </summary>
        /// <value>
        /// The Workstation from which the user is currently logged in (if any).
        /// </value>
        public static string LogonWorkstation => "logonWorkstation";

        /// <summary>
        /// Gets the groups that the user belongs to.
        /// </summary>
        /// <value>
        /// The groups that the user belongs to.
        /// </value>
        public static string UserGroups => "memberOf";

        /// <summary>
        /// Gets the id of the users primary group.
        /// </summary>
        /// <value>
        /// The id of the users primary group.
        /// </value>
        public static string PrimaryGroupId => "primaryGroupID";

        /// <summary>
        /// Gets the time and date at which the account has been locked out.
        /// </summary>
        /// <value>
        /// The time and date at which the account has been locked out.
        /// </value>
        public static string LockoutTime => "lockoutTime";

        /// <summary>
        /// Gets the amount of time the user has been locked out.
        /// </summary>
        /// <value>
        /// The amount of time the user has been locked out.
        /// </value>
        public static string LockoutDuration => "lockoutDuration";

        /// <summary>
        /// Gets a flag indicating whether or not the user must change his password at next login.
        /// </summary>
        /// <value>
        /// The flag indicating whether or not the user must change his password at next login.
        /// </value>
        public static string UserMustResetPassword => "pwdLastSet";

        /// <summary>
        /// Gets the flags for account specific states. See: https://msdn.microsoft.com/en-us/library/ms680832(v=vs.85).aspx .
        /// </summary>
        /// <value>
        /// The flags for account specific states. 
        /// </value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Comments contain URL.")]
        public static string UserAccountControl => "userAccountControl";

        /// <summary>
        /// Gets the date the account will expire at.
        /// </summary>
        /// <value>
        /// The date the account will expire at.
        /// </value>
        public static string AccountExpires => "accountExpires";

        /// <summary>
        /// Gets the script path.
        /// </summary>
        /// <value>
        /// The script path.
        /// </value>
        public static string ScriptPath => "scriptPath";

        /// <summary>
        /// Gets the home directory.
        /// </summary>
        /// <value>
        /// The home directory.
        /// </value>
        public static string HomeDirectory => "homeDirectory";

        /// <summary>
        /// Gets the home drive.
        /// </summary>
        /// <value>
        /// The home drive.
        /// </value>
        public static string HomeDrive => "homeDrive";

        /// <summary>
        /// Gets the home phone main.
        /// </summary>
        /// <value>
        /// The home phone main.
        /// </value>
        public static string HomePhoneMain => "homePhone";

        /// <summary>
        /// Gets the home phone other.
        /// </summary>
        /// <value>
        /// The home phone other.
        /// </value>
        public static string HomePhoneOther => "otherHomePhone";

        /// <summary>
        /// Gets the pager main.
        /// </summary>
        /// <value>
        /// The pager main.
        /// </value>
        public static string PagerMain => "pager";

        /// <summary>
        /// Gets the pager other.
        /// </summary>
        /// <value>
        /// The pager other.
        /// </value>
        public static string PagerOther => "otherPager";

        /// <summary>
        /// Gets the mobile main.
        /// </summary>
        /// <value>
        /// The mobile main.
        /// </value>
        public static string MobileMain => "mobile";

        /// <summary>
        /// Gets the mobile other.
        /// </summary>
        /// <value>
        /// The mobile other.
        /// </value>
        public static string MobileOther => "otherMobile";

        /// <summary>
        /// Gets the fax main.
        /// </summary>
        /// <value>
        /// The fax main.
        /// </value>
        public static string FaxMain => "facsimileTelephoneNumber";

        /// <summary>
        /// Gets the fax other.
        /// </summary>
        /// <value>
        /// The fax other.
        /// </value>
        public static string FaxOther => "otherFacsimileTelephoneNumber";

        /// <summary>
        /// Gets the ip phone main.
        /// </summary>
        /// <value>
        /// The ip phone main.
        /// </value>
        public static string IpPhoneMain => "ipPhone";

        /// <summary>
        /// Gets the ip phone other.
        /// </summary>
        /// <value>
        /// The ip phone other.
        /// </value>
        public static string IpPhoneOther => "otherIpPhone";

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public static string Notes => "info";

        /// <summary>
        /// Gets the date at which the account was created.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public static string CreatedAt => "whenCreated";

        /// <summary>
        /// Gets the date at which the account was last changed.
        /// </summary>
        /// <value>
        /// The changing date.
        /// </value>
        public static string ChangedAt => "whenChanged";

        /// <summary>
        /// Gets a list of all ActiveDirectory attributes.
        /// </summary>
        /// <returns>The ActiveDirectory Attributes.</returns>
        public static IEnumerable<string> GetAll()
        {
            return new[]
            {
                UserId,
                UserSecurityId,
                UserPrincipalName,
                SamAccountName,
                Password,
                Cn,
                Name,
                DistinguishedName,
                FirstName,
                LastName,
                Initials,
                DisplayName,
                Description,
                MainOffice,
                PhoneNumber,
                AdditionalPhoneNumbers,
                Mail,
                EmailAddresses,
                HomePage,
                OtherHomePages,
                Street,
                PostOfficeBox,
                City,
                State,
                PostalCode,
                CountryCode,
                Company,
                Manager,
                Title,
                Department,
                DirectReports,
                LogonHours,
                LogonWorkstation,
                UserGroups,
                PrimaryGroupId,
                LockoutTime,
                LockoutDuration,
                UserMustResetPassword,
                UserAccountControl,
                AccountExpires,
                ScriptPath,
                HomeDirectory,
                HomeDrive,
                HomePhoneMain,
                HomePhoneOther,
                PagerMain,
                PagerOther,
                MobileMain,
                MobileOther,
                FaxMain,
                FaxOther,
                IpPhoneMain,
                IpPhoneOther,
                Notes,
                CreatedAt,
                ChangedAt
            };
        }
    }
}
