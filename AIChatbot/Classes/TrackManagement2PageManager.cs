using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Ls.Prj.Utility;
using Newtonsoft.Json;

namespace Re2017.Classes
{
     public  class TrackManagement2PageManager
    {

        #region "codice vecchio"

        //public List<AuditDTO> GetEventi()
        //{

        //    using (AuditEFRepository Rep = new AuditEFRepository(""))
        //    {


        //        List<Audit> Audits = Rep.Context.Audits.ToList();
        //        List<AuditDTO> LstAuditDto = new List<AuditDTO>();
        //        var config = new MapperConfiguration(cfg => {
        //            cfg.CreateMap<Audit, AuditDTO>()
        //            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.RoleName));
        //        });
        //        IMapper mapper = config.CreateMapper();

        //        LstAuditDto = mapper.Map<List<Audit>, List<AuditDTO>>(Audits);


        //        return LstAuditDto;

        //    }
        //}

        //public void InsertAudit(User CurrUsr, string Description)
        //{
        //    Audit NewAudit = new Audit();
        //    NewAudit.AuditUser = CurrUsr.Name;
        //    NewAudit.ModTime =DateTime.Now;
        //    NewAudit.Description = Description;
        //    NewAudit.IdUser = CurrUsr.IdUser;
        //    NewAudit.Role = CurrUsr.Role.RoleName;

        //    using (AuditEFRepository Rep = new AuditEFRepository(""))
        //    {
        //        Rep.Context.Audits.Add(NewAudit);
        //        Rep.Context.SaveChanges();
        //    }



        //}


        //public List<User> GetUsers()
        //{
        //    List<User> LstUser = new List<User>();
        //    using (UserEFRepository UserRep = new UserEFRepository(""))
        //    {
        //        LstUser = UserRep.Context.Users.ToList();

        //    }

        //    return LstUser;
        //}

        #endregion


         HttpClient client = new HttpClient();

       public TrackManagement2PageManager()
        {
            client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Eventi
        public List<EventoDTO> GetEventi(DateTime Da, DateTime A)
        {

            List<Evento> LstEventi = new List<Evento>();
            string da = Da.ToString("yyyy-MM-dd");
            string a = A.ToString("yyyy-MM-dd");
            LstEventi = GetAsyncEventi("events?startDate=" + da + "&endDate=" + a).Result;
            //LstEventi = GetAsyncEventi("events?startDate=2018-08-01&endDate=2018-08-31").Result;

            //mapping su DTO
            List<EventoDTO> LstEventoDto = new List<EventoDTO>();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Evento, EventoDTO>();
            });
            IMapper mapper = config.CreateMapper();
            LstEventoDto = mapper.Map<List<Evento>, List<EventoDTO>>(LstEventi);

            return LstEventoDto;

            #region codice nascosto
            //using (AuditEFRepository Rep = new AuditEFRepository(""))
            //{
            //    string sqlwhere = " where 1=1 ";

            //    if (IdUser != 0)
            //    {
            //        sqlwhere += " and iduser=" + IdUser;
            //    }

            //    if (Da != DateTime.MinValue && A != DateTime.MinValue)
            //    {
            //        sqlwhere += " and (ModTime>='" + Da + "' and ModTime<='" + new DateTime(A.Year, A.Month, A.Day, 23, 59, 59) + "') ";
            //    }
            //    if (Da != DateTime.MinValue && A == DateTime.MinValue)
            //    {
            //        sqlwhere += " and (ModTime>='" + Da + "') ";
            //    }
            //    if (Da == DateTime.MinValue && A != DateTime.MinValue)
            //    {
            //        sqlwhere += " and (ModTime<='" + new DateTime(A.Year, A.Month, A.Day, 23, 59, 59) + "') ";
            //    }

            //    List<Audit> Audits=null;
            //    if (sqlwhere != " where 1=1 ")
            //    {
            //        Audits = Rep.Context.Audits.SqlQuery("select * from Audit " + sqlwhere).ToList();
            //    }
            //    else
            //    {
            //        Audits = Rep.Context.Audits.ToList();
            //    }


            //    List <AuditDTO> LstAuditDto = new List<AuditDTO>();

            //    var config = new MapperConfiguration(cfg => {
            //        cfg.CreateMap<Audit, AuditDTO>();
            //    });
            //    IMapper mapper = config.CreateMapper();

            //    LstAuditDto = mapper.Map<List<Audit>, List<AuditDTO>>(Audits);


            //    return LstAuditDto;
            #endregion


        }

        async Task<List<Evento>> GetAsyncEventi(string path)
        {
            List<Evento> LstEvento = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                LstEvento = await response.Content.ReadAsAsync<List<Evento>>();
            }
            return LstEvento;
        }

        public void DeleteEvt(int IdEvt)
        {

            //var myContent = JsonConvert.SerializeObject(ObjUpdateHouseEvtInputDto);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.DeleteAsync("events/" + IdEvt).Result;
        }
        #endregion


        #region eventsType
        public List<EventTypeDTO> GetEventsType()
        {

            //client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            List<EventTypeDTO> LstEventType = new List<EventTypeDTO>();

            LstEventType = GetAsyncEventsType("event-types").Result;

            return LstEventType;
        }
        async Task<List<EventTypeDTO>> GetAsyncEventsType(string path)
        {
            List<EventTypeDTO> Lstevt = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lstevt = await response.Content.ReadAsAsync<List<EventTypeDTO>>();
            }
            return Lstevt;
        }

        #endregion

        #region House
        public List<HouseDTO> GetHouse()
        {

            //client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            List<HouseDTO> LstHouse = new List<HouseDTO>();

            LstHouse = GetAsyncHouse("houses/names").Result;

            return LstHouse;
        }
        async Task<List<HouseDTO>> GetAsyncHouse(string path)
        {
            List<HouseDTO> Lst = null;
           
           
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lst = await response.Content.ReadAsAsync<List<HouseDTO>>();
            }
            return Lst;
        }

        public void UpdateHouseEvt(UpdateHouseEvtInputDto ObjUpdateHouseEvtInputDto)
        {

            var myContent = JsonConvert.SerializeObject(ObjUpdateHouseEvtInputDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("events/" + ObjUpdateHouseEvtInputDto.id, byteContent).Result;
        }
       
        #endregion

    }
}