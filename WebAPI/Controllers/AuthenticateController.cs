using Application.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IdentityService identityService;

        public AuthenticateController(
            IdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result =  await identityService.Login(model);
            if (result.response.Status != "Error") 
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result.token),
                    expiration = result.token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await identityService.Register(model);
            if (result.Status == "Error")
                return StatusCode(StatusCodes.Status500InternalServerError, result);

            return Ok(result);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await identityService.RegisterAdmin(model);
            if (result.Status == "Error")
                return StatusCode(StatusCodes.Status500InternalServerError, result);

            return Ok(result);
        }
    }
}
