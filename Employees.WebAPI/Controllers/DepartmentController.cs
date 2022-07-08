using Employees.WebAPI.Entities;
using Employees.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace departments.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _departmentService.GetById(id);
            _departmentService.Delete(id);

            return Ok(department);
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var department = _departmentService.GetById(id);

            return Ok(department);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            var departments = _departmentService.GetAll();

            return Ok(departments);
        }

        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            int newId = _departmentService.Create(department);

            return CreatedAtAction("GetDepartment", new { id = newId }, department);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            _departmentService.Update(id, department);

            return Ok(department);
        }
    }
}