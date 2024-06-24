using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.In
{
    public class AddViewModel
    {
        public string InNo { get; set; }
        public string InDate { get; set; }
        public string VendorNo { get; set; }
        public string Memo { get; set; }
        public string Ntus { get; set; }
        public double? Total1 { get; set; }
        public string Userid { get; set; }
        public string Paymonth { get; set; }
        public string StockPass { get; set; }
        public string YnClose { get; set; }
        public string InvoiceNo { get; set; }
        public string InType { get; set; }
        public double? Total { get; set; }
        public double? YesGet { get; set; }
        public double? CashDis { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public double? Total0 { get; set; }
        public double? Discount { get; set; }
        public List<In10List> in10List { get; set; }
        public List<In20List> in20List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
