namespace Application.DTOs;
public class RegisterUserRequest
{

    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}