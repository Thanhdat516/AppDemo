using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Company> CompanyRepository { get; }

        IGenericRepository<Employee> EmployeeRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
