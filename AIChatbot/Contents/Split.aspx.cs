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
using Re2017.Base;

namespace Ls.Re2017.Contents
{
    public partial class Split: Re2017BasePage
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
                    PopolaCboTemplate(CboTemplates);

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
        protected void BtnCloseInformation_Click(object sender, EventArgs e)
        {
            DivInformation.Attributes.Add("Class", "ParentDivDeleting Disattivato");

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrackManagement.aspx");
        }

        protected void LnkBtnApplyTemplate_Click(object sender, EventArgs e)
        {
            //per ogni template del tipo selezionato recupera gli eventi template e li inserisce sull'evento corrente
           
                TemplateDetailPageManager ObjTemplateDetailPageManager = new TemplateDetailPageManager();
                TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
                ////recupera il template
                //TemplateDTO ObjTemplateDTO;
                //ObjTemplateDTO = ObjTemplateDetailPageManager.GetTemplate(Convert.ToInt32(CboTemplates.SelectedValue));
                try
                {
                if (CboTemplates.SelectedValue != "0")
                {
                    //Recupera gli eventi del template
                    List<EventoDTO> LstEvtDto = ObjTemplateDetailPageManager.GetTemplateEvents(Convert.ToInt32(CboTemplates.SelectedValue));
                foreach (EventoDTO CurrEvt in LstEvtDto)
                {
                    Evento ObjInsertEvtInput = new Evento(); //data = "{'id': 99,'houseId':6}";
                    ObjInsertEvtInput.amount = CurrEvt.amountNoFormat;
                    ObjInsertEvtInput.bankReportEntryId =Convert.ToInt32(Request.QueryString["bankReportEntryId"]);
                  
                    ObjInsertEvtInput.date = DateTime.Now;  
                    ObjInsertEvtInput.description = CurrEvt.description;
                    ObjInsertEvtInput.eventTypeId = CurrEvt.eventTypeId;
                    ObjInsertEvtInput.filePath = CurrEvt.filePath;
                    ObjInsertEvtInput.houseId = CurrEvt.houseId;
                    ObjInsertEvtInput.id = 0;
                    ObjInsertEvtInput.invoiceId = CurrEvt.invoiceId;
                    ObjInsertEvtInput.reminderDate = CurrEvt.reminderDate;
                    ObjInsertEvtInput.reminderMessage = CurrEvt.reminderMessage;

                   
                        ObjTrackManagement2PageManager.NewEvt(ObjInsertEvtInput);
                  

                   
                }
                BindBrotherEvts();
                }
            }
                catch (Exception ex)
                {
                    PrintError(ex);
                }
            
        }

            protected void LnkBtnSplit_Click(object sender, EventArgs e)
        {
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            //recupera l'evento da cui si arriva
           
            Evento ObjEvento = null;
            try
            {
                ObjEvento = ObjTrackManagement2PageManager.GetAsyncEvento("events/" + Request.QueryString["evtId"].ToString()).Result;

          
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
                //Label LblAmount = e.Item.FindControl("LblAmount") as Label;

                //if (LblAmount.Text.Contains("("))
                //{
                //    LblAmount.ForeColor = System.Drawing.Color.Red;
                //}
                //else
                //{
                //    LblAmount.ForeColor = System.Drawing.Color.Green;
                //}
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

                ////colora di verde o rosso l'importo a seconda che sia un credito o debito
                //Label LblAmount = e.Item.FindControl("LblAmount") as Label;

                //if (LblAmount.Text.Contains("("))
                //{
                //    LblAmount.ForeColor = System.Drawing.Color.Red;
                //}
                //else
                //{
                //    LblAmount.ForeColor = System.Drawing.Color.Green;
                //}
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
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();

            try
            {
                //si scorre il repeater ed aggiorna i dati
                foreach (RepeaterItem item in RptSelEvt.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var TxtAmount = (TextBox)item.FindControl("TxtAmount");
                        var TxtDescription = (TextBox)item.FindControl("TxtDescription");
                        DropDownList CboCase = (DropDownList)item.FindControl("CboCase");
                        int idEvt =Convert.ToInt32( CboCase.Attributes["MemIdEvt"]);
                        //MemIdEvt
                        //fa il put del dato
                        UpdateBrotherEvtDto ObjUpdateBrotherEvtDto = new UpdateBrotherEvtDto();
                        ObjUpdateBrotherEvtDto.id = idEvt;
                        ObjUpdateBrotherEvtDto.amount =Convert.ToDouble(TxtAmount.Text);
                        ObjUpdateBrotherEvtDto.description = TxtDescription.Text;
                        ObjTrackManagement2PageManager.UpdateBrotherEvt(ObjUpdateBrotherEvtDto);


                    }
                }
                //messaggio di conferma salvataggio
                LitMessaggioInformativo.Text = "Events update successfully completed.";
                DivInformation.Attributes.Add("Class", "ParentDivDeleting Attivo");
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
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
            LstEvtDto = ObjTrackManagement2PageManager.GetEventi(Convert.ToDateTime("1000-01-01"), Convert.ToDateTime("3000-01-01"), Convert.ToInt32(Request.QueryString["bankReportEntryId"].ToString()));
         
 
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

        private void PopolaCboTemplate(DropDownList drop)
        {
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            List<TemplateDTO> LstTemplate;
            LstTemplate = ObjTrackManagement2PageManager.GetTemplate();
            if (LstTemplate != null)
            {

                foreach (TemplateDTO Curr in LstTemplate)
                {
                    var listItem = new ListItem();
                    listItem.Value = Curr.id.ToString();
                    listItem.Text = Curr.description;
                    drop.Items.Add(listItem);

                }
            }
            drop.Items.Add(new ListItem("--Select template--", "0"));
            Utility.SetDropByValue(drop, "0");
        }

        #endregion



    }
}