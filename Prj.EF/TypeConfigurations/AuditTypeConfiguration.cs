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
    public class AuditTypeConfiguration : LsEntityTypeConfiguration<Audit>
    {
        public AuditTypeConfiguration(string schema) : 
            base(schema, "IdAudit")
        {
            ToTable("Audit");
            //************
            HasKey(x => x.IdAudit);
            Property(x => x.IdAudit).
            HasColumnName("IdAudit").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]

            #endregion
        }
    }
}
