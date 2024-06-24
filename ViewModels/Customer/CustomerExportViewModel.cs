using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Customer
{
    public class StockExportViewModel
    {
        public string StartCoNo { get; set; }

        public string EndCoNo { get; set; }

        public List<CustomerList> customerList { get; set; }

        public string CustomerType { get; set; }
    }
}
