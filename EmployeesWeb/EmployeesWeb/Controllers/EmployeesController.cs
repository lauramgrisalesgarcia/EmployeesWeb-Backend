using System.Data;
using EmployeesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly Connection _context;

        public EmployeesController(Connection context)
        {
            _context = context;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Getemployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("El empleado no existe.");
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            
            try
            {
                var existingEmployee = await _context.Employees.FindAsync(id);

                if (!EmployeeExists(id))
                {
                    return NotFound("El empleado no existe.");
                }

                if (EmployeeExists(id, employee.Identification))
                {
                    if (!EmailExist(id, employee.Email))
                    {
                        existingEmployee.Name = employee.Name;
                        existingEmployee.Surname = employee.Surname;
                        existingEmployee.DateOfBirth = employee.DateOfBirth;
                        existingEmployee.Email = employee.Email;
                        existingEmployee.RoleId = employee.RoleId;

                        _context.Employees.Update(existingEmployee);
                        await _context.SaveChangesAsync();

                        return Ok();
                    }

                }

                return Conflict("Los datos ya existen.");

            }
            catch (Exception e)
            {
                return NotFound("El empleado no existe.");
            }
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(Employee employee)
        {
            var newEmployee = new EmployeeDto
            {
                Identification = employee.Identification,
                Name = employee.Name,
                Surname = employee.Surname,
                DateOfBirth = employee.DateOfBirth,
                Email = employee.Email,
                RoleId = employee.RoleId
            };

            try
            {
                if (!EmployeeNotExists(employee.Identification, employee.Email))
                {
                    if (EmployeeAge(employee.DateOfBirth))
                    {
                        _context.Employees.Add(newEmployee);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest("La edad debe ser mayor a 18 años y menor a 65 años.");
                    }
                }
                else
                {
                    return Conflict("Los datos ya existen.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return await GetEmployee(newEmployee.Id);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("El empleado no existe.");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("list")]
        public async Task<ActionResult> DeleteEmployees([FromBody] List<long> employeeIds)
        {
            if (employeeIds == null || employeeIds.Count == 0)
            {
                return BadRequest("No se han encontrado empleados para eliminar.");
            }

            var employeesToDelete = await _context.Employees
                                                  .Where(e => employeeIds.Contains(e.Id))
                                                  .ToListAsync();

            if (employeesToDelete.Count == 0)
            {
                return NotFound("No se han encontrado empleados para eliminar.");
            }

            _context.Employees.RemoveRange(employeesToDelete);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Empleado(s) eliminado(s)", deletedIds = employeeIds });
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        private bool EmployeeExists(long id, string identification)
        {
            return _context.Employees.Any(e => e.Id == id && e.Identification.Equals(identification));
        }

        private bool EmployeeNotExists(string identification, string email)
        {
            return _context.Employees.Any(e => e.Identification.Equals(identification) || e.Email.Equals(email));
        }

        private bool EmailExist(long id, string email)
        {
            return _context.Employees.Any(e => e.Id != id && e.Email.Equals(email));
        }

        private bool EmployeeAge(DateOnly DateOfBirth)
        {
            DateTime todayDate = DateTime.Now;
            DateOnly minDate = DateOnly.FromDateTime(todayDate.AddYears(-18));
            DateOnly maxDate = DateOnly.FromDateTime(todayDate.AddYears(-65));
            return (DateOfBirth < maxDate || DateOfBirth > minDate) ? false : true;
        }
    }
}
