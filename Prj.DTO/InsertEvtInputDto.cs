using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class InsertEvtInputDto
    {

        public double? amount { get; set; }
        public int bankReportEntryId { get; set; }
        //public string date { get; set; }
        public DateTime? date { get; set; }
        public string description { get; set; }
        public int eventTypeId { get; set; }
        public string filePath { get; set; }
        public int houseId { get; set; }
        public int id { get; set; }
        public int invoiceId { get; set; }
        //public string reminderDate { get; set; }
        public DateTime? reminderDate { get; set; }
        public string reminderMessage { get; set; }

    }
}
