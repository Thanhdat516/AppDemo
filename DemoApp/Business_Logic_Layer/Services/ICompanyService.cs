using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business_Logic_Layer.Services
{
    public interface ICompanyService
    {
        public Task<List<CompanyModel>> GetCompanies();
        public Task<IActionResult> CreateCompany(CompanyModel company);
        public Task<IActionResult> UpdateCompany(CompanyModel company);
        public Task<IActionResult> DeleteCompany(int companyID);
    }
}
