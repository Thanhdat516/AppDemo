using Business_Logic_Layer.Models;

namespace Business_Logic_Layer.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetEmployees();
        public Task<bool?> CreateEmployee(EmployeeModel company);
        public Task<bool?> UpdateEmployee(EmployeeModel company);
        public Task<bool?> DeleteEmployee(int companyID);
    }
}
