using Business_Logic_Layer.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;


namespace Business_Logic_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string secret;
        private readonly string clientID;
        private readonly string authServerURL;
        private readonly string realm;


        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            secret = _configuration["Keycloak:credentials:secret"];
            clientID = _configuration["Keycloak:resource"];
            authServerURL = _configuration["Keycloak:auth-server-url"];
            realm = _configuration["Keycloak:realm"];
        }

        public async Task<ApiResponseModel> ValidateLogin(UserModel model)
        {
            var checkUser = await GetToken(model);

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
                Data = new TokenModel
                {
                    AccessToken = checkUser.AccessToken,
                    RefreshToken = checkUser.RefreshToken,
                }
            };
        }

        public async Task<TokenModel> GetToken(UserModel user)
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(authServerURL + $"realms/{realm}/");

            if (discoveryDocument.IsError)
            {
                throw new Exception("Discovery document error: " + discoveryDocument.Error);
            }

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = clientID,
                ClientSecret = secret,
                UserName = user.Username,
                Password = user.Password
            });

            if (tokenResponse.IsError)
            {
                return null;
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
