using System.ComponentModel.DataAnnotations;

namespace RestAPICompanyDemo.Model
{
    public class Department
    {
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
