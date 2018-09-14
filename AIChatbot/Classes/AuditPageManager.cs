using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;

using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;

namespace AIChatbot.Classes
{
    public class AuditPageManager
    {
        public List<AuditDTO> GetAudits()
        {

            //using (AuditEFRepository Rep = new AuditEFRepository(""))
            //{
               
             
            //    List<Audit> Audits = Rep.Context.Audits.ToList();
            //    List<AuditDTO> LstAuditDto = new List<AuditDTO>();
            //    var config = new MapperConfiguration(cfg => {
            //        cfg.CreateMap<Audit, AuditDTO>()
            //        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.RoleName));
            //    });
            //    IMapper mapper = config.CreateMapper();

            //    LstAuditDto = mapper.Map<List<Audit>, List<AuditDTO>>(Audits);


            //    return LstAuditDto;

            //}



            List<AuditDTO> Audits = new List<AuditDTO>();
            return Audits;
        }
        public List<AuditDTO> GetAudits(DateTime Da, DateTime A, int IdUser)
        {


            //    using (AuditEFRepository Rep = new AuditEFRepository(""))
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



            //    //List<Audit> Audits = Rep.Context.Audits.SqlQuery("select [IdUser],[ModTime],[Description],[AuditUser] from audit").ToList(); //where Id=" + IdUser
            //    //List<Audit> Audits = Rep.Context.Audits.SqlQuery("select * from audit where iduser=@Iduser" , IdUser).ToList();
            //    //using (AuditEFRepository Rep2 = new AuditEFRepository(""))
            //    //{
            //    //    List<Ls.Prj.Entity.Audit> Audits = Rep2.Context.Audits.SqlQuery("select * from Audit").ToList(); //where Id=" + IdUser
            //    //}
            //    List<Audit> Audits=null;
            //    if (sqlwhere != " where 1=1 ")
            //    {
            //        Audits = Rep.Context.Audits.SqlQuery("select * from Audit " + sqlwhere).ToList();
            //    }
            //    else
            //    {
            //        Audits = Rep.Context.Audits.ToList();
            //    }


            //    //List<Audit> Audits = Rep.Context.Audits.ToList();

            //    List <AuditDTO> LstAuditDto = new List<AuditDTO>();
            //    //var config = new MapperConfiguration(cfg => {
            //    //    cfg.CreateMap<Audit, AuditDTO>()
            //    //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.RoleName));
            //    //});
            //    var config = new MapperConfiguration(cfg => {
            //        cfg.CreateMap<Audit, AuditDTO>();
            //    });
            //    IMapper mapper = config.CreateMapper();

            //    LstAuditDto = mapper.Map<List<Audit>, List<AuditDTO>>(Audits);


            //    return LstAuditDto;

            //}

            //**********
            List<AuditDTO> Audits = new List<AuditDTO>();
            return Audits;
        }

        public void InsertAudit(User CurrUsr, string Description)
        {
            Audit NewAudit = new Audit();
            NewAudit.AuditUser = CurrUsr.Name;
            NewAudit.ModTime =DateTime.Now;
            NewAudit.Description = Description;
            NewAudit.IdUser = CurrUsr.IdUser;
            NewAudit.Role = CurrUsr.Role.RoleName;

            //using (AuditEFRepository Rep = new AuditEFRepository(""))
            //{
            //    Rep.Context.Audits.Add(NewAudit);
            //    Rep.Context.SaveChanges();
            //}



        }


        public List<User> GetUsers()
        {
            List<User> LstUser = new List<User>();
            //using (UserEFRepository UserRep = new UserEFRepository(""))
            //{
            //    LstUser = UserRep.Context.Users.ToList();

            //}

            return LstUser;
        }
    }
}