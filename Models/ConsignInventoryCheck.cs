using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Models
{
    /// <summary>
    /// 寄賣客戶庫存盤點表
    /// </summary>
    public class ConsignInventoryCheck
    {
        public string CusNo { get; set; }
        public string CusName { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string BillingMonth { get; set; }
        public double PreInventory { get; set; }
        public double In { get; set; }
        public double Return { get; set; }
        public double Sale { get; set; }
        public double CurrentInventory { get; set; }
    }
}
