using System.ComponentModel.DataAnnotations;

namespace EmployeesWeb.Models
{
    public class EmployeeDto
    {
        [Key]
        public long Id { get; set; }
        public required string Identification { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required long RoleId { get; set; }
    }
}
