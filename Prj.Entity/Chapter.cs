namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

    
    public partial class Chapter: LsEntity
    {
       
        public int IdChapter { get; set; }

       
        public string ChapterName { get; set; }

       
        public string Argument { get; set; }

        
        public string Device { get; set; }

        public string Alias { get; set; }

        public int? PageNumber { get; set; }

        public int? IdDocument { get; set; }

        public bool? Enabled { get; set; }

        public virtual Document Document { get; set; }
    }
}
