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
    public class DocumentEFRepository : GenericEFRepository<AiChatboxContest, Document>
    {
        public DocumentEFRepository(string Username)
            : base (Username)
        {
            //this.Context.SetCurrentUser(Username);
        }
        //qui possiamo inserire altri metodi specifici 
        public Document SelectEntity(Int32 IdEntity)
        {
            Document Obj = null;
            using (DocumentEFRepository Rep = new DocumentEFRepository(""))
            {

                Obj = Rep.Context.Documents.Where(x => x.IdDocument == IdEntity).ToList().FirstOrDefault();

            }

            return Obj;

        }
    }
}
