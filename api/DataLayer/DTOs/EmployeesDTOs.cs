using System.Text.Json.Serialization;


namespace DataLayer.DTOs
{
    public class EmployeeShortDTO
    {
        [JsonPropertyName("workdayNumber")]
        public int Workday { get; set; }
        [JsonPropertyName("identificationCard")]
        public int IdCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
    public class EmployeeLongDTO : EmployeeShortDTO
    {
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
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
    }

    public class InitDataDTO
    {
        public List<DropDownItemDTO> Sites { get; set; }
        public List<BanksDropDownItemDTO> Banks { get; set; }
        public List<DropDownItemDTO> Sups { get; set; }
        public List<DropDownItemDTO> Jobs { get; set; }

    }
}
