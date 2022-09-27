using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthenticateService
{
    Task<AuthenticateResponse> Authenticate(User user,CancellationToken cancellationToken);
}