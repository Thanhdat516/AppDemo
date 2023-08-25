using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.Services
{
    public interface ICompanyService
    {
        public Task<List<CompanyModel>> GetCompanies();
        public Task<List<Company>> CreateCompany(CompanyModel company);
        public Task<List<Company>> UpdateCompany(CompanyModel company);
        public Task<List<Company>> DeleteCompany(int companyID);
    }
}
