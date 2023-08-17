using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public interface ICompanyRepository
    {
        public Task<List<Company>> GetCompanies();
        public Task<List<Company>> CreateCompany(Company company);
        public Task<List<Company>> UpdateCompany(Company company);
        public Task<List<Company>> DeleteCompany(int companyID);
        public Task<String> GetNameCompany(int companyID);
    }
}
