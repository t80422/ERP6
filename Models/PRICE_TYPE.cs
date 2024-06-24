using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Models
{
    public partial class PRICE_TYPE
    {
        public int PT_ID { get; set; }
        public DateTime PT_TIME { get; set; }
        public string PT_TEXT { get; set; }
        public string PT_VALUE { get; set; }
        public string PT_STOCKCOL { get; set; }
        public string PT_STATE { get; set; }
    }
}
