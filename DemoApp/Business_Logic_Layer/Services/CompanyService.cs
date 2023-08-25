using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;

namespace Business_Logic_Layer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly Mapper _CompanyMapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            var _configCompany = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyModel>().ReverseMap());
            _CompanyMapper = new Mapper(_configCompany);
        }

        public async Task<List<CompanyModel>> GetCompanies()
        {
            List<Company> companyEntity = await _companyRepository.GetCompanies();

            List<CompanyModel> companyModel = _CompanyMapper.Map<List<CompanyModel>>(companyEntity);

            return companyModel;
        }

        /*        public CompanyModel GetCompanyById(int id)
                {
                    var companyEntity = _context.Companies.Find(id);

                    CompanyModel companyModel = _CompanyMapper.Map<Company, CompanyModel>(companyEntity);

                    return companyModel;
                }*/

        public async Task<List<Company>> CreateCompany(CompanyModel company)
        {
            Company companyEntity = _CompanyMapper.Map<Company>(company);

            return await _companyRepository.CreateCompany(companyEntity);
        }

        public async Task<List<Company>> UpdateCompany(CompanyModel company)
        {
            Company companyEntity = _CompanyMapper.Map<Company>(company);

            return await _companyRepository.UpdateCompany(companyEntity);
        }

        public async Task<List<Company>> DeleteCompany(int id)
        {
            return await _companyRepository.DeleteCompany(id);
        }

        /*        public async Task<ActionResult<String>> GetNameCompany(int id)
                {
                    return await _companyRepository.GetNameCompany(id);
                }*/
    }
}