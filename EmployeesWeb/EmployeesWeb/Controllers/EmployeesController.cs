using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesWeb.Models;

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
            return await _context.employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
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
                if (EmployeeExists(id) 
                    && !EmployeeNotExists (employee.Identification, employee.Email) 
                    && EmployeeExists (id, employee.Identification))
                {
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return NoContent();

            //id y el empoyee id empaten. Que corresponda uno con el otro. Sino, que no permita editar
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
                DateofBirth = employee.DateOfBirth,
                Email = employee.Email,
                RoleId = employee.RoleId
            };

            try
            {
                if (!EmployeeNotExists(employee.Identification, employee.Email))
                {
                    _context.employees.Add(newEmployee);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Conflict();
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
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.Id == id);
        }

        private bool EmployeeExists(long id, string identification)
        {
            return _context.employees.Any(e => e.Id == id && e.Identification.Equals(identification));
        }

        private bool EmployeeNotExists(string identification, string email)
        {
            return _context.employees.Any(e => e.Identification.Equals(identification) || e.Email.Equals(email));
        }
    }
}
