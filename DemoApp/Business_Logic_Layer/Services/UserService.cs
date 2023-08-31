using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Business_Logic_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly Mapper _UserMapper;
        private readonly AppSettingModel _appSetting;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public UserService(IUserRepository userRepository, IOptionsMonitor<AppSettingModel> optionsMonitor, HttpClient httpClient)
        {
            _userRepository = userRepository;
            var _configCompany = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>().ReverseMap());
            _UserMapper = new Mapper(_configCompany);
            _appSetting = optionsMonitor.CurrentValue;
            _httpClient = httpClient;
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
                Data = GenerateToken().Result
            };
        }

        #region Old Generate token
        /*public string GenerateToken(UserModel user)
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
        }*/
        #endregion

        #region New Generate token
        public async Task<string> GenerateToken()
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync("http://localhost:8080/realms/myapp/");

            if (discoveryDocument.IsError)
            {
                throw new Exception("Discovery document error: " + discoveryDocument.Error);
            }

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "MyClient",
                ClientSecret = "2mYkbk2k9Tv3eA7RC2jFGBsXUGs3rBdT",
                UserName = "demoapp",
                Password = "admin123"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Token request error: " + tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
        #endregion
    }
}
