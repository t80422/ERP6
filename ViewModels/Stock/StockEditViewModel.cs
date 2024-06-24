using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Stock
{
    public class StockEditViewModel
    {
        public string PartNo { get; set; }
        public string Barcode { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public double? Cost1 { get; set; }
        public double? Cost2 { get; set; }
        public double? Cost3 { get; set; }
        public double? SalesCost { get; set; }
        public double? Price1 { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }
        public string TaxType { get; set; }
        public string Type { get; set; }
        public string Atti { get; set; }
        public string LastIn { get; set; }
        public double? LastCost { get; set; }
        public double? LastDiscount { get; set; }
        public string LastOut { get; set; }
        public double? SafeQty { get; set; }
        public double? StQty { get; set; }
        public string Location { get; set; }
        public double? TranPara1 { get; set; }
        public double? TranPara2 { get; set; }
        public double? TranPara3 { get; set; }
        public double? SPrice1 { get; set; }
        public double? SPrice2 { get; set; }
        public double? SPrice3 { get; set; }
        public string TUnit1 { get; set; }
        public string TUnit2 { get; set; }
        public string TUnit3 { get; set; }
        public string YnCount { get; set; }
        public double? PackQty { get; set; }
        public double? InitQty1 { get; set; }
        public double? InitQty2 { get; set; }
        public double? InitCost1 { get; set; }
        public double? InitCost2 { get; set; }
        public double? InitCost3 { get; set; }
        public double? InitCost4 { get; set; }
        public double? InitCost5 { get; set; }
        public double? CompCost { get; set; }
        public double? L { get; set; }
        public double? W { get; set; }
        public double? H { get; set; }
        public double? Cuft { get; set; }
        public string LastModidate { get; set; }
        public string LastModiuser { get; set; }

        public string ST_PS { get; set; }
        public bool IsOpen { get; set; }
        public bool IsSearch { get; set; }
        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool IsShow { get; set; }
        public string UnitPara1 { get; set; }
        public string UnitPara2 { get; set; }
        public string UnitPara3 { get; set; }
        public double? SPrice4 { get; set; }
        public double? SPrice5 { get; set; }
        public double? SPrice6 { get; set; }
        public double? SPrice7 { get; set; }
        public double? SPrice8 { get; set; }
        public double? DefaultPrice1 { get; set; }
        public double? DefaultPrice2 { get; set; }
        public double? DefaultPrice3 { get; set; }
        public double? DefaultPrice4 { get; set; }
        public double? DefaultPrice5 { get; set; }
        public List<StockList> stockList { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }

        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

        public string IsReturn { get; set; }
    }
}
