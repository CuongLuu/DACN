using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models.Context;
using Demo.Models;

namespace Demo.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        DBcontext context = new DBcontext();
        public ActionResult Pay()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                //lấy thông tin giỏ hàng từ session

                Cart cart = Session["Cart"] as Cart;

                //gán dữu liệu cho table hóa đơn
                HoaDon objHoaDon = new HoaDon();
                objHoaDon.ghichu = "Đơn Hàng " + DateTime.Now.ToString("yyyyMMddHHmmss");
                objHoaDon.ngaymua = DateTime.Now;
                objHoaDon.sotien = cart.Items.Sum(p => p.Shopping_sanpham.gia * p.Shopping_soLuong);
                objHoaDon.soluong = cart.Items.Sum(p => p.Shopping_soLuong);
                context.HoaDons.Add(objHoaDon);
                context.SaveChanges();
                ViewBag.hoadon = objHoaDon;
                int mahoadon = objHoaDon.maHD;
                List<CTHD> chitiethoadon = new List<CTHD>();
                NguoiDung u = (NguoiDung)Session["Account"];
                foreach (var item in cart.Items)
                {
                    CTHD objCTHD = new CTHD();
                    objCTHD.maHD = mahoadon;
                    objCTHD.maCH = item.Shopping_sanpham.maCH;
                    objCTHD.maND = u.maND;
                    objCTHD.maSP = item.Shopping_sanpham.maSP;
                    objCTHD.soluong = item.Shopping_soLuong;
                    objCTHD.giaban = item.Shopping_sanpham.gia;
                    objCTHD.thanhtien = item.Shopping_soLuong * item.Shopping_sanpham.gia;

                    chitiethoadon.Add(objCTHD);

                }
                context.CTHDs.AddRange(chitiethoadon);
                context.SaveChanges();
                return RedirectToAction("Thongbao", "Payment");

            }
        }
        public ActionResult Payment()

        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (Session["Cart"] == null)
                    return RedirectToAction("Payment", "Payment");
                Cart cart = Session["Cart"] as Cart;

                return View(cart);
            }
        }
        public ActionResult Thongbao()
        {
            return View();
        }
        ///
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            var sanpham = context.SanPhams.SingleOrDefault(p => p.maSP == id);
            if (sanpham != null)
            {
                GetCart().Add(sanpham, 1);

            }
            return RedirectToAction("Payment", "Payment");
        }
        public ActionResult infUser()
        {
            
            NguoiDung u = (NguoiDung)Session["Account"];
            
                var ten =u.tenND;
                var diachi = u.diachi;
                var sdt = u.sdt;
                ViewBag.ten = ten;
                ViewBag.diachi = diachi;
                ViewBag.sdt = sdt;
                ViewBag.tg = DateTime.Now;
            
            
            return PartialView("infUser");
            

        }
    }
}