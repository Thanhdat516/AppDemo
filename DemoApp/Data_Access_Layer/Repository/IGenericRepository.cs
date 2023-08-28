namespace Data_Access_Layer.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        public Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        public Task Add(T entity);

        public Task Update(T entity);

        public Task Delete(int id);

    }
}
