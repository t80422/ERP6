using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class STO_ORDER
    {
        public int ORD_ID { get; set; }
        public Nullable<System.DateTime> ORD_TIME { get; set; }
        public Nullable<int> ORD_STATE { get; set; }
        public string ORD_FTIME { get; set; }
        public string ORD_CODE { get; set; }
        public Nullable<decimal> ORD_PAYNONEY { get; set; }
        public Nullable<decimal> ORD_RECMONEY { get; set; }
        public string ORD_RECTIME { get; set; }
        public string ORD_AREA { get; set; }
        public string ORD_CLIENT { get; set; }
        public string ORD_USERID { get; set; }
        public string ORD_USERNAME { get; set; }
        public Nullable<int> ORD_ORDER { get; set; }
        public string ORD_LINKID { get; set; }
        public string ORD_PS { get; set; }
        public string ORD_PTIME { get; set; }
    }
}
