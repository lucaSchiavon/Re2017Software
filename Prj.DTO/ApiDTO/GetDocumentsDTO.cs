using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class GetDocumentsDTO
    {
       
        public string DocName
        {
            get;
            set;
        }
       
        public string UrlDoc
        {
            get;
            set;
        }
        public string ChapterName
        {
            get;
            set;
        }

        public string PageNumber
        {
            get;
            set;
        }
    }
}
