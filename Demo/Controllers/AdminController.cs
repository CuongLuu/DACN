using Demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class AdminController : Controller
    {
        DBcontext context = new DBcontext();
        // GET: Admin
        public ActionResult AdminManage()
        {

            return View();
        }
        public ActionResult Decentralization()
        {
            var listEmp = context.Admins.ToList();
            return View(listEmp);
        }
        public ActionResult ChangePosition(int id)
        {
            var emp = context.Admins.Where(p => p.idAdmin == id).SingleOrDefault();
            return View(emp);
        }
        [HttpPost, ActionName("ChangePosition")]
        public ActionResult ChangePosition(Admin e)
        {
            if (Session["AccountAdmin"] != null)
            {
                e.ngaysua = DateTime.Now;
            }
            context.Admins.AddOrUpdate(e);
            context.SaveChanges();
            return RedirectToAction("Decentralization", "Admin");
        }
        public ActionResult Active()
        {
            var listEmp = context.Admins.Where(p => p.status == 0).ToList();
            return View(listEmp);
        }
        public ActionResult ActiveAdmin(int id)
        {
            var emp = context.Admins.FirstOrDefault(p => p.idAdmin == id);
            emp.status = 1;
            context.Admins.AddOrUpdate(emp);
            context.SaveChanges();
            return RedirectToAction("Active", "Admin");
        }
        [ActionName("Trash")]
        public ActionResult Trash()
        {
            var listEmp = context.Admins.Where(p => p.status == 3).ToList();
            return View(listEmp);
        }
        public ActionResult Details(int id)
        {
            var emp = context.Admins.FirstOrDefault(p => p.idAdmin == id);
            return View(emp);
        }
        public ActionResult List()
        {
            var listEmp = context.Admins.Where(p => p.status != 3).ToList();
            return View(listEmp);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var emp = context.Admins.Where(p => p.idAdmin == id).SingleOrDefault();
            context.Admins.Remove(emp);
            context.SaveChanges();
            return RedirectToAction("List", "Admin");
        }
        [ActionName("Restore")]
        public ActionResult Restore(int id)
        {
            var emp = context.Admins.Where(p => p.idAdmin == id).SingleOrDefault();
            emp.status = 1;
            context.Admins.AddOrUpdate(emp);
            context.SaveChanges();
            return RedirectToAction("List", "Admin");
        }
        public ActionResult Remove(int id)
        {
            var emp = context.Admins.FirstOrDefault(p => p.idAdmin == id);
            emp.status = 3;
            context.Admins.AddOrUpdate(emp);
            context.SaveChanges();
            return RedirectToAction("List", "Admin");
        }
    }
}