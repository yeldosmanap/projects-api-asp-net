namespace ProductsAPI.Persistence.Auth;

public class JwtSettings
{
    public string Secret { get; set; }
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
}