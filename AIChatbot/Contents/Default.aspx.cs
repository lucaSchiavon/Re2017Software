using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIChatbot.Classes;
using Ls.Prj.Entity;
using System.Text;
using Ls.Prj.DTO;
using System.Data.Entity;
using AIChatbot.Base;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class Default : AICBBasePage //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ////registra in fondo alla pagina lo script js per le funzionalità jquery della tabella
                //AppendJqeryScriptForTable();

                ////carica tabella
                //LoadList();
                
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }

        #region Gestione lightbox
      
        protected void BtnCancelDeleting_Click(object sender, EventArgs e)
        {
            DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
        }

        protected void BtnConfirmDeleting_Click(object sender, EventArgs e)
        {
            try
            {
                //Int32 IdToDelete = Convert.ToInt32(HydIdToDelete.Value);

                //UserDetailPageManager ObjUserDetailPageManager = new UserDetailPageManager();
             
                //if (IdToDelete!=LoginUsr.IdUser)
                //{
                //    DeleteEntity(IdToDelete);
                //    LoadList();

                //    DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
                //}
                //else
                //{
                //    Exception ex = new Exception("Current user logged can't delete himself.");
                //    PrintError(ex);
                //}
               

            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");
            //DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
        }

        #endregion


        #region routine private alla pagina
        public void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
        public void DeleteEntity(Int32 IdEntity)
        {
         
            //using (UserEFRepository UserRep = new UserEFRepository(""))
            //{
            //    var entity = new User { IdUser = IdEntity };
            //    UserRep.Context.Entry(entity).State = EntityState.Deleted;
            //    UserRep.Context.SaveChanges();
            //}



        }
        private void AppendJqeryScriptForTable()
        {
            //registra in fondo alla pagina lo script js per le funzionalità jquery della tabella
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$(document).ready(function() { $('#dataTables-example').DataTable({responsive: true});});");
            sb.Append(@"</script>");

            if (!ClientScript.IsClientScriptBlockRegistered("JSScriptBlock"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "JSScriptBlock",
                sb.ToString());
            }

        }
        private void LoadList()
        {
            //UserPageManager ObjUserPageManager = new UserPageManager();
           
            //List<UserDTO> LstUsrDto  = ObjUserPageManager.GetUsers(); 
            //StringBuilder Sb = new StringBuilder();

            //foreach (UserDTO CurrUsrDto in LstUsrDto)
            //{
            //    //Sb.Append("<tr class='odd gradeA'><td>" + CurrUsrDto.Name + "</td><td>" + CurrUsrDto.UserId + "</td><td>" + CurrUsrDto.Role + "</td><td class='center'>" + CurrUsrDto.Enabled + "</td><td class='center'><a class='btn btn-primary' href='UserDetail.aspx?Id=" + CurrUsrDto.IdUser + "'><i class='fa fa-edit'></i> Update</a> <a class='btn btn-danger' href='javascript:ShowDelForm(" + CurrUsrDto.IdUser + ");'><i class='fa fa-times'></i> Delete</a></td></tr>");

            //    Sb.Append("<tr class='odd gradeA'>");

            //    Sb.Append("<td>" + CurrUsrDto.Name + "</td>");
            //    Sb.Append("<td>" + CurrUsrDto.UserId + "</td>");
            //    Sb.Append("<td>" + CurrUsrDto.Role + "</td>");
            //    Sb.Append("<td class='center'>" + CurrUsrDto.Enabled + "</td>");

            //    Sb.Append("<td class='center'>");
            //    Sb.Append("<a class='btn btn-primary' href='UserDetail.aspx?Id=" + CurrUsrDto.IdUser + "'><i class='fa fa-edit'></i> Update</a> ");
            //    Sb.Append("<a class='btn btn-danger' href='javascript:ShowDelForm(" + CurrUsrDto.IdUser + ");'><i class='fa fa-times'></i> Delete</a>");
            //    Sb.Append("</td>");

            //    Sb.Append("</tr>");


            //}

            //LitContentTable.Text = Sb.ToString() ;         
        }

        #endregion

       
    }


}