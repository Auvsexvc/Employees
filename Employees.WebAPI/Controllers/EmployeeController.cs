using Employees.WebAPI.Entities;
using Employees.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employees.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IWebHostEnvironment env, IEmployeeService employeeService)
        {
            _env = env;
            _employeeService = employeeService;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employeeService.GetById(id);
            return _employeeService.Delete(id) ? Ok(employee) : NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = _employeeService.GetAll();
            return employees != null ? Ok(employees) : NoContent();
        }

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            return _employeeService.Create(employee) ? CreatedAtAction("GetEmployee", new { id = employee.Id }, employee) : StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            return _employeeService.Update(id, employee) ? Ok(employee) : NotFound();
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}