using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business_Logic_Layer.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetEmployees();
        public Task<IActionResult> CreateEmployee(EmployeeModel company);
        public Task<IActionResult> UpdateEmployee(EmployeeModel company);
        public Task<IActionResult> DeleteEmployee(int companyID);
    }
}
