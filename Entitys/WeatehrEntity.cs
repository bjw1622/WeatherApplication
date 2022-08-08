using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApplication.Models
{
    public class WeatherEntity
    {
        public string Region { get; set; }
        public double Main_Temp { get; set; }
        public double Min_Temp { get; set; }
        public double Max_Temp { get; set; }
        public double Feel_Temp { get; set; }
        public long Wind { get; set; }
        public string Date_T { get; set; }
    }
}