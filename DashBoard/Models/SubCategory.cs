using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCateName { get; set; }
        public string enSubName { get; set; }
        public int FKServID { get; set; }
    }
}