using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
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
        public ActionResult Index(int? page)
        {
            var listCuahang = db.Cuahangs.ToList();
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(listCuahang.ToPagedList(pageNum, pageSize));
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
        public ActionResult Create([Bind(Include = "maCH,tenCH,diachi,sdt,email,idAdmin")] Cuahang cuahang)
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
        public ActionResult Edit([Bind(Include = "maCH,tenCH,diachi,sdt,email,idAdmin")] Cuahang cuahang)
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

        public ActionResult RegisterShop()
        {
            return View();
        }

        [HttpPost, ActionName("RegisterShop")]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterShop(FormCollection f)
        {        
                Admin u = (Admin)Session["AccountAdmin"];
                Cuahang s = new Cuahang();
                if (Session["CuaHang"]==null)
                {
                    s.maCH = u.idAdmin;
                    s.email = f["email"].ToString();
                    s.tenCH = f["name"].ToString();
                    s.sdt = f["phone"].ToString();
                    s.diachi = f["address"].ToString();
                    s.anh = "defaultAvatar.png";
                    s.idAdmin = u.idAdmin;
                    db.Cuahangs.Add(s);
                    db.SaveChanges();
                    Session["CuaHang"] = s;
                    ViewBag.Status = "Đăng kí cửa hàng thành công!";
                    return View();
                }
                else
                {
                    ViewBag.Status = "Đăng ký thất bại!";
                    return View("");
                }

        }
        [HttpGet]
        public ActionResult Editcuahang(int id)
        {

            Cuahang shop = db.Cuahangs.SingleOrDefault(p => p.maCH == id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }
        [HttpPost, ActionName("Editcuahang")]
        public ActionResult Editcuahang(Cuahang shop)
        {

            Cuahang dbUpdate = db.Cuahangs.FirstOrDefault(p => p.maCH == shop.maCH);
            if (dbUpdate != null)
            {
                
                // get photo
                if (shop.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(shop.ImageUpload.FileName);
                    string extension = Path.GetExtension(shop.ImageUpload.FileName);
                    filename = filename + extension;
                    shop.anh = filename;
                    string path = Path.Combine(Server.MapPath("~/Image/ImageUpload/"), filename);
                    shop.ImageUpload.SaveAs(path);
                }
                db.Cuahangs.AddOrUpdate(shop);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("ProfileShop", "Home", new { id = shop.maCH });
        }
    }
}