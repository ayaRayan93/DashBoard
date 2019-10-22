using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public float Price { get; set; }
        public string Text { get; set; }
         public string img { get; set; }
        public int FKSubID{get;set;}

    }
}