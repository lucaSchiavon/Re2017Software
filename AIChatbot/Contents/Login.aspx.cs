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
using Newtonsoft.Json;
using Re2017.Classes;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TxtUsername.Text = "re2017";
            //TxtPassword.Text = "re2017";
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {


            LblLoginError.Visible = false;
            LoginPageManager ObjLoginPageManager = new LoginPageManager();
            LoginCredentialsDto ObjLoginCredentialsDto = new LoginCredentialsDto();
            ObjLoginCredentialsDto.email = TxtUsername.Text;
            ObjLoginCredentialsDto.password = TxtPassword.Text;
          var result=  ObjLoginPageManager.LoginUser(ObjLoginCredentialsDto);


            string RisultatoBoby = result.Content.ReadAsStringAsync().Result;
         
            if (result.IsSuccessStatusCode)
            {
                Utente ObjUtente = JsonConvert.DeserializeObject<Utente>(RisultatoBoby);
                Response.Cookies["IdUser"].Value = ObjUtente.id.ToString();

                FormsAuthentication.RedirectFromLoginPage
                                  (ObjUtente.email, false);
            }
            else
            {
                ErroreDTO ObjErroreDTO = JsonConvert.DeserializeObject<ErroreDTO>(RisultatoBoby);
                LblLoginError.Text = ObjErroreDTO.errorMessage;
                LblLoginError.Visible = true;
            }
            //bool IsValidUser = ObjLoginManager.ValidateUser(TxtUsername.Text, TxtPassword.Text);
            //bool IsValidUser = true;
            //if (IsValidUser)
            //{
            //    FormsAuthentication.RedirectFromLoginPage
            //                       (TxtUsername.Text, false);
            //}
            //else
            //{
            //    LblLoginError.Visible = true;
            //}


        }


    }
}