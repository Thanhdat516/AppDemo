using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business_Logic_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly Mapper _UserMapper;
        private readonly AppSettingModel _appSetting;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IOptionsMonitor<AppSettingModel> optionsMonitor)
        {
            _userRepository = userRepository;
            var _configCompany = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>().ReverseMap());
            _UserMapper = new Mapper(_configCompany);
            _appSetting = optionsMonitor.CurrentValue;
        }

        public async Task<ApiResponseModel> ValidateLogin(UserModel model)
        {
            List<User> userEntity = await _userRepository.getUsers();

            List<UserModel> userModel = _UserMapper.Map<List<UserModel>>(userEntity);

            var checkUser = userModel.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (checkUser == null)
            {
                return new ApiResponseModel
                {
                    Success = false,
                    Message = "Invalid user/password"
                };
            }

            return new ApiResponseModel
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(checkUser)
            };
        }

        public string GenerateToken(UserModel user)
        {
            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _appSetting.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _appSetting.Audience)
            };


            var SecretKey = Encoding.UTF8.GetBytes(_appSetting.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
