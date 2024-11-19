// Description: EmployeeDto model. This will be the structure for the API
// Author: Laura Grisales
// Date: 18/11/2024
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
