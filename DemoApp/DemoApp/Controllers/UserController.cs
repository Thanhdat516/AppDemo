using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public UserController(IUserService userService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _user = userService ?? throw new ArgumentNullException(nameof(userService));
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> AuthenticationUser(UserModel model)
        {

            ApiResponseModel ResponseToken = await _user.ValidateLogin(model);

            if (ResponseToken.Success == true)
            {
                Response.Cookies.Append("refresh_token", ResponseToken.Data.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                });
            }
            return Ok(ResponseToken);
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {

            string ClientId = _configuration["Keycloak:resource"];
            string ClientSecret = _configuration["Keycloak:credentials:secret"];
            string keycloakLogoutUrl = "http://localhost:8080/realms/myapp/protocol/openid-connect/logout";
            string myRefreshToken = HttpContext.Request.Cookies["refresh_token"];
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, keycloakLogoutUrl);
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "refresh_token", myRefreshToken }
            });

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { message = "Logout successful" });
                }
                else
                {
                    return BadRequest(new { message = "Logout failed" });
                }
            }
        }
    }
}
