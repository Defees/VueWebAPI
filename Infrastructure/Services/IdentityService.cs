using Application.Common.Interfaces;
using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        public IdentityService(
            IAuthenticateService authenticateService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IRefreshTokenValidator refreshTokenValidator)
        {
            _authenticateService = authenticateService;
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenValidator = refreshTokenValidator;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IResponse<AuthenticateResponse>> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new UserNotFoundException();

        var signInResult =
            await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!signInResult.Succeeded) throw new SignInException();

            return Response.Success(await _authenticateService.Authenticate(user, cancellationToken));
        }

        public async Task<IResponse<bool>> LogOut(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");
            if (userId is null) throw new UserNotFoundException();

            await _signInManager.SignOutAsync();

            var refreshTokens = await _context.RefreshTokens
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
            _context.RefreshTokens.RemoveRange(refreshTokens);

            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(true);
        }

        public async Task<IResponse<User>> Register(RegisterUserRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.Email,
            };
            var createResult = await _userManager.CreateAsync(user, request.Password);

            if(!createResult.Succeeded) throw new RegisterException();

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
       
            return Response.Success(user);
        }

        public async Task<IResponse<User>> RegisterAdmin(RegisterUserRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.Email,
            };
            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            
            return Response.Success(user);
        }

        public async Task<IResponse<AuthenticateResponse>> RefreshToken(RefreshRequest request, CancellationToken cancellationToken)
        {
            var refreshRequest = request;
            var isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if(!isValidRefreshToken) throw new InvalidRefreshTokenException();

            var refreshToken =
                await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshRequest.RefreshToken,
                    cancellationToken);
            if (refreshToken is null) throw new InvalidRefreshTokenException();

            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if(user is null) throw new UserNotFoundException();

            return Response.Success(await _authenticateService.Authenticate(user, cancellationToken));
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
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

    }
}
