using Re2017.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AQuest.ChatBotGsk.PigeonCms.pgn_content.ascx
{
    public partial class Header : Re2017BaseCtl //System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (LoginUsr !=null)
            {
                LblUsername.Text ="Hy " + LoginUsr.name + "!";
            }
        }
    }
}