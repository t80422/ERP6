using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Vendor
{
    public class VendorExportViewModel
    {
        public string StartVendorNo { get; set; }

        public string EndVendorNo { get; set; }

        public List<VendorList> vendorList { get; set; }

        public string VendorType { get; set; }
    }
}
