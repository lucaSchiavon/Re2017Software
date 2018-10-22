using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Prj.DTO
{
    public class EventoDTO
    {

        public int id { get; set; }
        public int eventTypeId { get; set; }
        public int bankReportEntryId { get; set; }
        public object bankReportEntry { get; set; }
        public int houseId { get; set; }
        public int landlordId { get; set; }
        //public DateTime? date { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }
        // public double? amount { get; set; }
        public string amount { get; set; }
        public double? amountNoFormat { get; set; }
        public int invoiceId { get; set; }
        public DateTime? reminderDate { get; set; }
        public string reminderMessage { get; set; }
        public object file { get; set; }
        public string filePath { get; set; }


    }
}
