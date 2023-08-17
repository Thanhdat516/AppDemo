using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetEmployees();
        public Task<List<Employee>> CreateEmployee(Employee employee);
        public Task<List<Employee>> UpdateEmployee(Employee employee);
        public Task<List<Employee>> DeleteEmployee(int employeeID);
    }
}
