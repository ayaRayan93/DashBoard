using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.Models
{
    public class SocialMedia
    {
        public int SocialID {get; set;}
        public string icon { get; set; }
        public string link { get; set; }
        public int FKBasicID { get; set; }
    }
}