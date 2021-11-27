using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    public class DonhangController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Donhang
        public ActionResult Index()
        {
            var donhangs = db.Donhangs.Include(d => d.Cuahang).Include(d => d.HoaDon).Include(d => d.NguoiDung);
            return View(donhangs.ToList());
        }

        // GET: Donhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // GET: Donhang/Create
        public ActionResult Create()
        {
            ViewBag.maCH = new SelectList(db.Cuahangs, "maCH", "tenCH");
            ViewBag.maHD = new SelectList(db.HoaDons, "maHD", "ghichu");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "tenND");
            return View();
        }

        // POST: Donhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDH,maHD,maCH,maND,tiendo")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Donhangs.Add(donhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maCH = new SelectList(db.Cuahangs, "maCH", "tenCH", donhang.maCH);
            ViewBag.maHD = new SelectList(db.HoaDons, "maHD", "ghichu", donhang.maHD);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "tenND", donhang.maND);
            return View(donhang);
        }

        // GET: Donhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.maCH = new SelectList(db.Cuahangs, "maCH", "tenCH", donhang.maCH);
            ViewBag.maHD = new SelectList(db.HoaDons, "maHD", "ghichu", donhang.maHD);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "tenND", donhang.maND);
            return View(donhang);
        }

        // POST: Donhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDH,maHD,maCH,maND,tiendo")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maCH = new SelectList(db.Cuahangs, "maCH", "tenCH", donhang.maCH);
            ViewBag.maHD = new SelectList(db.HoaDons, "maHD", "ghichu", donhang.maHD);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "tenND", donhang.maND);
            return View(donhang);
        }

        // GET: Donhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: Donhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donhang donhang = db.Donhangs.Find(id);
            db.Donhangs.Remove(donhang);
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
