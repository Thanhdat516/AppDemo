using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _bllCompany;

        public CompanyController(ICompanyService companyService)
        {
            _bllCompany = companyService ?? throw new ArgumentNullException(nameof(companyService));
            /*_BLL = new Business_Logic_Layer.Services.ICompanyService;*/
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyModel>>> GetCompanies()
        {
            var companies = await _bllCompany.GetCompanies();
            if (companies == null)
            {
                return NotFound();
            }
            return Ok(companies);
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
            var companies = await _bllCompany.CreateCompany(company);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        [HttpPut]
        public async Task<ActionResult<List<CompanyModel>>> UpdateCompany(CompanyModel company)
        {
            var companies = await _bllCompany.UpdateCompany(company);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CompanyModel>>> DeleteCompany(int id)
        {
            var companies = await _bllCompany.DeleteCompany(id);
            if (companies is null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        /*        [HttpGet("{id}")]
                public async Task<ActionResult<String>> GetNameCompany(int id)
                {
                    var namecompany = await _BLL.GetNameCompany(id);
                    if (namecompany is null)
                    {
                        return NotFound();
                    }

                    return Ok(namecompany.Value);
                }*/
    }
}
