namespace Systematix.WebAPI.Models.DTO.EmployeeDetailsDTO
{
    public class UpdateEmployeeDetailRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public double Salary { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string HighestEducation { get; set; } = string.Empty;
    }
}
