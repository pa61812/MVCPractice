using Architecture.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Web.Service._Service
{
    public class MemberFun
    {
        PracticeEntities practiceEntities = new PracticeEntities();
        //新增USER
        public bool InsertUser(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
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
                
                using (var dbContext = practiceEntities)
                {
                    dbContext.Members.Add(toDb);
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                      
                return false;
            }


        }


        //USER資料檢查
        public string Confirmation(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            string Message = "";
 
            var IsExisits = practiceEntities.Members.Where(x => x.Account == userID).Count();

            if (userID == "" || userID == null )
            {

                Message = "帳號有誤";
                return (Message);
            }
            if ( IsExisits >= 1)
            {

                Message = "用戶已註冊";
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



        //找單一USER
        public object SearchUser(string userID)
        {
 
            var User = practiceEntities.Members.Where(x => x.Account.Contains(userID))
                                                                  .Select (x=>new {
                                                                      Account=x.Account,
                                                                      Password=x.Password,
                                                                      Name=x.Name,
                                                                      Phone=x.Phone,
                                                                      Tel=x.Tel,
                                                                      Gender =  x.Gender,
                                                                      Birthday= SqlFunctions.DateName("year", x.Birthday) + "/" + x.Birthday.Value.Month + "/" + SqlFunctions.DateName("day", x.Birthday)
                                                                  })                                                                  
                                                                  .ToList();

            return (User);

        }
        //找所有USER
        public object AllUser()
        {

            var User = practiceEntities.Members.Select(x => new {
                                                                      Account = x.Account,
                                                                      Password = x.Password,
                                                                      Name = x.Name,
                                                                      Phone = x.Phone,
                                                                      Tel = x.Tel,
                                                                      Gender = x.Gender,
                                                                      Birthday = SqlFunctions.DateName("year", x.Birthday) + "/" + x.Birthday.Value.Month + "/" + SqlFunctions.DateName("day", x.Birthday)
                                                                     // ,Idx = index + 1
            }) .ToList();
            var result = User.AsEnumerable()
                                     .Select((x, index) => new
                                     {
                                         Account = x.Account,
                                         Password = x.Password,
                                         Name = x.Name,
                                         Phone = x.Phone,
                                         Tel = x.Tel,
                                         Gender = x.Gender,
                                         Birthday = x.Birthday,
                                         Idx = index + 1
                                     }).ToList();
            return (User);

        }

        //刪除USER
        public bool DeleteUser(string userID)
        {
            try
            {
                var DeMember = practiceEntities.Members.Where(x => x.Account == userID).FirstOrDefault();


                using (var dbContext = practiceEntities)
                {
                    dbContext.Members.Remove(DeMember);
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                return false;
            }


        }

        public bool UpdateUser(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            try
            {
                // Query the database for the row to be updated.
                var query =
                    (from mem in practiceEntities.Members
                    where mem.Account == userID
                    select mem).ToList();

                // Execute the query, and change the column values
                // you want to change.
                foreach (Member mem in query)
                {
                    mem.Password = pass;
                    mem.Name = cName;
                    mem.Phone = phone;
                    mem.Tel = tel;
                    mem.Gender = gender;
                    mem.Birthday = DateTime.Parse(birth);
                }

       
                practiceEntities.SaveChanges();
    
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                return false;
            }


        }

    }
}
