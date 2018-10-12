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
     public  class LoginPageManager
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

       public LoginPageManager()
        {
            client.BaseAddress = new Uri(Utility.ReadSetting("Re2017ApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Login
     


        public HttpResponseMessage LoginUser(LoginCredentialsDto ObjLoginCredentialsDto)
        {

            var myContent = JsonConvert.SerializeObject(ObjLoginCredentialsDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("users/login", byteContent).Result;
            return result;
        }
       

    
    
        #endregion


    

    }
}