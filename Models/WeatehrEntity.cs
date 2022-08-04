using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApplication.Models
{
    public class WeatherEntity
    {
        public string Region { get; set; }
        public long Main_Temp { get; set; }
        public long Min_Temp { get; set; }
        public long Max_Temp { get; set; }
        public long Feel_Temp { get; set; }
        public long Wind { get; set; }
        public string Date_T { get; set; }
    }
}