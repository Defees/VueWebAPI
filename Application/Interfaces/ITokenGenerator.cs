using System.Security.Claims;

namespace Application.Interfaces;

public interface ITokenGenerator
{
    string Generate(string secretKey, string issuer, string audience, double expires,
        IEnumerable<Claim> claims = null);
}