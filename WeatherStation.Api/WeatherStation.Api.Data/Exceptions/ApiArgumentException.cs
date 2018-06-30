namespace WeatherStation.Api.Data.Exceptions
{
    public class ApiArgumentException : ApiException
    {
        public ApiArgumentException(string message) : base(message){ }
    }
}