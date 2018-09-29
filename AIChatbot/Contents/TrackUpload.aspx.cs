using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Re2017.Classes;
using Ls.Prj.Entity;
using System.Text;
using Ls.Prj.DTO;
using System.Data.Entity;
using System.Collections;
using Ls.Prj.Utility;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Ls.Re2017.Contents
{
    public partial class TrackUpload : System.Web.UI.Page
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
                LnkBtnBrowse.Attributes.Add("onclick", "document.getElementById('" + FileUpload1.ClientID + "').click(); return false;");
                if (!Page.IsPostBack)
                {
                    //GetBanks();

                    //TrackManagementPageManager ObjTrackManagementPageManager = new TrackManagementPageManager();
                    //List<BankDTO> LstBankDto = TrackManagementPageManager.GetBanks();
                    //List<BankDTO> LstDto = TrackManagementPageManager.RunAsyncBanks();
                    ////popola le combo
                    PopolaCboBanks(CboBank);
                    CboBank.Items.Add(new ListItem("--Select a bank--", "0"));
                    Utility.SetDropByValue(CboBank, "0");

                    //inizializza classi CSS del paginatore
                    HidePreviousClass = "disabled";

                    //binding a tabella
                    BindRepeater();
                }
                   
              
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }

        #region codice prova
        //static HttpClient client = new HttpClient();
        //static void GetBanks()
        //{
       
        //    //RunAsyncBanks().GetAwaiter().GetResult();
        //    List<BankDTO> LstDto = RunAsyncBanks();
        //}
        //static List<BankDTO> RunAsyncBanks()
        //{
        //    // Update port # in the following line.
        //    client.BaseAddress = new Uri("http://2.235.241.7:8080/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    List<BankDTO> LstEvento = new List<BankDTO>();
        //    try
        //    {

              
        //        LstEvento = GetEventAsyncBanks("banks").Result;
        //        //Task ObjTask= GetEventAsyncBanks("banks");
        //        // LstEvento = ObjTask.Wait();
        //        //await Task.Factory.StartNew(() =>
        //        //{
        //        //    LstEvento = GetEventAsyncBanks("banks").Result;
        //        //});


              

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    return LstEvento;
        //}
        ////static async Task RunAsyncBanks()
        ////{
        ////    // Update port # in the following line.
        ////    client.BaseAddress = new Uri("http://2.235.241.7:8080/");
        ////    client.DefaultRequestHeaders.Accept.Clear();
        ////    client.DefaultRequestHeaders.Accept.Add(
        ////        new MediaTypeWithQualityHeaderValue("application/json"));

        ////    try
        ////    {

        ////        List<BankDTO> LstEvento = new List<BankDTO>();
        ////    LstEvento = await GetEventAsyncBanks("banks");
        ////    //Task ObjTask= GetEventAsyncBanks("banks");
        ////    // LstEvento = ObjTask.Wait();
        ////    //await Task.Factory.StartNew(() =>
        ////    //{
        ////    //    LstEvento = GetEventAsyncBanks("banks").Result;
        ////    //});


        ////    foreach (BankDTO Evt in LstEvento)
        ////        {
        ////            Console.WriteLine(Evt.Name);
        ////        }

        ////    }
        ////    catch (Exception e)
        ////    {
        ////        Console.WriteLine(e.Message);
        ////    }


        ////}
        //static async Task<List<BankDTO>> GetEventAsyncBanks(string path)
        //{
        //    List<BankDTO> LstEvento = null;
        //    //HttpResponseMessage response = await client.GetAsync(path);
        //    HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        LstEvento = await response.Content.ReadAsAsync<List<BankDTO>>();
        //    }
        //    return LstEvento;
        //}
        //// static async Task<List<BankDTO>> GetEventAsyncBanks(string path)
        ////{
        ////    List<BankDTO> LstEvento = null;
        ////    //HttpResponseMessage response = await client.GetAsync(path);
        ////    HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        ////    if (response.IsSuccessStatusCode)
        ////    {
        ////        LstEvento = await response.Content.ReadAsAsync<List<BankDTO>>();
        ////    }
        ////    return LstEvento;
        ////}
        #endregion

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            DivError.Attributes.Add("Class", "ParentDivDeleting Disattivato");

        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            BindRepeater();
        }

        protected void CboRowsInPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }

        #region routine private pagina (re2017)

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

        private void PopolaCboBanks(DropDownList drop)
        {
            List<BankDTO> LstDto = TrackManagementPageManager.GetBanks();
            foreach (BankDTO Curr in LstDto)
            {
                var listItem = new ListItem();
                listItem.Value = Curr.id.ToString();
                listItem.Text = Curr.Name;
                drop.Items.Add(listItem);
            }
        }
        private void BindRepeater()
        {
            //List<AuditDTO> LstAudDto = LoadList();
            ////Create the PagedDataSource that will be used in paging
            //PagedDataSource pgitems = new PagedDataSource();
            //pgitems.DataSource = LstAudDto.OrderByDescending(x=>x.ModTime).ToList();
            //pgitems.AllowPaging = true;

            ////Control page size from here 
            //pgitems.PageSize =Convert.ToInt32(CboRowsInPages.SelectedValue);
           
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


            //LitShowOneOf.Text = "Showing 1 to " + pgitems.PageSize + " of " + LstAudDto.Count + " entries";
            //btnPage.Text = "Pag. " + (PageNumber + 1);
           
        }

        //private List<AuditDTO> LoadList()
        //{
        //    TrackManagementPageManager ObjPageManager = new TrackManagementPageManager();
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
     
        #endregion

       
    }


}