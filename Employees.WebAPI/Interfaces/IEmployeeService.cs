using Employees.WebAPI.Entities;

namespace Employees.WebAPI.Interfaces
{
    public interface IEmployeeService
    {
        bool Update(int id, Employee employee);
        bool Create(Employee employee);
        IEnumerable<Employee>? GetAll();
        Employee? GetById(int id);
        bool Delete(int id);
    }
}