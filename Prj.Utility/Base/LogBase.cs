using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Utility.Base
{
    public enum Typology {Info=1,Warning=2,Error=3,None=4 }
    public abstract class LogBase
    {
        public abstract void Log(string Message, Typology TypeErr );
    }
}
