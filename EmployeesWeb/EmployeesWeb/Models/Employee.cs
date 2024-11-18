using System.ComponentModel.DataAnnotations;

namespace EmployeesWeb.Models
{
    public class Employee
    {
        public required string Identification { get; set; }

        
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe tener una longitud máxima de {1} caracteres.")]
        public required string Name { get; set; }

        public string? Surname { get; set; }

        public required DateOnly DateOfBirth { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
