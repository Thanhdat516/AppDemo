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
            var isListed = await _bllCompany.GetCompanies();
            if (isListed == null)
            {
                return BadRequest();
            }
            return Ok(isListed);
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
        public async Task<IActionResult> CreateCompany(CompanyModel company)
        {
            var isCreated = await _bllCompany.CreateCompany(company);
            if (isCreated is null || isCreated is false)
            {
                return BadRequest();
            }

            return Ok("Created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(CompanyModel company)
        {
            var isUpdated = await _bllCompany.UpdateCompany(company);
            if (isUpdated is null || isUpdated is false)
            {
                return BadRequest();
            }

            return Ok("Updated successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var isDeleted = await _bllCompany.DeleteCompany(id);
            if (isDeleted is null || isDeleted is false)
            {
                return BadRequest();
            }

            return Ok("Deleted successfully");
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
