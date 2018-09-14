namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

    
    public partial class Image: LsEntity
    {
        
        public int IdImage { get; set; }

        
        public string ImageName { get; set; }

       
        public string Description { get; set; }

        public string Tags { get; set; }

       
        public string Argument { get; set; }

       
        public string Device { get; set; }

        public string Alias { get; set; }

        public bool? Enabled { get; set; }
    }
}
