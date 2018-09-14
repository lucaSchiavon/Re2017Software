using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;

namespace AIChatbot.Classes
{
    public class AICBasePageManager
    {

        public User GetLoginUsr()
        {
            //UserEFRepository ObjUserEFRepository = new UserEFRepository("");
            //Int32 idUser = Convert.ToInt32(HttpContext.Current.Request.Cookies["IdUser"].Value);
            //List<User> Usr = ObjUserEFRepository.Context.Users.Where(s => s.IdUser == idUser).ToList();
            //return Usr.FirstOrDefault();

            return new User { Name="Mario rossi" };
        }

       

    }
}