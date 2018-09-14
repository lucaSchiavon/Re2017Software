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
using System.Collections;
using AIChatbot.Base;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents
{
    public partial class Images : AICBBasePage
    {

        #region public propery of page
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }
        public int TotalNumPages
        {
            get
            {
                if (ViewState["TotalNumPages"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalNumPages"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["TotalNumPages"] = value; }
        }

        public string HidePreviousClass
        {
            get
            {
                if (ViewState["HidePreviousClass"] != null)
                {
                    return ViewState["HidePreviousClass"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { ViewState["HidePreviousClass"] = value; }
        }
        public string HideNextClass
        {
            get
            {
                if (ViewState["HideNextClass"] != null)
                {
                    return ViewState["HideNextClass"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { ViewState["HideNextClass"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                   
                    //inizializza classi CSS del paginatore
                    HidePreviousClass = "disabled";

                    //bindong a tabella
                    BindRepeater();
                }
               
                
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }

        protected void CboRowsInPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }

        #region Gestione paginatore
        protected void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (PageNumber >= 1)
            {
                PageNumber = PageNumber - 1;
                BindRepeater();
                HideNextClass = "";
                HidePreviousClass = "";
                if (PageNumber == 0)
                {
                    HideNextClass = "";
                    HidePreviousClass = "disabled";
                }

            }
            else
            {

                //disabilita o abilita i bottoni previous next del pager
                HideNextClass = "";
                HidePreviousClass = "disabled";

            }


        }
        protected void BtnNext_Click(object sender, EventArgs e)
        {
            //if (PageNumber < TotalNumPages - 2)
            //{
                if (PageNumber < TotalNumPages - 1)
            {
                PageNumber = PageNumber + 1;
                BindRepeater();
                HideNextClass = "";
                HidePreviousClass = "";
                if (PageNumber == (TotalNumPages - 1))
                {
                    HideNextClass = "disabled";
                    HidePreviousClass = "";
                }
            }
            else
            {
                //disabilita o abilita i bottoni previous next del pager
                HideNextClass = "disabled";
                HidePreviousClass = "";

            }

        }
        #endregion

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

                ImageEFRepository rep = new ImageEFRepository("");
              Ls.Prj.Entity.Image ImgToStoreInAudit = rep.SelectEntity(IdToDelete);

               
                //cancella la riga 
                DeleteEntity(IdToDelete);

                //cancella l'immagine fisica
                string DestPdfFullPath = HttpContext.Current.Server.MapPath("~/Public/Photos/");
                string ImgName = ImgToStoreInAudit.ImageName;
                System.IO.File.Delete(DestPdfFullPath + ImgName);

                AuditPageManager ObjPageManager = new AuditPageManager();
                ObjPageManager.InsertAudit(LoginUsr, "Image deleted: " + ImgToStoreInAudit.ImageName);


                BindRepeater();

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
            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                var entity = new Ls.Prj.Entity.Image { IdImage = IdEntity };
                Rep.Context.Entry(entity).State = EntityState.Deleted;
                Rep.Context.SaveChanges();
            }
        }
       



        private void BindRepeater()
        {
            List<ImageDTO> LstDto = LoadList();
            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = LstDto;
            pgitems.AllowPaging = true;

            //Control page size from here 
            pgitems.PageSize = Convert.ToInt32(CboRowsInPages.SelectedValue);
            //pgitems.PageSize = 5;
            pgitems.CurrentPageIndex = PageNumber;
            //Raccolgo il numero pagine
            TotalNumPages = pgitems.PageCount;
            if (pgitems.PageCount > 1)
            {
                rptPaging.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    pages.Add((i + 1).ToString());
                }
                rptPaging.DataSource = pages;
                rptPaging.DataBind();
            }
            else
            {
                rptPaging.Visible = false;
            }

            //Finally, set the datasource of the repeater
            Repeater1.DataSource = pgitems;
            Repeater1.DataBind();


            LitShowOneOf.Text = "Showing 1 to " + pgitems.PageSize + " of " + LstDto.Count + " entries";
            btnPage.Text = "Pag. " + (PageNumber + 1);

        }
        private List<ImageDTO> LoadList()
        {
            ImagesPageManager ObjImgPageManager = new ImagesPageManager();
           
            List<ImageDTO> LstImgDto  = ObjImgPageManager.GetImgs();

            return LstImgDto;
        }

        #endregion

       
    }


}