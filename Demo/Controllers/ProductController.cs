using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Data;
using System.Net;
using Demo.Models.Context;

namespace Demo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBcontext context = new DBcontext();
        // GET: News

        public ActionResult List(string currentFilter, string SearchString, int? page)
        {
            var listSP = new List<SanPham>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                listSP = context.SanPhams.Where(n => n.tenSP.Contains(SearchString)).ToList();
            }
            else
            {
                listSP = context.SanPhams.ToList();
            }
            ViewBag.currentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listSP.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]

        public ActionResult Create()
        {
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten");
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH");
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP");
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize");
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP");
            return View();
        }

        // POST: SanPhams1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSP,maLoaiSP,idAdmin,maTP,maSize,maCH,tenSP,gia,mota,hinhanh,status,ngaytao,ngaycapnhat")] SanPham sanPham)
        {

            if (ModelState.IsValid)
            {
                if (sanPham.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sanPham.ImageUpload.FileName);
                    string extension = Path.GetExtension(sanPham.ImageUpload.FileName);
                    filename = filename + extension;
                    sanPham.hinhanh = filename;
                    string path = Path.Combine(Server.MapPath("~/Content/Template/img/"), filename);
                    sanPham.ImageUpload.SaveAs(path);
                }
                context.SanPhams.Add(sanPham);
                context.SaveChanges();
                return RedirectToAction("List", "Product");
            }

            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
            return View(sanPham);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = context.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = context.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
            return View(sanPham);
        }

        // POST: SanPhams1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSP,maLoaiSP,idAdmin,maTP,maSize,maCH,tenSP,gia,mota,hinhanh,status,ngaytao,ngaycapnhat")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {

                context.Entry(sanPham).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
            return View(sanPham);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = context.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = context.SanPhams.Find(id);
            context.SanPhams.Remove(sanPham);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult allProduct(string currentFilter, string SearchString, int? page)
        {
            HomeModel obj = new HomeModel();

            obj.ListSP = context.SanPhams.ToList();
            obj.ListCH = context.Cuahangs.ToList();
            obj.ListSP = obj.ListSP.OrderByDescending(n => n.maSP).ToList();
            return View(obj);
        }

        public ActionResult ProductCategory(int id, int? page)
        {
            var listProduct = context.SanPhams.Where(n => n.maCH == id).ToList();
            if (page == null)
                page = 1;
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(listProduct.ToPagedList(pageNum, pageSize));
        }
        public ActionResult CreateUser()
        {
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten");
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH");
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP");
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize");
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(SanPham n, [Bind(Include = "maSP,maLoaiSP,idAdmin,maTP,maSize,maCH,tenSP,gia,mota,hinhanh,status,ngaytao,ngaycapnhat")] SanPham sanPham)
        {
            try
            {
                n.status = 0;
                n.ngaytao = DateTime.Now;
                if (Session["AccountAdmin"] != null)
                {
                    Admin e = (Admin)Session["AccountAdmin"];


                    n.idAdmin = e.idAdmin;
                }
                //get photo
                if (ModelState.IsValid)
                {
                    if (n.ImageUpload != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(n.ImageUpload.FileName);
                        string extension = Path.GetExtension(n.ImageUpload.FileName);
                        filename = filename + extension;
                        n.hinhanh = filename;
                        string path = Path.Combine(Server.MapPath("~/Content/Template/img/"), filename);
                        n.ImageUpload.SaveAs(path);
                    }
                    context.SanPhams.Add(n);
                    context.SaveChanges();
                    return RedirectToAction("HomeOfAuthor", "Home");
                }

                ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
                ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
                ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
                ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
                ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
                return View(sanPham);
            }
            catch
            {
                return RedirectToAction("HomeOfAuthor", "Home");
            }

        }
        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = context.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        public ActionResult ListDuyet(int? page)
        {
            var listEmp = context.SanPhams.Where(p => p.status == 0).ToList();
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(listEmp.ToPagedList(pageNum, pageSize));
        }
        public ActionResult EditDuyet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = context.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
            return View(sanPham);
        }

        // POST: SanPhams1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDuyet([Bind(Include = "maSP,maLoaiSP,idAdmin,maTP,maSize,maCH,tenSP,gia,mota,hinhanh,status,ngaytao,ngaycapnhat")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {

                context.Entry(sanPham).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.idAdmin = new SelectList(context.Admins, "idAdmin", "ten", sanPham.idAdmin);
            ViewBag.maCH = new SelectList(context.Cuahangs, "maCH", "tenCH", sanPham.maCH);
            ViewBag.maLoaiSP = new SelectList(context.LoaiSPs, "maLoaiSP", "tenLoaiSP", sanPham.maLoaiSP);
            ViewBag.maSize = new SelectList(context.Sizes, "maSize", "tenSize", sanPham.maSize);
            ViewBag.maTP = new SelectList(context.Toppings, "maTP", "tenTP", sanPham.maTP);
            return View(sanPham);
        }
        public ActionResult ProductCH(int id, int? page)
        {
            var listProductCH = context.SanPhams.Where(n => n.idAdmin == id).ToList();
            if (page == null)
                page = 1;
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(listProductCH.ToPagedList(pageNum, pageSize));
        }
    }
}