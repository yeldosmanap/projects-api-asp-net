using ProductsAPI.Application.Common;
using ProductsAPI.Contracts;

namespace ProductsAPI.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthResponse Register(RegisterRequest registerRequest)
    {
        var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), registerRequest.Username, registerRequest.Email);
        
        return new AuthResponse(Guid.NewGuid(), registerRequest.Username, registerRequest.Email, token);
    }

    public AuthResponse Login(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }
}