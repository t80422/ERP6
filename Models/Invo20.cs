using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Invo20
    {
        public string InvoiceNo { get; set; }
        public int Serno { get; set; }
        public string PartNo { get; set; }
        public double? Qty { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
    }
}
