using ERP6.ViewComponent.NavListViewComponent.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewComponent.NavListViewComponent
{
    public class NavListViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private NavListService _service;
        public NavListViewComponent(NavListService service)
        {
            _service = service;
        }

        public IViewComponentResult InvokeAsync(string userAc)
        {
            var weather = _service.GetNavListData(userAc);

            return View(weather);
        }
    }
}
