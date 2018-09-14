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
    public class TagTypeConfiguration : LsEntityTypeConfiguration<Tag>
    {
        public TagTypeConfiguration(string schema) : 
            base(schema, "IdTag")
        {
            ToTable("Tag");

            //************
            HasKey(x => x.IdTag);
            Property(x => x.IdTag).
            HasColumnName("IdTag").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]

            #endregion
        }
    }
}
