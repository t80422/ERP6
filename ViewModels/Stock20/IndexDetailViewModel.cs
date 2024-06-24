using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Stock20
{
    public class IndexDetailViewModel
    {
        public string SpNo { get; set; }
        public int Serno { get; set; }
        public string PartNo { get; set; }
        public double? Oldprice { get; set; }
        public double? Oldsprice { get; set; }
        public double? Newprice { get; set; }
        public double? Newsprice { get; set; }
        public string Memo { get; set; }
        public double? Qty { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string TaxType { get; set; }
        public List<Stock21List> stock21List { get; set; }
        public bool IsSearch { get; set; }
    }
}
