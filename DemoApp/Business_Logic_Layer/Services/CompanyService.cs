using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using Microsoft.Extensions.Logging;

namespace Business_Logic_Layer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly Mapper _CompanyMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IUnitOfWork unitOfWork, ILogger<CompanyService> logger)
        {
            this._unitOfWork = unitOfWork;
            _logger = logger;
            var _configCompany = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyModel>().ReverseMap());
            _CompanyMapper = new Mapper(_configCompany);
        }

        public async Task<List<CompanyModel>> GetCompanies()
        {

            List<Company> companyEntity = await _unitOfWork.CompanyRepository.GetAllAsync();

            List<CompanyModel> companyModel = _CompanyMapper.Map<List<CompanyModel>>(companyEntity);

            return companyModel;
        }

        /*        public CompanyModel GetCompanyById(int id)
                {
                    var companyEntity = _context.Companies.Find(id);

                    CompanyModel companyModel = _CompanyMapper.Map<Company, CompanyModel>(companyEntity);

                    return companyModel;
                }*/

        public async Task<bool?> CreateCompany(CompanyModel company)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Company companyEntity = _CompanyMapper.Map<Company>(company);
                await _unitOfWork.CompanyRepository.Add(companyEntity);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("Adding a new name company: ", company.NameCompany);
                    _logger.LogInformation("Adding a new address company: ", company.AddressCompany);
                    _logger.LogInformation("Adding a new phone company: ", company.PhoneCompany);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the company: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return null;
            }
        }

        public async Task<bool?> UpdateCompany(CompanyModel company)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingCompany = await _unitOfWork.CompanyRepository.GetByIdAsync(company.CompanyID);
                if (existingCompany == null)
                {
                    return false;
                }
                Company companyEntity = _CompanyMapper.Map<Company>(existingCompany);

                companyEntity.NameCompany = company.NameCompany;
                companyEntity.AddressCompany = company.AddressCompany;
                companyEntity.PhoneCompany = company.PhoneCompany;

                await _unitOfWork.CompanyRepository.Update(companyEntity);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    await _unitOfWork.CommitTransactionAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the company: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return null;
            }
        }

        public async Task<bool?> DeleteCompany(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingCompanyID = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
                if (existingCompanyID == null)
                {
                    return false;
                }
                await _unitOfWork.CompanyRepository.Delete(id);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("Delete a company with CompanyID ", existingCompanyID);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the company: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return null;
            }
        }

        /*        public async Task<ActionResult<String>> GetNameCompany(int id)
                {
                    return await _companyRepository.GetNameCompany(id);
                }*/
    }
}