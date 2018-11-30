namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class Template: LsEntity
    {


        public int id { get; set; }
        public string description { get; set; }
        public int eventTypeId { get; set; }
        public Boolean disabled { get; set; }
    }
}
