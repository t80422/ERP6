using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{ 
    public partial class STO_ORDERF
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
