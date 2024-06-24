using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{    
    public partial class STO_AREA
    {
        public int AREA_ID { get; set; }
        public Nullable<System.DateTime> AREA_TIME { get; set; }
        public string AREA_CODE { get; set; }
        public string AREA_NAME { get; set; }
        public string AREA_OWNER { get; set; }
        public string AREA_CLIENT { get; set; }
        public string AREA_GROUP { get; set; }
        public Nullable<int> AREA_ORDER { get; set; }
        public Nullable<int> AREA_STATE { get; set; }
    }
}
