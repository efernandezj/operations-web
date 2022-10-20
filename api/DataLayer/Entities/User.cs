using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.Entities
{
    public class User
    {
        [JsonPropertyName("workdayNumber")]
        public int Workday { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public long? RefreshTokenExp { get; set; }
        public bool IsActive { get; set; }


        /* Navigation Properties */
        [JsonIgnore]
        public Employee Employee { get; set; }
        public List<Role> RolesCreator { get; set; }
        public List<Role> RolesModifier { get; set; }
    }
}
