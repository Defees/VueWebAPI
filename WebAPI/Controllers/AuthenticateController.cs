﻿using Application.DTOs;
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
        CancellationToken cancellationToken = new();
        public AuthenticateController(
            IdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result =  await identityService.Login(request, cancellationToken);
            if (result.Succeeded) 
            {
                return Ok(new
                {
                    accessToken = result.Data.AccessToken,
                    refreshToken = result.Data.RefreshToken
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await identityService.Register(request);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserRequest request)
        {
            var result = await identityService.RegisterAdmin(request);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest();
        }
    }
}
