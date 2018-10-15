using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using Ls.Prj.EFRepository;
using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;
using Ls.Prj.Utility;

namespace AIChatbot.Classes
{
    public class UserDetailPageManager
    {

        public User GetSelectedUser(int IdUser)
        {
            User Usr = null;

            using (UserEFRepository UsrRep = new UserEFRepository(""))
            {
                Usr = UsrRep.Context.Users.Where(x => x.IdUser == IdUser).FirstOrDefault();

            }

            return Usr;
        }
        public bool IsUserAlreadyInDb(string UserId,string Password)
        {
            User Usr = null;
            string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(Password, Utility.GetPrivSimKey("CriptographyKey"));
            using (UserEFRepository UsrRep = new UserEFRepository(""))
            {
                Usr = UsrRep.Context.Users.Where(x => x.UserId == UserId && x.Pwd== PwdEncripted).FirstOrDefault();

            }
            if (Usr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public List<Role> GetRoles()
        {
            List<Role> LstRole = new List<Role>();
            using (RoleEFRepository RoleRep = new RoleEFRepository(""))
            {
                LstRole = RoleRep.Context.Roles.ToList();

            }

            return LstRole;
        }

      
    }
}