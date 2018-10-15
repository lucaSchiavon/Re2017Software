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
     public  class UserDetailPageManager
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

       public UserDetailPageManager()
        {
            client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region User

        public Utente GetUtente(int Id)
        {

            Utente Obj = null;

            Obj = GetAsyncUtente("users/" + Id.ToString()).Result;


            ////mapping su DTO
            //EventoDTO ObjEventoDto = new EventoDTO();
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Evento, EventoDTO>()
            //    .ForMember(dest => dest.amount, opt => opt.MapFrom(src => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:c}", src.amount)))
            //    .ForMember(dest => dest.date, opt => opt.MapFrom(src => string.Format("{0:MM/dd/yyyy}", src.date)));
            //});

            //IMapper mapper = config.CreateMapper();
            //ObjEventoDto = mapper.Map<Evento, EventoDTO>(ObjEvento);

            return Obj;

        }

        public async Task<Utente> GetAsyncUtente(string path)
        {
            Utente Obj = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Obj = await response.Content.ReadAsAsync<Utente>();
            }
            return Obj;
        }



        public void UpdateUtente(Utente ObjUtente)
        {

            var myContent = JsonConvert.SerializeObject(ObjUtente);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("users/" + ObjUtente.id, byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred during updating of the user.");
            }
        }

        public void NewUtente(Utente ObjUtente)
        {

            var myContent = JsonConvert.SerializeObject(ObjUtente);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("users", byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred during creation of the user.");
            }
        }
        #endregion

        #region Roles

        public  List<string> GetRoles()
        {
            List<string> LstRoles = new List<string>();
            LstRoles = GetAsyncRoles("users/roles").Result;

            return LstRoles;
        }
        async Task<List<string>> GetAsyncRoles(string path)
        {
            List<string> Lst = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lst = await response.Content.ReadAsAsync<List<string>>();
            }
            return Lst;
        }
       

        #endregion

        




    }
}