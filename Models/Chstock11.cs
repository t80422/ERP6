using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Chstock11
    {
        public string ChNo { get; set; }
        public string PartNo { get; set; }
        public string InitDate { get; set; }
        public double? CheckQty { get; set; }
        public double? SubQty { get; set; }
    }
}
