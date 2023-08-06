using ProductsAPI.Application.Common;

namespace ProductsAPI.Persistence.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}