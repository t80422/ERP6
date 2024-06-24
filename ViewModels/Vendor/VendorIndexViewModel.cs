using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Vendor
{
    public class VendorIndexViewModel
    {
        public string VendorNo { get; set; }
        public string Vename { get; set; }
        public string Company { get; set; }
        public string Invocomp { get; set; }
        public string Dlien { get; set; }
        public string Fax { get; set; }
        public string Boss { get; set; }
        public string Sales { get; set; }
        public string Memo { get; set; }
        public string TaxType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Uniform { get; set; }
        public string Invoaddr { get; set; }
        public string Factaddr { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public double? Prenotpay { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesPay { get; set; }
        public double? SubTot { get; set; }
        public double? NotPay { get; set; }
        public string Accno1 { get; set; }
        public string Product { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }
        public string Ntus { get; set; }
        public double? Taxrate { get; set; }
        public string Mobile { get; set; }
        public double? Discount { get; set; }
        public string PriceType { get; set; }
        public string Payaccount { get; set; }
        public string Paybank { get; set; }
        public string LineId { get; set; }
        public bool IsSearch { get; set; }
        public List<VendorList> vendorList { get; set; }
    }

    public class VendorList
    {
        public string VendorNo { get; set; }
        public string Vename { get; set; }
        public string Company { get; set; }
        public string Invocomp { get; set; }
        public string Dlien { get; set; }
        public string Fax { get; set; }
        public string Boss { get; set; }
        public string Sales { get; set; }
        public string Memo { get; set; }
        public string TaxType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Uniform { get; set; }
        public string Invoaddr { get; set; }
        public string Factaddr { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public double? Prenotpay { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesPay { get; set; }
        public double? SubTot { get; set; }
        public double? NotPay { get; set; }
        public string Accno1 { get; set; }
        public string Product { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }
        public string Ntus { get; set; }
        public double? Taxrate { get; set; }
        public string Mobile { get; set; }
        public double? Discount { get; set; }
        public string PriceType { get; set; }
        public string Payaccount { get; set; }
        public string Paybank { get; set; }
        public string LineId { get; set; }
    }
}
