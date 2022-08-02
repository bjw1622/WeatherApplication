using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Context;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class WeathersController : Controller
    {
        private WeatherDb db = new WeatherDb();

        // GET: Weathers
        public ActionResult Index()
        {
            return (View());
            //return View(db.Weathers.ToList());
        }

        // GET: Weathers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // GET: Weathers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Weathers/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Weather_Num,Region,Date,Main_Temp,Min_Temp,Max_Temp,Feel_Temp,Weather_Status,Wind")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                db.Weathers.Add(weather);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weather);
        }

        // GET: Weathers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Weather_Num,Region,Date,Main_Temp,Min_Temp,Max_Temp,Feel_Temp,Weather_Status,Wind")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weather).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weather);
        }

        // GET: Weathers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weather weather = db.Weathers.Find(id);
            db.Weathers.Remove(weather);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
