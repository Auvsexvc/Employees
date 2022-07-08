using Employees.WebAPI.Entities;

namespace Employees.WebAPI.Interfaces
{
    public interface IDepartmentService
    {
        void Update(int id, Department department);
        int Create(Department department);
        IEnumerable<Department>? GetAll();
        Department? GetById(int id);
        void Delete(int id);
    }
}
