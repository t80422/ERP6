using ERP6.Models;
using ERP6.ViewComponent.NavListViewComponent.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewComponent.NavListViewComponent.Service
{
    public class NavListService
    {
        private readonly EEPEF01Context _context;

        public NavListService(EEPEF01Context context)
        {
            _context = context;
        }

        public List<NavListModel> GetNavListData(string userAc)
        {
            var data = (from userGroups in _context.Usergroups
                        join groupMenus in _context.Groupmenus on userGroups.Groupid equals groupMenus.Groupid
                        join menuTable in _context.Menutable on groupMenus.Menuid equals menuTable.Menuid
                        where userGroups.Userid == userAc
                        select new NavListModel
                        {

                        }).Take(5).ToList();

            return data;
        }
    }
}
