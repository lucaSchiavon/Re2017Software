using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class DocumentDTO
    {
        public int IdDocument { get; set; }
        public string DocName { get; set; }
        public string Argument { get; set; }
        public string Device { get; set; }
        public string Typology { get; set; }
        public string Enabled { get; set; }

    }
}
