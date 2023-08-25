using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetEmployees();
        public Task<List<Employee>> CreateEmployee(EmployeeModel company);
        public Task<List<Employee>> UpdateEmployee(EmployeeModel company);
        public Task<List<Employee>> DeleteEmployee(int companyID);
    }
}
