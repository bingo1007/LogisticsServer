namespace LogisticsServer.Models
{
    public class ParcelRequest  //언리얼에서 서버로 보내는 데이터 정보를 담기 위한 데이터 모델(학습용 모델이다. 약식으로 진행했다.)
    {
        public int ParcelId { get; set; }

        public string Destination { get; set; } = "";
    }
}
