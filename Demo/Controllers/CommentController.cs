using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Microsoft.AspNet.Identity;

namespace Demo.Controllers
{
    public class CommentController : Controller
    {
        DBcontext context = new DBcontext();
        // GET: Comment
        [ChildActionOnly]
        public ActionResult Index(int Id)
        {
            var commentList = context.CMTs.Where(item => item.maSP == Id).OrderByDescending(x => x.ngaytao).ToList();
            NguoiDung u = (NguoiDung)Session["Account"];
            var currentUser = context.NguoiDungs.Find(u.maND);
            ViewBag.currentUser = currentUser;
            ViewBag.postId = Id;
            return PartialView("Comment", commentList);
        }
        [HttpPost]
        public JsonResult CreateComment(int postId, string content)
        {
            NguoiDung u = (NguoiDung)Session["Account"];
            CMT newComment = new CMT
            {
                maSP = postId,
                maND = u.maND,
                content = content,
                ngaytao = DateTime.Now,
                ngaysua = DateTime.Now,
            };
            context.CMTs.Add(newComment);
            context.SaveChanges();

            var user = context.NguoiDungs.Find(u.maND);
            newComment.NguoiDung = user;
            return Json(new { message = "Success", data = newComment }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateSubComment(int commentId, string content)
        {
            NguoiDung u = (NguoiDung)Session["Account"];
            SubCMT newSubComment = new SubCMT
            {
                maCMT = commentId,
                maND = u.maND,
                content = content,
                ngaytao = DateTime.Now,
                ngaysua = DateTime.Now,
            };
            context.SubCMTs.Add(newSubComment);
            context.SaveChanges();
            var user = context.NguoiDungs.Find(u.maND);
            newSubComment.NguoiDung = user;
            return Json(new { message = "Success", data = newSubComment }, JsonRequestBehavior.AllowGet);
        }
    }
}