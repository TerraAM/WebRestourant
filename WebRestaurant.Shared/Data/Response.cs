using System;
using WebRestaurant.Domain.Data;

namespace WebRestaurant.Domain.Data
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorInfo { get; set; } = string.Empty;
    }
}

public class Response<T> : Response
{
    public T Value { get; set; }
}