using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels
{
    public class Out40Stock10Dto
    {
        public string PartNo { get; set; }
        public string Spec { get; set; }
        public string Barcode { get; set; }
        public string Unit { get; set; }
        public string TaxType { get; set; }
        public double? Price { get; set; }
        public double? LQty { get; set; }
        public double? StQty { get; set; }
    }
}
