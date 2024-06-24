using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Fix10
    {
        public string FixNo { get; set; }
        public string FixType { get; set; }
        public string FixName { get; set; }
        public string SourceName { get; set; }
        public string Spec { get; set; }
        public double? Qty { get; set; }
        public string InDate { get; set; }
        public string SetPlace { get; set; }
        public string Unit { get; set; }
        public string TypeNo { get; set; }
        public string Remark { get; set; }
        public double? RepairAmt { get; set; }
        public double? InAmt { get; set; }
        public string RepairDate { get; set; }
        public int? Useyear { get; set; }
        public double? HoldAmt { get; set; }
        public double? ThisAmt { get; set; }
        public double? TotalAmt { get; set; }
        public double? NotAmt { get; set; }
        public string ThisDate { get; set; }
        public string YnCont { get; set; }
        public double? YnHoldAmt { get; set; }
        public int? YnUseyear { get; set; }
        public string Ntus { get; set; }
    }
}
