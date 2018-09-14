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
   
    public partial class DocumentDetail : AICBBasePage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 

            if (!Page.IsPostBack)
            {
                DocumentDetailPageManager ObjDocumentDetailPageManager = new DocumentDetailPageManager();

                
                    PopolaCboTypology(CboTypology, ObjDocumentDetailPageManager.GetTypes());
                    CboTypology.Items.Add(new ListItem("--Select--", "0"));

                    #region script

                    //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
                    Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");
                    LitPathFormScriptValidation.Text = "<script src='../js/DocumentDetail.js'></script>";

                    //appende gli script alla pagina per far funzionare la dropzone area
                    Literal LitDropZoneJs = (Literal)Master.FindControl("LitDropZoneJs");
                    LitDropZoneJs.Text = GetDropZoneScript();

                    #endregion



                    if (Request.QueryString["Id"] != null)
                {
                    //siamo in modifica
                    Document Doc = ObjDocumentDetailPageManager.GetSelectedDocument(Convert.ToInt32(Request.QueryString["Id"]));
                        LitEntity.Text = "Document " + Doc.DocName + " - Heading info:";
                    ValorizzaForm(Doc);
                        //nascondo possibilità di fare upload
                        PhdDropZone.Visible = false;   
                        PhdLnkPdfDoc.Visible = true;
                        HypLnkPdfDoc.Text = Doc.PDFName;
                        HypLnkPdfDoc.NavigateUrl = "/Public/Images/" + Doc.PDFName;

                        //carica i capitoli...
                        PhdChapters.Visible = true;
                        List<Chapter> LstChapt = GetChapters(Doc.IdDocument); 
                        RepCapters.DataSource = LstChapt;
                        RepCapters.DataBind();
                    }
                else
                {
                        //siamo in inserimento
                        LitEntity.Text = "New document...";
                    ResettaForm();
                        PhdDropZone.Visible = true;
                        PhdLnkPdfDoc.Visible = false;
                        PhdChapters.Visible = false;
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
                    Document Result= UpdateDoc(Convert.ToInt32(Request.QueryString["Id"]));
                   

                    ObjPageManager.InsertAudit(LoginUsr, "Tag updated: " + Result.DocName);
                }
                else
                {
                    //inserimento
                    
                        Document Result = InsertDoc();
                    ObjPageManager.InsertAudit(LoginUsr, "Tag inserted: " + Result.DocName);
                    //salva il documento definitivamente
                    string OrigPdfFullPath = HttpContext.Current.Server.MapPath("~/Public/Images/Temp/");
                        string DestPdfFullPath = HttpContext.Current.Server.MapPath("~/Public/Images/");
                        string PdfName = Result.IdDocument + "_" + TxtNomeDocumento.Text.Replace(" ", "_") + ".pdf";
                    try
                    {
                        System.IO.File.Move(OrigPdfFullPath + HidTempFileName.Value, DestPdfFullPath + PdfName);
                        try
                        {
                            Document Result2 = UpdateFileNameOfDoc(Result.IdDocument, PdfName);
                            //ora estrae dal documento pdf i capitoli e se li salva nel db
                            //con numero di pagina
                            List<Title> TitleLst = Utility.ReadPdfFile(DestPdfFullPath + PdfName);
                            //per ciascun titolo salva nel db nei capitoli
                            foreach (Title CurrTit in TitleLst)
                            {
                                Chapter Cap = new Chapter();
                                Cap.ChapterName = CurrTit.Riga;
                                Cap.Argument = Result.Argument;
                                Cap.Device = Result.Device;
                                Cap.Alias = Result.Alias;
                                Cap.PageNumber = CurrTit.Pagina;
                                Cap.IdDocument = Result.IdDocument;
                                Cap.Enabled = true;

                                InsertChapter(Cap);
                            }
                            //Response.Redirect("DocumentDetail.aspx?Id=" + Result.IdDocument);
                            //Response.Redirect("Documents.aspx");
                        }
                        catch (Exception ex)
                        {
                            PrintError(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No pdf document, please upload it.");
                    }
                        
                   

                }
                Response.Redirect("Documents.aspx");
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


        protected void RepCapters_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Salva")
            {
                try
                {
                    TextBox TxtArg = (TextBox)e.Item.FindControl("TxtArg");
                    TextBox TxtDev = (TextBox)e.Item.FindControl("TxtDev");
                    TextBox TxtAli = (TextBox)e.Item.FindControl("TxtAli");
                    Literal LitOk = (Literal)e.Item.FindControl("LitOk");
                    LitOk.Visible = true;

                    Chapter ModChap=  UpdateChapter(Convert.ToInt32(e.CommandArgument), TxtArg.Text, TxtDev.Text, TxtAli.Text);
                   
                    AuditPageManager ObjPageManager = new AuditPageManager();
                    ObjPageManager.InsertAudit(LoginUsr, "Chapter of document " + ModChap.Document.DocName + " updated: '" + ModChap.ChapterName + "'");
                }
                catch (Exception ex)
                {
                    PrintError(ex);
                }
            }
            
        }

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
            Sb.Append("url: '../Uploader.ashx?filename=' + filename.toString(),");
            Sb.Append("maxFiles: 1,");
            Sb.Append("acceptedFiles: '.pdf',");
            Sb.Append("addRemoveLinks: true,");
            Sb.Append("success: function (file, response) {");
            Sb.Append("var imgName = response;");
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
        private List<Chapter> GetChapters(int IdDoc)
        {
            using (ChapterEFRepository Rep = new ChapterEFRepository(""))
            {
                List<Chapter> LstChapters = Rep.Context.Chapters.Where(x => x.IdDocument == IdDoc).ToList();
                //List<UserDTO> LstUserDto = new List<UserDTO>();
                //var config = new MapperConfiguration(cfg => {
                //    cfg.CreateMap<User, UserDTO>()
                //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
                //     .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                //     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                //});
                //IMapper mapper = config.CreateMapper();

                //LstUserDto = mapper.Map<List<User>, List<UserDTO>>(Users);

                //foreach (User Usr in Users)
                //{
                //  List<Audit> LstAud = Usr.Audits.ToList();
                //    Role Rol = Usr.Role;
                //}

                return LstChapters;
            }

        }

        public Chapter UpdateChapter(int Id,string Arg,string Dev,string Alias)
        {
            Chapter result = null;
            Document Doc=null;
            using (ChapterEFRepository Rep = new ChapterEFRepository(""))
            {
                result = Rep.Context.Chapters.SingleOrDefault(x => x.IdChapter == Id);
                if (result != null)
                {
                    result.Argument = Arg;
                    result.Device = Dev;
                    result.Alias = Alias;
                    Doc = result.Document;
                    Rep.Context.SaveChanges();
                }
            }
            result.Document = Doc;
            return result;


        }
        private void InsertChapter(Chapter NewChapter)
        {
            
                using (ChapterEFRepository Rep = new ChapterEFRepository(""))
                {
                
                    Rep.Context.Chapters.Add(NewChapter);
                    Rep.Context.SaveChanges();
                }
        }
        private void PopolaCboTypology(DropDownList drop, List<Ls.Prj.Entity.Type> list)
        {
            foreach (Ls.Prj.Entity.Type Curr in list)
            {
                var listItem = new ListItem();
                listItem.Value = Curr.IdTypology.ToString();
                listItem.Text = Curr.Typology;
                drop.Items.Add(listItem);
            }
        }

        private void ResettaForm()
        {

            TxtNomeDocumento.Text = "";
            TxtArgument.Text = "";
            TxtDevice.Text = "";
            TxtAlias.Text = "";
            TxtDocNumber.Text = "";
            Utility.SetDropByValue(CboTypology, "0");
            Utility.SetDropByValue(CboEnable, "1");

        }
        private void ValorizzaForm(Document Doc)
        {
            TxtNomeDocumento.Text = Doc.DocName;
            TxtArgument.Text = Doc.Argument;
            TxtDevice.Text = Doc.Device;
            TxtAlias.Text = Doc.Alias;
            TxtDocNumber.Text = Doc.DocNumer;
            Utility.SetDropByValue(CboTypology, Doc.IdTypology.ToString());
            Utility.SetDropByValue(CboEnable, Doc.Enabled.ToString());
  
          
        }
        public Document UpdateDoc(int Id)
        {
            Document result = null;
            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {
                 result = Rep.Context.Documents.SingleOrDefault(x => x.IdDocument == Id);
                if (result != null)
                {

                    result.DocName = TxtNomeDocumento.Text;
                    result.Argument = TxtArgument.Text;
                    result.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
                    result.Device = TxtDevice.Text;
                    result.Alias = TxtAlias.Text;
                    result.DocNumer = TxtDocNumber.Text;
                    result.IdTypology = Convert.ToInt32(CboTypology.SelectedValue);
                    Rep.Context.SaveChanges();
                }
            }
            return result;


        }
        public Document UpdateFileNameOfDoc(int Id, string PDFName)
        {
            Document result = null;
            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {
                result = Rep.Context.Documents.SingleOrDefault(x => x.IdDocument == Id);
                if (result != null)
                {
                    result.PDFName = PDFName;
                    Rep.Context.SaveChanges();
                }
            }
            return result;


        }
        public Document InsertDoc()
        {
            Document NewDocument = new Document();


            NewDocument.DocName = TxtNomeDocumento.Text;
            NewDocument.Argument = TxtArgument.Text;
            NewDocument.Device = TxtDevice.Text;
            NewDocument.Alias = TxtAlias.Text;
            NewDocument.DocNumer = TxtDocNumber.Text;
            NewDocument.Enabled = Convert.ToBoolean(CboEnable.SelectedValue);
            NewDocument.IdTypology = Convert.ToInt32(CboTypology.SelectedValue);
           

            using (DocumentEFRepository DocRep = new DocumentEFRepository(""))
            {
                DocRep.Context.Documents.Add(NewDocument);
                DocRep.Context.SaveChanges();
            }

            return NewDocument;

        }


        public void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
        #endregion


    }


}