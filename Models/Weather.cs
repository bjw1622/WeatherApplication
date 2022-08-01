using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class Weather
	{
		[Key]
        // key 값
        public int Weather_Num { get; set; }
        public string Weather_date { get; set; }
        public int degree { get; set; }
	}
}