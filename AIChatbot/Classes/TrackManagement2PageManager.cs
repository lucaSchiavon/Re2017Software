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

namespace Re2017.Classes
{
     public class TrackManagement2PageManager
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
        public List<EventoDTO> GetEventi(DateTime Da, DateTime A)
        {
            client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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


    }
}