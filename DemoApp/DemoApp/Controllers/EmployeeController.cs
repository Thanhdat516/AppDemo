using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeService _BLL;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _BLL = new Business_Logic_Layer.Services.EmployeeService(_employeeRepository);
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployees()
        {
            var employees = await _BLL.GetEmployees();
            if (employees is null)
            {
                return NotFound();
            }
            return Ok(employees.Value);
        }

        [HttpPost]
        public async Task<ActionResult<List<EmployeeModel>>> CreateEmployee(EmployeeModel employee)
        {
            var employees = await _BLL.CreateEmployee(employee);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees.Value);
        }

        [HttpPut]
        public async Task<ActionResult<List<EmployeeModel>>> UpdateEmployee(EmployeeModel employee)
        {
            var employees = await _BLL.UpdateEmployee(employee);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<EmployeeModel>>> DeleteEmployee(int id)
        {
            var employees = await _BLL.DeleteEmployee(id);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees.Value);
        }
    }
}
