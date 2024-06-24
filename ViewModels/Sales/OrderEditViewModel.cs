using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Sales
{
    public class OrderEditViewModel
    {
        /// <summary>
        /// 訂單編號(出貨單編號)
        /// </summary>
        public string OutNo { get; set; }

        /// <summary>
        /// 訂單狀態
        /// </summary>
        public string OrderState { get; set; }

        /// <summary>
        /// 訂購時間
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 區域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 訂單明細(出貨單明細)
        /// </summary>
        public List<OutOrderDetail> OutDetail { get; set; }

        /// <summary>
        /// 產品分類
        /// </summary>
        public List<StockTitle> StockTitleList { get; set; }

        /// <summary>
        /// 產品列表
        /// </summary>
        public List<StockInfo> StockList { get; set; }

        /// <summary>
        /// 其他備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 業務人員
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// 司機人員
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 複製新訂單
        /// </summary>
        public string IsCopy { get; set; }
    }

    public class StockTitle
    {
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string Name { get; set; }
    }

    public class OutOrderDetail
    {
        /// <summary>
        /// OutDetailNo
        /// </summary>
        public string OutDetailNo { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 促銷
        /// </summary>
        public bool Sale { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public bool? Stats { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }
    }

    public class StockInfo
    {
        /// <summary>
        /// 是否促銷
        /// </summary>
        public bool IsPromotion { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public double? PackQty { get; set; }

        /// <summary>
        /// 販售狀態
        /// </summary>
        public bool IsSalesStatus { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string StPs { get; set; }

        /// <summary>
        /// 產品分類
        /// </summary>
        public string StockType { get; set; }
    }
}
