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
        /*        private readonly DataContext _context;
                private readonly AppSetting _appSetting;*/
        private readonly IUserService _user;

        public UserController(IUserService userService)
        {
            _user = userService ?? throw new ArgumentNullException(nameof(userService));

            /*            _context = context;
                        _appSetting = optionsMonitor.CurrentValue;*/
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> AuthenticationUser(UserModel model)
        {
            return Ok(await _user.ValidateLogin(model));
        }
    }
}
