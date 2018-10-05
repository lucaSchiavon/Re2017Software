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
using System.Collections;
using Ls.Prj.Utility;
using Re2017;
using Re2017.Classes;
using System.Globalization;

namespace Ls.Re2017.Contents
{
    public partial class TrackManagement : System.Web.UI.Page
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

       private  List<EventTypeDTO> LstEvtType;
        private List<HouseDTO> LstHouse;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
                LstEvtType = ObjTrackManagement2PageManager.GetEventsType();
                LstHouse = ObjTrackManagement2PageManager.GetHouse();
                if (!Page.IsPostBack)
                {
                    if (Request.Cookies["TxtDa"] == null)
                    {
                        TxtDa.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("yyyy-MM-dd");
                        TxtA.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("yyyy-MM-dd");
                      
                    }
                    else
                    {
                        TxtDa.Text = Request.Cookies["TxtDa"].Value;
                        TxtA.Text = Request.Cookies["TxtA"].Value;
                    }
                   
                  
                    //TextBox1.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30)).ToString();
                    //string date = DateTime.Now;
                    //dataprova.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30)).ToString();
                    //AuditPageManager ObjAuditPageManager = new AuditPageManager();

                    //popola le combo
                    //PopolaCboUsers(CboUsers, ObjAuditPageManager.GetUsers());
                    //CboUsers.Items.Add(new ListItem("--Select--", "0"));
                    //Utility.SetDropByValue(CboUsers, "0");

                    //inizializza classi CSS del paginatore
                    HidePreviousClass = "disabled";

                   

                    //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
                    Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");
                    //LitPathFormScriptValidation.Text = "<script src='../js/TrackManagement.js'></script>";
                    //LitRe2017ScriptInject.Text= "<script src='../js/TrackManagement.js'></script>";
                    // ViewState["LstEvtType"] = LstEvtType;
                }

                //bindong a tabella
                BindRepeater();
                ////*******************
                ////chiamata put da togliere
                //UpdateHouseEvtInputDto ObjUpdateHouseEvtInputDto = new UpdateHouseEvtInputDto(); //data = "{'id': 99,'houseId':6}";
                //ObjUpdateHouseEvtInputDto.id = 99;
                //ObjUpdateHouseEvtInputDto.houseId = 7;
                //ObjTrackManagement2PageManager.UpdateHouseEvt(ObjUpdateHouseEvtInputDto);
                ////*****************


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

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            Response.Cookies["TxtDa"].Value = TxtDa.Text;
            Response.Cookies["TxtA"].Value = TxtA.Text;
            BindRepeater();
        }

        protected void CboRowsInPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList CboEventi = e.Item.FindControl("CboEventi") as DropDownList;
                PopolaCboEventi(CboEventi);
                DropDownList CboCase = e.Item.FindControl("CboCase") as DropDownList;
                PopolaCboCase(CboCase);

                Ls.Prj.DTO.EventoDTO drv = (Ls.Prj.DTO.EventoDTO)e.Item.DataItem;

                CboCase.Attributes.Add("onchange", "UpdateHouse(this)");
                CboEventi.Attributes.Add("onchange", "UpdateEvtType(this)");
                //DataRowView drv = e.Row.DataItem as DataRowView;
                Utility.SetDropByValue(CboEventi, CboEventi.Attributes["MemId"]);
                Utility.SetDropByValue(CboCase, CboCase.Attributes["MemId"]);

                if (CboEventi.Attributes["MemId"] == "0")
                {
                    CboEventi.Attributes.Add("style", "font-weight:bold");
                }

                if (CboCase.Attributes["MemId"] == "0")
                {
                    CboCase.Attributes.Add("style", "font-weight:bold");
                }

                //colora di verde o rosso l'importo a seconda che sia un credito o debito
                Label LblAmount = e.Item.FindControl("LblAmount") as Label;

                if (LblAmount.Text.Contains("("))
                {
                    LblAmount.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    LblAmount.ForeColor = System.Drawing.Color.Green;
                }
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
                TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
                ObjTrackManagement2PageManager.DeleteEvt(IdToDelete);
                //ImageEFRepository rep = new ImageEFRepository("");
                //Ls.Prj.Entity.Image ImgToStoreInAudit = rep.SelectEntity(IdToDelete);
             

                ////cancella la riga 
                //DeleteEntity(IdToDelete);

                ////cancella l'immagine fisica
                //string DestPdfFullPath = HttpContext.Current.Server.MapPath("~/Public/Photos/");
                //string ImgName = ImgToStoreInAudit.ImageName;
                //System.IO.File.Delete(DestPdfFullPath + ImgName);

                //AuditPageManager ObjPageManager = new AuditPageManager();
                //ObjPageManager.InsertAudit(LoginUsr, "Image deleted: " + ImgToStoreInAudit.ImageName);


                BindRepeater();

                DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }

        //protected void BtnClose_Click(object sender, EventArgs e)
        //{
        //    DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");

        //}

        #endregion

        #region Gestione paginatore
        protected void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (PageNumber >= 1)
            {
                PageNumber = PageNumber - 1;
                BindRepeater();
                HideNextClass = "";
                HidePreviousClass = "";

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
            if (PageNumber < TotalNumPages - 2)
            {
                PageNumber = PageNumber + 1;
                BindRepeater();
                HideNextClass = "";
                HidePreviousClass = "";

            }
            else
            {
                //disabilita o abilita i bottoni previous next del pager
                HideNextClass = "disabled";
                HidePreviousClass = "";

            }

        }
        #endregion


        #region routine private alla pagina

        //private void PopolaCboUsers(DropDownList drop, List<User> list)
        //{
        //    foreach (User Curr in list)
        //    {
        //        var listItem = new ListItem();
        //        listItem.Value = Curr.IdUser.ToString();
        //        listItem.Text = Curr.Name;
        //        drop.Items.Add(listItem);
        //    }
        //}
        private void BindRepeater()
        {
            //if (ViewState["LstEvtDto"] == null)
            //{
            //    ViewState["LstEvtDto"] = TrackManagement2PageManager.GetEventi(Convert.ToDateTime(TxtDa.Text), Convert.ToDateTime(TxtA.Text));
            //}
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            List<EventoDTO> LstEvtDto = ObjTrackManagement2PageManager.GetEventi(Convert.ToDateTime(TxtDa.Text), Convert.ToDateTime(TxtA.Text));
            //List<EventoDTO> LstEvtDto = TrackManagement2PageManager.GetEventi(Convert.ToDateTime(TxtDa.Text), Convert.ToDateTime(TxtA.Text));

            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            //List<EventoDTO> LstEvtDto =(List<EventoDTO>)ViewState["LstEvtDto"];
            //pgitems.DataSource = LstEvtDto.OrderByDescending(x => x.date).ToList();
            List<EventoDTO> LstEvtDtoOrdered = LstEvtDto.OrderByDescending(x => x.date).ToList();
            pgitems.DataSource = LstEvtDtoOrdered;
            //pgitems.DataSource = LstEvtDto.ToList();
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


            LitShowOneOf.Text = "Showing 1 to " + pgitems.PageSize + " of " + LstEvtDto.Count + " entries";
            btnPage.Text = "Pag. " + (PageNumber + 1);

        }

        //private List<AuditDTO> LoadList()
        //{
        //    AuditPageManager ObjPageManager = new AuditPageManager();
        //    DateTime Da = Ls.Prj.Utility.Utility.DateParse(TxtDa.Text);
        //    DateTime A = Ls.Prj.Utility.Utility.DateParse(TxtA.Text);
        //    List<AuditDTO> LstDto = ObjPageManager.GetAudits(Da, A, Convert.ToInt32(CboUsers.SelectedValue));




        //    return LstDto;
        //}
        private void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }

        private void PopolaCboEventi(DropDownList drop)
        {
            //List<EventTypeDTO> LstDto = (List<EventTypeDTO>)ViewState["LstEvtType"];
            // TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            //List<EventTypeDTO> LstEvtType = ObjTrackManagement2PageManager.GetEventsType();
            if (LstEvtType != null)
            { 
                foreach (EventTypeDTO Curr in LstEvtType)
                {
                    var listItem = new ListItem();
                    listItem.Value = Curr.id.ToString();
                    listItem.Text = Curr.displayValue;
                    drop.Items.Add(listItem);
              
                }
            }
            //drop.Items.Add(new ListItem("--Select event type--", "0"));
            Utility.SetDropByValue(drop, "0");
        }

        private void PopolaCboCase(DropDownList drop)
        {
            if (LstHouse != null)
            {
           
                foreach (HouseDTO Curr in LstHouse)
                {
                    var listItem = new ListItem();
                    listItem.Value = Curr.id.ToString();
                    listItem.Text = Curr.nickname;
                    drop.Items.Add(listItem);

                }
            }
            drop.Items.Add(new ListItem("--Select house--", "0"));
            Utility.SetDropByValue(drop, "0");
        }
        #endregion



    }
}