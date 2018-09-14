using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class AuditDTO
    {

        public int IdAudit { get; set; }
        public string AuditUser { get; set; }
        public DateTime ModTime { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
      

    }
}
