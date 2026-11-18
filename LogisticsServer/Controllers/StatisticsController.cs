using LogisticsServer.Data;
using LogisticsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServer.Controllers
{
    [ApiController]  //API 개발에 유용한 기능들 자동 사용. API goat
    [Route("api/[controller]")]

    public class StatisticsController : ControllerBase
    {

        private readonly LogisticsDbContext _context;  //데이터베이스 작업에 사용할 객체 보관

        public StatisticsController(LogisticsDbContext context)  //생성자, ASP.NET Core가 StatisticsController 객체를 생성할 때 필요한 LogisticsDbContext를 전달
        {
            _context = context;
        }

        [HttpGet]  //GET
        public IActionResult GetStatistics()
        {
            int totalParcels = _context.Parcels.Count();  //지금까지 저장된 처리한 택배의 총 개수
            int scannedEvents = _context.ParcelEvents.Count(e => e.EventType == "SCANNED");  //SCANNED 이벤트 처리 횟수
            int sortedEvents = _context.ParcelEvents.Count(e => e.EventType == "SORTED");  //SORTED 이벤트 처리 횟수



            return Ok(new { totalParcels, scannedEvents, sortedEvents });
        }
    }
}
