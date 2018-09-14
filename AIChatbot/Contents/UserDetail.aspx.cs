using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIChatbot.Classes;
using Ls.Prj.Entity;
using Ls.Prj.Utility;
using System.Text;
using Ls.Prj.EFRepository;
using AIChatbot.Base;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
   
    public partial class UserDetail : AICBBasePage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 

            if (!Page.IsPostBack)
            {
                UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();
                PopolaCboRoles(CboRoles, ObjUserDetailPageManager.GetRoles());
                CboRoles.Items.Add(new ListItem("--Select--", "0"));

                //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
               Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");
                LitPathFormScriptValidation.Text = "<script src='../js/UserDetail.js'></script>";


                if (Request.QueryString["Id"] != null)
                {
                    //siamo in modifica
                    User Usr = ObjUserDetailPageManager.GetSelectedUser(Convert.ToInt32(Request.QueryString["Id"]));
                    LitUser.Text = "User " + Usr.Name;
                    ValorizzaForm(Usr);
                }
                else
                {
                    //siamo in inserimento
                    LitUser.Text = "New user...";
                    ResettaForm();
                }
                  
            }

            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        protected void BtnSalva_Click(object sender, EventArgs e)
        {
            try
            {
                AuditPageManager ObjPageManager = new AuditPageManager();
                //qui va validazione... meglio lato client
                if (Request.QueryString["Id"] != null)
                {
                    //modifica
                  User Result=  UpdateUser(Convert.ToInt32(Request.QueryString["Id"]));

                    ObjPageManager.InsertAudit(LoginUsr, "User updated: " + Result.Name );
                    Response.Redirect("Users.aspx");
                }
                else
                {
                    //inserimento
                    //qui controlla che l'abinata userid e pwd non esista già
                    UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();
                    if (!ObjUserDetailPageManager.IsUserAlreadyInDb(TxtUsername.Text, TxtPassword.Text))
                    {
                        User Result = InsertUser();

                        ObjPageManager.InsertAudit(LoginUsr, "User inserted: " + Result.Name);
                        Response.Redirect("Users.aspx");
                    }
                    else
                    {
                        Exception ex = new Exception("Another user with the same userid and password exist in database.");
                        PrintError(ex);
                    }

                 
                }
               
            }
            catch (Exception ex)
            {
                //qui segnalare errore su un pannello del form
                PrintError(ex);
            }
        }

        //protected void BtnBack_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect("Default.aspx");
        //    }
        //    catch (Exception ex)
        //    {
        //        //qui segnalare errore su un pannello del form
        //        PrintError(ex);
        //    }
        //}
        protected void BtnClose_Click(object sender, EventArgs e)
        {
            DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");
            
        }

        #region routine private alla pagina



        private void PopolaCboRoles(DropDownList drop,List<Role> list)
        {
            foreach (Role CurrRole in list)
            {
                var listItem = new ListItem();
                listItem.Value = CurrRole.IdRole.ToString();
                listItem.Text = CurrRole.RoleName;
                drop.Items.Add(listItem);
            }
        }


        private void ResettaForm()
        {
            TxtNomeUtente.Text = "";
            TxtUsername.Text = "";

            Utility.SetDropByValue(CboRoles, "0");
            Utility.SetDropByValue(CboEnable, "1");
            TxtPassword.Text = "";
            //TxtConfirmPassword.Text = "";
        }
        private void ValorizzaForm(User Usr)
        {
            TxtNomeUtente.Text = Usr.Name;
            TxtUsername.Text = Usr.UserId;
            Utility.SetDropByValue(CboRoles, Usr.IdRole.ToString());
            Utility.SetDropByValue(CboEnable, Usr.Enabled.ToString());
            string PwdDecripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Decrypt(Usr.Pwd, Utility.GetPrivSimKey("CriptographyKey"));
            TxtPassword.Text = PwdDecripted;
            // TxtConfirmPassword.Text = Usr.Pwd;
            //TxtUsername.Text = "";
        }
        public User UpdateUser(int Id)
        {
            User result = null;
            using (UserEFRepository UserRep = new UserEFRepository(""))
            {
               
                result = UserRep.Context.Users.SingleOrDefault(x => x.IdUser == Id);
                if (result != null)
                {
                    result.Name = TxtNomeUtente.Text;
                    result.IdRole =Convert.ToInt32(CboRoles.SelectedValue);
                    result.UserId = TxtUsername.Text;
                    string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TxtPassword.Text, Utility.GetPrivSimKey("CriptographyKey"));
                    result.Pwd = PwdEncripted;
                    result.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
                    UserRep.Context.SaveChanges();
                }
            }

            return result;

        }
        public User InsertUser()
        {
            User NewUsr = new User();
            NewUsr.Name = TxtNomeUtente.Text;
            NewUsr.IdRole = Convert.ToInt32(CboRoles.SelectedValue);
            NewUsr.UserId = TxtUsername.Text;
            string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TxtPassword.Text, Utility.GetPrivSimKey("CriptographyKey"));
            NewUsr.Pwd = PwdEncripted;
            NewUsr.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
           
            
            using (UserEFRepository UserRep = new UserEFRepository(""))
            {
                UserRep.Context.Users.Add(NewUsr);
                UserRep.Context.SaveChanges();
            }

            return NewUsr;

        }


        public void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
        #endregion


    }


}