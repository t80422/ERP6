using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewComponent.NavListViewComponent.ServiceModel
{
    public class NavListModel
    {
        public string Name { get; set; }

        public List<NavGroupList> navGroupList { get; set; }
    }

    public class NavGroupList
    {
        /// <summary>
        /// GroupId
        /// </summary>
        string GroupId { get; set; }

        public List<NavGroupItemList> NavGroupItemList { get; set; }
    }

    public class NavGroupItemList
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Caption { get; set; }
    }
}
