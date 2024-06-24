using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Work40
    {
        public string DailyNo { get; set; }
        public int Serno { get; set; }
        public string WorkNo { get; set; }
        public double? 派工序 { get; set; }
        public string OrderNo { get; set; }
        public double? 訂單序 { get; set; }
        public string GoodNo { get; set; }
        public string PartName { get; set; }
        public string Spec { get; set; }
        public string 預計生產 { get; set; }
        public string 累計生產 { get; set; }
        public double? Qty { get; set; }
        public double? Peponum { get; set; }
        public string Workvalue { get; set; }
        public string Btime { get; set; }
        public string Etime { get; set; }
        public int? TotHour { get; set; }
        public int? TotMin { get; set; }
        public string OkWork { get; set; }
        public double? Price { get; set; }
    }
}
