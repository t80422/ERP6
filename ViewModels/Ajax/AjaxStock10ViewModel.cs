using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Ajax
{
    public class AjaxStock10ViewModel
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 規格
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double? initQty2 { get; set; }

        /// <summary>
        /// 廠牌
        /// </summary>
        public string Atti { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public double? Price1 { get; set; }

        /// <summary>
        /// 牌價
        /// </summary>
        public double? SPrice1 { get; set; }

        /// <summary>
        /// 促銷價
        /// </summary>
        public double? PPrice1 { get; set; }
    }
}
