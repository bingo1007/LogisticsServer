using LogisticsServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsServer.Data
{
    public class LogisticsDbContext : DbContext  //DbContext는 C#과 데이터 베이스를 연결하는 객체. 이 객체를 사용하여 데이터 베이스를 접근, 관리할 수 있음.
    {
        //데이터 베이스를 어떤 방식으로 사용할지 설정, ': base(options)'는 전달받은 설정을 부모 클래스로 올리는 과정, 앞으로 이렇게 DB 사용할거야~~
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) 
        { }
        public DbSet<Parcel> Parcels { get; set; } //데이터 베이스에 저장할 Parcel 데이터들을 조회하거나 추가하는데 사용되는 자료
        public DbSet<ParcelEvent> ParcelEvents { get; set; }  //데이터 베이스에 저장할 ParcelEvent 데이터들을 조회하거나 추가하는데 사용되는 자료
    }
}
