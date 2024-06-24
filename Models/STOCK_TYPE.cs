using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Models
{
    public partial class STOCK_TYPE
    {
        public int TYPE_ID { get; set; }
        public DateTime TYPE_TIME { get; set; }
        public string TYPE_NAME { get; set; }
        public int? TYPE_ORDER { get; set; }
        public bool TYPE_ISOPEN { get; set; }
        public string TYPE_STATE { get; set; }
        
    }
}
