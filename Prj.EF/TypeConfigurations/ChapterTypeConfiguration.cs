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
    public class ChapterTypeConfiguration : LsEntityTypeConfiguration<Chapter>
    {
        public ChapterTypeConfiguration(string schema) : 
            base(schema, "IdChapter")
        {
            ToTable("Chapter");

            HasKey(x => x.IdChapter);
            Property(x => x.IdChapter).
            HasColumnName("IdChapter").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            #region [ Relations ]

            #endregion
        }
    }
}
