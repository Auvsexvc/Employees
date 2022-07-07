using Employees.WebAPI.Entities;

namespace Employees.WebAPI.Interfaces
{
    public interface IDepartmentService
    {
        bool Update(int id, Department department);
        bool Create(Department department);
        IEnumerable<Department>? GetAll();
        Department? GetById(int id);
        bool Delete(int id);
    }
}
