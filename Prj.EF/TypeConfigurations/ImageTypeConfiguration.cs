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
    public class ImageTypeConfiguration : LsEntityTypeConfiguration<Image>
    {
        public ImageTypeConfiguration(string schema) : 
            base(schema, "IdImage")
        {
            ToTable("Image");

            //************
            HasKey(x => x.IdImage);
            Property(x => x.IdImage).
            HasColumnName("IdImage").
            HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //*************

            #region [ Relations ]

            #endregion
        }
    }
}
