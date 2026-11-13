using LogisticsServer.Data;
using Microsoft.EntityFrameworkCore;


namespace LogisticsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LogisticsDbContext>(options =>  //ASP.NET core에 우리가 만든 LogisticsDbContext 등록. 언제든 사용 가능
            {  //앞서 LogisticsDbContext에서 사용할 옵션 정보 설정, SQLite를 사용할 것이고 데이터 베이스 파일 이름은 이거임~~
                options.UseSqlite("Data Source=logistics.db");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
