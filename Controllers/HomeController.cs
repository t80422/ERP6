using ERP6.Models;
using ERP6.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EEPEF01Context _context;

        public HomeController(ILogger<HomeController> logger, EEPEF01Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(HomeIndexViewModel vm)
        {
            var userData = _context.Users.Where(x => x.Userid == vm.UserAC).FirstOrDefault();

            if (userData == null)
            {
                return RedirectToAction("Index", "Login");
            }

            vm = new HomeIndexViewModel
            {
                UserAC = userData.Userid,
                NewPwd = userData.NewPwd,
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
