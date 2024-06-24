using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Models
{
    /// <summary>
    /// 寄賣客戶庫存對帳表
    /// </summary>
    public class ConsignReconciliation
    {
        public string CusNo { get; set; }
        public string CusName { get; set; }
        public string BillingMonth { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public double PreInventory { get; set; }
        public double In { get; set; }
        public double Return { get; set; }
        public double CurrentInventory { get; set; }
        public double Sale { get; set; }
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double TaxFreeAmount { get; set; }
        public double TaxAmount { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public double CashDiscount { get; set; }
        public double Discount { get; set; }
        public double AmountNotCollected { get; set; }

    }
}
