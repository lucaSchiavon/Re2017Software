using Prj.Utility.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Utility.Classes
{
    public class EventLogger : LogBase
    {

        public override void Log(string Message, Typology TypeErr)
        {
            //EventLog eventLog = new EventLog("");
            //eventLog.Source = "IDGEventLog";
            //eventLog.WriteEntry(message);
        }
    }
}
