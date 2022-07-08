using Employees.WebAPI.Entities;
using Employees.WebAPI.Exceptions;
using Employees.WebAPI.Interfaces;

namespace Employees.WebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext _context;

        public DepartmentService(EmployeeDbContext employeeDbContext)
        {
            _context = employeeDbContext;
        }

        public int Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();

            return department.Id;
        }

        public void Delete(int id)
        {
            var department = GetById(id);

            if (department is null)
            {
                throw new NotFoundException("Department not found");
            }
            var employeesWithDepartment = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employeesWithDepartment != null)
            {
                throw new BadRequestException("There are still employees assigned to this department");
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public IEnumerable<Department>? GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);

            if (department is null)
            {
                throw new NotFoundException("Department not found");
            }

            return department;
        }

        public void Update(int id, Department department)
        {
            var departmentDb = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (departmentDb is null)
            {
                throw new NotFoundException("Department not found");
            }
            departmentDb.Name = department.Name;
            _context.SaveChanges();
        }
    }
}