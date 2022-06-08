using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestAPICompanyDemo.Model;
using RestAPICompanyDemo.util;

namespace RestAPICompanyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContextCompany _context;
        private readonly uploadFile _uploadFile;

        public EmployeeController(DBContextCompany context)
        {
            _context = context;
            Console.WriteLine("OKE");
            _uploadFile = new uploadFile();
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            //return await _context.employees.Select(x => ItemToDTO(x))
            //    .ToListAsync();

            var employee = _context.employees.FromSqlRaw("Select * from employees").ToList();
            employee.ForEach(e =>
            {
                Console.WriteLine(e);
                var department = _context.departments.FromSqlRaw("Select * from \"departments\" where \"DepartmentId\" =" + e.CurentDepartmentId).FirstOrDefault();
                Console.WriteLine(department);
                if (department != null)
                {
                    e.Department.DepartmentId = department.DepartmentId;
                    e.Department.DepartmentDescription = department.DepartmentDescription;
                    e.Department.DepartmentName = department.DepartmentName;
                    e.Department.Employees = null;
                }
                var image = _context.images.FromSqlRaw("Select * from \"images\" where \"Imageid\" = \'" + e.ImageId + "\'").FirstOrDefault();
                if(image != null)
                {
                    e.Image.Imageid = image.Imageid;
                    e.Image.ImageUrl = image.ImageUrl;
                    e.Image.Employee = image.Employee;
                }
                ItemToDTO(e);
            });
            Console.WriteLine(employee);
            return employee;
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

        [HttpPost("upload")]
        public async Task<ActionResult<Image>> Post(List<IFormFile> files)
        {
            var newImage = new Image();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),"images", formFile.FileName);
                    using (Stream stream = new FileStream(filePath, FileMode.Create))
                    {
                     
                        await formFile.CopyToAsync(stream);
                        Console.WriteLine(filePath);
                    }
                    var image = _uploadFile.uploadImage(filePath);

                    newImage.Imageid = image.Imageid;
                    newImage.ImageUrl = image.ImageUrl;
                    newImage.Employee = image.Employee;
                    _context.images.Add(newImage);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok(newImage);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {

            var newEmployee = new Employee
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                OnboardDate = employee.OnboardDate,
                Phone = employee.Phone,
                CurentDepartmentId = employee.CurentDepartmentId,
                Address = employee.Address,
                Description = employee.Description,
                ImageId = employee.ImageId,
                User = null,
                Department = null,
                Image = null
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
            findEmployee.Phone = employee.Phone;
            findEmployee.Address = employee.Address;
            findEmployee.Description = employee.Description;
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
                Phone = employee.Phone,
                CurentDepartmentId = employee.CurentDepartmentId,
                Address = employee.Address,
                Description = employee.Description,
            };

         
    }
}
