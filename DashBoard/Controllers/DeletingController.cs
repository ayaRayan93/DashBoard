using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class DeletingController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CQI3I4K\MYSQLSERVER;Initial Catalog=maindb;Integrated Security=True");
        SqlCommand com;
        // GET: Deleting
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void DeleteSocial(int id)
        {
            try
            {
                con.Open();
          
                com = new SqlCommand("delete from SocialMedia where SocialID=" + id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }
        [HttpPost]
        public void Deletesubcat(int id)
        {try {
                con.Open();
                com = new SqlCommand("delete from Products where FKSubID=" + id, con);
                com.ExecuteNonQuery();
                com = new SqlCommand("delete from SubCategory where SubCategoryID="+id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

        [HttpPost]
        public void Deleteserv(int id)
        {
            try
            {
                con.Open();
                //com = new SqlCommand("delete from Products join SubCategory on FKSubID=SubCategoryID where SubCategory.FKServID="+id, con);
                //com.ExecuteNonQuery();
                //com = new SqlCommand("delete from SubCategory where FKServID=" + id, con);
                //com.ExecuteNonQuery();
                com = new SqlCommand("delete from Services where ServicesID=" + id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

        [HttpPost]
        public void DeleteimgNew(int id)
        {
            try
            {
                con.Open();
                com = new SqlCommand("delete from imgNew where NewID=" + id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

        [HttpPost]
        public void Deleteproducts(int id)
        {
            try
            {
                con.Open();

                com = new SqlCommand("delete from Products where ProductID=" + id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

        [HttpPost]
        public void Deletebranch(int id)
        {
            try
            {
                con.Open();
                com = new SqlCommand("delete from BranchDetail where FKBrancheID=" + id, con);
                com.ExecuteNonQuery();
                com = new SqlCommand("delete from Branches where BID=" + id, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

       
    }
}