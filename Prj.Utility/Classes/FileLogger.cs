using Prj.Utility.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Prj.Utility.Classes
{
    public class FileLogger : LogBase
    {
        // public string filePath = @"D:\IDGLog.txt";
        private string filePath;// = HttpContext.Current.Server.MapPath("~/Public/Log/");

        private string _LogName;
        public string LogName
        {
            get
            {

                return _LogName;
            }
        }

        public FileLogger(string FilePath)
            {
            filePath = FilePath;
            }
        public override void Log(string Message, Typology TypeErr)
        {
            string DataOra = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            _LogName = "Log_" + DataOra + ".txt";
            string LogPath = filePath + LogName;
            using (StreamWriter streamWriter = new StreamWriter(LogPath, true))
            {
                string TipoMsg = "";
                if (TypeErr !=Typology.None)
                {
                    TipoMsg = "[" + Enum.GetName(typeof(Typology), TypeErr) + "] ";
                }

                streamWriter.WriteLine("[" + DateTime.Now + "] " + TipoMsg + Message);
                streamWriter.Close();
            }
        }
    }
}
