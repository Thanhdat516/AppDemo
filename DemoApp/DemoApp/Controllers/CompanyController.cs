using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyService _BLL;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _BLL = new Business_Logic_Layer.Services.CompanyService(_companyRepository);
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyModel>>> GetCompanies()
        {
            var companies = await _BLL.GetCompanies();
            if (companies is null)
            {
                return NotFound();
            }
            return Ok(companies.Value);
        }

        /*        [HttpGet]
                [Route("getPerson")]
                public ActionResult<CompanyModel> GetCompanyById(int id)
                {
                    var data = _BLL.GetCompanyById(id);

                    if (data == null)
                    {
                        return NotFound("Invalid ID");
                    }
                    return Ok(data);
                }*/

        [HttpPost]
        public async Task<ActionResult<List<CompanyModel>>> CreateCompany(CompanyModel company)
        {
            var companies = await _BLL.CreateCompany(company);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies.Value);
        }

        [HttpPut]
        public async Task<ActionResult<List<CompanyModel>>> UpdateCompany(CompanyModel company)
        {
            var companies = await _BLL.UpdateCompany(company);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CompanyModel>>> DeleteCompany(int id)
        {
            var companies = await _BLL.DeleteCompany(id);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<String>> GetNameCompany(int id)
        {
            var namecompany = await _BLL.GetNameCompany(id);
            if (namecompany is null)
            {
                return NotFound();
            }

            return Ok(namecompany.Value);
        }
    }
}
