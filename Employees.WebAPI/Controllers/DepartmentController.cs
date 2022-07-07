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
            return _departmentService.Delete(id) ? Ok(department) : NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var department = _departmentService.GetById(id);
            return department == null ? NotFound() : Ok(department);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            var departments = _departmentService.GetAll();
            return departments != null ? Ok(departments) : NoContent();
        }

        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            return _departmentService.Create(department) ? CreatedAtAction("GetDepartment", new { id = department.Id }, department) : StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            return _departmentService.Update(id, department) ? Ok(department) : NotFound();
        }
    }
}