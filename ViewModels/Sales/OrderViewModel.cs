using X.PagedList;

namespace ERP6.ViewModels.Sales
{
    public class OrderViewModel
    {
        /// <summary>
        /// 已出貨
        /// </summary>
        public int Shipped { get; set; }

        /// <summary>
        /// 未出貨
        /// </summary>
        public int NotShipped { get; set; }

        /// <summary>
        /// 訂單狀態
        /// </summary>
        public string OrderState { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 訂購時間
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 區域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 建檔日期
        /// </summary>
        public string KeyInDate { get; set; }

        /// <summary>
        /// 業務人員
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// 司機人員
        /// </summary>
        public string Driver { get; set; }

        public IPagedList<OrderList> orderList { get; set; }
    }

    public class OrderList
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public string OrderState { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 訂單時間
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 訂單金額
        /// </summary>
        public double? OrderAmount { get; set; }

        /// <summary>
        /// 建檔日期
        /// </summary>
        public string KeyInDate { get; set; }

        /// <summary>
        /// 業務人員
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// 司機人員
        /// </summary>
        public string Driver { get; set; }
    }
}
