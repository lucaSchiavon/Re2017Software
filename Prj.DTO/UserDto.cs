using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class UserDTO
    {

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public int IdRole { get; set; }
        public string Enabled { get; set; }

    }
}
