using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Out30
    {
        public string CoNo { get; set; }
        public string Paymonth { get; set; }
        public string Memo { get; set; }
        public string Userid { get; set; }
        public string StockPass { get; set; }
        public double? Total0 { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public string TaxType { get; set; }
        public double? CashDis { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public double? Discount { get; set; }
    }
}
