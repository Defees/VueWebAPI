using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenService
{
    string Generate(User user);
}