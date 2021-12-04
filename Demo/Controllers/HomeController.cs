using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using DocumentFormat.OpenXml.Bibliography;
using Demo.Models.Context;
using PagedList;
using PagedList.Mvc;


namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        DBcontext context = new DBcontext();
        public ActionResult Index()
        {
            HomeModel obj = new HomeModel();
            obj.ListSP = context.SanPhams.ToList();
            obj.ListSlide = context.Sildes.ToList();
            return View(obj);
        }
        public ActionResult Index2()
        {
            var listProduct = context.Cuahangs.ToList();
           
            return PartialView(listProduct);
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ProfileUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditProfileUser(int id)
        {

            NguoiDung user = context.NguoiDungs.SingleOrDefault(p => p.maND == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("EditProfileUser")]
        public ActionResult EditProfileUser(NguoiDung user)
        {

            NguoiDung dbUpdate = context.NguoiDungs.FirstOrDefault(p => p.maND == user.maND);
            if (dbUpdate != null)
            {
                //get date modified
                user.ngaysua = DateTime.Now;
                // get photo
                if (user.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(user.ImageUpload.FileName);
                    string extension = Path.GetExtension(user.ImageUpload.FileName);
                    filename = filename + extension;
                    user.anh = filename;
                    string path = Path.Combine(Server.MapPath("~/Image/ImageUpload/"), filename);
                    user.ImageUpload.SaveAs(path);
                }
                context.NguoiDungs.AddOrUpdate(user);
                context.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("ProfileUser", "Home", new { id = user.maND });
        }
        /* [HttpPost, ActionName("EditProfileUser")]
         public ActionResult EditProfileUser(NguoiDung user, HttpPostedFileBase uploadhinh)
         {
             NguoiDung dbUpdate = context.NguoiDungs.FirstOrDefault(p => p.maND == user.maND);
             if (dbUpdate != null)
             {
                 //get date modified
                 user.ngaysua = DateTime.Now;
                 // get photo
                 if (uploadhinh != null && uploadhinh.ContentLength > 0)
                 {
                     int id = user.maND;
                     string filename = "";
                     int index = uploadhinh.FileName.IndexOf('.');
                     filename = "user" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                     string path = Path.Combine(Server.MapPath("~/Image/ImageUpload"), filename);
                     uploadhinh.SaveAs(path);
                     dbUpdate.anh = filename;
                 }
                 context.NguoiDungs.AddOrUpdate(user);
                 context.SaveChanges();
             }
             return RedirectToAction("Index", "Home");
         }
 */
        public ActionResult ProfileAdmin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Productdetails(int id)
        {
            SanPham dbsanpham = context.SanPhams.FirstOrDefault(p => p.maSP == id);
            return View(dbsanpham);
        }

        public ActionResult Search(string strSearch)
        {
            List<SanPham> list = new List<SanPham>();
            if (string.IsNullOrEmpty(strSearch))
            {
                ViewBag.Message = "Your contact page.";
            }
            else
            {
                list = context.SanPhams.SqlQuery("Select * from SanPham where tenSP like '%" + strSearch + "%'").ToList();
            }
            return View(list);
        }
        public ActionResult HomeOfAuthor(int id,int? page)
        {
            var listProductCH = context.SanPhams.Where(n => n.idAdmin == id).ToList();
            if (page == null)
                page = 1;
            int pageSize = 9;
            int pageNum = (page ?? 1);
            listProductCH = listProductCH.OrderByDescending(n => n.maSP).ToList();
            return View(listProductCH.ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult EditProfileAdmin(int id)
        {
            Admin dbUpdate = context.Admins.SingleOrDefault(p => p.idAdmin == id);
            if (dbUpdate == null)
            {
                return HttpNotFound();
            }
            return View(dbUpdate);
        }
        [HttpPost, ActionName("EditProfileAdmin")]
        public ActionResult EditProfileAdmin(Admin e)
        {
            try
            {
                if (Session["AccountAdmin"] != null)
                {
                    Admin e2 = (Admin)Session["AccountAdmin"];
                    e.ngaysua = DateTime.Now;
                    // get photo
                    if (e.ImageUpload != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(e.ImageUpload.FileName);
                        string extension = Path.GetExtension(e.ImageUpload.FileName);
                        filename = filename + extension;
                        e.anh = filename;
                        string path = Path.Combine(Server.MapPath("~/Image/ImageUpload/"), filename);
                        e.ImageUpload.SaveAs(path);
                    }
                }
                context.Admins.AddOrUpdate(e);
                context.SaveChanges();
                return RedirectToAction("AdminManage", "Admin");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ManagerShop()
        {
            return View();
        }
        public ActionResult AuthorProfile()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult EditProfileAuthor(int id)
        {
            Admin dbUpdate = context.Admins.SingleOrDefault(p => p.idAdmin == id);
            if (dbUpdate == null)
            {
                return HttpNotFound();
            }
            return View(dbUpdate);
        }
        [HttpPost, ActionName("EditProfileAuthor")]
        public ActionResult EditProfileAuthor(Admin e)
        {
            try
            {
                if (Session["AccountAdmin"] != null)
                {
                    Admin e2 = (Admin)Session["AccountAdmin"];
                    e.ngaysua = DateTime.Now;
                    // get photo
                    if (e.ImageUpload != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(e.ImageUpload.FileName);
                        string extension = Path.GetExtension(e.ImageUpload.FileName);
                        filename = filename + extension;
                        e.anh = filename;
                        string path = Path.Combine(Server.MapPath("~/Image/ImageUpload/"), filename);
                        e.ImageUpload.SaveAs(path);
                    }
                }
                context.Admins.AddOrUpdate(e);
                context.SaveChanges();
                return RedirectToAction("AuthorProfile", "Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ProfileShop()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}