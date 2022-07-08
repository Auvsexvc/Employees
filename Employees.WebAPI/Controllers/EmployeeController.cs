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
            _employeeService.Delete(id);

            return Ok(employee);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetById(id);

            return Ok(employee);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = _employeeService.GetAll();

            return Ok(employees);
        }

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            int newId = _employeeService.Create(employee);

            return CreatedAtAction("GetEmployee", new { id = newId }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            _employeeService.Update(id, employee);

            return Ok(employee);
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