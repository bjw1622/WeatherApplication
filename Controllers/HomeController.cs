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
    public class HomeController : Controller
    {
        // LibrayDb를 인스턴스화
        private LibraryDb db = new LibraryDb();

        // GET: Home
        // View에 인자가 object로 들어감
        // db.Books.ToList()는 db의 Books라는 Table에 접근해서 결과값을 리스트로 변환해서 인자 값으로 던지는 것
        public ActionResult Index()
        {
            // listCount는 한 페이지에 나오는 게시글 갯수
            // pageNum은 현재 페이지
            int MaxListCount = 3;
            int pageNum = 1;
            int totalCount = 0;

            // keyword를 get방식으로 받겠다.
            string keyword = Request.QueryString["keyword"] ?? string.Empty;

            // Request.QueryString은 주소를 통해서 오는 매개변수
            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);

            // 몇개의 결과값을 가져올건가?? => listCount만큼 가져온다.
            // OrderBy는 정렬을 "SELECT * FROM Books ORDER BY Book_U ASC" 오름차순으로 정렬
            // Skip
            var books = new List<Book>();

            //키워드가 있을 때 없을 때
            if (string.IsNullOrWhiteSpace(keyword))
            {
                books = db.Books.OrderBy(x => x.Book_U).
                 Skip((pageNum - 1) * 3).
                 Take(MaxListCount).ToList();
                
                totalCount = db.Books.Count();
            }
            else
            {
                books = db.Books.Where(x => x.Title.Contains(keyword)).OrderBy(x => x.Book_U).
                Skip((pageNum - 1) * 3).
                Take(MaxListCount).ToList();
                totalCount = db.Books.Where(x => x.Title.Contains(keyword)).Count();
            }

            ViewBag.Page = pageNum;

            // 테이블의 전체 데이터 갯수
            ViewBag.TotalCount = totalCount;
            ViewBag.MaxListCount = MaxListCount;

            return View(books);
        }

        // GET: Home/Details/5
        //상세의 화면
        //인자값으로 id를 받음, 단 값이 없으면 400에러(BadRequest로 던짐)
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // book 이라는 변수는 데이터에서 id를 가진 것을 하나만 찾음.
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Home/Create
        // 생성이기에 별도의 Object를 던질게 필요 X
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]

        //토큰 값 체크
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);

                // crud가 발생하면 db를 꼭 저장해주기.
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Book book = db.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Home/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = db.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
