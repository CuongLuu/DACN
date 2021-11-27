using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo.Models.Context
{
    public class HomeModel
    {
        public List<SanPham> ListSP { get; set; }
        public List<Silde> ListSlide { get; set; }

        public List<Cuahang> ListCH { get; set; }
    }
}