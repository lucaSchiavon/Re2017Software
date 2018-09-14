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
    public class ImagesPageManager
    {
       
        public List<ImageDTO> GetImgs()
        {

            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                List<Image> Imgs = Rep.Context.Images.ToList();
                List<ImageDTO> LstImageDTO = new List<ImageDTO>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Image, ImageDTO>()
                    .ForMember(dest => dest.UrlImageSmall, opt => opt.MapFrom(src =>("/Public/Photos/" + src.ImageName + "?w=60&h=60")))
                     .ForMember(dest => dest.UrlImage, opt => opt.MapFrom(src => ("/Public/Photos/" + src.ImageName )))
                     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                });
               
                IMapper mapper = config.CreateMapper();

                LstImageDTO = mapper.Map<List<Image>, List<ImageDTO>>(Imgs);

              

                return LstImageDTO;
             
            }
        }

    }
}