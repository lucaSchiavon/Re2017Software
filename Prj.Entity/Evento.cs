namespace Ls.Prj.Entity
{
    using System;
    using System.Collections.Generic;
    using Ls.Base.Entity;

   
    public partial class Evento: LsEntity
    {
       
      
        public int id { get; set; }
        public int eventTypeId { get; set; }
        public int bankReportEntryId { get; set; }
        public object bankReportEntry { get; set; }
        public int houseId { get; set; }
        public DateTime? date { get; set; }
        public string description { get; set; }
        public double? amount { get; set; }
        public int invoiceId { get; set; }
        public DateTime? reminderDate { get; set; }
        public string reminderMessage { get; set; }
        public object file { get; set; }
        public string filePath { get; set; }
       
    }
}
