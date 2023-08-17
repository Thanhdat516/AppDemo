using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;
        public CompanyController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompany()
        {
            var companies = await _context.Companies.ToListAsync();
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        [HttpPost]
        public async Task<ActionResult<List<Company>>> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var companies = await _context.Companies.ToListAsync();
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        [HttpPut]
        public async Task<ActionResult<List<Company>>> UpdateCompany(Company company)
        {
            var dbCompany = await _context.Companies.FindAsync(company.companyID);
            if (dbCompany == null)
            {
                return BadRequest("Company not found");
            }
            dbCompany.Name = company.Name;
            dbCompany.Phone = company.Phone;
            dbCompany.Address = company.Address;

            await _context.SaveChangesAsync();

            var companies = await _context.Companies.ToListAsync();
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        [HttpDelete("{id}")]        
        public async Task<ActionResult<List<Company>>> DeleteCompany(int id)
        {
            var dbCompany = await _context.Companies.FindAsync(id);
            if (dbCompany == null)
            {
                return BadRequest("Company not found");
            }
            _context.Companies.Remove(dbCompany);
            await _context.SaveChangesAsync();

            var companies = await _context.Companies.ToListAsync();
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }
    }
}
