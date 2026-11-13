using LogisticsServer.Models;
//using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using LogisticsServer.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LogisticsServer.Controllers
{
    [ApiController]  //API 개발에 유용한 기능들 자동 사용. API goat
    [Route("api/[controller]")]
    public class ParcelController : ControllerBase
    {
        private readonly LogisticsDbContext _context;  //데이터베이스 작업에 사용할 객체 보관

        public ParcelController(LogisticsDbContext context)  //생성자, ASP.NET Core가 ParcelController 객체를 생성할 때 필요한 LogisticsDbContext를 전달
        {
            _context = context;
        }

        [HttpPost]  //해당 주소로 post 요청이 온다면 실행할 로직
        public IActionResult CreateParcel([FromBody] ParcelRequest request)  //Http 요청의 바디에서 데이터(JSON)를 가져옴. 그 데이터는 다시 c#의 데이터 모델로 할당. 즉 이 CreateParcel을 실행하기 전 이미 request 객체는 만들어지고 할당도 끝남.
        {
            if (request.ParcelId < 1)
            {
                return BadRequest("Parcel ID must be greater than 0.");
            }

            if (request.Destination != "OKCHEON" && request.Destination != "DAEJEON" && request.Destination != "GONJIAM")
            {
                return BadRequest("Invalid Parcel Destination.");
            }

            Parcel parcel = new Parcel()  //요청받은 parcelRequest객체를 db에 저장할 parcel 객체로 변환하는 과정.
            {
                ParcelId = request.ParcelId,
                Destination = request.Destination,
                CreatedAt = DateTime.UtcNow
            };

            _context.Parcels.Add(parcel);  //만든 parcel객체를 context의 parcels에 저장.
            _context.SaveChanges();  //변동사항을 db에 저장

            return Ok(new  //만들어진 request객체를 사용하여 응답 json을 만드는 과정
            {
                Message = "Parcel Received",
                ParcelId = request.ParcelId,
                Destination = request.Destination
            });
        }

        [HttpGet]  //이 메서드가 Get을 처리
        public IActionResult GetParcels()
        {
            //var parcels = _context.Parcels.ToList();  //데이터를 조회하여 List형태로 가져옴, 그 결과값을 parcels라는 변수에 저장. 즉 parcels에는 지금 db의 parcel 데이터 집합이 들어있다.
            var parcels = _context.Parcels
                .Where(p => p.Destination == "DAEJEON").ToList();  //LINQ는 조건부 검색을 도와준다. where을 사용하여 조건을 입력할 수 있다.

            return Ok(parcels);  //조회한 택배 목록을 ok와 함께 반환
        }

        [HttpGet("{id}")]  //특정 id의 parcel만 따로 호출하는 메서드
        public IActionResult GetParcelById(int id)
        {
            var parcel = _context.Parcels.FirstOrDefault(p => p.Id == id);
            if(parcel==null)
            {
                return NotFound("Parcel not found...");  //찾지 못했을때 반환되는 값. 404가 반환된다. 
            }
            return Ok(parcel);  //조회한 택배 목록을 ok와 함께 반환
        }
    }
}
