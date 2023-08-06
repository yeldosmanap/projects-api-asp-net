namespace ProductsAPI.Contracts;

public record RegisterRequest(
    string Username,
    string Email,
    string Password
);