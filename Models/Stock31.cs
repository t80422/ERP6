using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Stock31
    {
        public string PartNo { get; set; }
        public string InitDate { get; set; }
        public double? Cost { get; set; }
        public double? StQty { get; set; }
    }
}
