namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;
   

   
    public partial class Role: LsEntity
    {
      
        public Role()
        {
            Users = new HashSet<User>();
        }

       
        public int IdRole { get; set; }

        //[Column("Role")]
        //[StringLength(50)]
        //public string Role1 { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
