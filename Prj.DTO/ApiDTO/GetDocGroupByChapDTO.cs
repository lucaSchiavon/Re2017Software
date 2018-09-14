using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class GetDocGroupByChapDTO
    {
        public GetDocGroupByChapDTO()
        {
            Chapters = new List<ChapDTO>();
        }
        public string DocName
        {
            get;
            set;
        }
        public string DocUrl
        {
            get;
            set;
        }
        public List<ChapDTO> Chapters
        {
            get;
            set;
        }

        //public List<GetDocumentContentsDTO> Chapters
        //{
        //    get;
        //    set;
        //}
     
    }
}
