namespace LogisticsServer.Models
{
    public class Parcel  //입력받은 데이터를 ParcelRequest로 변환하여 처리 후 다시 Parcel자료형으로 묶어서 데이터 베이스에 올림. => 데이터 베이스에 어떤 정보를 저장할지 C#클래스로 정의
    {
        public int Id { get; set; }
        public int ParcelId { get; set; }
        public string Destination { get; set; } = "";
        public DateTime CreatedAt { get; set; }

    }
}
