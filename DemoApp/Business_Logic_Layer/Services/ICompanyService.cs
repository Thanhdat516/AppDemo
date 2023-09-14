using Business_Logic_Layer.Models;

namespace Business_Logic_Layer.Services
{
    public interface ICompanyService
    {
        public Task<List<CompanyModel>> GetCompanies();
        public Task<bool?> CreateCompany(CompanyModel company);
        public Task<bool?> UpdateCompany(CompanyModel company);
        public Task<bool?> DeleteCompany(int companyID);
    }
}
