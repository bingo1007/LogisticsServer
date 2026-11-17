namespace LogisticsServer.Models
{
    public class ParcelEvent  //택배 이벤트 데이터 모델
    {
        public int Id { get; set; }

        public int ParcelId { get; set; }

        public string EventType { get; set; } = "";

        public string Destination { get; set; } = "";

        public DateTime OccurredAt { get; set; }
    }
}
