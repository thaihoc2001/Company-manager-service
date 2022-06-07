using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICompanyDemo.Model
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public String Password { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurentEmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
