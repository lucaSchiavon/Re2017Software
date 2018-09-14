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
    public class TypeTypeConfiguration : LsEntityTypeConfiguration<Ls.Prj.Entity.Type>
    {
        public TypeTypeConfiguration(string schema) : 
            base(schema, "IdTypology")
        {
            ToTable("Type");

            //************
            HasKey(x => x.IdTypology);
            Property(x => x.IdTypology).
            HasColumnName("IdTypology").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]
            HasMany(e => e.Documents)
            .WithOptional(e => e.Type)
            .HasForeignKey(e => e.IdTypology)
            .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
