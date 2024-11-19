// Description: Employee Controller. Here we admin all the methods forthe API
// Author: Laura Grisales
// Date: 18/11/2024

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
        // Description: GET all the employees
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Getemployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // Description: GET one employee
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees/{id}
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
        // Description: PUT one employee
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees/{id}
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
                    if (!EmailExist(id, employee.Email) && existingEmployee is not null)
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
        // Description: POST a new employee
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(Employee employee)
        {
            // Description: Create a API estructure because we don't need the employee id
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
        // Description: DELETE a employee
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees/{id}
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
        // Description: DELETE a list of employees
        // Author: Laura Grisales
        // Date: 18/11/2024
        // URL: api/Employees/list
        [HttpDelete("list")]
        public async Task<ActionResult> DeleteEmployees([FromBody] List<long> employeeIds)
        {
            if (employeeIds == null || employeeIds.Count == 0)
            {
                return BadRequest("No se han encontrado empleados para eliminar.");
            }
            // Description: Finds all employees in the employeesId list
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
        // Description: Returns true if the employee id exists
        // Author: Laura Grisales
        // Date: 18/11/2024
        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        // Description: Returns true if the employee id and the employee identification exists
        // Author: Laura Grisales
        // Date: 18/11/2024
        private bool EmployeeExists(long id, string identification)
        {
            return _context.Employees.Any(e => e.Id == id && e.Identification.Equals(identification));
        }
        // Description: Returns true if the employee identification or the employee email exists
        // Author: Laura Grisales
        // Date: 18/11/2024
        private bool EmployeeNotExists(string identification, string email)
        {
            return _context.Employees.Any(e => e.Identification.Equals(identification) || e.Email.Equals(email));
        }
        // Description: Returns true if other employee has the same employee mail  
        // Author: Laura Grisales
        // Date: 18/11/2024
        private bool EmailExist(long id, string email)
        {
            return _context.Employees.Any(e => e.Id != id && e.Email.Equals(email));
        }
        // Description: Returns true if the employee dateofbirth is between the range
        // Author: Laura Grisales
        // Date: 18/11/2024
        private bool EmployeeAge(DateOnly DateOfBirth)
        {
            DateTime todayDate = DateTime.Now;
            DateOnly minDate = DateOnly.FromDateTime(todayDate.AddYears(-18));
            DateOnly maxDate = DateOnly.FromDateTime(todayDate.AddYears(-65));
            return (DateOfBirth < maxDate || DateOfBirth > minDate) ? false : true;
        }
    }
}
