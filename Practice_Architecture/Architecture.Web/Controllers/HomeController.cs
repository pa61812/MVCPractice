using Architecture.Common;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Architecture.Web.Controllers
{
    public class HomeController : Controller
    {

        Architecture.Web.Service._Service.MemberFun memberfun = new Service._Service.MemberFun();
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


            string Message = "成功";

            var result = memberfun.Confirmation(UserID, Pass, CName, Phone, Tel, Gender, Birth);
            if (result.ToString() == "")
            {
               var InsertResult= memberfun.InsertUser(UserID, Pass, CName, Phone, Tel, Gender, Birth);
                if (InsertResult !=true)
                {
                    Message = "新增失敗";
                }
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
            PracticeEntities practiceEntities = new PracticeEntities();
            var result =practiceEntities.Members.ToList();           
            return View(result);
        }

        public JsonResult UserSerach (string key) 
        {
            var result = memberfun.SearchUser(key);

            return Json(result);
        }
        public ActionResult PartialAllUser()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Delete(string key)
        {
            var result = memberfun.DeleteUser(key) ;
            
            return RedirectToAction("Contact", "Home", new { Area = "" });
        }

        [HttpGet]
        public ActionResult Edit(string key)
        {

            PracticeEntities practiceEntities = new PracticeEntities();
            var result = practiceEntities.Members.Where(x=>x.Account==key).ToList();           
            return View(result);
        }

        public virtual JsonResult EditMember(string UserID, string Pass, string CName, string Phone, string Tel, string Gender, string Birth)
        {


            string Message = "成功";

            var result = memberfun.Confirmation("pass", Pass, CName, Phone, Tel, Gender, Birth);
            if (result.ToString() == "")
            {  
                var InsertResult = memberfun.UpdateUser(UserID, Pass, CName, Phone, Tel, Gender, Birth);
                if (InsertResult != true)
                {
                    Message = "修改失敗";
                }
            }
            else
            {
                Message = result.ToString();
            }


            return Json(Message);
        }


        [HttpGet]
        public JsonResult AllUser()
        {

            var result = memberfun.AllUser();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}