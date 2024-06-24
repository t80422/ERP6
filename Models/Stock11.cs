using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Stock11
    {
        public string PartNo { get; set; }
        public string InitDate { get; set; }
        public double? LastQty { get; set; }
        public double? InQty { get; set; }
        public double? OutQty { get; set; }
        public double? IsQty { get; set; }
        public double? IoQty { get; set; }
        public double? BalQty { get; set; }
        public double? CheckQty { get; set; }
        public double? SubQty { get; set; }
        public double? StQty { get; set; }
    }
}
