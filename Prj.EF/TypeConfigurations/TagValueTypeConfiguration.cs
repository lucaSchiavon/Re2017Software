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
    public class TagValueTypeConfiguration : LsEntityTypeConfiguration<TagValue>
    {
        public TagValueTypeConfiguration(string schema) : 
            base(schema, "IdTagValue")
        {
            ToTable("TagValue");

            //************
            HasKey(x => x.IdTagValue);
            Property(x => x.IdTagValue).
            HasColumnName("IdTagValue").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]
            HasMany(e => e.Tags)
            .WithOptional(e => e.TagValue)
            .HasForeignKey(e => e.IdTagValue)
            .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
