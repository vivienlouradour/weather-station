using System;

namespace WeatherStation.Api.Data.Exceptions
{
    public class ApiException : Exception
    {
        protected ApiException(string message) : base(message){ }
    }
}