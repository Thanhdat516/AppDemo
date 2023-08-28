using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }
    }
}
