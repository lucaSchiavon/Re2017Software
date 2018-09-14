using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.Api.DTO
{
    public class GetImgDTO
    {
       
        public string ImageName
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public string Tag
        {
            get;
            set;
        }

        public string Argument
        {
            get;
            set;
        }
        public string Device
        {
            get;
            set;
        }
    }
}
