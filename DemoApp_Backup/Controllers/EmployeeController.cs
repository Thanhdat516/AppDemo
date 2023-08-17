using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            var employees = await _context.Employees.Include(c => c.Company).ToListAsync();
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employees = await _context.Employees.ToListAsync();
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee)
        {
            var dbEmployee = await _context.Employees.FindAsync(employee.employeeID);
            if (dbEmployee == null)
            {
                return BadRequest("Company not found");
            }
            dbEmployee.Name = employee.Name;
            dbEmployee.Phone = employee.Phone;
            dbEmployee.Address = employee.Address;
/*          dbEmployee.companyID = (int)employee.companyID;*/

            await _context.SaveChangesAsync();

            var employees = await _context.Companies.ToListAsync();
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbEmployee = await _context.Employees.FindAsync(id);
            if (dbEmployee == null)
            {
                return BadRequest("Company not found");
            }
            _context.Employees.Remove(dbEmployee);
            await _context.SaveChangesAsync();

            var employees = await _context.Employees.ToListAsync();
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }


    }
}
