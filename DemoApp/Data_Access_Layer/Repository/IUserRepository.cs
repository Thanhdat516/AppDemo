using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public interface IUserRepository
    {
        public Task<List<User>> getUsers();
    }
}
