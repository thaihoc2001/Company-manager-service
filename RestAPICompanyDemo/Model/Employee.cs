using System.ComponentModel.DataAnnotations;

namespace RestAPICompanyDemo.Model
{
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime OnboardDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int CurentDepartmentId { get; set; }
        public string ImageId { get; set; }
        public Image? Image { get; set; }
        public User? User { get; set; }
        public Department? Department { get; set; }
    }
}
