using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Architecture.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //註冊
        #region
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public virtual JsonResult Registered(string UserID, string Pass, string CName, string Phone, string Tel, string Gender, string Birth)
        {
            Architecture.Web.Service._Service.MemberFun AddUser = new Service._Service.MemberFun();


            string Message = "成功";

            var result = AddUser.Confirmation(UserID, Pass, CName, Phone, Tel, Gender, Birth);
            if (result.ToString() == "")
            {
                AddUser.InsertUser(UserID, Pass, CName, Phone, Tel, Gender, Birth);
            }
            else
            {
                Message = result.ToString();
            }


            return Json(Message);
        }






        #endregion
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}