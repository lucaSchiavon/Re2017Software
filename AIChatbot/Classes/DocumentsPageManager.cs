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
    public class DocumentsPageManager
    {
       
        public List<DocumentDTO> GetDocs()
        {

            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {
                List<Document> Docs = Rep.Context.Documents.ToList();
                List<DocumentDTO> LstDocumentDTO = new List<DocumentDTO>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Document, DocumentDTO>()
                     .ForMember(dest => dest.IdDocument, opt => opt.MapFrom(src => src.IdDocument))
                      .ForMember(dest => dest.Typology, opt => opt.MapFrom(src => src.Type.Typology ))
                     .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => (((bool)src.Enabled) ? "YES" : "NO")));
                });
               
                IMapper mapper = config.CreateMapper();

                LstDocumentDTO = mapper.Map<List<Document>, List<DocumentDTO>>(Docs);

                //foreach (User Usr in Users)
                //{
                //  List<Audit> LstAud = Usr.Audits.ToList();
                //    Role Rol = Usr.Role;
                //}

                return LstDocumentDTO;
             
            }
        }

    }
}