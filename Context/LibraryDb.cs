using System.Data.Entity;
using WeatherApplication.Models;

namespace WeatherApplication.Context
{
    // DbContext를 참조
    public class LibraryDb : DbContext
    {
        //생성자 생성
        //Web.config에서 설정한 이름으로
        public LibraryDb() : base("name=DBCS") { }
        public DbSet<Book> Books { get; set; }
    }
}