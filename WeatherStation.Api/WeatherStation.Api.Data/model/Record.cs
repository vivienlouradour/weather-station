using System;

namespace WeatherStation.Api.Data.model
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public int BroadcasterId { get; set; }
        public Broadcaster Broadcaster { get; set; }
    }
}