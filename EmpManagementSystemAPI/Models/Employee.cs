using System.ComponentModel.DataAnnotations;

namespace EmpManagementSystemAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
