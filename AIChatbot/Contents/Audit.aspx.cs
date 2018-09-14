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
           
namespace Ls.Re2017.PigeonCms.pgn_content.Contents
{
    public partial class Audit : System.Web.UI.Page
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
                    AuditPageManager ObjAuditPageManager = new AuditPageManager();

                    ////popola le combo
                    //PopolaCboUsers(CboUsers, ObjAuditPageManager.GetUsers());
                    //CboUsers.Items.Add(new ListItem("--Select--", "0"));
                    //Utility.SetDropByValue(CboUsers, "0");

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

        private void PopolaCboUsers(DropDownList drop, List<User> list)
        {
            foreach (User Curr in list)
            {
                var listItem = new ListItem();
                listItem.Value = Curr.IdUser.ToString();
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

        private List<AuditDTO> LoadList()
        {
            AuditPageManager ObjPageManager = new AuditPageManager();
            DateTime Da = Ls.Prj.Utility.Utility.DateParse(TxtDa.Text);
            DateTime A = Ls.Prj.Utility.Utility.DateParse(TxtA.Text);
            List<AuditDTO> LstDto = ObjPageManager.GetAudits(Da, A, Convert.ToInt32(CboUsers.SelectedValue));
            



            return LstDto;
        }
        private void PrintError(Exception ex)
        {
            LitError.Text = "An unhandled error occurred:<br>" + ex.Message;
            DivError.Attributes.Add("Class", "ParentDivDeleting Attivo");
        }
     
        #endregion

       
    }


}