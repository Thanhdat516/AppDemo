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
            var isListed = await _bllEmployee.GetEmployees();
            if (isListed is null)
            {
                return BadRequest();
            }
            return Ok(isListed);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeModel employee)
        {
            var isCreated = await _bllEmployee.CreateEmployee(employee);
            if (isCreated is null || isCreated is false)
            {
                return BadRequest();
            }

            return Ok("Created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employee)
        {
            var isUpdated = await _bllEmployee.UpdateEmployee(employee);
            if (isUpdated is null || isUpdated is false)
            {
                return BadRequest();
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _bllEmployee.DeleteEmployee(id);
            if (isDeleted is null || isDeleted is false)
            {
                return BadRequest();
            }

            return Ok("Deleted successfully");
        }
    }
}
