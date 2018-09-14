namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class Document: LsEntity
    {
       
        public Document()
        {
            Chapters = new HashSet<Chapter>();
        }
  
        public int IdDocument { get; set; }

        public string PDFName { get; set; }

        public string DocName { get; set; }

       
        public string Argument { get; set; }

       
        public string Device { get; set; }

        public string Alias { get; set; }


        public string DocNumer { get; set; }

        public int? IdTypology { get; set; }

        public bool? Enabled { get; set; }

       
        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual Type Type { get; set; }
    }
}
