using Ls.Prj.EFRepository;
using Ls.Prj.Entity;
using Ls.Prj.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIChatbot
{
    public partial class TestCeriptoaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCripta_Click(object sender, EventArgs e)
        {
           //Label1.Text= Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TextBox1.Text, Utility.GetPrivSimKey("CriptographyKey"));
            InsertUser();
        }

        protected void BtnDecripta_Click(object sender, EventArgs e)
        {
            string PwdDecripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Decrypt(Label1.Text, Utility.GetPrivSimKey("CriptographyKey"));
            Label2.Text = PwdDecripted;
            // int a = 1014;
            //User Usr = GetSelectedUser(a);
            // Label2.Text = ValorizzaForm(Usr);
        }

        public User InsertUser()
        {
            User NewUsr = new User();
            NewUsr.Name ="test999";
            NewUsr.IdRole = 1;
            NewUsr.UserId = "test999";
            string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TextBox1.Text, Utility.GetPrivSimKey("CriptographyKey"));
            NewUsr.Pwd = PwdEncripted;
            NewUsr.Enabled = true;


            using (UserEFRepository UserRep = new UserEFRepository(""))
            {
                UserRep.Context.Users.Add(NewUsr);
                UserRep.Context.SaveChanges();
            }

            return NewUsr;

        }
        private string ValorizzaForm(User Usr)
        {
         
            string PwdDecripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Decrypt(Usr.Pwd, Utility.GetPrivSimKey("CriptographyKey"));
            return PwdDecripted;
        }
        public User GetSelectedUser(int IdUser)
        {
            User Usr = null;

            using (UserEFRepository UsrRep = new UserEFRepository(""))
            {
                Usr = UsrRep.Context.Users.Where(x => x.IdUser == IdUser).FirstOrDefault();

            }

            return Usr;
        }
    }
}