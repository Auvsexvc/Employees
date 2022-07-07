using Employees.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.WebAPI
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Department>? Departments { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
    }
}
