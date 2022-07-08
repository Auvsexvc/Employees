using Employees.WebAPI.Entities;
using Employees.WebAPI.Exceptions;
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

        public int Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.Id;
        }

        public void Delete(int id)
        {
            var employee = GetById(id);

            if (employee is null)
            {
                throw new NotFoundException("Employee not found");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee>? GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {

            var employee = _context.Employees.Find(id);
            if (employee is null)
            {
                throw new NotFoundException("Employee not found");
            }

            return employee;
        }

        public void Update(int id, Employee employee)
        {
            var employeeDb = _context.Employees.Find(id);
            if (employeeDb is null)
            {
                throw new NotFoundException("Employee not found");
            }

            employeeDb.Name = employee.Name;
            employeeDb.Department = employee.Department;
            employeeDb.DateOfJoining = employee.DateOfJoining;
            employeeDb.PhotoFileName = employee.PhotoFileName;
            _context.SaveChanges();
        }
    }
}