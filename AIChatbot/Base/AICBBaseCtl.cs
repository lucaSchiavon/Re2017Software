using AIChatbot.Classes;
using Ls.Prj.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIChatbot.Base
{
    public class AICBBaseCtl : System.Web.UI.UserControl
    {

        #region public propery of page
        public User LoginUsr
        {
            get {  return GetLoginUser(); }
          
        }

        #endregion



        #region routine private alla pagina
        private User GetLoginUser()
        {
            AICBasePageManager ObjAICBasePageManager = new AICBasePageManager();
            User LogUsr = ObjAICBasePageManager.GetLoginUsr();
            return LogUsr;
        }

       
        #endregion

    }
}
