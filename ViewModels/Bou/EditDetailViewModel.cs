using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Bou
{
    public class EditDetailViewModel
    {
        public string QuNo { get; set; }
        public int Serno { get; set; }
        public string PartNo { get; set; }
        public string Spec { get; set; }
        public double? Qty { get; set; }
        public string Unit { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public double? Discount { get; set; }
        public string Memo { get; set; }
        public double? SPrice { get; set; }
        public double? Profit { get; set; }
        public string TaxType { get; set; }
        public List<Bou20List> bou20List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
