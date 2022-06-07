using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPICompanyDemo.Model;

namespace RestAPICompanyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DBContextCompany _context;

        public DepartmentController(DBContextCompany context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Department>>> getAllDepartment()
        {
            return await _context.Departments.Select(d => DepartmentToDTO(d))
                    .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> getDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return DepartmentToDTO(department);
        }
        [HttpPost]
        public async Task<ActionResult<Department>> AddDeepartment(Department department)
        {
            var newDepartment = new Department
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                DepartmentDescription = department.DepartmentDescription,
                Employees = null
            };
            _context.Departments.Add(newDepartment);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(getDepartmentById),
                new { id = newDepartment.DepartmentId },
                DepartmentToDTO(newDepartment)
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, Department department)
        {
            Department findDepartment = await _context.Departments.FindAsync(id);
            if (findDepartment == null)
            {
                return NotFound();
            }
            findDepartment.DepartmentName = department.DepartmentName;
            findDepartment.DepartmentDescription = department.DepartmentDescription;

            _context.Departments.Update(findDepartment);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(getDepartmentById),
                new { id = findDepartment.DepartmentId },
                DepartmentToDTO(findDepartment)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department findDepartment = await _context.Departments.FindAsync(id);
            if (findDepartment == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(findDepartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static Department DepartmentToDTO(Department department) =>
            new Department
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                DepartmentDescription = department.DepartmentDescription,
            };
    }
}
