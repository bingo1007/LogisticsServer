using LogisticsServer.Data;
using LogisticsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServer.Controllers
{

    [ApiController]  //API 개발에 유용한 기능들 자동 사용. API goat
    [Route("api/[controller]")]
    public class ParcelEventController : ControllerBase
    {
        private readonly LogisticsDbContext _context;  //데이터베이스 작업에 사용할 객체 보관

        public ParcelEventController(LogisticsDbContext context)  //생성자, ASP.NET Core가 ParceleventController 객체를 생성할 때 필요한 LogisticsDbContext를 전달
        {
            _context = context;
        }

        [HttpPost]  //해당 주소로 post 요청이 온다면 실행할 로직
        public IActionResult CreateParcelEvent([FromBody] ParcelEventRequest request)  //Http 요청의 바디에서 데이터(JSON)를 가져옴. 그 데이터는 다시 c#의 데이터 모델로 할당. 즉 이 CreateParcel을 실행하기 전 이미 request 객체는 만들어지고 할당도 끝남.
        {
            if (request.ParcelId < 1)
            {
                return BadRequest("Parcel ID must be greater than 0.");
            }

            if (request.EventType != "SCANNED" && request.EventType != "SORTED")
            {
                return BadRequest("Invalid Event.");
            }

            if (request.Destination != "OKCHEON" && request.Destination != "DAEJEON" && request.Destination != "GONJIAM")
            {
                return BadRequest("Invalid Parcel Destination.");
            }

            ParcelEvent parcelEvent = new ParcelEvent()  //요청받은 parcelRequest객체를 db에 저장할 parcel 객체로 변환하는 과정.
            {
                ParcelId = request.ParcelId,
                EventType = request.EventType,
                Destination = request.Destination,
                OccurredAt = DateTime.UtcNow
            };

            _context.ParcelEvents.Add(parcelEvent);  //만든 parcelrequest객체를 context의 parcelevents에 저장.
            _context.SaveChanges();  //변동사항을 db에 저장

            return Ok(new  //만들어진 request객체를 사용하여 응답 json을 만드는 과정
            {
                Message = "Parcel Event Received",
                ParcelId = request.ParcelId,
                ParcelEvent = request.EventType,
                Destination = request.Destination
            });
        }

        [HttpGet]
        public IActionResult GetParcelEvents()
        {
            var events = _context.ParcelEvents.ToList();

            return Ok(events);
        }

        [HttpGet("recent")]
        public IActionResult GetRecentEvents()
        {
            var events = _context.ParcelEvents
                .OrderByDescending(e => e.OccurredAt)
                .ThenByDescending(e => e.Id)
                .Take(5)
                .ToList();

            return Ok(events);
        }
    }
}
