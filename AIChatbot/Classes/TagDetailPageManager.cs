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
    public class TagDetailPageManager
    {

        public Tag GetSelectedTag(int IdEntity)
        {
            Tag Ent = null;

            using (TagEFRepository Rep = new TagEFRepository(""))
            {
                Ent = Rep.Context.Tags.Where(x => x.IdTag == IdEntity).FirstOrDefault();

            }

            return Ent;
        }
        public List<TagValue> GetTagValues()
        {
            List<TagValue> Lst = new List<TagValue>();
            using (TagValueEFRepository Rep = new TagValueEFRepository(""))
            {
                Lst = Rep.Context.TagValues.ToList();

            }

            return Lst;
        }


    }
}