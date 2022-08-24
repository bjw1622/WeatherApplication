using System.Web.Mvc;
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