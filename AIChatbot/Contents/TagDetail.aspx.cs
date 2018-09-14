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
   
    public partial class TagDetail : AICBBasePage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 

            if (!Page.IsPostBack)
            {
                TagDetailPageManager ObjTagDetailPageManager = new TagDetailPageManager();

                    PopolaCboTagValue(CboTagValue, ObjTagDetailPageManager.GetTagValues());
                    CboTagValue.Items.Add(new ListItem("--Select--", "0"));
                    Utility.SetDropByValue(CboTagValue, "0");

                    //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
                    Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");
                LitPathFormScriptValidation.Text = "<script src='../js/TagDetail.js'></script>";


                if (Request.QueryString["Id"] != null)
                {
                    //siamo in modifica
                    Tag Tag = ObjTagDetailPageManager.GetSelectedTag(Convert.ToInt32(Request.QueryString["Id"]));
                        LitEntity.Text = "Tag " + Tag.TagName;
                    ValorizzaForm(Tag);
                }
                else
                {
                        //siamo in inserimento
                        LitEntity.Text = "New tag...";
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
                   Tag Result= UpdateTag(Convert.ToInt32(Request.QueryString["Id"]));

                    ObjPageManager.InsertAudit(LoginUsr, "Tag updated: " + Result.TagName + ", Machine: " + Result.Machine);
                }
                else
                {
                    Tag Result = InsertTag();
                   
                    ObjPageManager.InsertAudit(LoginUsr, "Tag inserted: " + Result.TagName + ", Machine: " + Result.Machine);
                    //inserimento
                }
                Response.Redirect("Tags.aspx");
            }
            catch (Exception ex)
            {
                //qui segnalare errore su un pannello del form
                PrintError(ex);
            }
        }

       
        protected void BtnClose_Click(object sender, EventArgs e)
        {
            DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");
            
        }

        #region routine private alla pagina

        private void PopolaCboTagValue(DropDownList drop, List<TagValue> list)
        {
            foreach (TagValue Curr in list)
            {
                var listItem = new ListItem();
                listItem.Value = Curr.IdTagValue.ToString();
                listItem.Text = Curr.TValue;
                drop.Items.Add(listItem);
            }
        }

        private void ResettaForm()
        {
           
            TxtNomeTag.Text = "";
            TxtMachine.Text = "";
            Utility.SetDropByValue(CboEnable, "1");

            TxtDescription.Text = "";
        }
        private void ValorizzaForm(Tag Tag)
        {
            TxtNomeTag.Text = Tag.TagName;
            TxtMachine.Text = Tag.Machine;
            Utility.SetDropByValue(CboEnable, Tag.Enabled.ToString());
           
            TxtDescription.Text = Tag.Description;

            TxtNode.Text= Tag.Node;
            TxtDevice.Text = Tag.Device;
            TxtValueType.Text = Tag.ValueType;
            Utility.SetDropByValue(CboTagValue, Tag.IdTagValue.ToString());
            Utility.SetDropByValue(CboAlarm, Tag.Alarm.ToString());
           

        }
        public Tag UpdateTag(int Id)
        {
            Tag result = null;
            using (TagEFRepository TagRep = new TagEFRepository(""))
            {
                 result = TagRep.Context.Tags.SingleOrDefault(x => x.IdTag == Id);
                if (result != null)
                {
                    result.TagName = TxtNomeTag.Text;
                    result.Machine = TxtMachine.Text;
                    result.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
                    result.Description = TxtDescription.Text;
                    result.Node=TxtNode.Text;
                    result.Device = TxtDevice.Text;
                    result.ValueType = TxtValueType.Text;
                    result.IdTagValue =Convert.ToInt32(CboTagValue.SelectedValue);
                    result.Alarm = Convert.ToInt32(CboAlarm.SelectedValue);

                    TagRep.Context.SaveChanges();
                }
            }
            return result;


        }
        public Tag InsertTag()
        {
            Tag NewTag = new Tag();
     

            NewTag.TagName = TxtNomeTag.Text;
            NewTag.Machine = TxtMachine.Text;
            NewTag.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
            NewTag.Description = TxtDescription.Text;
            NewTag.Node = TxtNode.Text;
            NewTag.Device = TxtDevice.Text;
            NewTag.ValueType = TxtValueType.Text;
            NewTag.IdTagValue = Convert.ToInt32(CboTagValue.SelectedValue);
            NewTag.Alarm = Convert.ToInt32(CboAlarm.SelectedValue);

            using (TagEFRepository TagRep = new TagEFRepository(""))
            {
                TagRep.Context.Tags.Add(NewTag);
                TagRep.Context.SaveChanges();
            }

            return NewTag;

        }


        public void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
        #endregion


    }


}