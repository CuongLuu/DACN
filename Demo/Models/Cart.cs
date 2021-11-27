using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class CartItem
    {
        public SanPham Shopping_sanpham { get; set; }
        public HoaDon Shopping_hoadon { get; set; }
        public NguoiDung Shopping_nguoidung { get; set; }
        public int Shopping_soLuong { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(SanPham sanPham, int soLuong)
        {
            var item = items.FirstOrDefault(p => p.Shopping_sanpham.maSP == sanPham.maSP);
            if(item==null)
            {
                items.Add(new CartItem
                {
                    Shopping_sanpham = sanPham,
                    Shopping_soLuong = soLuong
                }) ;
            }
            else
            {
                item.Shopping_soLuong += soLuong;
            }
        }
        public void UpdateSoLuong(int id, int soluong)
        {
            var item = items.Find(p => p.Shopping_sanpham.maSP == id);
            if (item != null)
            {
                item.Shopping_soLuong = soluong;
            }
        }
        public float Tongtien()
        {
            var tongtien = items.Sum(p => p.Shopping_sanpham.gia * p.Shopping_soLuong);
            return (float)tongtien;
        }
        public void DeleteItemCart(int id)
        {
            items.RemoveAll(p => p.Shopping_sanpham.maSP==id);
            
        }
        public int SoluongItemCart()
        {
            return items.Sum(p => p.Shopping_soLuong);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}