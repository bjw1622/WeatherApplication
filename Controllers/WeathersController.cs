using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Context;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class WeathersController : Controller

    {
        Weather wea = new Weather();

        // GET: Weathers
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWeathers(WeatherEntity obj)
        {
            wea.AddDetails(obj);
            return View();
        }

        public ActionResult ListWeather()
        {
            var weatherList = wea.GetList();
            return View(weatherList);
        }
    }
}