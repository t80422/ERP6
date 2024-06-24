using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Order20
    {
        public string OrderNo { get; set; }
        public int Serno { get; set; }
        public string GoodNo { get; set; }
        public string Ntus { get; set; }
        public double? Qty { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public string Barcode { get; set; }
        public string SendDate { get; set; }
        public string YnClose { get; set; }
        public string OutNo { get; set; }
        public double? OutQty { get; set; }
        public string YnWork { get; set; }
        public string Packing { get; set; }
        public string Cno { get; set; }
        public string Ctnsize { get; set; }
        public double? Pcsctn { get; set; }
        public int? Ctn { get; set; }
        public double? Pcs { get; set; }
        public double? Nw1 { get; set; }
        public double? Nw2 { get; set; }
        public double? Gw1 { get; set; }
        public double? Gw2 { get; set; }
        public double? Cuft1 { get; set; }
        public double? Cuft2 { get; set; }
        public double? WorkQty { get; set; }
        public string WorkOk { get; set; }
    }
}
