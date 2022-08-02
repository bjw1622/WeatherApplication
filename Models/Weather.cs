using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class Weather
	{
		[Key]
        // key 값
        public int Weather_Num { get; set; }
        public string Region { get; set; }
        public string Date { get; set; }
        public long Main_Temp { get; set; }
        public long Min_Temp { get; set; }
        public long Max_Temp { get; set; }
        public long Feel_Temp { get; set; }
        public string Weather_Status { get; set; }
        public long Wind { get; set; }
}
}