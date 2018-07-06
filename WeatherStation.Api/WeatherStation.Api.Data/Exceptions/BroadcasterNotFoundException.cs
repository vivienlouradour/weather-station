namespace WeatherStation.Api.Data.Exceptions
{
    public class BroadcasterNotFoundException : ApiException
    {
        public BroadcasterNotFoundException(string message) : base(message){ }
    }
}