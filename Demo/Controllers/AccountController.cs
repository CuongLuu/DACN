using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;


namespace Demo.Controllers
{
    public class AccountController : Controller
    {
        DBcontext context = new DBcontext();



        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection f)
        {
            // check username and password
            string user = f["email"].ToString();
            string pass = f["password"].ToString();


            NguoiDung u = context.NguoiDungs.Where(n => n.email == user && n.status == 1).SingleOrDefault();
            if (u != null)
            {
                string hashpass = u.matkhau;
                Boolean checkPass = BCrypt.Net.BCrypt.Verify(pass, hashpass);
                //if user input right account, show homepage(index)
                if (checkPass)
                {
                    Session["Account"] = u;
                    return RedirectToAction("Index", "Home");
                }
                else

                    //back to login
                    return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection f)
        {
            string pass = f["password"].ToString();
            string repass = f["repassword"].ToString();
            if (pass != repass)
            {
                ViewBag.Status = "Mật khẩu không khớp nhau!";
                return View();
            }
            else
            {
                string email = f["email"].ToString();
                NguoiDung userInvalid = context.NguoiDungs.SingleOrDefault(n => n.email == email);
                NguoiDung u = new NguoiDung();
                if (userInvalid == null)
                {
                    u.tenND = f["name"].ToString();
                    u.email = f["email"].ToString();
                    // encrypt password by BCrypt
                    u.matkhau = BCrypt.Net.BCrypt.HashPassword(f["password"].ToString(), 12);
                    u.status = 1;
                    u.ngaytao = DateTime.Now;
                    u.gioitinh = null;
                    u.sdt = f["phone"].ToString();
                    u.ngaysua = null;
                    u.ngaysinh = System.Convert.ToDateTime(f["birthday"]);
                    u.diachi = f["address"].ToString();
                    u.anh = "defaultAvatar.png";
                    context.NguoiDungs.Add(u);
                    context.SaveChanges();
                    Session["Account"] = u;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Status = "Email dã được đăng ký!";
                    return View();
                }
            }



        }
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost, ActionName("LoginAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(FormCollection f)
        {
            string username = f["email"].ToString();
            string pass = f["password"].ToString();
            Admin u = context.Admins.Where(n => n.email == username && n.status == 1).SingleOrDefault();
            if (u != null)
            {
                string hashpass = u.matkhau;
                Boolean checkPass = BCrypt.Net.BCrypt.Verify(pass, hashpass);
                //if user input right account, show homepage(index)
                if (checkPass)
                {

                    if (u.type == 1)
                    {
                        Session["AccountAdmin"] = u;
                        return RedirectToAction("AdminManage", "Admin");
                    }
                    else if (u.type == 0)
                    {
                        Session["AccountAdmin"] = u;
                        Cuahang s = context.Cuahangs.Where(n => n.maCH == u.idAdmin).SingleOrDefault();
                        Session["CuaHang"] = s;
                        return RedirectToAction("ManagerShop", "Home");
                    }

                }
                else

                    //back to login
                    return View();
            }
            return View();
        }
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost, ActionName("RegisterAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin(FormCollection f)
        {
            string pass = f["password"].ToString();
            string repass = f["repassword"].ToString();
            if (pass != repass)
            {
                ViewBag.Status = "Mật khẩu không khớp nhau!";
                return View();
            }
            else
            {
                string email = f["email"].ToString();
                Admin employeeInvalid = context.Admins.SingleOrDefault(n => n.email == email);
                Admin u = new Admin();
                if (employeeInvalid == null)
                {
                    u.ten = f["name"].ToString();
                    u.email = f["email"].ToString();
                    u.ngaysinh = System.Convert.ToDateTime(f["birthday"]);
                    u.matkhau = BCrypt.Net.BCrypt.HashPassword(f["password"].ToString(), 12);
                    u.status = 0;
                    u.gioitinh = null;
                    u.type = 0;
                    u.cmnd = f["cmnd"].ToString();
                    u.sdt = f["phone"].ToString();
                    u.diachi = f["address"].ToString();
                    u.ngaytao = DateTime.Now;
                    u.ngaysua = null;
                    u.anh = "defaultAvatar.png";
                    context.Admins.Add(u);
                    context.SaveChanges();
                    ViewBag.Status = "Đăng kí thành công!Vui lòng chờ xét duyệt!";
                    return View();
                }
                else
                {
                    ViewBag.Status = "Email đã được đăng ký!";
                    return View("");
                }
            }

        }
        public ActionResult LogoutAdmin()
        {
            Session["AccountAdmin"] = null;
            return RedirectToAction("LoginAdmin", "Account");
        }
        public ActionResult ChangepasswordAdmin()
        {
            return View();
        }

        [HttpPost, ActionName("ChangepasswordAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangepasswordAdmin(FormCollection f)
        {
            string oldpass = f["oldPass"].ToString();
            string newpass = f["newPass"].ToString();
            string renewpass = f["reNewPass"].ToString();
            if (Session["AccountAdmin"] != null)
            {
                Admin e = (Admin)Session["AccountAdmin"];
                string hashpass = e.matkhau;
                Boolean checkPass = BCrypt.Net.BCrypt.Verify(oldpass, hashpass);
                // if user input right password 
                if (checkPass)
                {
                    //if new pass = old pass return view()
                    if (newpass == oldpass)
                    {
                        ViewBag.status = "Mật khẩu mới không được trùng mật khẩu cũ!";
                        return View();
                    }
                    else
                    {
                        if (newpass != renewpass)
                        {
                            ViewBag.status = "Mật khẩu mới không khớp nhau!";
                            return View();
                        }
                        else
                        {
                            e.matkhau = BCrypt.Net.BCrypt.HashPassword(f["newpass"].ToString(), 12);
                            context.Admins.AddOrUpdate(e);
                            context.SaveChanges();
                        }
                    }


                }
                else
                {
                    ViewBag.status = "Mật khẩu không đúng!";
                    return View();
                }
            }
            return RedirectToAction("AdminManage", "Admin");

        }
        public ActionResult ChangepasswordUser()
        {
            return View();
        }
        [HttpPost, ActionName("ChangepasswordUser")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangepasswordUser(FormCollection f)
        {
            string oldpass = f["oldPass"].ToString();
            string newpass = f["newPass"].ToString();
            string renewpass = f["reNewPass"].ToString();
            if (Session["Account"] != null)
            {
                NguoiDung e = (NguoiDung)Session["Account"];
                string hashpass = e.matkhau;
                Boolean checkPass = BCrypt.Net.BCrypt.Verify(oldpass, hashpass);
                // if user input right password 
                if (checkPass)
                {
                    //if new pass = old pass return view()
                    if (newpass == oldpass)
                    {
                        ViewBag.status = "Mật khẩu mới không được trùng mật khẩu cũ!";
                        return View();
                    }
                    else
                    {
                        if (newpass != renewpass)
                        {
                            ViewBag.status = "Mật khẩu mới không khớp nhau!";
                            return View();
                        }
                        else
                        {
                            e.matkhau = BCrypt.Net.BCrypt.HashPassword(f["newpass"].ToString(), 12);
                            context.NguoiDungs.AddOrUpdate(e);
                            context.SaveChanges();
                        }
                    }
                }
                else
                {
                    ViewBag.status = "Mật khẩu không đúng!";
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ChangepasswordAuthor()
        {
            return View();
        }

        [HttpPost, ActionName("ChangepasswordAuthor")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangepasswordAuthor(FormCollection f)
        {
            string oldpass = f["oldPass"].ToString();
            string newpass = f["newPass"].ToString();
            string renewpass = f["reNewPass"].ToString();
            if (Session["AccountAdmin"] != null)
            {
                Admin e = (Admin)Session["AccountAdmin"];
                string hashpass = e.matkhau;
                Boolean checkPass = BCrypt.Net.BCrypt.Verify(oldpass, hashpass);
                // if user input right password 
                if (checkPass)
                {
                    //if new pass = old pass return view()
                    if (newpass == oldpass)
                    {
                        ViewBag.status = "Mật khẩu mới không được trùng mật khẩu cũ!";
                        return View();
                    }
                    else
                    {
                        if (newpass != renewpass)
                        {
                            ViewBag.status = "Mật khẩu mới không khớp nhau!";
                            return View();
                        }
                        else
                        {
                            e.matkhau = BCrypt.Net.BCrypt.HashPassword(f["newpass"].ToString(), 12);
                            context.Admins.AddOrUpdate(e);
                            context.SaveChanges();
                        }
                    }


                }
                else
                {
                    ViewBag.status = "Mật khẩu không đúng!";
                    return View();
                }
            }
            return RedirectToAction("ManagerShop", "Home");

        }
    }
}