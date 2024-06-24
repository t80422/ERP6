using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.OutNew
{
    public class OutNewIndexViewModel
    {
        public string CoNo { get; set; }
        // 客戶編號
        public string KeyInDate { get; set; }
        // 建檔日期
        
        // 結帳日期
        public string OutNo { get; set; }
        // 出貨單號
        public string OutType { get; set; }
        // 出貨類別
        public string OutDate { get; set; }
        // 出貨日期
        public string PayMonth { get; set; }
        // 帳款月份
        public string Ntus { get; set; }
        // 計價幣別
        public string Memo { get; set; }
        // 備註
        public string PeNo { get; set; }
        // 業務人員
        public string DriveNo { get; set; }
        // 司機人員
        public List<Out10List> out10List { get; set; }
        // 出貨單列表
        public bool IsSearch { get; set; }
        // 是否搜尋
        public string Area { get; set; }
        //區域
        public int AreaOrder { get; set; }
        // 區域排序
    }

    public class Out10List
    {
        public string OutNo { get; set; }
        // 出貨單號
    }
}
