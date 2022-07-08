using Employees.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.WebAPI
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
    }
}
