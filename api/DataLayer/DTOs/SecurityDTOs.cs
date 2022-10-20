using System.ComponentModel.DataAnnotations;

namespace DataLayer.DTOs 
{
    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AccountInfoShort
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LabelAccountName { get => $"{FirstName[0]}. {LastName.Split()[0]}"; }
        public string Role { get; set; }

    }

    public class PasswordHash
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }

    public class AuthenticatedResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
