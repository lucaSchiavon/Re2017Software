namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

    
    public partial class Tag: LsEntity
    {
      
       public int IdTag { get; set; }

       
        public string TagName { get; set; }

      
        public string Machine { get; set; }

        public string Description { get; set; }

        public bool? Enabled { get; set; }

        public string Node { get; set; }

        public string Device { get; set; }

        public string ValueType { get; set; }

        public int? IdTagValue { get; set; }

        public int? Alarm { get; set; }

        public virtual TagValue TagValue { get; set; }
    }
}
