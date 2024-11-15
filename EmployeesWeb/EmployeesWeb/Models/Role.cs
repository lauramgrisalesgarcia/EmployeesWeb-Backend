using System.ComponentModel.DataAnnotations;

namespace EmployeesWeb.Models
{
    public class Role
    {
        [Key]
        public long Id { get; set; }

        [Required]
        
        public string Name { get; set; }

    }
}
