using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class GetAuditDTO
    {
       
        public string Data
        {
            get;
            set;
        }
       
        public string Description
        {
            get;
            set;
        }
        public string Node
        {
            get;
            set;
        }

        public string Device
        {
            get;
            set;
        }

        public string TagValue
        {
            get;
            set;
        }
    }
}
