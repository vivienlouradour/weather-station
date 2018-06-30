namespace WeatherStation.Api.Data.Exceptions
{
    public class JsonApiResult
    {
        public string Status { get; set; }
        public string Type { get; private set; }
        public string Message { get; private set; }

        public JsonApiResult(ApiException ex)
        {
            this.Status = "Error";
            this.Type = ex.GetType().ToString();
            this.Message = ex.Message;
        }

        public JsonApiResult(string successMessage)
        {
            this.Status = "Success";
            this.Message = successMessage;
        }
    }
}