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
    public class DocumentDetailPageManager
    {

        public Document GetSelectedDocument(int IdEntity)
        {
            Document Ent = null;

            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {
                Ent = Rep.Context.Documents.Where(x => x.IdDocument == IdEntity).FirstOrDefault();

            }

            return Ent;
        }

        public List<Ls.Prj.Entity.Type> GetTypes()
        {
            List<Ls.Prj.Entity.Type> LstType = new List<Ls.Prj.Entity.Type>();
            using (TypeEFRepository Rep = new TypeEFRepository(""))
            {
                LstType = Rep.Context.Types.ToList();

            }

            return LstType;
        }



    }
}