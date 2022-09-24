using Application.Models;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(string userId, Response response)> Register(RegisterModel model);
        Task<(string userId, Response response)> RegisterAdmin(RegisterModel model);
        Task<(JwtSecurityToken token, Response response)> Login(LoginModel model);
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<User> CreateUserAsync(string userName, string password);
        Task<bool> DeleteUserAsync(string userId);
    }

}
