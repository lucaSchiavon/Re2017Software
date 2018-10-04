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
    public partial class Split: System.Web.UI.Page
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


                    //TextBox1.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30)).ToString();
                    //string date = DateTime.Now;
                    //dataprova.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30)).ToString();
                    //AuditPageManager ObjAuditPageManager = new AuditPageManager();

                    //popola le combo
                    //PopolaCboUsers(CboUsers, ObjAuditPageManager.GetUsers());
                    //CboUsers.Items.Add(new ListItem("--Select--", "0"));
                    //Utility.SetDropByValue(CboUsers, "0");

                    ////inizializza classi CSS del paginatore
                    //HidePreviousClass = "disabled";

                    //bindong a tabella
                    BindBrotherEvts();
                    //BindRepeater();

                    //aggiunge il tag script con il path del file jquery con la validazione della pagina nella masterpage
                    Literal LitPathFormScriptValidation = (Literal)Master.FindControl("LitPathFormScriptValidation");

                    //LitPathFormScriptValidation.Text = "<script src='../js/TrackManagement.js'></script>";
                    //LitRe2017ScriptInject.Text= "<script src='../js/TrackManagement.js'></script>";
                    // ViewState["LstEvtType"] = LstEvtType;
                }
              
                
               

               
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


        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrackManagement.aspx");
        }

        protected void LnkBtnSplit_Click(object sender, EventArgs e)
        {
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            //recupera l'evento da cui si arriva
           // EventoDTO ObjEvtDto = ObjTrackManagement2PageManager.GetEvento(Convert.ToInt32(Request.QueryString["evtId"].ToString()));
            Evento ObjEvento = null;
            try
            {
                ObjEvento = ObjTrackManagement2PageManager.GetAsyncEvento("events/" + Request.QueryString["evtId"].ToString()).Result;

            //crea clone
            //InsertEvtInputDto ObjInsertEvtInputDto = new InsertEvtInputDto(); //data = "{'id': 99,'houseId':6}";
            //ObjInsertEvtInputDto.amount = ObjEvento.amount;
            //ObjInsertEvtInputDto.bankReportEntryId = ObjEvento.bankReportEntryId;
            ////ObjInsertEvtInputDto.date = ObjEvento.date != null ? ObjEvento.date.Value.ToString("yyyy-MM-ddThh:mm:ss"):null;  //.ToString("yyyy-MM-dd");  //2018-10-04T07:11:09.833+0000
            //ObjInsertEvtInputDto.date = ObjEvento.date;  //.ToString("yyyy-MM-dd");  //2018-10-04T07:11:09.833+0000
            //ObjInsertEvtInputDto.description = ObjEvento.description;
            //ObjInsertEvtInputDto.eventTypeId = ObjEvento.eventTypeId;
            //ObjInsertEvtInputDto.filePath = ObjEvento.filePath;
            //ObjInsertEvtInputDto.houseId = ObjEvento.houseId;
            //ObjInsertEvtInputDto.id = 0;
            //ObjInsertEvtInputDto.invoiceId = ObjEvento.invoiceId;
            //ObjInsertEvtInputDto.reminderDate = ObjEvento.reminderDate;
            //ObjInsertEvtInputDto.reminderMessage = ObjEvento.reminderMessage;


            Evento ObjInsertEvtInput = new Evento(); //data = "{'id': 99,'houseId':6}";
            ObjInsertEvtInput.amount = ObjEvento.amount;
            ObjInsertEvtInput.bankReportEntryId = ObjEvento.bankReportEntryId;
            //ObjInsertEvtInputDto.date = ObjEvento.date != null ? ObjEvento.date.Value.ToString("yyyy-MM-ddThh:mm:ss"):null;  //.ToString("yyyy-MM-dd");  //2018-10-04T07:11:09.833+0000
            ObjInsertEvtInput.date = ObjEvento.date;  //.ToString("yyyy-MM-dd");  //2018-10-04T07:11:09.833+0000
            ObjInsertEvtInput.description = ObjEvento.description;
            ObjInsertEvtInput.eventTypeId = ObjEvento.eventTypeId;
            ObjInsertEvtInput.filePath = ObjEvento.filePath;
            ObjInsertEvtInput.houseId = ObjEvento.houseId;
            ObjInsertEvtInput.id = 0;
            ObjInsertEvtInput.invoiceId = ObjEvento.invoiceId;
            ObjInsertEvtInput.reminderDate = ObjEvento.reminderDate;
            ObjInsertEvtInput.reminderMessage = ObjEvento.reminderMessage;

            //inserisce i nuovi eventi
            int NumberEvtToCreate =Convert.ToInt32(CboSplitNumber.Value);
            for (int i = 0; i < NumberEvtToCreate; i++)
            {
                ObjTrackManagement2PageManager.NewEvt(ObjInsertEvtInput);
            }

                BindBrotherEvts();
           
            }
            catch (Exception ex)
            {
                PrintError(ex);
    }

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

        protected void RptSelEvt_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                //CboEventi.Enabled = false;
                //CboCase.Enabled = false;
            }
        }

        #region Gestione lightbox

        protected void BtnCancelDeleting_Click(object sender, EventArgs e)
        {
            DivDelete.Attributes.Add("Class", "ParentDivDeleting Disattivato");
        }
        protected void BtnUpdateAllBrothers_Click(object sender, EventArgs e)
        {
           
        }
        

        protected void BtnConfirmDeleting_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 IdToDelete = Convert.ToInt32(HydIdToDelete.Value);
                TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
                ObjTrackManagement2PageManager.DeleteEvt(IdToDelete);

                BindBrotherEvts();

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



        #region routine private alla pagina

        private void BindBrotherEvts()
        {

            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            //arrivato qui-----------------------------
            //startDate=2018-08-01&endDate=2018-08-31&
            //EventoDTO ObjEvtDto = ObjTrackManagement2PageManager.GetEvento(Convert.ToInt32(Request.QueryString["bankReportEntryId"].ToString()));
            //List<EventoDTO> LstEvtDto = new List<EventoDTO>();
            //LstEvtDto.Add(ObjEvtDto);
            List<EventoDTO> LstEvtDto = new List<EventoDTO>();
            LstEvtDto = ObjTrackManagement2PageManager.GetEventi(Convert.ToDateTime("2018-08-01"), Convert.ToDateTime("2018-08-31"), Convert.ToInt32(Request.QueryString["bankReportEntryId"].ToString()));
         
 
            RptSelEvt.DataSource = LstEvtDto;
            RptSelEvt.DataBind();
         

            ////Finally, set the datasource of the repeater
            //Repeater1.DataSource = pgitems;
            //Repeater1.DataBind();

        }
        private void BindRepeater()
        {
          
            //TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            //List<EventoDTO> LstEvtDto = ObjTrackManagement2PageManager.GetEventi(Convert.ToDateTime(TxtDa.Text), Convert.ToDateTime(TxtA.Text));
           

            ////Create the PagedDataSource that will be used in paging
            //PagedDataSource pgitems = new PagedDataSource();
          
            //List<EventoDTO> LstEvtDtoOrdered = LstEvtDto.OrderByDescending(x => x.date).ToList();
            //pgitems.DataSource = LstEvtDtoOrdered;
           
            //pgitems.AllowPaging = true;

            ////Control page size from here 
            //pgitems.PageSize = Convert.ToInt32(CboRowsInPages.SelectedValue);
          
            //pgitems.CurrentPageIndex = PageNumber;
            ////Raccolgo il numero pagine
            //TotalNumPages = pgitems.PageCount;
            //if (pgitems.PageCount > 1)
            //{
            //    rptPaging.Visible = true;
            //    ArrayList pages = new ArrayList();
            //    for (int i = 0; i <= pgitems.PageCount - 1; i++)
            //    {
            //        pages.Add((i + 1).ToString());
            //    }
            //    rptPaging.DataSource = pages;
            //    rptPaging.DataBind();
            //}
            //else
            //{
            //    rptPaging.Visible = false;
            //}

            ////Finally, set the datasource of the repeater
            //Repeater1.DataSource = pgitems;
            //Repeater1.DataBind();


            //LitShowOneOf.Text = "Showing 1 to " + pgitems.PageSize + " of " + LstEvtDto.Count + " entries";
            //btnPage.Text = "Pag. " + (PageNumber + 1);

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
            drop.Items.Add(new ListItem("--Select event type--", "0"));
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