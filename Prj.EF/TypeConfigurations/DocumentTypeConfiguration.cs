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
    public class DocumentTypeConfiguration : LsEntityTypeConfiguration<Document>
    {
        public DocumentTypeConfiguration(string schema) : 
            base(schema, "IdDocument")
        {
            ToTable("Document");

            //************
            HasKey(x => x.IdDocument);
            Property(x => x.IdDocument).
            HasColumnName("IdDocument").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]

            HasMany(e => e.Chapters)
          .WithOptional(e => e.Document)
          .HasForeignKey(e => e.IdDocument)
          .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
