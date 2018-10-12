using AIChatbot.Classes;
using Ls.Prj.Entity;
using Re2017.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Re2017.Base
{
    public class Re2017BaseCtl : System.Web.UI.UserControl
    {

        #region public propery of page
        public Utente LoginUsr
        {
            get {  return GetLoginUser(); }
          
        }

        #endregion



        #region routine private alla pagina
        private Utente GetLoginUser()
        {
            UserPageManager ObjUserPageManager = new UserPageManager();
            string IdUsr = HttpContext.Current.Request.Cookies["IdUser"].Value;
            Utente LogUsr = ObjUserPageManager.GetUtente(Convert.ToInt32(IdUsr));
            return LogUsr;
        }

       
        #endregion

    }
}
