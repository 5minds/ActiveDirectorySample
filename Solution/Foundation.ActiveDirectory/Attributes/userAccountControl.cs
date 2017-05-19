// ReSharper disable InconsistentNaming
// Reason: These names correspond exactly to the flags used by ActiveDirectory. They should not be altered.
namespace ActiveDirectoryBlogpost.Foundation.Attributes
{
    /// <summary>
    /// Contains all the flags that the userAccountControl Attribute can have.
    /// </summary>
    public enum UserAccountControl
    {
        /// <summary>
        /// Account is disabled.
        /// </summary>
        UF_ACCOUNT_DISABLE = 2,

        /// <summary>
        /// A home-directory is required.
        /// </summary>
        UF_HOMEDIR_REQUIRED = 8, 

        /// <summary>
        /// Account is locked out.
        /// </summary>
        UF_LOCKOUT = 16,

        /// <summary>
        /// No password is required.
        /// </summary>
        UF_PASSWD_NOTREQD = 32,

        /// <summary>
        /// Password cannot be changed.
        /// </summary>
        UF_PASSWD_CANT_CHANGE = 64, 

        /// <summary>
        /// Password encryption allowed.
        /// </summary>
        UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 128, 

        /// <summary>
        /// The account is a normal account.
        /// </summary>
        UF_NORMAL_ACCOUNT = 512,

        /// <summary>
        /// The account is an inter-domain trusted account.
        /// </summary>
        UF_INTERDOMAIN_TRUST_ACCOUNT = 2048, 

        /// <summary>
        /// The account is a workstation trusted account.
        /// </summary>
        UF_WORKSTATION_TRUST_ACCOUNT = 4096, 

        /// <summary>
        /// The account is a server trusted account.
        /// </summary>
        UF_SERVER_TRUST_ACCOUNT = 8192, 

        /// <summary>
        /// The accounts password does not expire.
        /// </summary>
        UF_DONT_EXPIRE_PASSWD = 65536, 

        /// <summary>
        /// Microsoft Logon account.
        /// </summary>
        UF_MNS_LOGON_ACCOUNT = 131072, 

        /// <summary>
        /// The account requires a smartcard.
        /// </summary>
        UF_SMARTCARD_REQUIRED = 262144,
         
        /// <summary>
        /// The account is trusted for delegation.
        /// </summary>
        UF_TRUSTED_FOR_DELEGATION = 524288, 

        /// <summary>
        /// Account cannot be delegated.
        /// </summary>
        UF_NOT_DELEGATED = 1048576,

        /// <summary>
        /// Account only uses DES encryption keys.
        /// </summary>
        UF_USE_DES_KEY_ONLY = 2097152, 

        /// <summary>
        /// Account does not require pre-authorization.
        /// </summary>
        UF_DONT_REQUIRE_PREAUTH = 4194304, 

        /// <summary>
        /// Password is expired.
        /// </summary>
        UF_PASSWORD_EXPIRED = 8388608,

        /// <summary>
        /// The account is trusted to authenticate others for delegation.
        /// </summary>
        UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 16777216,

        /// <summary>
        /// The account does not require Authentication Data.
        /// </summary>
        UF_NO_AUTH_DATA_REQUIRED = 33554432,

        /// <summary>
        /// The account contains partial secrets.
        /// </summary>
        UF_PARTIAL_SECRETS_ACCOUNT = 67108864
    }
}
