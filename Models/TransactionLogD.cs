using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class TransactionLogD
    {
        public string TrnDate { get; set; }
        public string TrnTime { get; set; }
        public string TrnSeq { get; set; }
        public string TrnUserid { get; set; }
        public string TrnDSeq { get; set; }
        public string ProgName { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
