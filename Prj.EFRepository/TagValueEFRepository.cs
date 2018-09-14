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
    public class TagValueEFRepository : GenericEFRepository<AiChatboxContest, TagValue>
    {
        public TagValueEFRepository(string Username)
            : base (Username)
        {
            //this.Context.SetCurrentUser(Username);
        }
        //qui possiamo inserire altri metodi specifici per estrarre  categorie
       
    }
}
