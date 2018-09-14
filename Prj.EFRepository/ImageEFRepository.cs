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
    public class ImageEFRepository : GenericEFRepository<AiChatboxContest, Image>
    {
        public ImageEFRepository(string Username)
            : base (Username)
        {
            //this.Context.SetCurrentUser(Username);
        }
        //qui possiamo inserire altri metodi specifici per estrarre  categorie
        public Image SelectEntity(Int32 IdEntity)
        {
            Image Obj = null;
            using (ImageEFRepository Rep = new ImageEFRepository(""))
            {

                Obj = Rep.Context.Images.Where(x => x.IdImage == IdEntity).ToList().FirstOrDefault();

            }

            return Obj;

        }
    }
}
