using Business.Abstract;
using Business.Constants;
using Core.Dtos;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLoginDto)
        {
            var userToLoginResult = await _authService.LoginAsync(userLoginDto);
            if (!userToLoginResult.Success)
                return BadRequest(userToLoginResult);

            var loginResult = await CreateAccessTokenAsync(userToLoginResult.Data);
            if (!loginResult.Success)
                return BadRequest(loginResult);

            return Ok(loginResult);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var registerResult = await _authService.RegisterAsync(userRegisterDto);
            if (!registerResult.Success)
                return BadRequest(registerResult);

            return Ok(registerResult);
        }

        private async Task<IDataResult<LoginResult>> CreateAccessTokenAsync(User user)
        {
            var createAccessTokenResult = await _authService.CreateAccessTokenAsync(user);
            if (!createAccessTokenResult.Success)
                return new ErrorDataResult<LoginResult>(null, createAccessTokenResult.Message);

            LoginResult loginResult = new LoginResult()
            {
                AccessToken = createAccessTokenResult.Data
            };

            return new SuccessDataResult<LoginResult>(loginResult, createAccessTokenResult.Message);
        }
    }
}
