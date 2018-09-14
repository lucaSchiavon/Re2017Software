using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ls.Prj.Entity;
using Ls.Base.EF;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ls.Prj.EF.TypeConfigurations
{
    public class UserTypeConfiguration : LsEntityTypeConfiguration<User>
    {
        public UserTypeConfiguration(string schema) : 
            base(schema, "IdUser")
        {
            ToTable("User");

            //************
            HasKey(x => x.IdUser);
            Property(x => x.IdUser).
            HasColumnName("IdUser").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]
            HasMany(e => e.Audits)
             .WithOptional(e => e.User)
             .HasForeignKey(e => e.IdUser)
             .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
