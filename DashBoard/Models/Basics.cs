using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DashBoard.Models
{
    public class Basics
    {
        public int BasicID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        public string Address { get; set; }
        public string website { get; set; }
        public string ourServtxt { get; set; }
        [StringLength(maximumLength:  200, ErrorMessage = "field must be less than 200 letters characters")]
        public string headerMidtxt { get; set; }
        public string ParagMidtxt { get; set; }
        [Required]
        public string callus { get; set; }
        public string maintextNews { get; set; }
       public string mainImagText1 { get; set; }
        public string mainImagText2 { get; set; }
        public string parImgtxt { get; set; }
        public string midHeaderIndexPage { get; set; }
        public string midParaIndexPage { get; set; }
    }
}