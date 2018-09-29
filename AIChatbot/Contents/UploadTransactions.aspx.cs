using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIChatbot.Base;
using Ls.Prj.DTO;
using Ls.Prj.Utility;
using Re2017.Classes;

namespace Re2017.Contents
{
    public partial class UploadTransactions : AICBBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopolaCboBanks(CboBank);
            CboBank.Items.Add(new ListItem("--Select a bank--", "0"));
            Utility.SetDropByValue(CboBank, "0");
        }

        #region routine private alla pagina
        private void PopolaCboBanks(DropDownList drop)
        {
            UploadTransactionsPageManager ObjUploadTransactionsPageManager = new UploadTransactionsPageManager();
            List<BankDTO> LstDto = ObjUploadTransactionsPageManager.GetBanks();
            foreach (BankDTO Curr in LstDto)
            {
                var listItem = new ListItem();
                listItem.Value = Curr.id.ToString();
                listItem.Text = Curr.Name;
                drop.Items.Add(listItem);
            }
        }
        #endregion
    }

}