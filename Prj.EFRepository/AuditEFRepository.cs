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
    public class AuditEFRepository : GenericEFRepository<AiChatboxContest, Audit>
    {
        public AuditEFRepository(string Username)
            : base (Username)
        {
            //this.Context.SetCurrentUser(Username);
        }
        //qui possiamo inserire altri metodi specifici per estrarre  categorie
        public List<Audit> SelectAudits(Int32 IdUser)
        {
            List<Audit> Obj = null;
            using (AuditEFRepository Rep = new AuditEFRepository(""))
            {

                Obj = Rep.Context.Audits.Where(x => x.IdUser == IdUser).ToList();

            }

            return Obj;

        }
    }
}
