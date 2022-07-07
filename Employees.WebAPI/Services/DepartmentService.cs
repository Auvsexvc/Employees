using Employees.WebAPI.Entities;
using Employees.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employees.WebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext _context;

        public DepartmentService(EmployeeDbContext employeeDbContext)
        {
            _context = employeeDbContext;
        }
        public bool Create(Department department)
{
            if (_context.Departments == null)
            {
                return false;
}
            _context.Departments.Add(department);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var department = GetById(id);
            if (department != null && _context.Departments != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Department>? GetAll()
        {
            return _context.Departments?.ToList();
        }

        public Department? GetById(int id)
        {
            return _context.Departments?.Find(id);
        }

        public bool Update(int id, Department department)
        {
            if (_context.Departments != null && DepartmentExists(department.Id) && department.Id == id)
            {
                _context.Entry(department).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}