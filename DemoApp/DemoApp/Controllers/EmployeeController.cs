using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _bllEmployee;

        public EmployeeController(IEmployeeService employeeService)
        {
            _bllEmployee = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployees()
        {
            var employees = await _bllEmployee.GetEmployees();
            if (employees is null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<List<EmployeeModel>>> CreateEmployee(EmployeeModel employee)
        {
            var employees = await _bllEmployee.CreateEmployee(employee);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpPut]
        public async Task<ActionResult<List<EmployeeModel>>> UpdateEmployee(EmployeeModel employee)
        {
            var employees = await _bllEmployee.UpdateEmployee(employee);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<EmployeeModel>>> DeleteEmployee(int id)
        {
            var employees = await _bllEmployee.DeleteEmployee(id);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }
    }
}
