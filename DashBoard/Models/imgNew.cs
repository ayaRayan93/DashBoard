using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.Models
{
    public class imgNew
    {
        public int NewID { get; set; }
       public string img{get; set;}
        public string Headertxt { get; set; }
        public DateTime DateNew { get; set; }
        public string Maintxt { get; set; }
        public int FKBasicID { get; set; }
    }
}