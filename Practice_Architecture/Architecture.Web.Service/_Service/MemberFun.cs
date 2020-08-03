using Architecture.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Web.Service._Service
{
    public class MemberFun
    {
        //新增USER
        public object InsertUser(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            try
            {
                var toDb = new Member
                {
                    Account = userID,
                    Password = pass,
                    Name = cName,
                    Phone = phone,
                    Tel = tel,
                    Gender = gender,
                    Birthday = DateTime.Parse(birth)
                };
                PracticeEntities practiceEntities = new PracticeEntities();
                using (var dbContext = practiceEntities)
                {
                    //dbContext.Members.Add(toDb);
                    //dbContext.SaveChanges();
                    var userSuppliedId = new SqlParameter("@PostId", PostID);
                    string sqlQuery = @"select c.Name CategoryTitle, pcm.Id MappingId, p.Title PostTitle from Posts_Categories pcm 
                                join Categories c on pcm.CategoryId = c.Id
                                join Posts p on pcm.PostId = p.Id where pcm.PostId =@PostId";
                }
                return true;
            }
            catch (Exception)
            {
               // return false;
                throw;
            }


        }


        //USER資料檢查
        public object Confirmation(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            string Message = "";
            if (userID == "" || userID == null)
            {

                Message = "帳號有誤";
                return (Message);
            }
            if (pass == "" || pass == null)
            {

                Message = "密碼有誤";
                return (Message);
            }
            if (cName == "" || cName == null)
            {

                Message = "姓名有誤";
                return (Message);
            }
            if (phone == "" || phone == null)
            {

                Message = "電話有誤";
                return (Message);
            }
            //if (tel == "" || tel == null)
            //{

            //    Message = "電話有誤";
            //    return (Message);
            //}
            if (gender.ToUpper() != "M" && gender.ToUpper() != "F")
            {
                Message = "性別有誤";
                return (Message);
            }
            if (ValidateDate(birth) != true)
            {
                Message = "日期有誤";
                return (Message);
            }


            return (Message);
        }

        //日期檢查
        private bool ValidateDate(string stringDateValue)

        {
            try

            {
                CultureInfo CultureInfoDateCulture = new CultureInfo("zh-TW"); //日期文化格式

                DateTime d = DateTime.ParseExact(

                 stringDateValue, "yyyy/MM/dd", CultureInfoDateCulture);

                return true;

            }

            catch { return false; }

        }

    }
}
