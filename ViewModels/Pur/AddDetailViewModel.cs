using System.Collections.Generic;

namespace ERP6.ViewModels.Pur
{
    public class AddDetailViewModel
    {
        public string PurNo { get; set; }
        public int Serno { get; set; }
        public string PartNo { get; set; }
        public double? Qty { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public string Memo { get; set; }
        public double? InQty { get; set; }
        public double? Discount { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string TaxType { get; set; }
        public List<Pur20List> pur20List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
