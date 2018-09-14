using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using System.Data.SqlClient;
using Ls.Prj.Utility;

namespace AIChatbot.Classes
{
    public class LoginPageManager
    {
        public bool ValidateUser(string UserName,string Password)
        {

            //string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(Password, Utility.GetPrivSimKey("CriptographyKey"));
            //using (UserEFRepository UsrRep = new UserEFRepository(UserName))
            //{


            //    User Usr = UsrRep.Context.Users
            //             .Where(s => s.UserId == UserName && s.Pwd == PwdEncripted)
            //             .FirstOrDefault<User>();

            //    if (Usr != null)
            //    {
            //        HttpContext.Current.Response.Cookies["IdUser"].Value = Usr.IdUser.ToString();
            //    }


            //    if (Usr != null)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}


            return true;
           
        }

    }
}