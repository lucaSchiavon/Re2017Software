using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIChatbot.Classes;
using Ls.Prj.DTO;
using Ls.Prj.Entity;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtUsername.Text = "re2017";
            TxtPassword.Text = "re2017";
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {


            LblLoginError.Visible = false;

            LoginPageManager ObjLoginManager = new LoginPageManager();
            bool IsValidUser = ObjLoginManager.ValidateUser(TxtUsername.Text, TxtPassword.Text);
            if (IsValidUser)
            {
                FormsAuthentication.RedirectFromLoginPage
                                   (TxtUsername.Text, false);
            }
            else
            {
                LblLoginError.Visible = true;
            }


        }


    }
}