using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UserAC { get; set; }

        /// <summary>
        /// 使用者密碼
        /// </summary>
        public string UserPW { get; set; }

        /// <summary>
        /// 新系統密碼
        /// </summary>
        public string NewPwd { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        public string CheckPwd { get; set; }
    }
}
