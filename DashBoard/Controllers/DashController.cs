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
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CQI3I4K\MYSQLSERVER;Initial Catalog=maindb;Integrated Security=True");
        SqlCommand com;

        // GET: Dash

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string pass)
        {
            try
            {

                if (username == "Admin" && pass == "111")
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
        public ActionResult Home(Basics bas,string addr)
        {
            try
            {

                con.Open();
                com = new SqlCommand("insert into  Basics(email,Address,website,callus) Values ('" + bas.email + "',N'" + bas.Address + "','" + bas.website + "','" + bas.callus + "')", con);
                com.ExecuteNonQuery();
                com = new SqlCommand("insert into  Basics(Address) Values ('" + addr + "')", con);
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
        public ActionResult mainText(Basics bas,string enourServtxt, string headerMidtxt, string enParagMidtxt, string maintextNews,string mainImagText1,string mainImagText2, string parImgtxt, string midHeaderIndexPage,string midParaIndexPage)
        {
            try
            {
                con.Open();
                com = new SqlCommand("insert into  Basics(ourServtxt,headerMidtxt,ParagMidtxt,maintextNews,mainImagText1,mainImagText2,parImgtxt,midHeaderIndexPage,midParaIndexPage) Values (N'" + bas.ourServtxt + "',N'" + bas.headerMidtxt + "',N'" + bas.ParagMidtxt + "',N'" + bas.maintextNews + "',N'"+bas.mainImagText1+"',N'"+bas.mainImagText2+"',N'"+bas.parImgtxt+"',N'"+bas.midHeaderIndexPage+"',N'"+bas.midParaIndexPage+"')", con);
                com.ExecuteNonQuery();
                com = new SqlCommand("insert into  Basics(ourServtxt,headerMidtxt,ParagMidtxt,maintextNews,mainImagText1,mainImagText2,parImgtxt,midHeaderIndexPage,midParaIndexPage) Values ('" + enourServtxt + "','" +headerMidtxt + "','" + enParagMidtxt + "','" + maintextNews + "','" + mainImagText1 + "','" + mainImagText2 + "','" + parImgtxt + "','" + midHeaderIndexPage + "','" + midParaIndexPage + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "your Data inserted successfully";
                return View();
            }
            catch { ViewBag.Emsg = "your Data didn't insert correctly"; return View(); }
        }
        public ActionResult socialmedia()
        {
            DDLSocialMedia();
            return View();
        }
        [HttpPost]
        public ActionResult socialmedia(SocialMedia sM)
        {
            try
            {
                con.Open();
                com = new SqlCommand("select icon from SocialMedia where icon='"+sM.icon+"'", con);
                SqlDataReader sqldr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sqldr);
                if (dt.Rows.Count <= 0)
                {
                    com = new SqlCommand("insert into  SocialMedia Values ('" + sM.icon + "','" + sM.link + "')", con);
                    com.ExecuteNonQuery();
                }
                else ViewBag.Emsg = "اللينك موجود من قبل";
                con.Close();
                DDLSocialMedia();
                return View();
            }
                catch { ViewBag.Emsg = "Error Ocurred"; return View();
                       }
         }

        
        public PartialViewResult _SocialLinks()
        {
            con.Open();
            com = new SqlCommand("select * from SocialMedia", con);
            SqlDataReader sqldr = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);
            con.Close();
            return PartialView("_SocialLinks", dt);
        }
        public void DDLFun()
        {

            //try
            //{
               
                 con.Open();
                com = new SqlCommand("select * from SocialMedia ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                DataRow newRow = d.NewRow();
                con.Close();
                string[] arr = new string[4] { "facebook", "twitter", "whatsapp", "instagram" };
                List<SocialMedia> List = new List<SocialMedia>();
           
                if (d.Rows.Count > 0)
                {

                    //for (int i = 0; i < d.Rows.Count; i++)
                    //{
                        for (int k = 0; k < arr.Length; k++)
                        {
                       

                        if (!arr[k].Contains(d.Rows[k]["icon"].ToString()))
                        {
                            List.Add(new SocialMedia() { SocialID = k + 1, icon = arr[k] });
                            break;
                        }
                        else { continue; }
                        }


                   // }

                    ViewBag.listIcons = List;
                }
           
                else {
                        for (int i = 0; i < arr.Length; i++) {
                        List.Add(new SocialMedia() { SocialID = i + 1, icon = arr[i] });
                       }
                    ViewBag.listIcons = List;
                }


                
            //}
           // catch {  }

        }

        public void DDLSocialMedia()
        {
            try
            {
                con.Open();
                List<SocialMedia> List = new List<SocialMedia>();
                List.Add(new SocialMedia() { SocialID = 1, icon = "facebook" });
                List.Add(new SocialMedia() { SocialID = 2, icon = "twitter" });
                List.Add(new SocialMedia() { SocialID = 3, icon = "instagram" });
                List.Add(new SocialMedia() { SocialID = 4, icon = "whatsapp" });
                ViewData["listSocial"] = new SelectList(List, "SocialID", "icon");

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
                com = new SqlCommand("insert into  SubCategory Values (N'" + sub.SubCateName + "','" + sub.FKServID + "',N'"+ sub.enSubName + "')", con);
                com.ExecuteNonQuery();
                con.Close();
                DDlSevices();
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
                    List.Add(new Services() { ServicesID = id, Name = name });
                    ViewData["listServ"] = new SelectList(List, "ServicesID", "Name");
                }

                con.Close();
            }

            catch { con.Close(); }
        }

        public PartialViewResult _subcat()
        {
            con.Open();
            com = new SqlCommand("select * from SubCategory", con);
            SqlDataReader sqldr = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);
            con.Close();

            return PartialView("_subcat", dt);
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
                    com = new SqlCommand("insert into  imgNew Values ('" + singlenews.img + "',N'" + singlenews.Headertxt + "','" + singlenews.DateNew + "',N'" + singlenews.Maintxt + "',N'" + singlenews.enHeadertxt + "',N'"+singlenews.enMaintxt+"')", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    return View();
                }
                catch { ViewBag.msg = "your news didn't insert correctly"; return View(); }

            else
            {
                ViewBag.msg = "file not uploaded"; return View();
            }
        }


        public PartialViewResult _lastNew()
        {
            con.Open();
            com = new SqlCommand("select * from ImgNew", con);
            SqlDataReader sqldr = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);
            con.Close();
          
            return PartialView("_lastNew", dt);
        }
        public ActionResult products()
        {
            DDlsub();

            return View();
        }
        [HttpPost]
        public ActionResult products(Products pro)
        {
            HttpPostedFileBase file = Request.Files["img"];
            if (file != null && file.ContentLength > 0)
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    pro.img = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("insert into  Products Values ('" + pro.Price + "',N'" + pro.Text + "','" + pro.img + "'," + pro.FKSubID + ",N'"+pro.enText+"')", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    
                    DDlsub();
                    return View();
                }
                catch { ViewBag.Emsg = "a product didn't insert correctly"; return View(); }

            else
            {
                ViewBag.Emsg = "file not uploaded"; return View();
            }
        }

        public void DDlsub()
        {
            try
            {
                con.Open();
                com = new SqlCommand("select * from SubCategory ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                List<SubCategory> List = new List<SubCategory>();
                for (int t = 0; t < d.Rows.Count; t++)
                {
                    int id = int.Parse(d.Rows[t]["SubCategoryID"].ToString());
                    string name = d.Rows[t]["SubCateName"].ToString();
                    List.Add(new SubCategory() { SubCategoryID = id, SubCateName = name });
                    ViewData["listsub"] = new SelectList(List, "SubCategoryID", "SubCateName");
                }

                con.Close();
            }

            catch { con.Close(); }
        }


        public PartialViewResult _product()
        {
            con.Open();
            com = new SqlCommand("select * from products", con);
            SqlDataReader sqldr = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);
            con.Close();

            return PartialView("_product", dt);
        }

        public ActionResult Services()
        {
            try
            {
                con.Open();
                com = new SqlCommand("select * from SubCategory ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(SqlDr);
                List<SubCategory> List = new List<SubCategory>();
                for (int t = 0; t < d.Rows.Count; t++)
                {
                    int id = int.Parse(d.Rows[t]["SubCategoryID"].ToString());
                    string name = d.Rows[t]["SubCateName"].ToString();
                    List.Add(new SubCategory() { SubCategoryID = id, SubCateName = name });
                    ViewData["listsub"] = new SelectList(List, "ServicesID", "Name");
                }

                con.Close();
            }
            catch { con.Close(); }
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
                        com = new SqlCommand("insert into  Services Values ('" + serv.Name + "','" + serv.textServe + "','" + serv.img + "','" + serv.enName + "','" + serv.entxtServe + "')", con);
                        com.ExecuteNonQuery();
                        con.Close();

                        return View();
                    }
                    catch
                    {
                        ViewBag.Emsg = "your services didn't insert correctly"; return View();
                    }

                else
                {
                    ViewBag.Emsg = "file not uploaded"; return View();
                }
           
        }

        public PartialViewResult _services()
        {
            con.Open();
            SqlCommand com1 = new SqlCommand("select * from Services", con);
            SqlDataReader sqldr = com1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);
            
            con.Close();
           // IEnumerable<DataRow> s = dt.AsEnumerable();
            return PartialView("_services", dt);
        }
        public PartialViewResult _Branches()
        {
            con.Open();
            SqlCommand com1 = new SqlCommand("select * from Branches", con);
            SqlDataReader sqldr = com1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqldr);

            con.Close();
            // IEnumerable<DataRow> s = dt.AsEnumerable();
            return PartialView("_Branches", dt);
        }
        public ActionResult AllBranches()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AllBranches(Branches br, HttpPostedFileBase[] imgsfile)
        {
            BranchDetail bd = new BranchDetail();
            HttpPostedFileBase file = Request.Files["imgfile"];

            //foreach (HttpPostedFileBase f in imgsfile)
            //{
            //    if (f.ContentLength > 0)
            //    {
            //        if (f != null && file.ContentLength > 0)
            //        {
            //            f.SaveAs(Path.Combine(Server.MapPath("/imgs"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
            //        }
            //    }
            //}
            //foreach (HttpPostedFileBase file1 in Request.Files)
            //{
            //    if (file1.ContentLength > 0)
            //    {
            //        file1.SaveAs(Server.MapPath("~/imgs/" + file1.FileName));
            //    }
            //}
            if (file != null && file.ContentLength > 0)
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    br.mainImg = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("insert into  Branches Values ('" + br.phone + "','" + br.Title + "','" + br.mainImg + "','" + br.AddBranche + "','"+br.enTitle+"','"+br.enAddrBranch+"')", con);
                    com.ExecuteNonQuery();
                    int id = getMaxId();
                    SqlCommand com2 = new SqlCommand("insert into BranchDetail values('" + bd.imgSrc + "'," + id + ")", con);
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
        [HttpPost]
        public JsonResult Upload()
        {
            foreach (HttpPostedFileBase file in Request.Files)
            {
                if (file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                }
            }

            return Json(new { result = true });
        }
        public int getMaxId()
        {
            try
            {
                com = new SqlCommand("select top(1)BID from Branches order by BID desc ");
                SqlDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch { con.Close(); return 0; }
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

        //----------------------------------------edit data in table ----------------------
        [HttpGet]
        public ActionResult EditServ(int id)
        {
            try
            {
                con.Open();
                Services j = new Services();
                com = new SqlCommand("select * from Services where ServicesID=" + id + " ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(SqlDr);
                j.ServicesID =int.Parse(dt.Rows[0]["ServicesID"].ToString());
                j.Name = dt.Rows[0]["Name"].ToString();
                j.textServe = dt.Rows[0]["textServe"].ToString();
                j.enName = dt.Rows[0]["enName"].ToString();
                j.entxtServe = dt.Rows[0]["entxtServe"].ToString();
                j.img= dt.Rows[0]["img"].ToString();
                con.Close();
                return View(j);
            }
            catch { ViewBag.Emsg = "Error occured"; return View(); };
            
        }
        [HttpPost]
        public ActionResult EditServ(Services serv)
        {
            
                HttpPostedFileBase file = Request.Files["imgfile"];
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    serv.img = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("update Services set Name=N'" + serv.Name + "',textServe=N'" + serv.textServe + "',img='" + serv.img + "',enName=N'" + serv.enName + "',entxtServe=N'" + serv.entxtServe + "'where ServicesID=" + serv.ServicesID, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Services");
               }

                catch
                {
                    ViewBag.Emsg = "فشل التعديل";
                    return View();
                }
            }
            else { ViewBag.Emsg = "file not uploaded"; return View(); }

            }

        [HttpGet]
        public ActionResult EditimgNew(int id)
        {
            try
            {
                con.Open();
                imgNew j = new imgNew();
                com = new SqlCommand("select * from imgNew where NewID=" + id + " ", con);
                SqlDataReader SqlDr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(SqlDr);
                j.NewID = int.Parse(dt.Rows[0]["NewID"].ToString());
                j.Headertxt = dt.Rows[0]["Headertxt"].ToString();
                j.Maintxt = dt.Rows[0]["Maintxt"].ToString();
                j.enHeadertxt = dt.Rows[0]["enHeadertxt"].ToString();
                j.enMaintxt = dt.Rows[0]["enMaintxt"].ToString();
                j.img = dt.Rows[0]["img"].ToString();
                j.DateNew = Convert.ToDateTime(dt.Rows[0]["DateNew"].ToString());
                con.Close();
                return View(j);
            }
            catch { ViewBag.Emsg = "Error occured"; return View(); };

        }
        [HttpPost]
        public ActionResult EditimgNew(imgNew im)
        {

            HttpPostedFileBase file = Request.Files["imgfile"];
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    file.SaveAs(Server.MapPath("~/imgs/" + file.FileName));
                    im.img = "~/imgs/" + file.FileName;
                    con.Open();
                    com = new SqlCommand("update imgNew set Headertxt=N'" + im.Headertxt + "',Maintxt=N'" + im.Maintxt + "',img='" + im.img + "',DateNew=N'" + im.DateNew + "',enHeadertxt='" + im.enHeadertxt + "', enMaintxt='" + im.enMaintxt+ "' where NewID=" + im.NewID, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("lastNew");
                }

                catch
                {
                    ViewBag.Emsg = "فشل التعديل";
                    return View();
                }
            }
            else { ViewBag.Emsg = "file not uploaded"; return View(); }

        }


    }
}