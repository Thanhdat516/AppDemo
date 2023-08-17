using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;
namespace Business_Logic_Layer.Services
{
    public class EmployeeService
    {
        private readonly Mapper _EmployeeMapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            var _configEmployee = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeModel>().ReverseMap());
            _EmployeeMapper = new Mapper(_configEmployee);
        }



        public async Task<ActionResult<List<EmployeeModel>>> GetEmployees()
        {
            List<Employee> employeeEntity = await _employeeRepository.GetEmployees();

            List<EmployeeModel> employeeModel = _EmployeeMapper.Map<List<EmployeeModel>>(employeeEntity);

            return employeeModel;
        }

        /*        public CompanyModel GetCompanyById(int id)
                {
                    var companyEntity = _context.Companies.Find(id);

                    CompanyModel companyModel = _CompanyMapper.Map<Company, CompanyModel>(companyEntity);

                    return companyModel;
                }*/

        public async Task<ActionResult<List<Employee>>> CreateEmployee(EmployeeModel employee)
        {
            Employee employeeEntity = _EmployeeMapper.Map<Employee>(employee);

            return await _employeeRepository.CreateEmployee(employeeEntity);
        }

        public async Task<ActionResult<List<Employee>>> UpdateEmployee(EmployeeModel employee)
        {
            Employee employeeEntity = _EmployeeMapper.Map<Employee>(employee);

            return await _employeeRepository.UpdateEmployee(employeeEntity);
        }

        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }
    }
}
