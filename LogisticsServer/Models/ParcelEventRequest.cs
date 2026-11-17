namespace LogisticsServer.Models
{
    public class ParcelEventRequest
    {
        public int ParcelId { get; set; }

        public string EventType { get; set; } = "";

        public string Destination { get; set; } = "";
    }
}