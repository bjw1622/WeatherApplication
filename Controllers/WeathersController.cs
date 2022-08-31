using System.Web.Mvc;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class WeathersController : Controller

    {
        // GET: Weathers
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddWeathers(WeatherEntity obj)
        {
            Weather wea = new Weather();
            wea.AddDetails(obj);
        }

        public ActionResult ListWeather()
        {
            Weather wea = new Weather();
            var weatherList = wea.GetList();
            return View(weatherList);
        }

    }
}