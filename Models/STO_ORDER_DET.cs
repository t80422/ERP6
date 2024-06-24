using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class STO_ORDER_DET
    {
        public int ORDD_ID { get; set; }
        public Nullable<System.DateTime> ORDD_TIME { get; set; }
        public Nullable<int> ORDD_STATE { get; set; }
        public string ORDD_FTIME { get; set; }
        public Nullable<int> ORDD_SALE { get; set; }
        public Nullable<int> ORDD_BOUGHT { get; set; }
        public string ORDD_PORDCODE { get; set; }
        public string ORDD_PORDNAME { get; set; }
        public Nullable<int> ORDD_PORDNUM { get; set; }
        public string ORDD_PS { get; set; }
        public string ORDD_LINK { get; set; }
        public string ORDD_PRODCLASS { get; set; }
        public Nullable<decimal> ORDD_PRODCOST { get; set; }
    }
}
