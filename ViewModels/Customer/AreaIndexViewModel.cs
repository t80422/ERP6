using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Customer
{
    public class AreaIndexViewModel
    {
        public string AreaNo { get; set; }

        public List<AreaList> areaLst { get; set; }
    }

    public class AreaList
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
