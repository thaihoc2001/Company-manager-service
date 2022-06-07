using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestAPICompanyDemo.Model;

namespace RestAPICompanyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContextCompany _context;

        public EmployeeController(DBContextCompany context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            return await _context.employees.Select(x => ItemToDTO(x))
                .ToListAsync();

            //return _context.employees.FromSqlRaw("Select * from employees").ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployById(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return ItemToDTO(employee);
        }
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesByDepartmentId(int departmentId)
        {
            var employee = _context.employees.FromSqlRaw("Select * from employees where \"CurentDepartmentId\" = " + departmentId).ToList();
            if(employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var newEmployee = new Employee
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                OnboardDate = employee.OnboardDate,
                ImageAvatar = employee.ImageAvatar,
                Phone = employee.Phone,
                CurentDepartmentId = employee.CurentDepartmentId,
                Address = employee.Address,
                User = null,
                Department = null,
            };
            _context.employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployById),
                new { id = newEmployee.EmployeeId },
                ItemToDTO(newEmployee)
                );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var findEmployee = await _context.employees.FindAsync(id);
            if (id != findEmployee.EmployeeId)
            {
                return BadRequest("Not user 1");
            }

            if (findEmployee == null)
            {
                return NotFound("Not user 2");
            }

            findEmployee.EmployeeId = employee.EmployeeId;
            findEmployee.EmployeeName = employee.EmployeeName;
            findEmployee.OnboardDate = employee.OnboardDate;
            findEmployee.ImageAvatar = employee.ImageAvatar;
            findEmployee.Phone = employee.Phone;
            findEmployee.Address = employee.Address;
            findEmployee.CurentDepartmentId = employee.CurentDepartmentId;
            findEmployee.User = null;
            findEmployee.Department = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!EmployeeExists(id))
            {
                return NotFound("Not user 3");
            }
            return NoContent();
        }
        [HttpDelete("{id}")]        
        
        public async Task<IActionResult> DeleteEmployee(int id)
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
            return _context.employees.Any(e => e.EmployeeId == id);
        }

        private static Employee ItemToDTO(Employee employee) =>
            new Employee
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                OnboardDate = employee.OnboardDate,
                ImageAvatar = employee.ImageAvatar,
                Phone = employee.Phone,
                CurentDepartmentId = employee.CurentDepartmentId,
                Address = employee.Address
            };

         
    }
}
