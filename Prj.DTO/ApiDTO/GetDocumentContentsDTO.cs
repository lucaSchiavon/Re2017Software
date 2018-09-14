using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class GetDocumentContentsDTO
    {
       
        public GetDocGroupByChapDTO[] Documents
        {
            get;
            set;
        }

        public GetImagesDTO[] Images
        {
            get;
            set;
        }
      
    }
}
