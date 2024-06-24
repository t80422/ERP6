using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Out30VMs
{
    public class ExportVM
    {
        public string CusNo_Start { get; set; }
        public string CusNo_End { get; set; }
        public string ProductNo_Start { get; set; }
        public string ProductNo_End { get; set; }

        /// <summary>
        /// 庫存閥值
        /// </summary>
        public int? StockThreshold { get; set; }

        /// <summary>
        /// 結帳月份起始值
        /// </summary>
        public string BillingMonthStart {get; set;}

        /// <summary>
        /// 結帳月份結束值
        /// </summary>
        public string BillingMonthEnd { get; set; }
        public string PrintType { get; set; }
        public SelectList CustomerList { get; set; }
        public SelectList ProductList { get; set; }
    }
}
