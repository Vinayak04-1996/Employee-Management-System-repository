using EmpManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpManagementSystemAPI.EmployeeDbContext
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options) 
        {
               
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
