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
using Prj.Utility.Classes;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
   
    public partial class ImageDetail : AICBBasePage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 

            if (!Page.IsPostBack)
            {
                ImageDetailPageManager ObjImageDetailPageManager = new ImageDetailPageManager();

                
                    //PopolaCboTypology(CboTypology, ObjDocumentDetailPageManager.GetTypes());
                    //CboTypology.Items.Add(new ListItem("--Select--", "0"));

                    #region script

                    //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
                    Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");
                    LitPathFormScriptValidation.Text = "<script src='../js/ImageDetail.js'></script>";

                    //appende gli script alla pagina per far funzionare la dropzone area
                    Literal LitDropZoneJs = (Literal)Master.FindControl("LitDropZoneJs");
                    LitDropZoneJs.Text = GetDropZoneScript();

                    #endregion



                    if (Request.QueryString["Id"] != null)
                {
                    //siamo in modifica
                  Ls.Prj.Entity.Image Img = ObjImageDetailPageManager.GetSelectedImage(Convert.ToInt32(Request.QueryString["Id"]));
                        LitEntity.Text = "Image " + Img.ImageName;
                    ValorizzaForm(Img);
                        //nascondo possibilità di fare upload
                        PhdDropZone.Visible = false;   
                        PhdLnkPdfDoc.Visible = true;
                        ImgPreview.ImageUrl = "/Public/Photos/" + Img.ImageName + "?w=200&mode=max";
                        //  HypLnkPdfDoc.Text = Img.ImageName;
                        //HypLnkPdfDoc.NavigateUrl = "/Public/Photos/" + Img.ImageName;

                    }
                else
                {
                        //siamo in inserimento
                        LitEntity.Text = "New image...";
                    ResettaForm();
                        PhdDropZone.Visible = true;
                        PhdLnkPdfDoc.Visible = false;
                     
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
                 Ls.Prj.Entity.Image Result= UpdateImg(Convert.ToInt32(Request.QueryString["Id"]));
                   

                    ObjPageManager.InsertAudit(LoginUsr, "Tag updated: " + Result.ImageName);
                }
                else
                {
                    //inserimento

                    Ls.Prj.Entity.Image Result = InsertImg();
                    ObjPageManager.InsertAudit(LoginUsr, "Tag inserted: " + Result.ImageName);
                    //salva il documento definitivamente
                    string OrigFullPath = HttpContext.Current.Server.MapPath("~/Public/Photos/Temp/");
                        string DestFullPath = HttpContext.Current.Server.MapPath("~/Public/Photos/");
                    System.IO.FileInfo Fi = new System.IO.FileInfo(OrigFullPath + HidTempFileNamePlusExt.Value);
                   
                    string Name = Result.IdImage + "_" + TxtNomeImmagine.Text.Replace(" ", "_") + Fi.Extension;
                    try
                    {
                      

                        System.IO.File.Move(OrigFullPath + HidTempFileNamePlusExt.Value, DestFullPath + Name);
                        try
                        {
                            Ls.Prj.Entity.Image Result2 = UpdateFileNameOfImg(Result.IdImage, Name);
    
                        }
                        catch (Exception ex)
                        {
                            PrintError(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No image, please upload it.");
                    }
                        
                   

                }
                Response.Redirect("Images.aspx");
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


        //protected void RepCapters_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Salva")
        //    {
        //        try
        //        {
        //            TextBox TxtArg = (TextBox)e.Item.FindControl("TxtArg");
        //            TextBox TxtDev = (TextBox)e.Item.FindControl("TxtDev");
        //            Literal LitOk = (Literal)e.Item.FindControl("LitOk");
        //            LitOk.Visible = true;

        //            Chapter ModChap=  UpdateChapter(Convert.ToInt32(e.CommandArgument), TxtArg.Text, TxtDev.Text);
                   
        //            AuditPageManager ObjPageManager = new AuditPageManager();
        //            ObjPageManager.InsertAudit(LoginUsr, "Chapter of document " + ModChap.Document.DocName + " updated: '" + ModChap.ChapterName + "'");
        //        }
        //        catch (Exception ex)
        //        {
        //            PrintError(ex);
        //        }
        //    }
            
        //}

        #region routine private alla pagina

        private string GetDropZoneScript()
        {
            StringBuilder Sb = new StringBuilder();

            Sb.Append("<script type='text/javascript'>");
            Sb.Append("$(document).ready(function () {");
            Sb.Append("Dropzone.autoDiscover = false;");
            Sb.Append("var guid = CreateGuid();");
            Sb.Append("var filename = guid;");
            Sb.Append("$('#ContentPlaceHolder1_HidTempFileName').val(filename);");
            Sb.Append("$('#dZUpload').dropzone({");
            Sb.Append("url: '../UploaderImage.ashx?filename=' + filename.toString(),");
            Sb.Append("maxFiles: 1,");
            Sb.Append("acceptedFiles: '.jpg,.png,.gif',");
            Sb.Append("addRemoveLinks: true,");
            Sb.Append("success: function (file, response) {");
            Sb.Append("var imgName = response;");
            Sb.Append("$('#ContentPlaceHolder1_HidTempFileNamePlusExt').val(imgName);");
            Sb.Append("file.previewElement.classList.add('dz-success');");
            Sb.Append("},");
            Sb.Append("error: function (file, response) {");
            Sb.Append("file.previewElement.classList.add('dz-error');");
            Sb.Append("}");
            Sb.Append("});");
            Sb.Append("});");
            Sb.Append("</script>");
            return Sb.ToString();
        }
        //private List<Chapter> GetChapters(int IdDoc)
        //{
        //    using (ChapterEFRepository Rep = new ChapterEFRepository(""))
        //    {
        //        List<Chapter> LstChapters = Rep.Context.Chapters.Where(x => x.IdDocument == IdDoc).ToList();
              

        //        return LstChapters;
        //    }

        //}

        //public Chapter UpdateChapter(int Id,string Arg,string Dev)
        //{
        //    Chapter result = null;
        //    Document Doc=null;
        //    using (ChapterEFRepository Rep = new ChapterEFRepository(""))
        //    {
        //        result = Rep.Context.Chapters.SingleOrDefault(x => x.IdChapter == Id);
        //        if (result != null)
        //        {
        //            result.Argument = Arg;
        //            result.Device = Dev;
        //            Doc = result.Document;
        //            Rep.Context.SaveChanges();
        //        }
        //    }
        //    result.Document = Doc;
        //    return result;


        //}
        //private void InsertChapter(Chapter NewChapter)
        //{
            
        //        using (ChapterEFRepository Rep = new ChapterEFRepository(""))
        //        {
                
        //            Rep.Context.Chapters.Add(NewChapter);
        //            Rep.Context.SaveChanges();
        //        }
        //}
        //private void PopolaCboTypology(DropDownList drop, List<Ls.Prj.Entity.Type> list)
        //{
        //    foreach (Ls.Prj.Entity.Type Curr in list)
        //    {
        //        var listItem = new ListItem();
        //        listItem.Value = Curr.IdTypology.ToString();
        //        listItem.Text = Curr.Typology;
        //        drop.Items.Add(listItem);
        //    }
        //}

        private void ResettaForm()
        {
            
            TxtNomeImmagine.Text = "";
            TxtArgument.Text = "";
            TxtDevice.Text = "";
            TxtAlias.Text = "";
            TxtTags.Text = "";
            TxtDescription.Text = "";
            Utility.SetDropByValue(CboEnable, "1");

        }
        private void ValorizzaForm(Ls.Prj.Entity.Image Img)
        {
            TxtNomeImmagine.Text = Img.ImageName;
            TxtArgument.Text = Img.Argument;
            TxtDevice.Text = Img.Device;
            TxtAlias.Text = Img.Alias;
            TxtTags.Text = Img.Tags;
            TxtDescription.Text = Img.Description;          
            Utility.SetDropByValue(CboEnable, Img.Enabled.ToString());  
        }
        public Ls.Prj.Entity.Image UpdateImg(int Id)
        {
            Ls.Prj.Entity.Image result = null;
            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                 result = Rep.Context.Images.SingleOrDefault(x => x.IdImage == Id);
                if (result != null)
                {

                    result.ImageName = TxtNomeImmagine.Text;
                    result.Argument = TxtArgument.Text;
                    result.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
                    result.Device = TxtDevice.Text;
                    result.Alias = TxtAlias.Text;
                    result.Tags = TxtTags.Text;
                    result.Description = TxtDescription.Text; 
                    Rep.Context.SaveChanges();
                }
            }
            return result;


        }
        public Ls.Prj.Entity.Image UpdateFileNameOfImg(int Id, string Name)
        {
            Ls.Prj.Entity.Image result = null;
            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                result = Rep.Context.Images.SingleOrDefault(x => x.IdImage == Id);
                if (result != null)
                {
                    result.ImageName = Name;
                    Rep.Context.SaveChanges();
                }
            }
            return result;


        }
        public Ls.Prj.Entity.Image InsertImg()
        {
            Ls.Prj.Entity.Image NewImage = new Ls.Prj.Entity.Image();
            
            NewImage.ImageName = TxtNomeImmagine.Text;
            NewImage.Argument = TxtArgument.Text;
            NewImage.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
            NewImage.Device = TxtDevice.Text;
            NewImage.Alias = TxtAlias.Text;
            NewImage.Tags = TxtTags.Text;
            NewImage.Description = TxtDescription.Text;


            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                Rep.Context.Images.Add(NewImage);
                Rep.Context.SaveChanges();
            }

            return NewImage;

        }


        public void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
        #endregion


    }


}