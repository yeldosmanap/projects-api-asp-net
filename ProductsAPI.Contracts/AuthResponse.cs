namespace ProductsAPI.Contracts;

public record AuthResponse(
    Guid UserId,
    string Username,
    string Email,
    string Token
);