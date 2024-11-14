using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesWeb.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Name lenght must be between {2} and {1}.")]
        public string Name { get; set; }

        public string? Surname { get; set; }

        [Required]
        [Range(typeof(DateOnly), "1959-01-01", "2006-01-01", ErrorMessage = " The age must be over 18 years old and under 65 years old")]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [Remote("EmployeeEmailExists", "Employee", ErrorMessage = "The employee email already exist")]
        public string Email { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
