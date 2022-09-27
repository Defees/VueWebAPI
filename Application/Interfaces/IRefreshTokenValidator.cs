namespace Application.Interfaces;

public interface IRefreshTokenValidator
{
    bool Validate(string refreshToken);
}