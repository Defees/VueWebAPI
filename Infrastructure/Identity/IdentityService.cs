using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly SignInManager<User> _signInManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        public IdentityService(
            UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(JwtSecurityToken token, Response response)> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return (token, new Response { Status = "Success", Message = "User login successfully!" });
            }
            throw new Exception();
        }

        public async Task<(string userId, Response response)> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
                return (userExists.Id, new Response { Status = "Error", Message = "User already exists!" });

            var user = await CreateUserAsync(model.Username, model.Password);

            return ( user.Id, new Response { Status = "Success", Message = "User created successfully!" });
        }

        public async Task<(string userId, Response response)> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (userExists.Id, new Response { Status = "Error", Message = "User already exists!" });

            var user = await CreateUserAsync(model.Username, model.Password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return (user.Id, new Response { Status = "Success", Message = "User created successfully!" });
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
        public async Task<User> CreateUserAsync(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = userName,
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return user;
            else
                throw new Exception();
        }
        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : true;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
