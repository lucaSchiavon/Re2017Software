using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ls.Prj.Entity;
using Ls.Prj.EFRepository;
using System.Data.SqlClient;
using AutoMapper;
using Ls.Prj.DTO;

namespace AIChatbot.Classes
{
    public class UserPageManager
    {
       
        public List<UserDTO> GetUsers()
        {

            using (UserEFRepository UsrRep = new UserEFRepository(""))
            {
                List<User> Users = UsrRep.Context.Users.ToList();
                List<UserDTO> LstUserDto = new List<UserDTO>();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
                     .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES":"NO")));
                });
                IMapper mapper = config.CreateMapper();

                LstUserDto = mapper.Map<List<User>, List<UserDTO>>(Users);

                //foreach (User Usr in Users)
                //{
                //  List<Audit> LstAud = Usr.Audits.ToList();
                //    Role Rol = Usr.Role;
                //}

                return LstUserDto;
             
            }
        }

    }
}