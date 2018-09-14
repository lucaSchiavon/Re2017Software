using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class AlmHistoryDTO
    {
       
        public string AH_NODE
        {
            get;
            set;
        }
       
        public string AH_DESCRIPTION
        {
            get;
            set;
        }
        public string AH_PRIORITY
        {
            get;
            set;
        }

        public string AH_DATEIN
        {
            get;
            set;
        }

        public string AH_DATELAST
        {
            get;
            set;
        }

        public string Device
        {
            get;
            set;
        }
    }
}
