namespace EmployeeManagement.API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public DateTime DateOfJoining { get; set; }
        public string Department { get; set; } = null!;
        public string Status { get; set; } = "Active";
        public string? Location { get; set; }
        public string? SalaryBand { get; set; }
    }
}