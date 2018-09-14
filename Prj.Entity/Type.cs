namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class Type: LsEntity
    {

        public Type()
        {
            Documents = new HashSet<Document>();
        }

        public int IdTypology { get; set; }

      
        public string Typology { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
