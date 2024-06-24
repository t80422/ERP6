using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Stock30
    {
        public string PartNo { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public double? Cost { get; set; }
        public double? StQty { get; set; }
        public string TaxType { get; set; }
        public string YnUpdate { get; set; }
    }
}
