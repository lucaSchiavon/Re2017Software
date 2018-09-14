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
    public class RoleTypeConfiguration : LsEntityTypeConfiguration<Role>
    {
        public RoleTypeConfiguration(string schema) : 
            base(schema, "IdRole")
        {
            ToTable("Role");

            //************
            HasKey(x => x.IdRole);
            Property(x => x.IdRole).
            HasColumnName("IdRole").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]

            HasMany(e => e.Users)
             .WithOptional(e => e.Role)
             .HasForeignKey(e => e.IdRole)
             .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
