using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class UserDTO
    {

        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string enabled { get; set; }
        public int landlordid { get; set; }
    }
}
