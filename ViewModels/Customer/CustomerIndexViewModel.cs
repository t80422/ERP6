using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Customer
{
    public class CustomerIndexViewModel
    {
        public string CoNo { get; set; }
        public string Coname { get; set; }
        public string Company { get; set; }
        public string Invocomp { get; set; }
        public string Dlien { get; set; }
        public string Fax { get; set; }
        public string PrintPrice { get; set; }
        public string Boss { get; set; }
        public string Sales { get; set; }
        public string Memo { get; set; }
        public string TaxType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Uniform { get; set; }
        public string Compaddr { get; set; }
        public string Sendaddr { get; set; }
        public string Payaccount { get; set; }
        public string Paybank { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public double? Prenotget { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public string PeNo { get; set; }
        public string Product { get; set; }
        public string AreaNo { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }
        public string Ntus { get; set; }
        public double? Taxrate { get; set; }
        public string Mobile { get; set; }
        public double? Discount { get; set; }
        public string CusType { get; set; }
        public string PriceType { get; set; }
        public string DriveNo { get; set; }
        public bool IsSearch { get; set; }
        public string ParentId { get; set; }
        public List<CSTMList> customerList { get; set; }

        #region 子公司相關欄位
        public string OldCoNo { get; set; }
        public string NewItemCoNo { get; set; }
        public string NewItemConame { get; set; }
        public string NewItemCompany { get; set; }
        public string NewItemInvocomp { get; set; }
        public string NewItemDlien { get; set; }
        public string NewItemFax { get; set; }
        public string NewItemPrintPrice { get; set; }
        public string NewItemBoss { get; set; }
        public string NewItemSales { get; set; }
        public string NewItemMemo { get; set; }
        public string NewItemTaxType { get; set; }
        public string NewItemTel1 { get; set; }
        public string NewItemTel2 { get; set; }
        public string NewItemUniform { get; set; }
        public string NewItemCompaddr { get; set; }
        public string NewItemSendaddr { get; set; }
        public string NewItemPayaccount { get; set; }
        public string NewItemPaybank { get; set; }
        public string NewItemEmail { get; set; }
        public string NewItemWww { get; set; }
        public double? NewItemPrenotget { get; set; }
        public double? NewItemTotal1 { get; set; }
        public double? NewItemTax { get; set; }
        public double? NewItemTotal2 { get; set; }
        public double? NewItemYesGet { get; set; }
        public double? NewItemSubTot { get; set; }
        public double? NewItemNotGet { get; set; }
        public string NewItemPeNo { get; set; }
        public string NewItemProduct { get; set; }
        public string NewItemAreaNo { get; set; }
        public string NewItemPayment { get; set; }
        public int? NewItemChehkDay { get; set; }
        public string NewItemNtus { get; set; }
        public double? NewItemTaxrate { get; set; }
        public string NewItemMobile { get; set; }
        public double? NewItemDiscount { get; set; }
        public string NewItemCusType { get; set; }
        public string NewItemPriceType { get; set; }
        public string NewItemDriveNo { get; set; }
        public string NewItemParentId { get; set; }

        #endregion

        public bool Back { get; set; }
    }

    public class CSTMList
    {
        public string CoNo { get; set; }
        public string CoName { get; set; }
    }

    public class CustomerList
    {
        public string CoNo { get; set; }
        public string Coname { get; set; }
        public string Company { get; set; }
        public string Invocomp { get; set; }
        public string Dlien { get; set; }
        public string Fax { get; set; }
        public string PrintPrice { get; set; }
        public string Boss { get; set; }
        public string Sales { get; set; }
        public string Memo { get; set; }
        public string TaxType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Uniform { get; set; }
        public string Compaddr { get; set; }
        public string Sendaddr { get; set; }
        public string Payaccount { get; set; }
        public string Paybank { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public double? Prenotget { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public string PeNo { get; set; }
        public string Product { get; set; }
        public string AreaNo { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }
        public string Ntus { get; set; }
        public double? Taxrate { get; set; }
        public string Mobile { get; set; }
        public double? Discount { get; set; }
        public string CusType { get; set; }
        public string PriceType { get; set; }
        public string DriveNo { get; set; }
        public string ParentId { get; set; }
    }
}
