using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Bank20
    {
        public string IoNo { get; set; }
        public double? InMoney { get; set; }
        public double? OutMoney { get; set; }
        public string IoDate { get; set; }
        public string DeNo { get; set; }
        public string DeMemo { get; set; }
        public string Accid { get; set; }
        public string CrMemo { get; set; }
        public string InBankno { get; set; }
        public string OutBankno { get; set; }
        public string CrNo { get; set; }
    }
}
