using Microsoft.AspNetCore.Mvc;
using UnitTest_Demo.Model;
using UnitTest_Demo.Services;

namespace UnitTest_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {  
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEmployee = _employeeService.Add(employee);
            ActionResult<Employee> result = CreatedAtAction(nameof(Get), new { id = newEmployee.EmployeeId }, newEmployee); 
            
            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_employeeService.Delete(id))
            {
                return Ok("Employee deleted successfully");
            }
            return NotFound();
        }
    }
}
