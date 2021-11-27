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
        public ActionResult Shop()
        {
            HomeModel obj = new HomeModel();
            obj.ListSP = context.SanPhams.ToList();
            obj.ListSlide = context.Sildes.ToList();
            return View(obj);
        }
    }
}