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

namespace Demo.Controllers
{
    public class ShopController : Controller
    {
        DBcontext context = new DBcontext();
        // GET: Shop
        public ActionResult Shop(int id)
        {

            List<SanPham> list = new List<SanPham>();
            list = context.SanPhams.Where(p => p.maCH == id).ToList();
            return View(list);

        }
        public ActionResult tenShop(int id)
        {
            Cuahang ch = context.Cuahangs.FirstOrDefault(p => p.maCH == id);
            var ten = ch.tenCH;
            ViewBag.tench = ten;
            return PartialView("tenShop");
        }
    }
}