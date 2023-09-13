using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace DemoApp.Controllers
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
            }
            else
            {
                bool isTokenValid = await IsTokenValid(context);

                if (!isTokenValid)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                await _next(context);
            }
        }

        public async Task<bool> IsTokenValid(HttpContext context, bool isRefreshToken = false)
        {

            var token = isRefreshToken
                ? context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
                : context.Request.Cookies["refresh_token"];

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var introspectEndpoint = "http://localhost:8080/realms/myapp/protocol/openid-connect/token/introspect";
            using (var httpClient = new HttpClient())
            {
                var formData = new Dictionary<string, string>()
                {
                    { "token", token },
                    { "client_id", _configuration["Keycloak:resource"] },

                    { "client_secret", _configuration["Keycloak:credentials:secret"] }
                };

                var response = await httpClient.PostAsync(introspectEndpoint, new FormUrlEncodedContent(formData));

                if (response.IsSuccessStatusCode)
                {
                    var introspectResponse = await response.Content.ReadAsStringAsync();
                    var introspectData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(introspectResponse);

                    if (introspectData.TryGetValue("active", out var activeValue) && (bool)activeValue)
                    {
                        return true; // Token hợp lệ
                    }
                }
            }
            return false;
        }
    }
}
