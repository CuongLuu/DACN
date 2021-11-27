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
            var userId = User.Identity.GetUserId();
            var currentUser = context.NguoiDungs.Find(userId);
            ViewBag.currentUser = currentUser;
            ViewBag.postId = Id;
            return PartialView("_CommentPartial", commentList);
        }
        [HttpPost]
        public JsonResult CreateComment(int masp, string content)
        {
            string userId = User.Identity.GetUserId();
            int id = int.Parse(userId);
            CMT newComment = new CMT
            {
                maSP = masp,
                maND = id,
                content = content,
                ngaytao = DateTime.Now,
                ngaysua = DateTime.Now,
            };
            context.CMTs.Add(newComment);
            context.SaveChanges();

            var user = context.NguoiDungs.Find(userId);
            newComment.NguoiDung = user;
            return Json(new { message = "Success", data = newComment }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateSubComment(int commentId, string content)
        {
            string userId = User.Identity.GetUserId();
            int id = int.Parse(userId);
            SubCMT newSubComment = new SubCMT
            {
                maCMT = commentId,
                maND = id,
                content = content,
                ngaytao = DateTime.Now,
                ngaysua = DateTime.Now,
            };
            context.SubCMTs.Add(newSubComment);
            context.SaveChanges();
            var user = context.NguoiDungs.Find(userId);
            newSubComment.NguoiDung = user;
            return Json(new { message = "Success", data = newSubComment }, JsonRequestBehavior.AllowGet);
        }
    }
}