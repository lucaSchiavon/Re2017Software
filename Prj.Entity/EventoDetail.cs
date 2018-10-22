namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class EventoDetail: LsEntity
    {
        public int id { get; set; }
        public int eventTypeId { get; set; }
        public string description { get; set; }
        public  Evento[] modelEvents { get; set; }
    }
}
