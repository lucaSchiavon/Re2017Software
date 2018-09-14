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
using Ls.Prj.EFRepository;
using System.Data.Entity;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class Tags2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //registra in fondo alla pagina lo script js per le funzionalità jquery della tabella
                AppendJqeryScriptForTable();

                //carica tabella
                LoadList();
                
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
                Int32 IdToDelete = Convert.ToInt32(HydIdToDelete.Value);
                DeleteEntity(IdToDelete);
                LoadList();
              
                DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
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
         
            using (TagEFRepository TagRep = new TagEFRepository(""))
            {
                var entity = new Tag { IdTag = IdEntity };
                TagRep.Context.Entry(entity).State = EntityState.Deleted;
                TagRep.Context.SaveChanges();
            }



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
            TagPageManager ObjTagPageManager = new TagPageManager();
           
            List<TagDTO> LstTagDto  = ObjTagPageManager.GetTags(); 
            StringBuilder Sb = new StringBuilder();

            foreach (TagDTO CurrTagDto in LstTagDto)
            {
                //Sb.Append("<tr class='odd gradeA'><td>" + CurrUsrDto.Name + "</td><td>" + CurrUsrDto.UserId + "</td><td>" + CurrUsrDto.Role + "</td><td class='center'>" + CurrUsrDto.Enabled + "</td><td class='center'><a class='btn btn-primary' href='UserDetail.aspx?Id=" + CurrUsrDto.IdUser + "'><i class='fa fa-edit'></i> Update</a> <a class='btn btn-danger' href='javascript:ShowDelForm(" + CurrUsrDto.IdUser + ");'><i class='fa fa-times'></i> Delete</a></td></tr>");

                Sb.Append("<tr class='odd gradeA'>");

                Sb.Append("<td>" + CurrTagDto.TagName + "</td>");
                Sb.Append("<td>" + CurrTagDto.Machine + "</td>");
                Sb.Append("<td>" + CurrTagDto.Description + "</td>");
                Sb.Append("<td class='center'>" + CurrTagDto.Enabled + "</td>");

                Sb.Append("<td class='center'>");
                Sb.Append("<a class='btn btn-primary' href='TagDetail.aspx?Id=" + CurrTagDto.IdTag + "'><i class='fa fa-edit'></i> Update</a> ");
                Sb.Append("<a class='btn btn-danger' href='javascript:ShowDelForm(" + CurrTagDto.IdTag + ");'><i class='fa fa-times'></i> Delete</a>");
                Sb.Append("</td>");

                Sb.Append("</tr>");


            }

            LitContentTable.Text = Sb.ToString() ;         
        }

        #endregion

       
    }


}