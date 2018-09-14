namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class TagValue: LsEntity
    {

        public TagValue()
        {
            Tags = new HashSet<Tag>();
        }

        public int IdTagValue { get; set; }

      
        public string TValue { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
