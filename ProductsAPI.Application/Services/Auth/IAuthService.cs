using ProductsAPI.Contracts;

namespace ProductsAPI.Application.Services.Auth;

public interface IAuthService
{
    AuthResponse Register(RegisterRequest registerRequest);
    AuthResponse Login(LoginRequest loginRequest);
}