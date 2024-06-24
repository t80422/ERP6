using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Stock
{
    public class StockExportViewModel
    {
        /// <summary>
        /// 產品編號(起)
        /// </summary>
        public string StartPartNo { get; set; }

        /// <summary>
        /// 產品編號(迄)
        /// </summary>
        public string EndPartNo { get; set; }

        /// <summary>
        /// 產品編號(起)(產品條碼標籤表)
        /// </summary>
        public string StartPartNoForBarcode { get; set; }

        /// <summary>
        /// 產品編號(迄)(產品條碼標籤表)
        /// </summary>
        public string EndPartNoForBarcode { get; set; }

        //public List<StockList> stockList { get; set; }

        /// <summary>
        /// 產品分類
        /// </summary>
        public string StockType { get; set; }

        /// <summary>
        /// 產品分類(產品條碼標籤表)
        /// </summary>
        public string StockTypeForBarcode { get; set; }

        /// <summary>
        /// 報表格式
        /// </summary>
        public string ReportFormat { get; set; }

        /// <summary>
        /// 報表排序
        /// </summary>
        public string ReportOrder { get; set; }

        /// <summary>
        /// 報表排序(產品條碼標籤表)
        /// </summary>
        public string ReportOrderForBarcode { get; set; }

        /// <summary>
        /// 張數設定
        /// </summary>
        public string SheetsSetting { get; set; }

        /// <summary>
        /// 是否為產品條碼標籤內容
        /// </summary>
        public string IsBarcode { get; set; }
    }
}
