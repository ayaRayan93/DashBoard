using DashBoard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class DashController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=maindb;Integrated Security=True");
        SqlCommand com;

        // GET: Dash

        public ActionResult Login()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult Login(string username,string pass)
        {
            try
            {
                
                if (username=="Admin"&& pass == "111")
                {
                    Session["UserLog"] = "login";
                    return RedirectToAction("Home");
                }
                else { return View(); }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["UserLog"] = null;
            return Redirect("Login");
        }
        public ActionResult Home()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult Home(Basics bas)
        {
            try
            {
                
                con.Open();
                com = new SqlCommand("insert into  Basics(email,Address,website,callus) Values ('" + bas.email + "','" + bas.Address + "','" + bas.website + "','" + bas.callus + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "your Data inserted successfully";
                return View();
            }
            catch
            {
                ViewBag.msg = "your Data didn't insert correctly";
                return View();
            }
        }

        public ActionResult mainText()
        {

            return View();
        }

        [HttpPost]
        public ActionResult mainText(Basics bas)
        {
            try
            {
                con.Open();
                com = new SqlCommand("insert into  Basics(ourServtxt,headerMidtxt,ParagMidtxt,maintextNews) Values ('" + bas.ourServtxt + "','" + bas.headerMidtxt + "','" + bas.ParagMidtxt + "','" + bas.maintextNews + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "your Data inserted successfully";
                return View(); }
            catch { ViewBag.Emsg = "your Data didn't insert correctly"; return View(); }
         }
        public ActionResult socialmedia()
        {
            DDLFun();
            return View(); }
        [HttpPost]
        public ActionResult socialmedia(SocialMedia sM)
        {
            try
            {
                con.Open();
                com = new SqlCommand("insert into  SocialMedia Values ('" + sM.icon + "','" + sM.link + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "Successded";
                return View();
            }
            catch { ViewBag.Emsg = "Error Ocurred"; return View(); }
        }
        public void DDLFun()
        {

            try
            {
                con.Open();
                com = new SqlCommand("select * from SocialMedia ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                if (d.Rows.Count > 0)
                {
                    List<SocialMedia> List = new List<SocialMedia>();
                    string[] arr=new string[4]{ "facebook", "twitter", "whatsapp", "instagram" };
                    for (int i = 0; i < arr.Length; i++)
                    {
                        for (int k = 0; k< d.Rows.Count; k++)
                        {
                            if (d.Rows[k]["icon"].ToString() == arr[i])
                            {
                                continue;
                            }
                        }
                        List.Add(new SocialMedia() { SocialID = i+1, icon = arr[i] });
                    }
                    
                    ViewBag.listIcons = List;
                }
               
               
                con.Close();
            }
            catch { con.Close(); }

        }



        public ActionResult subcategory()
        {
            DDlSevices();
            return View();
        }
        [HttpPost]
        public ActionResult subcategory(SubCategory sub)
        {
            try
            {
                con.Open();
                com = new SqlCommand("insert into  SubCategory Values ('" + sub.SubCateName + "','" + sub.FKServID + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "Successded";
                return View();
            }
            catch { ViewBag.Emsg = "Error Ocurred"; return View(); }
        }
       public void DDlSevices()
        {
            try
            {
                con.Open();
                com = new SqlCommand("select * from Services ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                 List<Services> List = new List<Services>();
                    for (int t = 0; t < d.Rows.Count; t++)
                    {
                        int id = int.Parse(d.Rows[t]["ServicesID"].ToString());
                        string name = d.Rows[t]["Name"].ToString();
                        List.Add(new Services() {ServicesID=id,Name=name});
                    }
                    ViewBag.listServ = new SelectList(List, "ServicesID", "Name");
                con.Close();
            }
            
            catch { con.Close(); }
        }
        public ActionResult LastNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LastNew(imgNew singlenews)
        {
            HttpPostedFileBase file = Request.Files["imgfile"];
            if (file != null && file.ContentLength > 0)
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    singlenews.img = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("insert into  imgNew Values ('" + singlenews.img + "','" + singlenews.Headertxt + "','" + singlenews.DateNew + "','" + singlenews.Maintxt + "'," + singlenews.FKBasicID + ")", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "your news inserted successfully";
                    return View();
                }
                catch { ViewBag.msg = "your news didn't insert correctly"; return View(); }

            else
            {
                ViewBag.msg = "file not uploaded"; return View();
            }
        }

        public ActionResult Services()
        {
            return View();
        }
        [HttpPost]
        public ActionResult services(Services serv)
        {
            HttpPostedFileBase file = Request.Files["imgfile"];
            if (file != null && file.ContentLength > 0)
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    serv.img = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("insert into  Services Values ('" + serv.Name + "','" + serv.textServe + "','" + serv.img + "')", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "your services inserted successfully";
                   return View();
                }
                catch {
                        ViewBag.Emsg = "your services didn't insert correctly"; return View();
                      }

            else
            {
                ViewBag.Emsg = "file not uploaded"; return View();
            }
        }


        public ActionResult AllBranches()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AllBranches(Branches br, HttpPostedFileBase[] imgsfile)
        {
            BranchDetail bd=new BranchDetail();
            HttpPostedFileBase file = Request.Files["imgfile"];
            
            foreach (HttpPostedFileBase f in imgsfile)
            {
                if (f.ContentLength > 0)
                {
                    if (f != null && file.ContentLength > 0)
                    {
                        f.SaveAs(Path.Combine(Server.MapPath("/imgs"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                    }
                }
            }
                    if (file != null && file.ContentLength > 0)
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    br.mainImg = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("insert into  Branches Values ('" + br.phone + "','" + br.Title + "','" + br.mainImg + "','"+br.AddBranche+"')", con);
                    com.ExecuteNonQuery();
                    int id=getMaxId();
                    SqlCommand com2 = new SqlCommand("insert into BranchDetail values('"+bd.imgSrc+"',"+id+")", con);
                    con.Close();
                    ViewBag.msg = "your data inserted successfully";
                    return View();
                }
                catch
                {
                    ViewBag.Emsg = "your data didn't insert correctly"; return View();
                }

            else
            {
                ViewBag.Emsg = "file not uploaded"; return View();
            }
        }



        public int getMaxId()
        {
            try {
                com = new SqlCommand("select top(1)BID from Branches order by BID desc ");
                SqlDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch { con.Close();return 0; }
        }
        public ActionResult branch()
        {
            try
            {

                con.Open();
                com = new SqlCommand("select * from Branches ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                List<Branches> List = new List<Branches>();
                for (int t = 0; t < d.Rows.Count; t++)
                {
                    int id = Convert.ToInt16(d.Rows[t]["BID"]);
                    string name = d.Rows[t]["Title"].ToString();
                    List.Add(new Branches() { BID = id, Title = name });
                    ViewBag.listBranches = new SelectList(List, "BID", "Title");
                }

                con.Close();
            }
            catch { con.Close(); }

            return View();
        }
        [HttpPost]
        public ActionResult branch(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                if (file.ContentLength > 0)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        file.SaveAs(Path.Combine(Server.MapPath("/imgs"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                    }
                    //var fileName = Path.GetFileName(file.FileName);
                    //var path = Path.Combine(Server.MapPath("~/imgs"), fileName);
                    //file.SaveAs(path);
                }
            }
            return View();
        }
        
    }
}