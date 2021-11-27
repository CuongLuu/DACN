using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using PagedList;
using PagedList.Mvc;

namespace Demo.Controllers
{
    public class CuaHangController : Controller
    {
        // GET: CuaHang
        DBcontext db = new DBcontext();
        // GET: News
        private List<Cuahang> GetCHList()
        {
            var listCH = db.Cuahangs.ToList();
            foreach (var l in listCH)
            {
                var mach = db.Cuahangs.Where(p => p.maCH == l.maCH).SingleOrDefault();
                l.tenCH = mach.tenCH;
                l.diachi = mach.diachi;
                l.email = mach.email;
                l.sdt = mach.sdt;
            }
            return listCH;
        }
        public ActionResult Index()
        {
            var cuahangs = db.Cuahangs.Include(c => c.Admin);
            return View(cuahangs.ToList());
        }
        public ActionResult List(int? page)
        {
            if (page == null)
                page = 1;
            var listNews = GetCHList();
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(listNews.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Details(int id)
        {
            var cuahang = db.Cuahangs.Where(n => n.maCH == id).FirstOrDefault();
            return View(cuahang);
        }

        // GET: Cuahangs/Create
        public ActionResult Create()
        {
            ViewBag.idAdmin = new SelectList(db.Admins, "idAdmin", "ten");
            return View();
        }

        // POST: Cuahangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maCH,tenCH,diachi,sdt,email,idAdmin,idLike,soluongSP")] Cuahang cuahang)
        {
            if (ModelState.IsValid)
            {
                db.Cuahangs.Add(cuahang);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.idAdmin = new SelectList(db.Admins, "idAdmin", "ten", cuahang.idAdmin);
            return View(cuahang);
        }

        // GET: Cuahangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuahang cuahang = db.Cuahangs.Find(id);
            if (cuahang == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdmin = new SelectList(db.Admins, "idAdmin", "ten", cuahang.idAdmin);
            return View(cuahang);
        }

        // POST: Cuahangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maCH,tenCH,diachi,sdt,email,idAdmin,idLike,soluongSP")] Cuahang cuahang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuahang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.idAdmin = new SelectList(db.Admins, "idAdmin", "ten", cuahang.idAdmin);
            return View(cuahang);
        }

        // GET: Cuahangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuahang cuahang = db.Cuahangs.Find(id);
            if (cuahang == null)
            {
                return HttpNotFound();
            }
            return View(cuahang);
        }

        // POST: Cuahangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuahang cuahang = db.Cuahangs.Find(id);
            db.Cuahangs.Remove(cuahang);
            db.SaveChanges();
            return RedirectToAction("List");
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