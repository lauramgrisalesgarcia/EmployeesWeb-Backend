using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesWeb.Models
{
    public class Employee
    {
        public required string Identification { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe tener una longitud máxima de {1} caracteres.")]
        public string Name { get; set; }

        public string? Surname { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
