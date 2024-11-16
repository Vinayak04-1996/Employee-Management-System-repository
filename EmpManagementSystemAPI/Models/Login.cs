using System.ComponentModel.DataAnnotations;

namespace EmpManagementSystemAPI.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        // Email address of the user
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Password for the user (hashed and stored securely, never store plain text password)
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // The date when the user was created
        public DateTime CreatedBy { get; set; } = DateTime.Now;


        // Flag to indicate whether the user is active or deactivated
        public bool IsActive { get; set; }
    }
}
