using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ls.Base.EFRepository;
using Ls.Prj.EF;
using Ls.Prj.Entity;
using AutoMapper;

namespace Ls.Prj.EFRepository
{
    public class TagEFRepository : GenericEFRepository<AiChatboxContest, Tag>
    {
        public TagEFRepository(string Username)
            : base (Username)
        {
            //this.Context.SetCurrentUser(Username);
        }
        //qui possiamo inserire altri metodi specifici per estrarre  Tag
        public Tag SelectEntity(Int32 IdEntity)
        {
            Tag ObjTag = null;
            using (TagEFRepository TagRep = new TagEFRepository(""))
            {

                ObjTag = TagRep.Context.Tags.Where(x => x.IdTag == IdEntity).ToList().FirstOrDefault();

            }

            return ObjTag;

        }
    }
}
