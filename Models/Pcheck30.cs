using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Pcheck30
    {
        public string PayNo { get; set; }
        public int Serno { get; set; }
        public string Paccno { get; set; }
        public string Memo { get; set; }
        public double? Pmoney { get; set; }
        public string Maccno { get; set; }
        public double? Mmoney { get; set; }
        public string Paymonth { get; set; }
        public string VendorNo { get; set; }
    }
}
