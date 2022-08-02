using System.Data.Entity;
using WeatherApplication.Models;

namespace WeatherApplication.Context
{
    public class WeatherDb  : DbContext
    {
        //생성자 생성
        //Web.config에서 설정한 이름으로
        public WeatherDb() : base("name=DBMS") { }
        public DbSet<Weather> Weathers { get; set; }
    }
}