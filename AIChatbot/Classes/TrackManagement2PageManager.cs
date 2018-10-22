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
using System.Globalization;

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
        public EventoDTO GetEvento(int IdEvt)
        {

            Evento ObjEvento=null;

            ObjEvento = GetAsyncEvento("events/" + IdEvt.ToString()).Result;
          

            //mapping su DTO
            EventoDTO ObjEventoDto = new EventoDTO();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.amount, opt => opt.MapFrom(src => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:c}", src.amount)))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => string.Format("{0:MM/dd/yyyy}", src.date)));
            });

            IMapper mapper = config.CreateMapper();
            ObjEventoDto = mapper.Map<Evento, EventoDTO>(ObjEvento);

            return ObjEventoDto;

        }

        public async Task<Evento> GetAsyncEvento(string path)
        {
            Evento ObjEvento = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                ObjEvento = await response.Content.ReadAsAsync<Evento>();
            }
            return ObjEvento;
        }

        public List<EventoDTO> GetEventi(DateTime Da, DateTime A, int BankReportEntryId=0)
        {

            List<Evento> LstEventi = new List<Evento>();
            string da = Da.ToString("yyyy-MM-dd");
            string a = A.ToString("yyyy-MM-dd");

            if (BankReportEntryId == 0)
            {
                LstEventi = GetAsyncEventi("events?startDate=" + da + "&endDate=" + a).Result;
            }
            else
            {
                //ritorna solo i fratelli
                LstEventi = GetAsyncEventi("events?startDate=" + da + "&endDate=" + a + "&bankReportEntry=" + BankReportEntryId).Result;
            }
           
            //LstEventi = GetAsyncEventi("events?startDate=2018-08-01&endDate=2018-08-31").Result;

            //mapping su DTO
            List<EventoDTO> LstEventoDto = new List<EventoDTO>();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.amount, opt => opt.MapFrom(src => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:c}", src.amount)))
                 .ForMember(dest => dest.amountNoFormat, opt => opt.MapFrom(src => src.amount))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => string.Format("{0:MM/dd/yyyy}", src.date)));
            });
           
             //.ForMember(dest => string.Format(new System.Globalization.CultureInfo("en-GB"), "{0:c}", dest.amount), opt => opt.MapFrom(src => src.amount))
            //string us = dec.ToString("C", new CultureInfo("en-US"));
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Document, DocumentDTO>()
            //     .ForMember(dest => dest.IdDocument, opt => opt.MapFrom(src => src.IdDocument))
            //      .ForMember(dest => dest.Typology, opt => opt.MapFrom(src => src.Type.Typology))
            //     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
            //});

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
            //var result = client.DeleteAsync("events/" + IdEvt).Result;
            HttpResponseMessage response = client.DeleteAsync("events/" + IdEvt).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred during deletion or the record can't be deleted due to the constraint's existence");
            }
            //string res = await response.Content.ReadAsStringAsync().Result;
            //return await JsonConvert.DeserializeObjectAsync<T>(response);
        }

        public void NewEvt(Evento ObjInsertEvtInput)
        {

            var myContent = JsonConvert.SerializeObject(ObjInsertEvtInput);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("events", byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred during creation of the event.");
            }
        }

        public void UpdateBrotherEvt(UpdateBrotherEvtDto ObjUpdateBrotherEvtDto)
        {

            var myContent = JsonConvert.SerializeObject(ObjUpdateBrotherEvtDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("events/" + ObjUpdateBrotherEvtDto.id, byteContent).Result;
        }
        #region codice nascosto
        //public void NewEvtDto(InsertEvtInputDto ObjInsertEvtInputDto)
        //{

        //    var myContent = JsonConvert.SerializeObject(ObjInsertEvtInputDto);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    var result = client.PostAsync("events", byteContent).Result;
        //    if (!result.IsSuccessStatusCode)
        //    {
        //        throw new Exception("An error occurred during creation of the event.");
        //    }
        //}
        #endregion

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


        #region Landlord
        public List<LandlordDTO> GetLandlords()
        {

            //client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            List<LandlordDTO> Lst = new List<LandlordDTO>();

            Lst = GetAsyncLandlords("landlords/names").Result;

            return Lst;
        }
        async Task<List<LandlordDTO>> GetAsyncLandlords(string path)
        {
            List<LandlordDTO> Lst = null;


            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lst = await response.Content.ReadAsAsync<List<LandlordDTO>>();
            }
            return Lst;
        }

        public void UpdateLandlordEvt(UpdateLandlordEvtInputDto ObjUpdateLandlordEvtInputDto)
        {

            var myContent = JsonConvert.SerializeObject(ObjUpdateLandlordEvtInputDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("events/" + ObjUpdateLandlordEvtInputDto.id, byteContent).Result;
        }

        #endregion


        #region Model
        public List<TemplateDTO> GetTemplate()
        {
            List<TemplateDTO> LstModel = new List<TemplateDTO>();
            LstModel = GetAsyncModel("events/templates").Result;
            return LstModel;
        }
        async Task<List<TemplateDTO>> GetAsyncModel(string path)
        {
            List<TemplateDTO> Lst = null;


            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lst = await response.Content.ReadAsAsync<List<TemplateDTO>>();
            }
            return Lst;
        }

       
        #endregion
    }
}