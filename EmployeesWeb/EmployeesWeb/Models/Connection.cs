using Microsoft.EntityFrameworkCore;
using EmployeesWeb.Models;

namespace EmployeesWeb.Models;

public class Connection: DbContext
{
    public Connection(DbContextOptions<Connection>  options)
        : base(options)
    {
    }

    public DbSet<Employee> employees { get; set; } = null!;

public DbSet<EmployeesWeb.Models.Role> Role { get; set; } = default!;
}
