using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Out30VMs
{
    public class AddViewModel
    {
        public string CoNo { get; set; }
        public string Paymonth { get; set; }
        public string Memo { get; set; }
        public string Userid { get; set; }
        public string StockPass { get; set; }
        public double? Total0 { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public string TaxType { get; set; }
        public double? CashDis { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public double? Discount { get; set; }
        public List<Out30List> out30List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
