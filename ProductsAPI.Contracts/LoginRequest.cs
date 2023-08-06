namespace ProductsAPI.Contracts;

public record LoginRequest(
    string Email,
    string Password
);