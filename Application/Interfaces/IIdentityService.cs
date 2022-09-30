using Application.DTOs;
using Application.Models;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IResponse<AuthenticateResponse>> Login(LoginUserRequest request, CancellationToken cancellationToken);
        Task<IResponse<bool>> LogOut(CancellationToken cancellationToken);
        Task<IResponse<User>> Register(RegisterUserRequest request);
        Task<IResponse<User>> RegisterAdmin(RegisterUserRequest request);
        Task<IResponse<AuthenticateResponse>> RefreshToken(RefreshRequest request, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<bool> DeleteUserAsync(string userId);
    }

}
