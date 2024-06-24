using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Vendor
{
    public class VendorDetailViewModel
    {
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }

        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VendorNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string StockNo { get; set; }

        /// <summary>
        /// 日期範圍(起)
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 日期範圍(迄)
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 只顯示未過帳
        /// </summary>
        public bool OnlyShow { get; set; }

        /// <summary>
        /// 進貨單資料
        /// </summary>
        public List<In10DataList> in10DataList { get; set; }

        /// <summary>
        /// 銷貨單資料
        /// </summary>
        public List<OutDataList> outDataList { get; set; }

        /// <summary>
        /// 應收帳款
        /// </summary>
        public List<OGetDataList> oGetDataList { get; set; }

        /// <summary>
        /// 應付帳款
        /// </summary>
        public List<IPayDataList> iPayDataList { get; set; }

        /// <summary>
        /// 報價資料
        /// </summary>
        public List<BouDataList> bouDataList { get; set; }

        /// <summary>
        /// 收款紀錄
        /// </summary>
        public List<GCheckDataList> gCheckDataList { get; set; }

        /// <summary>
        /// 付款紀錄
        /// </summary>
        public List<PCheckDataList> pCheckDataList { get; set; }

        /// <summary>
        /// 採購紀錄
        /// </summary>
        public List<PurDataList> purDataList { get; set; }
    }

    /// <summary>
    /// 進貨資料
    /// </summary>
    public class In10DataList
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int SerNo { get; set; }

        /// <summary>
        /// 進貨日期
        /// </summary>
        public string InDate { get; set; }

        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VendorNo { get; set; }

        /// <summary>
        /// 廠商簡稱
        /// </summary>
        public string VeName { get; set; }

        /// <summary>
        /// 進貨單號
        /// </summary>
        public string InNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 品名規格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// %(折扣)
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public double? Amount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 過庫存
        /// </summary>
        public string StockPass { get; set; }
    }

    /// <summary>
    /// 出貨資料
    /// </summary>
    public class OutDataList
    {
        /// <summary>
        /// 出貨日期
        /// </summary>
        public string OutDate { get; set; }

        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }

        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string CoName { get; set; }

        /// <summary>
        /// 出貨單號
        /// </summary>
        public string OutNo { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        public string OutType { get; set; }

        /// <summary>
        /// 序號
        /// </summary>
        public int SerNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 品名規格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// %(折扣)
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public double? Amount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 過庫存
        /// </summary>
        public string StockPass { get; set; }
    }

    /// <summary>
    /// 應收帳款
    /// </summary>
    public class OGetDataList
    {
        /// <summary>
        /// 帳款月份
        /// </summary>
        public string PayMonth { get; set; }

        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }

        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string CoName { get; set; }

        /// <summary>
        /// 幣別
        /// </summary>
        public string Ntus { get; set; }

        /// <summary>
        /// 免稅金額
        /// </summary>
        public double? Total0 { get; set; }

        /// <summary>
        /// 應稅金額
        /// </summary>
        public double? Total1 { get; set; }

        /// <summary>
        /// 稅額
        /// </summary>
        public double? Tax { get; set; }

        /// <summary>
        /// 已收金額
        /// </summary>
        public double? YesGet { get; set; }

        /// <summary>
        /// 折讓金額
        /// </summary>
        public double? SubTot { get; set; }

        /// <summary>
        /// 本月未收
        /// </summary>
        public double? NotGet { get; set; }

        /// <summary>
        /// 前期未收
        /// </summary>
        public double? LNotGet { get; set; }

        /// <summary>
        /// 總未收額
        /// </summary>
        public double? TNotGet { get; set; }
    }

    /// <summary>
    /// 應付帳款
    /// </summary>
    public class IPayDataList
    {
        /// <summary>
        /// 帳款月份
        /// </summary>
        public string PayMonth { get; set; }

        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VendorNo { get; set; }

        /// <summary>
        /// 廠商簡稱
        /// </summary>
        public string VeName { get; set; }

        /// <summary>
        /// 幣別
        /// </summary>
        public string Ntus { get; set; }

        /// <summary>
        /// 免稅金額
        /// </summary>
        public double? Total0 { get; set; }

        /// <summary>
        /// 應稅金額
        /// </summary>
        public double? Total1 { get; set; }

        /// <summary>
        /// 稅額
        /// </summary>
        public double? Tax { get; set; }

        /// <summary>
        /// 已付金額
        /// </summary>
        public double? YesGet { get; set; }

        /// <summary>
        /// 折讓金額
        /// </summary>
        public double? SubTot { get; set; }

        /// <summary>
        /// 本月未付
        /// </summary>
        public double? NotGet { get; set; }

        /// <summary>
        /// 前期未付
        /// </summary>
        public double? LNotGet { get; set; }

        /// <summary>
        /// 總未付額
        /// </summary>
        public double? TNotGet { get; set; }
    }

    /// <summary>
    /// 報價資料
    /// </summary>
    public class BouDataList
    {
        /// <summary>
        /// 報價日期
        /// </summary>
        public string QuDate { get; set; }

        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 報價單號
        /// </summary>
        public string QuNo { get; set; }

        /// <summary>
        /// 序號
        /// </summary>
        public int SerNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 品名規格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// %(折扣)
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public double? Amount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }
    }

    /// <summary>
    /// 收款紀錄
    /// </summary>
    public class GCheckDataList
    {
        /// <summary>
        /// 收款日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string CoName { get; set; }

        /// <summary>
        /// 收款單號
        /// </summary>
        public string PayNo { get; set; }

        /// <summary>
        /// 付款類別
        /// </summary>
        public string Get_Type { get; set; }

        /// <summary>
        /// 帳款月份
        /// </summary>
        public string PayMonth { get; set; }

        /// <summary>
        /// 支票號碼
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string Dline { get; set; }

        /// <summary>
        /// 票據狀況
        /// </summary>
        public string CheckType { get; set; }

        /// <summary>
        /// 沖銷金額
        /// </summary>
        public double? PMoney { get; set; }

        /// <summary>
        /// 扣抵金額
        /// </summary>
        public double? MMoney { get; set; }

        /// <summary>
        /// 已沖銷
        /// </summary>
        public string YnPass { get; set; }

        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }
    }

    /// <summary>
    /// 付款紀錄
    /// </summary>
    public class PCheckDataList
    {
        /// <summary>
        /// 付款日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VendorNo { get; set; }

        /// <summary>
        /// 廠商簡稱
        /// </summary>
        public string VeName { get; set; }

        /// <summary>
        /// 付款單號
        /// </summary>
        public string PayNo { get; set; }

        /// <summary>
        /// 付款類別
        /// </summary>
        public string Get_Type { get; set; }

        /// <summary>
        /// 帳款月份
        /// </summary>
        public string PayMonth { get; set; }

        /// <summary>
        /// 支票號碼
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string Dline { get; set; }

        /// <summary>
        /// 票據狀況
        /// </summary>
        public string CheckType { get; set; }

        /// <summary>
        /// 沖銷金額
        /// </summary>
        public double? PMoney { get; set; }

        /// <summary>
        /// 扣抵金額
        /// </summary>
        public double? MMoney { get; set; }

        /// <summary>
        /// 已沖銷
        /// </summary>
        public string YnPass { get; set; }
    }

    /// <summary>
    /// 採購紀錄
    /// </summary>
    public class PurDataList
    {
        /// <summary>
        /// 採購日期
        /// </summary>
        public string PurDate { get; set; }

        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VendorNo { get; set; }

        /// <summary>
        /// 廠商簡稱
        /// </summary>
        public string VeName { get; set; }

        /// <summary>
        /// 採購單號
        /// </summary>
        public string PurNo { get; set; }

        /// <summary>
        /// 序號
        /// </summary>
        public int SerNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 品名規格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// %(折扣)
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 已進量
        /// </summary>
        public double? InQty { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }
    }
}
