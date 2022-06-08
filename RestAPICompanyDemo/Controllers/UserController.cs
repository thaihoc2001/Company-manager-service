using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPICompanyDemo.Model;

namespace RestAPICompanyDemo.Controllers
{
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private static DBContextCompany _context;
        public UserController(DBContextCompany context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> getAllUser()
        {
            return await _context.users.Select(u => UserToDTO(u)).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        private static User UserToDTO(User user) =>
            new User
            {
                UserName = user.UserName,
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Employee = user.Employee,
                CreatedDate = user.CreatedDate,
                CurentEmployeeId = user.CurentEmployeeId,
                Role = user.Role
            };
    }
}
