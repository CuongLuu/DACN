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

namespace Demo.Controllers
{
    public class ShopController : Controller
    {
        DBcontext context = new DBcontext();
        // GET: Shop
        public ActionResult Shop(int id, int? page)
        {            
            var list = context.SanPhams.Where(p => p.maCH == id).ToList();
            if (page == null)
                page = 1;
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(list.ToPagedList(pageNum, pageSize));

        }
        public ActionResult tenShop(int id)
        {
            Cuahang ch = context.Cuahangs.FirstOrDefault(p => p.maCH == id);
            ViewBag.tench = ch.tenCH;
            ViewBag.anh = ch.anh;
            return PartialView("tenShop");
        }
    }
}