using System.Text.Json.Serialization;

namespace DataLayer.Entities
{
    public class Employee
    {
        [JsonPropertyName("workdayNumber")]
        public int Workday { get; set; }
        [JsonPropertyName("identificationCard")]
        public int IdCard { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("birthDate")]
        public DateTime Birthdate { get; set; }
        [JsonPropertyName("jobTitle")]
        public int JobId { get; set; }
        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }
        [JsonPropertyName("bankName")]
        public int BankId { get; set; }
        [JsonPropertyName("bacAccountNumber")]
        public string BackAccountNumber { get; set; }
        [JsonPropertyName("supervisor")]
        public int SupervisorId { get; set; }
        [JsonPropertyName("site")]
        public int SiteId { get; set; }
        [JsonPropertyName("hireDate")]
        public DateTime HireDate { get; set; }
        [JsonPropertyName("fireDate")]
        public DateTime? FireDate { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        /* Navigation Properties */
        [JsonIgnore]
        public JobTitle JobTitle { get; set; }
        [JsonIgnore]
        public Site Site { get; set; }
        [JsonIgnore]
        public Bank Bank { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
