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
        public void AddWeathers(WeatherEntity obj)
        {
            wea.AddDetails(obj);
        }

        public ActionResult ListWeather()
        {
            var weatherList = wea.GetList();
            return View(weatherList);
        }

    }
}