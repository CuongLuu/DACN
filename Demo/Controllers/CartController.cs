using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    public class CartController : Controller
    {
        DBcontext context = new DBcontext();
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
            return RedirectToAction("ShowtoCart", "Cart");
        }
        public ActionResult AddtoCart1(int id, string strURL)
        {
            var sanpham = context.SanPhams.SingleOrDefault(p => p.maSP == id);
            if (sanpham != null)
            {
                GetCart().Add(sanpham, 1);
                return Redirect(strURL);
            }
            return Redirect(strURL);
        }
        public ActionResult ShowtoCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowtoCart", "Cart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult UpdateSoluongCart(FormCollection from)
        {
            Cart cart = Session["Cart"] as Cart;
            int masp = int.Parse(from["maSP"]);
            int soluong = int.Parse(from["SoLuong"]);
            cart.UpdateSoLuong(masp, soluong);
            return RedirectToAction("ShowtoCart", "Cart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteItemCart(id);
            return RedirectToAction("ShowtoCart", "Cart");
        }
        public PartialViewResult BagCart()
        {
            int totalitem = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)

                totalitem = cart.SoluongItemCart();
            ViewBag.soluongitem = totalitem;
            return PartialView("BagCart");

        }
        /*DBcontext context = new DBcontext();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Cart(int id)
        {
            var cart = Session[CartSession];
            var listitem = new List<CartItem>();
            if(cart != null)
            {
                listitem = (List<CartItem>)cart;
                return View(listitem);
            }
            return View("Cart");
        }

        public ActionResult AddNewItem(int masp, int soluong)
        {
            var sanpham = new SanPham();
            var cart = Session[CartSession];
            if(cart != null)
            {
                var listitem = (List<CartItem>)cart;
                if(listitem.Exists(x=>x.SanPham.maSP==masp))
                {
                    foreach (var item in listitem)
                    {
                        if (item.SanPham.maSP == masp)
                        {
                            item.soluong += soluong;
                        }
                    }
                }
                else
                {
                    //them moi mot san pham
                    var item = new CartItem();
                    item.SanPham = sanpham;
                    item.soluong = soluong;
                    listitem.Add(item);
                   
                }
                //gan vao session
                Session[CartSession] = listitem;
            }
            else
            {
                //them moi mot san pham
                var item = new CartItem();
                item.SanPham = sanpham;
                item.soluong = soluong;
                var listitem = new List<CartItem>();
                listitem.Add(item);
                //gan vao session
                Session[CartSession] = listitem;
            }
            return RedirectToAction("Cart");
        }*/

    }
}