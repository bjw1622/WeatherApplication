using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class Book
    {
        [Key]
        // key 값
        public int Book_U { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public string Summary { get; set; }
        public string Publisher { get; set; }
        public int Published_date { get; set; }
    }
}