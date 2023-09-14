using Business_Logic_Layer.Models;

namespace Business_Logic_Layer.Services
{
    public interface IUserService
    {
        public Task<ApiResponseModel> ValidateLogin(UserModel model);
    }
}
