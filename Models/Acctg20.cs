using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Acctg20
    {
        public string Accid { get; set; }
        public int Serno { get; set; }
        public string Accno { get; set; }
        public double? Cmoney { get; set; }
        public double? Dmoney { get; set; }
        public string Memo { get; set; }
        public string Subaccno { get; set; }
    }
}
