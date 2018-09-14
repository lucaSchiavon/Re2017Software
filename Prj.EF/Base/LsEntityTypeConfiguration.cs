
using Ls.Base.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ls.Base.EF
{
   public class LsEntityTypeConfiguration<T> : EntityTypeConfiguration<T>
        where T: LsEntity, ILsEntity
    {
        public LsEntityTypeConfiguration(string schema = "dbo", string PkColumn = "")
        {
            ToTable(typeof(T).Name, schema);

            ////**********
            //if (!string.IsNullOrEmpty(PkColumn))
            //{

            //    HasKey(x => x.Id);
            //    Property(x => x.Id).
            //    HasColumnName(PkColumn).
            //    HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            //}
            //else
            //{
            //    Ignore(x => x.Id);
            //}
            ////**********

        }
    }
}
