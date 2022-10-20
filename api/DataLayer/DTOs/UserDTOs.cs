using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayer.DTOs
{
    public class UserCreationDTO
    {
        [Required]
        [JsonPropertyName("workdayNumber")]
        public int Workday { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserInfoDTO
    {
        [JsonPropertyName("workdayNumber")]
        public int Workday { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get => $"{FirstName} {LastName}"; }

        public string Username { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}
