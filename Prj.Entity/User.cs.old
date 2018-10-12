namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;


    public partial class User: LsEntity
    {
     
        public User()
        {
            Audits = new HashSet<Audit>();
        }

       
        public int IdUser { get; set; }

       
        public string Name { get; set; }

      
        public string UserId { get; set; }

       
        public string Pwd { get; set; }

        public int? IdRole { get; set; }

        public bool? Enabled { get; set; }

       
        public virtual ICollection<Audit> Audits { get; set; }

        public virtual Role Role { get; set; }
    }
}
