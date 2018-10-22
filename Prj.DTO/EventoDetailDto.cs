using Ls.Prj.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class EventoDetailDTO
    {

        public int id { get; set; }
        public int eventTypeId { get; set; }
        public string description { get; set; }
        public Evento[] modelEvents { get; set; }
    }
}
