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
     public  class UserPageManager
    {

         HttpClient client = new HttpClient();

       public UserPageManager()
        {
            client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Users
        public UserDTO GetUtente(int IdUsr)
        {

            Utente ObjUtente = null;

            ObjUtente = GetAsyncUtente("users/" + IdUsr.ToString()).Result;


            //mapping su DTO
            UserDTO UserDTO = new UserDTO();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Utente, UserDTO>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.lastName + " " + src.firstName))
                .ForMember(dest => dest.enabled, opt => opt.MapFrom(src => (((bool)src.active) ? "YES" : "NO")))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => PrintRoles(src.roles)));
            });

            IMapper mapper = config.CreateMapper();
            UserDTO = mapper.Map<Utente, UserDTO>(ObjUtente);

            return UserDTO;

        }

        public async Task<Utente> GetAsyncUtente(string path)
        {
            Utente ObjUtente = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                ObjUtente = await response.Content.ReadAsAsync<Utente>();
            }
            return ObjUtente;
        }

        public List<UserDTO> GetUtenti()
        {

            List<Utente> LstUtenti = new List<Utente>();
            LstUtenti = GetAsyncUtenti("users").Result;

            //mapping su DTO
            List<UserDTO> LstUserDTO = new List<UserDTO>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Utente, UserDTO>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.lastName + " " + src.firstName))
                .ForMember(dest => dest.enabled, opt => opt.MapFrom(src => (((bool)src.active) ? "YES" : "NO")))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => PrintRoles(src.roles)));
            });



            IMapper mapper = config.CreateMapper();
            LstUserDTO = mapper.Map<List<Utente>, List<UserDTO>>(LstUtenti);

            return LstUserDTO;


        }

        private string PrintRoles(string[] Roles)
        {
            string Rtn = "";
            foreach (string Role in Roles)
            {
                Rtn += Role + " ";
            }

            return Rtn;
        }

        async Task<List<Utente>> GetAsyncUtenti(string path)
        {
            List<Utente> Lst = null;
            HttpResponseMessage response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Lst = await response.Content.ReadAsAsync<List<Utente>>();
            }
            return Lst;
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

    }
}