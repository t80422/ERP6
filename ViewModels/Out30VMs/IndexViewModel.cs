using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Out30VMs
{
    public class IndexViewModel
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
        public List<Out40List> out40List { get; set; }
        public bool IsSearch { get; set; }
    }

    public class Out30List
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
    }

    public class Out40List
    {
        public string CoNo { get; set; }
        public string Paymonth { get; set; }
        public string Barcode { get; set; }
        public string PartNo { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public double? LQty { get; set; }
        public double? InQty { get; set; }
        public double? InretQty { get; set; }
        public double? OutQty { get; set; }
        public double? StQty { get; set; }
        public double? Amount { get; set; }
        public double? Discount { get; set; }
        public string Spec { get; set; }
        public string TaxType { get; set; }
    }
}
