﻿namespace ProductsAPI.Application.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get;  }
}