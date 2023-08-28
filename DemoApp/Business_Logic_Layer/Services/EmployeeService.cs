using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Business_Logic_Layer.Services
{
    public class EmployeeService : ControllerBase, IEmployeeService
    {
        private readonly Mapper _EmployeeMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
        {
            this._unitOfWork = unitOfWork;
            _logger = logger;
            var _configEmployee = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeModel>().ReverseMap());
            _EmployeeMapper = new Mapper(_configEmployee);
        }



        public async Task<List<EmployeeModel>> GetEmployees()
        {
            List<Employee> employeeEntity = await _unitOfWork.EmployeeRepository.GetAllAsync();

            List<EmployeeModel> employeeModel = _EmployeeMapper.Map<List<EmployeeModel>>(employeeEntity);

            return employeeModel;
        }

        /*        public CompanyModel GetCompanyById(int id)
                {
                    var companyEntity = _context.Companies.Find(id);

                    CompanyModel companyModel = _CompanyMapper.Map<Company, CompanyModel>(companyEntity);

                    return companyModel;
                }*/

        public async Task<IActionResult> CreateEmployee(EmployeeModel employee)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Employee employeeEntity = _EmployeeMapper.Map<Employee>(employee);
                await _unitOfWork.EmployeeRepository.Add(employeeEntity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Adding a new name employee: ", employee.NameEmployee);
                _logger.LogInformation("Adding a new address employee: ", employee.AddressEmployee);
                _logger.LogInformation("Adding a new phone employee: ", employee.PhoneEmployee);
                _logger.LogInformation("Adding a new phone employee: ", employee.CompanyID);

                return Ok("Data processed successfully within the transaction.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the company: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, "An error occurred while adding the company.");
            }
        }

        public async Task<IActionResult> UpdateEmployee(EmployeeModel employee)
        {
            /*            Employee employeeEntity = _EmployeeMapper.Map<Employee>(employee);

                        return await _employeeRepository.UpdateEmployee(employeeEntity);*/

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingEmployee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employee.EmployeeID);
                if (existingEmployee == null)
                {
                    return NotFound();
                }
                Employee employeeEntity = _EmployeeMapper.Map<Employee>(existingEmployee);

                employeeEntity.NameEmployee = employee.NameEmployee;
                employeeEntity.AddressEmployee = employee.AddressEmployee;
                employeeEntity.PhoneEmployee = employee.PhoneEmployee;
                employeeEntity.CompanyID = employee.CompanyID;

                await _unitOfWork.EmployeeRepository.Update(employeeEntity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, "An error occurred while adding the employee.");
            }
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingEmployeeID = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
                if (existingEmployeeID == null)
                {
                    return NotFound();
                }
                await _unitOfWork.EmployeeRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Delete a company with CompanyID ", existingEmployeeID);
                return Ok("Delete successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee: {ErrorMessage}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, "An error occurred while adding the employee.");
            }
        }
    }
}
