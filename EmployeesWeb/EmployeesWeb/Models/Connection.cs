using Microsoft.EntityFrameworkCore;

namespace EmployeesWeb.Models;

public class Connection: DbContext
{
    public Connection(DbContextOptions<Connection>  options)
        : base(options)
    {
    }

    public DbSet<EmployeeDto> employees { get; set; } = null!;

public DbSet<Role> Role { get; set; } = default!;
}
