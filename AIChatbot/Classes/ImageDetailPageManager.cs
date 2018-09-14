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
    public class ImageDetailPageManager
    {

        public Image GetSelectedImage(int IdEntity)
        {
            Image Ent = null;

            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {
                Ent = Rep.Context.Images.Where(x => x.IdImage == IdEntity).FirstOrDefault();

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