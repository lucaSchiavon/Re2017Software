using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Runtime.Serialization;
using Re2017.Classes;
using Ls.Prj.DTO;

namespace RemoteAssistantAdmin
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static JsonVerifyIdentity UpdateHouse(int IdEvt, int IdHouse)
        {
            JsonVerifyIdentity ObjJsonVerifyIdentity = new JsonVerifyIdentity();
            TrackManagement2PageManager ObjTrackManagement2PageManager = new TrackManagement2PageManager();
            UpdateHouseEvtInputDto ObjUpdateHouseEvtInputDto = new UpdateHouseEvtInputDto(); 
            ObjUpdateHouseEvtInputDto.id = IdEvt;
            ObjUpdateHouseEvtInputDto.houseId = IdHouse;
            ObjTrackManagement2PageManager.UpdateHouseEvt(ObjUpdateHouseEvtInputDto);


            //if (email != "lully" || pwd != "lully")
            //{
            //    ObjJsonVerifyIdentity.errore = 1;
            //    ObjJsonVerifyIdentity.dettaglio = "Errore di autenticazione";



            //}
            //else
            //{
            //    ObjJsonVerifyIdentity.errore = 0;
            //    ObjJsonVerifyIdentity.destUrl = "/default.aspx";
              
            //}
            return ObjJsonVerifyIdentity;
        }

        [WebMethod]
        public static JsonVerifyIdentity VerifyIdentity(string email, string pwd)
        {
            JsonVerifyIdentity ObjJsonVerifyIdentity = new JsonVerifyIdentity();

            if (email != "lully" || pwd != "lully")
            {
                ObjJsonVerifyIdentity.errore = 1;
                ObjJsonVerifyIdentity.dettaglio="Errore di autenticazione";



            }
            else
            {
                ObjJsonVerifyIdentity.errore = 0;
                ObjJsonVerifyIdentity.destUrl = "/default.aspx";
                //ObjJsonVerifyIdentity.dettaglio = "";
            }
            return ObjJsonVerifyIdentity;
        }

        [DataContract]
        public class JsonVerifyIdentity
        {
            [DataMember]
            public int errore;
            [DataMember]
            public string dettaglio;
            [DataMember]
            public string destUrl;
        }

    }
}