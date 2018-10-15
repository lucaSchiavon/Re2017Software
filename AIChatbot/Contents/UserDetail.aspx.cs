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
using Re2017.Base;
using Re2017.Classes;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
   
    public partial class UserDetail : Re2017BasePage
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
                    Utente Usr = ObjUserDetailPageManager.GetUtente(Convert.ToInt32(Request.QueryString["Id"]));
                    LitUser.Text = "User " + Usr.lastName + " " + Usr.firstName;
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
            
                //qui va validazione... meglio lato client
                if (Request.QueryString["Id"] != null)
                {
                    //modifica
                  Utente Result=  UpdateUser(Convert.ToInt32(Request.QueryString["Id"]));

                   
                    Response.Redirect("Users.aspx");
                }
                else
                {
                    //inserimento
                    //qui controlla che l'abinata userid e pwd non esista già
                    UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();
                    //if (!ObjUserDetailPageManager.IsUserAlreadyInDb(TxtUsername.Text, TxtPassword.Text))
                    //{
                        Utente Result = InsertUser();

                     
                        Response.Redirect("Users.aspx");
                    //}
                    //else
                    //{
                    //    Exception ex = new Exception("Another user with the same userid and password exist in database.");
                    //    PrintError(ex);
                    //}

                 
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



        private void PopolaCboRoles(DropDownList drop,List<string> list)
        {
            foreach (string CurrRole in list)
            {
                var listItem = new ListItem();
                listItem.Value = CurrRole.ToString();
                listItem.Text = CurrRole.ToString();
                drop.Items.Add(listItem);
            }
        }


        private void ResettaForm()
        {
            TxtNomeUtente.Text = "";
            TxtCognomeUtente.Text = "";
            TxtUsername.Text = "";
            Utility.SetDropByValue(CboRoles, "0");
            Utility.SetDropByValue(CboEnable, "1");
            TxtPassword.Text = "";
            //TxtConfirmPassword.Text = "";
        }
        private void ValorizzaForm(Utente Usr)
        {
            TxtNomeUtente.Text = Usr.firstName;
            TxtCognomeUtente.Text = Usr.lastName;
            TxtUsername.Text = Usr.email;

            //qui i ruoli potrebbero essere più di uno... si vedrà...
            Utility.SetDropByValue(CboRoles, Usr.roles[0].ToString());
            Utility.SetDropByValue(CboEnable, Usr.active.ToString());
            //string PwdDecripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Decrypt(Usr.Pwd, Utility.GetPrivSimKey("CriptographyKey"));
            TxtPassword.Text = Usr.password;

        }
        public Utente UpdateUser(int Id)
        {
            Utente result = null;

            UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();

            result = ObjUserDetailPageManager.GetUtente(Id);

                if (result != null)
                {
                    result.firstName = TxtNomeUtente.Text;
                    result.lastName = TxtCognomeUtente.Text;
                result.roles = new string[] { CboRoles.SelectedValue.ToString() };
                result.email = TxtUsername.Text;
                   // string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TxtPassword.Text, Utility.GetPrivSimKey("CriptographyKey"));
                    result.password = TxtPassword.Text;
                    result.active = Convert.ToBoolean(CboEnable.SelectedValue);
                  
                }

            ObjUserDetailPageManager.UpdateUtente(result);
            return result;

        }
        public Utente InsertUser()
        {
            UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();
            Utente NewUsr = new Utente();
            NewUsr.firstName = TxtNomeUtente.Text;
            NewUsr.lastName = TxtCognomeUtente.Text;
            NewUsr.roles =new string[] { CboRoles.SelectedValue } ;
            NewUsr.email = TxtUsername.Text;
           // string PwdEncripted = Ls.Prj.Utility.SG.Algoritm.Cipher.Encrypt(TxtPassword.Text, Utility.GetPrivSimKey("CriptographyKey"));
            NewUsr.password = TxtPassword.Text;
            NewUsr.active = Convert.ToBoolean(CboEnable.SelectedValue);

            //inserisce nuovo utente
            //................
            ObjUserDetailPageManager.NewUtente(NewUsr);
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