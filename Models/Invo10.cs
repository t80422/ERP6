using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Invo10
    {
        public string InvoiceNo { get; set; }
        public string InvoDate { get; set; }
        public string VendorNo { get; set; }
        public string Paymonth { get; set; }
        public double? Total1 { get; set; }
        public string TaxType { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public string Memo { get; set; }
        public string Ynclose { get; set; }
        public string Type { get; set; }
        public string Userid { get; set; }
        public string StockPass { get; set; }
    }
}
