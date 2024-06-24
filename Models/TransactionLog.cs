using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class TransactionLog
    {
        public string TrnDate { get; set; }
        public string TrnTime { get; set; }
        public string TrnSeq { get; set; }
        public string TrnUserid { get; set; }
        public decimal? TrnSCount { get; set; }
        public decimal? TrnECount { get; set; }
        public string TrnName { get; set; }
        public string TrnIndex1 { get; set; }
        public string TrnIndex2 { get; set; }
        public string TrnErrorFlag { get; set; }
        public string TrnPackage { get; set; }
        public string TrnMethod { get; set; }
    }
}
