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
    public class TagPageManager
    {
       
        public List<TagDTO> GetTags()
        {

            using (TagEFRepository TagRep = new TagEFRepository(""))
            {
                List<Tag> Tags = TagRep.Context.Tags.ToList();
                List<TagDTO> LstTagDto = new List<TagDTO>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tag, TagDTO>()
                     .ForMember(dest => dest.IdTag, opt => opt.MapFrom(src => src.IdTag))
                      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => (((bool)(src.Description == "")) ? src.Description : ((bool)(src.Description.Length < 200)) ? src.Description : src.Description.Substring(0, 200) + "...")))
                     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                });
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Tag, TagDTO>()
                //     .ForMember(dest => dest.IdTag, opt => opt.MapFrom(src => src.Id))
                //      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                //     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                //});
                IMapper mapper = config.CreateMapper();

                LstTagDto = mapper.Map<List<Tag>, List<TagDTO>>(Tags);

                //foreach (User Usr in Users)
                //{
                //  List<Audit> LstAud = Usr.Audits.ToList();
                //    Role Rol = Usr.Role;
                //}

                return LstTagDto;
             
            }
        }

    }
}