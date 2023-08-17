using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.Include(c => c.Company).ToListAsync();
        }

        public async Task<List<Employee>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> DeleteEmployee(int employeeID)
        {
            var dbEmployee = await _context.Employees.FindAsync(employeeID);
            _context.Employees.Remove(dbEmployee);
            await _context.SaveChangesAsync();

            return await _context.Employees.ToListAsync();
        }


        public async Task<List<Employee>> UpdateEmployee(Employee employee)
        {
            var dbEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
            dbEmployee.NameEmployee = employee.NameEmployee;
            dbEmployee.PhoneEmployee = employee.PhoneEmployee;
            dbEmployee.AddressEmployee = employee.AddressEmployee;

            await _context.SaveChangesAsync();

            return await _context.Employees.ToListAsync();
        }
    }
}
