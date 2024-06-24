using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Customer
{
    public class CustomerAddViewModel
    {
        public bool Back { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CoNo { get; set; }

        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string Coname { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 發票抬頭
        /// </summary>
        public string Invocomp { get; set; }

        /// <summary>
        /// 結帳日期
        /// </summary>
        public string Dlien { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 單價列印
        /// </summary>
        public string PrintPrice { get; set; }

        /// <summary>
        /// 負責人
        /// </summary>
        public string Boss { get; set; }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public string Sales { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 稅別
        /// </summary>
        public string TaxType { get; set; }

        /// <summary>
        /// 電話一
        /// </summary>
        public string Tel1 { get; set; }

        /// <summary>
        /// 電話二
        /// </summary>
        public string Tel2 { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        public string Uniform { get; set; }

        /// <summary>
        /// 通訊地址
        /// </summary>
        public string Compaddr { get; set; }

        /// <summary>
        /// 倉庫地址
        /// </summary>
        public string Sendaddr { get; set; }

        /// <summary>
        /// 付款帳號
        /// </summary>
        public string Payaccount { get; set; }

        /// <summary>
        /// 付款銀行
        /// </summary>
        public string Paybank { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 網址
        /// </summary>
        public string Www { get; set; }
        public double? Prenotget { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }

        /// <summary>
        /// 負責業務
        /// </summary>
        public string PeNo { get; set; }

        /// <summary>
        /// 營業項目
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// 區域類別
        /// </summary>
        public string AreaNo { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }

        /// <summary>
        /// 計價幣別
        /// </summary>
        public string Ntus { get; set; }

        /// <summary>
        /// 稅率
        /// </summary>
        public double? Taxrate { get; set; }

        /// <summary>
        /// 行動電話
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 現金扣%
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 客戶類別
        /// </summary>
        public string CusType { get; set; }

        /// <summary>
        /// 售價類別
        /// </summary>
        public string PriceType { get; set; }

        /// <summary>
        /// 司機
        /// </summary>
        public string DriveNo { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
        public List<CustomerList> customerList { get; set; }
    }
}
