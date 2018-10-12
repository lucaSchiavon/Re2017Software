namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class Utente: LsEntity
    {
       
      
        public int id { get; set; }
        public bool active { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string[] roles { get; set; }
        public int landlordId { get; set; }
      
        
       
    }
}
