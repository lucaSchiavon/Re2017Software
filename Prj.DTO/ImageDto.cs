using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class ImageDTO
    {
        public int IdImage { get; set; }
        public string UrlImageSmall { get; set; }
        public string UrlImage { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Argument { get; set; }
        public string Device { get; set; }
        public string Enabled { get; set; }

    }
}
