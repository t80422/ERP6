using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.OGet
{
    public class IndexViewModel
    {
        public string Paymonth { get; set; }
        public string CoNo { get; set; }
        public string CoNoDDL { get; set; }
        public string Ntus { get; set; }
        public double? Total0 { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public string Accid { get; set; }
        public double? LnotGet { get; set; }
        public string Memo { get; set; }
        public double? TnotGet { get; set; }
        public double? Total { get; set; }
        public double? RetTotal { get; set; }
        public double? RetPercent { get; set; }
        public double? CashDiscount { get; set; }
        public List<OGetList> oGetList { get; set; }
        public string CoName { get; set; }
        public bool IsSearch { get; set; }
    }

    public class OGetList
    {
        public string Paymonth { get; set; }
        public string CoNo { get; set; }
        public string Ntus { get; set; }
        public double? Total0 { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public string Accid { get; set; }
        public double? LnotGet { get; set; }
        public string Memo { get; set; }
        public double? TnotGet { get; set; }
        public double? Total { get; set; }
        public double? RetTotal { get; set; }
        public double? RetPercent { get; set; }
        public double? CashDiscount { get; set; }
        public string CoName { get; set; }
    }
}
