using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Application.Services.Auth;
using ProductsAPI.Contracts;

namespace ProductsAPI.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        var response = _authService.Register(registerRequest);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var response = _authService.Login(loginRequest);
        
        return Ok(response);
    }
}