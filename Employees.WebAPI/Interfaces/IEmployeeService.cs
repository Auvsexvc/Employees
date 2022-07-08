using Employees.WebAPI.Entities;

namespace Employees.WebAPI.Interfaces
{
    public interface IEmployeeService
    {
        void Update(int id, Employee employee);
        int Create(Employee employee);
        IEnumerable<Employee>? GetAll();
        Employee GetById(int id);
        void Delete(int id);
    }
}