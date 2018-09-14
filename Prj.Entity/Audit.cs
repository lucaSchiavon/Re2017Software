namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;


    public partial class Audit: LsEntity
    {

        //**********
        public int IdAudit { get; set; }
        //**********

        public int? IdUser { get; set; }

        public DateTime? ModTime { get; set; }

        public string Description { get; set; }

        public string AuditUser { get; set; }

        public virtual User User { get; set; }
        public string Role { get; set; }
    }
}
