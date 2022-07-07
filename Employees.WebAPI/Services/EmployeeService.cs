using Employees.WebAPI.Entities;
using Employees.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employees.WebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext employeeDbContext)
        {
            _context = employeeDbContext;
        }

        public bool Create(Employee employee)
        {
            if (_context.Employees == null)
            {
                return false;
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null && _context.Employees != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Employee>? GetAll()
        {
            return _context.Employees?.ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees?.Find(id);
        }

        public bool Update(int id, Employee employee)
        {
            if (_context.Employees != null && EmployeeExists(employee.Id) && employee.Id == id)
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}