using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Out40
    {
        public string CoNo { get; set; }
        public string Paymonth { get; set; }
        public string Barcode { get; set; }
        public string PartNo { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public double? LQty { get; set; }
        public double? InQty { get; set; }
        public double? InretQty { get; set; }
        public double? OutQty { get; set; }
        public double? StQty { get; set; }
        public double? Amount { get; set; }
        public double? Discount { get; set; }
    }
}
