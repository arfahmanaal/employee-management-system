using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, MinimumLength = 10)]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(100)]
        public string Designation { get; set; } = null!;

        [Required(ErrorMessage = "Department is required")]
        [StringLength(50)]
        public string Department { get; set; } = null!;

        [Required(ErrorMessage = "Date of joining is required")]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public string Status { get; set; } = "Active";

        public string? Location { get; set; }
        public string? SalaryBand { get; set; }
    }
}