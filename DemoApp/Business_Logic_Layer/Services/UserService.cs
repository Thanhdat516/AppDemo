using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;


namespace Business_Logic_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly Mapper _UserMapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public UserService(IUserRepository userRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _userRepository = userRepository;
            var _configCompany = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>().ReverseMap());
            _UserMapper = new Mapper(_configCompany);
            _httpClient = httpClient;
            _configuration = configuration;
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

        public async Task<TokenModel> GenerateToken()
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(_configuration["Keycloak:auth-server-url"] + $"realms/{_configuration["Keycloak:realm"]}/");

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

            TokenModel token = new TokenModel
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
            };

            return token;
        }
    }
}
