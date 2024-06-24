using ERP6.Models;
using ERP6.ViewModels.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ERP6.Controllers
{
    public class OutController : Controller
    {
        private readonly EEPEF01Context _context;
        private readonly AjaxsController _ajax;

        public OutController(EEPEF01Context context, AjaxsController ajax)
        {
            _context = context;
            _ajax = ajax;
        }

        ///<summary>
        /// 首頁
        ///</summary>
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            var outTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="出貨", Value="1" },
                   new SelectListItem {Text="出貨退回", Value="2" },
                };
            ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

            //=====20240531=====
            //var dlienList = new List<SelectListItem>()
            //{
            //   new SelectListItem {Text="請選擇", Value="" },
            //   new SelectListItem {Text="15", Value="15" },
            //   new SelectListItem { Text = "20", Value = "20" },
            //   new SelectListItem {Text="25", Value="25" },
            //   new SelectListItem {Text="月底", Value="月底" },
            //   new SelectListItem {Text="21", Value="21" },
            //   new SelectListItem {Text="31", Value="31" },
            //};

            //ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);
            //=====20240531=====

            var USERS = _context.Users.ToList();

            if (!String.IsNullOrEmpty(vm.OutNo))
            {
                var outno = await _context.Out10.Where(x => x.OutNo == vm.OutNo).Select(x => new IndexViewModel
                {
                    OutNo = x.OutNo ?? "",
                    OutType = x.OutType ?? "",
                    OutDate = !string.IsNullOrEmpty(x.OutDate) ?
                                        DateTime.ParseExact(x.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : "",
                    Paymonth = !string.IsNullOrEmpty(x.Paymonth) ?
                                        DateTime.ParseExact(x.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : "",
                    Ntus = x.Ntus ?? "",
                    CoNo = x.CoNo ?? "",
                    Memo = x.Memo ?? "",
                    Total1 = (double)Math.Round((decimal)x.Total1, 2),
                    Userid = _context.Users.Where(y => y.Userid == x.Userid).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.Userid).FirstOrDefault().Username : "",
                    StockPass = x.StockPass ?? "",
                    YnClose = x.YnClose ?? "",
                    PeNo = x.PeNo ?? "",
                    KeyinDate = !string.IsNullOrEmpty(x.KeyinDate) ?
                                        DateTime.ParseExact((x.KeyinDate).PadRight(12, '0'), "yyyyMMddHHmm", null).ToString("yyyy-MM-dd") : "",
                    YesGet = (double)Math.Round((decimal)x.YesGet, 2),
                    CashDis = (double)Math.Round((decimal)x.CashDis, 2),
                    SubTot = (double)Math.Round((decimal)x.SubTot, 2),
                    NotGet = (double)Math.Round((decimal)x.NotGet, 2),
                    Total0 = (double)Math.Round((decimal)x.Total0, 2),
                    Total = (double)Math.Round((decimal)x.Total, 2),
                    DriveNo = x.DriveNo ?? "",
                    Discount = x.Discount ?? 0,
                    Tax = x.Tax ?? 0,
                    Kg = x.Kg ?? 0,
                    Promotion_No = x.Promotion_No ?? "",
                    Dlien = _context.Cstm10.Where(y => y.CoNo == x.CoNo).FirstOrDefault() != null ? _context.Cstm10.Where(y => y.CoNo == x.CoNo).FirstOrDefault().Dlien : "",
                    Editid = _context.Users.Where(y => y.Userid == x.EDITID).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.EDITID).FirstOrDefault().Username : "",
                    Tallyid = _context.Users.Where(y => y.Userid == x.TALLYID).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.TALLYID).FirstOrDefault().Username : ""
                }).FirstOrDefaultAsync();

                vm.OutType = outno != null ? outno.OutType : "";
                vm.OutDate = outno != null ? outno.OutDate : "";
                vm.Paymonth = outno != null ? outno.Paymonth : "";
                vm.Ntus = outno != null ? outno.Ntus : "";
                vm.CoNo = outno != null ? outno.CoNo : "";
                vm.Memo = outno != null ? outno.Memo : "";
                vm.Total1 = outno != null ? outno.Total1 : 0;
                vm.Userid = outno != null ? outno.Userid : "";
                vm.StockPass = outno != null ? outno.StockPass : "";
                vm.YnClose = outno != null ? outno.YnClose : "";
                vm.PeNo = outno != null ? outno.PeNo : "";
                vm.KeyinDate = outno != null ? outno.KeyinDate : "";
                vm.YesGet = outno != null ? outno.YesGet : 0;
                vm.CashDis = outno != null ? outno.CashDis : 0;
                vm.SubTot = outno != null ? outno.SubTot : 0;
                vm.NotGet = outno != null ? outno.NotGet : 0;
                vm.Total0 = outno != null ? outno.Total0 : 0;
                vm.Total = outno != null ? outno.Total : 0;
                vm.DriveNo = outno != null ? outno.DriveNo : "";
                vm.Discount = outno != null ? outno.Discount : 0;
                vm.Tax = outno != null ? outno.Tax : 0;
                vm.Kg = outno != null ? outno.Kg : 0;
                vm.Promotion_No = outno != null ? outno.Promotion_No : "";
                vm.Dlien = outno != null ? outno.Dlien : "";
                vm.Editid = outno != null ? outno.Editid : "";
                vm.Tallyid = outno != null ? outno.Tallyid : "";
                vm.out20List = await _context.Out20.Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new Out20List
                {
                    OutNo = x.OutNo ?? "",
                    Serno = x.Serno,
                    PartNo = x.PartNo ?? "",
                    Qty = x.Qty ?? 0,
                    Price = x.Price ?? 0,
                    Amount = (double)Math.Round((decimal)x.Amount, 2),
                    Discount = x.Discount ?? 0,
                    Unit = x.Unit ?? "",
                    Memo = x.Memo ?? "",
                    SPrice = x.SPrice ?? 0,
                    PPrice = x.PPrice ?? 0,
                    STQty = y.StQty ?? 0,
                }).Where(x => x.OutNo == vm.OutNo).OrderBy(x => x.Serno).ToListAsync();

            }

            // 依照區域排的客戶名單
            var CSTMLIST = await _context.Cstm10.Join(_context.STO_AREA, x => x.AreaNo, y => y.AREA_NAME, (x, y) => new
            {
                AREAID = x.AreaNo,
                CONO = x.CoNo,
                AREA = y.AREA_NAME,
                ORDER = y.AREA_ORDER,
                x.Dlien
            }).OrderBy(z => z.ORDER).ThenBy(z => z.AREAID).ToListAsync();

            // 所有出貨單 
            var OUT10LIST = await _context.Out10.OrderByDescending(x => x.OutNo).ToListAsync();

            // 所有按照區域、客戶排序的出貨單
            vm.out10List = await OUT10LIST.Join(CSTMLIST, x => x.CoNo, y => y.CONO, (x, y) => new Out10List
            {
                OutNo = x.OutNo ?? "",
                Area_Order = y.ORDER ?? 0,
                Area = y.AREA ?? "",
                OutType = x.OutType ?? "",
                OutDate = !string.IsNullOrEmpty(x.OutDate) ?
                                        DateTime.ParseExact(x.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : "",
                Paymonth = !string.IsNullOrEmpty(x.Paymonth) ?
                                        DateTime.ParseExact(x.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : "",
                Ntus = x.Ntus ?? "",
                CoNo = x.CoNo ?? "",
                CoName = _context.Cstm10.Find(x.CoNo)?.Coname,
                Memo = x.Memo ?? "",
                Total1 = (double)Math.Round((decimal)(x.Total1 ?? 0), 2),
                Userid = x.Userid ?? "",
                StockPass = x.StockPass ?? "",
                YnClose = x.YnClose ?? "",
                PeNo = x.PeNo ?? "",
                KeyinDate = !string.IsNullOrEmpty(x.KeyinDate) ?
                                        DateTime.ParseExact((x.KeyinDate).PadRight(12, '0'), "yyyyMMddHHmm", null).ToString("yyyy-MM-dd") : "",
                YesGet = (double)Math.Round((decimal)(x.YesGet ?? 0), 2),
                CashDis = (double)Math.Round((decimal)(x.CashDis ?? 0), 2),
                SubTot = (double)Math.Round((decimal)(x.SubTot ?? 0), 2),
                NotGet = (double)Math.Round((decimal)(x.NotGet ?? 0), 2),
                Total0 = (double)Math.Round((decimal)(x.Total0 ?? 0), 2),
                Total = (double)Math.Round((decimal)(x.Total ?? 0), 2),
                DriveNo = x.DriveNo ?? "",
                Discount = x.Discount ?? 0,
                Tax = x.Tax ?? 0,
                Kg = x.Kg ?? 0,
                Promotion_No = x.Promotion_No ?? "",
                Dlien = y.Dlien ?? ""
            }).OrderByDescending(z => z.OutDate).ThenBy(z => z.Area_Order).ThenBy(z => z.Area).ThenByDescending(z => z.OutNo).ToListAsync();

            // 如果有搜尋條件
            if (vm.IsSearch)
            {
                //客戶編號
                if (!string.IsNullOrEmpty(vm.CoNo))
                    vm.out10List = await vm.out10List.Where(x => x.CoNo == vm.CoNo).ToListAsync();

                //建檔日期
                if (!string.IsNullOrEmpty(vm.KeyinDate))
                    vm.out10List = await vm.out10List.Where(x => x.KeyinDate == vm.KeyinDate).ToListAsync();

                //出貨單號
                if (!string.IsNullOrEmpty(vm.OutNo))
                    vm.out10List = await vm.out10List.Where(x => x.OutNo.Contains(vm.OutNo)).ToListAsync();

                //結帳日期
                if (!string.IsNullOrEmpty(vm.Dlien))
                    vm.out10List = await vm.out10List.Where(x => x.Dlien.Contains(vm.Dlien)).ToListAsync();

                //出貨類別
                if (!string.IsNullOrEmpty(vm.OutType))
                    vm.out10List = await vm.out10List.Where(x => x.OutType == vm.OutType).ToListAsync();

                //出貨日期
                if (!string.IsNullOrEmpty(vm.OutDate))
                    vm.out10List = await vm.out10List.Where(x => x.OutDate == vm.OutDate).ToListAsync();

                //帳款月份
                if (!string.IsNullOrEmpty(vm.Paymonth))
                    vm.out10List = await vm.out10List.Where(x => x.Paymonth == vm.Paymonth).ToListAsync();

                //備註
                if (!string.IsNullOrEmpty(vm.Memo))
                    vm.out10List = await vm.out10List.Where(x => x.Memo.Contains(vm.Memo)).ToListAsync();

                //業務人員
                if (!string.IsNullOrEmpty(vm.PeNo))
                    vm.out10List = await vm.out10List.Where(x => x.PeNo == vm.PeNo).ToListAsync();

                //司機人員
                if (!string.IsNullOrEmpty(vm.DriveNo))
                    vm.out10List = await vm.out10List.Where(x => x.DriveNo == vm.DriveNo).ToListAsync();
            }

            if (!vm.IsSearch) vm.out10List = await vm.out10List.Take(100).ToListAsync();

            vm.out10List = await vm.out10List.Select(x => new Out10List { OutNo = x.OutNo, CoName = x.CoName }).ToListAsync();

            //今日訂單
            var today = DateTime.Now.ToString("yyyyMMdd");
            var todayOrder = _context.Out10.Where(x => x.KeyinDate.Contains(today) && x.OutType == "1").ToList();
            if (todayOrder.Count != 0)
            {
                vm.TodayOrderId_Start = todayOrder.First().OutNo;
                vm.TodayOrderId_End = todayOrder.Last().OutNo;
            }

            return View(vm);
        }

        public async Task<IActionResult> BatchPrintSettings()
        {
            var vm = new BatchPrintVM()
            {
                Salespersons = _context.Pepo10.Where(x => x.IsSales == true)
                    .Select(x => new BatchPrintVM.SalespersonCheckboxVM()
                    {
                        Id = x.PeNo,
                        Name = x.Name,
                        IsChecked = true
                    }).ToList()
            };

            return View(vm);
        }

        ///<summary>
        /// 畫面搜尋
        ///</summary>
        public async Task<IActionResult> Search(string OutNo)
        {
            IndexViewModel vm = new IndexViewModel();
            var outno = await _context.Out10.Where(x => x.OutNo == OutNo).Select(x => new IndexViewModel
            {
                OutNo = x.OutNo,
                OutType = x.OutType,
                OutDate = !string.IsNullOrEmpty(x.OutDate) ?
                                            DateTime.ParseExact(x.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                Paymonth = !string.IsNullOrEmpty(x.Paymonth) ?
                                            DateTime.ParseExact(x.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                Ntus = x.Ntus,
                CoNo = x.CoNo,
                Memo = x.Memo,
                Total1 = (double)Math.Round((decimal)x.Total1, 2),
                Userid = _context.Users.Where(y => y.Userid == x.Userid).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.Userid).FirstOrDefault().Username : "",
                StockPass = x.StockPass,
                YnClose = x.YnClose,
                PeNo = x.PeNo,
                KeyinDate = !string.IsNullOrEmpty(x.KeyinDate) ?
                                            DateTime.ParseExact((x.KeyinDate).PadRight(12, '0'), "yyyyMMddHHmm", null).ToString("yyyy-MM-dd") : null,
                YesGet = (double)Math.Round((decimal)x.YesGet, 2),
                CashDis = (double)Math.Round((decimal)x.CashDis, 2),
                SubTot = (double)Math.Round((decimal)x.SubTot, 2),
                NotGet = (double)Math.Round((decimal)x.NotGet, 2),
                Total0 = (double)Math.Round((decimal)x.Total0, 2),
                Total = (double)Math.Round((decimal)x.Total, 2),
                DriveNo = x.DriveNo,
                Discount = x.Discount,
                Tax = x.Tax,
                Kg = x.Kg,
                Promotion_No = x.Promotion_No,
                Editid = _context.Users.Where(y => y.Userid == x.EDITID).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.EDITID).FirstOrDefault().Username : "",
                Tallyid = _context.Users.Where(y => y.Userid == x.TALLYID).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == x.TALLYID).FirstOrDefault().Username : "",
                Dlien = _context.Cstm10.Where(y => y.CoNo == x.CoNo).FirstOrDefault() != null ? _context.Cstm10.Where(y => y.CoNo == x.CoNo).FirstOrDefault().Dlien : "",

            }).FirstOrDefaultAsync();
            vm.OutNo = outno != null ? outno.OutNo : "";
            vm.OutType = outno != null ? outno.OutType : "";
            vm.OutDate = outno != null ? outno.OutDate : "";
            vm.Paymonth = outno != null ? outno.Paymonth : "";
            vm.Ntus = outno != null ? outno.Ntus : "";
            vm.CoNo = outno != null ? outno.CoNo : "";
            vm.Memo = outno != null ? outno.Memo : "";
            vm.Total1 = outno != null ? outno.Total1 : 0;
            vm.Userid = outno != null ? outno.Userid : "";
            vm.StockPass = outno != null ? outno.StockPass : "";
            vm.YnClose = outno != null ? outno.YnClose : "";
            vm.PeNo = outno != null ? outno.PeNo : "";
            vm.KeyinDate = outno != null ? outno.KeyinDate : "";
            vm.YesGet = outno != null ? outno.YesGet : 0;
            vm.CashDis = outno != null ? outno.CashDis : 0;
            vm.SubTot = outno != null ? outno.SubTot : 0;
            vm.NotGet = outno != null ? outno.NotGet : 0;
            vm.Total0 = outno != null ? outno.Total0 : 0;
            vm.Total = outno != null ? outno.Total : 0;
            vm.DriveNo = outno != null ? outno.DriveNo : "";
            vm.Discount = outno != null ? outno.Discount : 0;
            vm.Tax = outno != null ? outno.Tax : 0;
            vm.Kg = outno != null ? outno.Kg : 0;
            vm.Promotion_No = outno != null ? outno.Promotion_No : "";
            vm.Dlien = outno != null ? outno.Dlien : "";
            vm.Editid = outno != null ? outno.Editid : "";
            vm.Tallyid = outno != null ? outno.Tallyid : "";


            // 上筆訂單
            var LastOut = _context.Out10.Where(x => x.CoNo == vm.CoNo && String.Compare(x.OutNo, OutNo) <= 0 && x.OutNo != OutNo && x.OutType == "1").OrderByDescending(x => x.OutNo).FirstOrDefault();
            var ListOutNO = LastOut != null ? LastOut.OutNo : "";

            // 所有訂購過的產品編號
            var LastOutPart = _context.Out20.Join(_context.Out10.Where(x => x.CoNo == vm.CoNo && String.Compare(x.OutNo, OutNo) <= 0 && x.OutNo != OutNo && x.OutType == "1")
                    , x => x.OutNo, y => y.OutNo, (x, y) => new { PartNo = x.PartNo, Qty = x.Qty }).Where(x => x.Qty > 0).Select(x => x.PartNo).Distinct().ToList();
            //var LastOutPart = LastOut != null ? _context.Out20.Where(x => x.OutNo == ListOutNO).OrderBy(x => x.Serno).Select(x => x.PartNo).ToList() : new List<string>();


            //var test = await _context.Out20.Where(x => x.OutNo == OutNo).ToListAsync();

            //var test2 = await _context.Out20.Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new { x, y }).ToListAsync();

            //var test3 = await _context.Out20.Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new { x , y } ).Where(z=>z.x.OutNo == OutNo).ToListAsync();
            //子表列表資料
            vm.out20List = await _context.Out20.Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new Out20List
            {
                OutNo = x.OutNo ?? "",
                Serno = x.Serno,
                PartNo = x.PartNo ?? "",
                Qty = x.Qty ?? 0,
                Price = x.Price ?? 0,
                Amount = (double)Math.Round((decimal)(x.Amount ?? 0), 2),
                Discount = x.Discount ?? 0,
                Unit = x.Unit ?? "",
                Memo = x.Memo ?? "",
                SPrice = x.SPrice ?? 0,
                PPrice = x.PPrice ?? 0,
                Spec = y.Spec,
                TaxType = y.TaxType,
                STQty = y.StQty ?? 0,
                IsSale = !String.IsNullOrEmpty(x.IsPromise),
                IsState = LastOutPart.Contains(x.PartNo),
                Bqty = x.BROKEN ?? 0,
                Eqty = x.EXPIRED ?? 0,
                Pqty = x.PERFECT ?? 0,
                Oqty = x.OTHER ?? 0,
            }).Where(z => z.OutNo == OutNo).OrderBy(z => z.Serno).ToListAsync();



            return Json(vm);

        }

        ///<summary>
        /// 新增
        ///</summary>
        public async Task<IActionResult> Add()
        {
            //出貨類別
            var outTypeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="出貨", Value="1" },
                new SelectListItem {Text="出貨退回", Value="2" },
            };
            ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

            var dlienList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="15", Value="15" },
                   new SelectListItem {Text="20", Value="20" },
                   new SelectListItem {Text="25", Value="25" },
                   new SelectListItem {Text="月底", Value="月底" },
                   new SelectListItem {Text="21", Value="21" },
                   new SelectListItem {Text="31", Value="31" },
                };

            ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

            ViewBag.phaseList = await _context.StockTypes.Where(x => x.TYPE_ISOPEN == true).OrderBy(x => x.TYPE_ORDER).Select(x => new Phase20List
            {
                WhereUser = "",
                Phase = x.TYPE_NAME,
                SerNo = x.TYPE_ORDER.ToString(),
            }).ToListAsync();

            var unit = await _context.Phase20.Where(x => x.Whereuse == "單位").Select(x => x.Phase).ToListAsync();

            var unitString = "";

            for (var i = 0; i < unit.Count(); i++)
            {
                unitString += i == 0 ? unit[i] : "," + unit[i];
            }

            ViewBag.UnitList = unitString;

            ViewBag.TaxTypeList = "應稅,免稅";


            // ViewBag.TaxTypeList

            AddViewModel vm = new AddViewModel();

            vm.out10List = new List<Out10List>();

            vm.Userid = _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault().Username : "";
            vm.Editid = _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault().Username : "";
            vm.OutType = "1";
            vm.KeyinDate = DateTime.Today.ToString("yyyy-MM-dd");
            vm.OutDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            //vm.Paymonth = DateTime.Today.AddDays(25).ToString("yyyy-MM");
            vm.Paymonth = "";

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddViewModel vm,
            List<string> out20SerNo, List<string> out20Sale, List<string> out20Status, List<string> out20PartNo, List<string> out20Qty, List<string> out20Unit, List<string> out20Tax, List<string> out20PPrice,
             List<string> out20Price, List<string> out20Discount, List<string> out20Amount, List<string> out20SPrice, List<string> out20Memo, List<string> out20BQty, List<string> out20EQty, List<string> out20PQty, List<string> out20OQty)
        {
            if (ModelState.IsValid)
            {
                var autoOutNo = string.Empty;
                var strDate = DateTime.Now.ToString("yyyyMM");
                var Out10DataCount = _context.Out10.Where(x => x.OutNo.Contains(strDate) && x.OutType == vm.OutType).Count();
                // 無資料取新的
                if (Out10DataCount == 0)
                {
                    autoOutNo = strDate + (vm.OutType == "1" ? "5001" : "8001");
                }
                else
                {
                    var LastOutNo = "";
                    if (vm.OutType == "1")
                    {
                        LastOutNo = _context.Out10.Where(x => x.OutNo.Contains(strDate)).OrderByDescending(x => x.OutNo).FirstOrDefault().OutNo;
                    }
                    else
                    {
                        var DefaultOut = _context.Out10.Where(x => x.OutNo.Contains(strDate) && !x.OutNo.Contains(strDate + "5") && x.OutType == vm.OutType).OrderByDescending(x => x.OutNo).FirstOrDefault();
                        LastOutNo = DefaultOut != null ? DefaultOut.OutNo : strDate + "8000";
                    }

                    var lastDataNum = int.Parse(LastOutNo) + 1;

                    autoOutNo = lastDataNum.ToString();
                }

                vm.OutNo = autoOutNo;

                var insertOut10Data = new Out10()
                {
                    OutNo = vm.OutNo ?? "",
                    OutType = vm.OutType ?? "",
                    OutDate = !String.IsNullOrEmpty(vm.OutDate) ? vm.OutDate.Replace("-", "") : "",
                    Paymonth = !String.IsNullOrEmpty(vm.Paymonth) ? vm.Paymonth.Replace("-", "") : "",
                    Ntus = vm.Ntus ?? "",
                    CoNo = vm.CoNo ?? "",
                    Memo = vm.Memo ?? "",
                    Total1 = vm.Total1 != null ? (double)Math.Round((decimal)vm.Total1, 2) : 0,
                    Userid = HttpContext.Session.GetString("UserAc"),
                    StockPass = vm.StockPass ?? null,
                    YnClose = vm.YnClose ?? null,
                    PeNo = vm.PeNo ?? "",
                    KeyinDate = DateTime.Now.ToString("yyyyMMddHHmm"),
                    YesGet = vm.YesGet != null ? (double)Math.Round((decimal)vm.YesGet, 2) : 0,
                    CashDis = vm.CashDis != null ? (double)Math.Round((decimal)vm.CashDis, 2) : 0,
                    SubTot = vm.SubTot != null ? (double)Math.Round((decimal)vm.SubTot, 2) : 0,
                    NotGet = vm.NotGet != null ? (double)Math.Round((decimal)vm.NotGet, 2) : 0,
                    Total0 = vm.Total0 != null ? (double)Math.Round((decimal)vm.Total0, 2) : 0,
                    Total = vm.Total != null ? (double)Math.Round((decimal)vm.Total, 2) : 0,
                    DriveNo = vm.DriveNo ?? "",
                    Discount = vm.Discount ?? 0,
                    Tax = vm.Tax ?? 0,
                    Kg = vm.Kg ?? 0,
                    Promotion_No = null,
                    TallyState = "2",
                    EDITID = HttpContext.Session.GetString("UserAc"),
                    TALLYID = "",
                };

                await _context.Out10.AddAsync(insertOut10Data);
                await _context.SaveChangesAsync();

                for (var i = 0; i < out20SerNo.Count(); i++)
                {
                    if (!String.IsNullOrEmpty(out20PartNo[i]))
                    {
                        var insertOut20Data = new Out20()
                        {
                            OutNo = vm.OutNo ?? "",
                            Serno = i + 1,
                            PartNo = out20PartNo[i] ?? "",
                            Qty = !String.IsNullOrEmpty(out20Qty[i]) ? Convert.ToDouble(out20Qty[i]) : 0,
                            Price = !String.IsNullOrEmpty(out20Price[i]) ? Convert.ToDouble(out20Price[i]) : 0,
                            Amount = !String.IsNullOrEmpty(out20Amount[i]) ? Convert.ToDouble(out20Amount[i]) : 0,
                            Discount = !String.IsNullOrEmpty(out20Discount[i]) ? Convert.ToDouble(out20Discount[i]) : 0,
                            Unit = out20Unit[i] ?? "",
                            Memo = out20Memo[i] ?? "",
                            SPrice = !String.IsNullOrEmpty(out20SPrice[i]) ? Convert.ToDouble(out20SPrice[i]) : 0,
                            PPrice = !String.IsNullOrEmpty(out20PPrice[i]) ? Convert.ToDouble(out20PPrice[i]) : 0,
                            IsPromise = out20Sale.Contains(out20SerNo[i]) ? "1" : null,
                        };

                        if (vm.OutType == "2")
                        {
                            insertOut20Data.BROKEN = !String.IsNullOrEmpty(out20BQty[i]) ? Convert.ToInt32(out20BQty[i]) : 0;
                            insertOut20Data.PERFECT = !String.IsNullOrEmpty(out20PQty[i]) ? Convert.ToInt32(out20PQty[i]) : 0;
                            insertOut20Data.EXPIRED = !String.IsNullOrEmpty(out20EQty[i]) ? Convert.ToInt32(out20EQty[i]) : 0;
                            insertOut20Data.OTHER = !String.IsNullOrEmpty(out20OQty[i]) ? Convert.ToInt32(out20OQty[i]) : 0;
                        }

                        await _context.Out20.AddAsync(insertOut20Data);
                        await _context.SaveChangesAsync();
                    }
                }

                IndexViewModel result = new IndexViewModel { OutNo = vm.OutNo };

                return RedirectToAction("Index", result);
            }
            return View(vm);

        }

        ///<summary>
        /// 編輯
        ///</summary>
        public async Task<IActionResult> Edit(string OutNo)
        {
            if (String.IsNullOrEmpty(OutNo)) return NotFound();

            //出貨類別
            var outTypeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="出貨", Value="1" },
                new SelectListItem {Text="出貨退回", Value="2" },
            };
            ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", "");

            var dlienList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="15", Value="15" },
                   new SelectListItem {Text="20", Value="20" },
                   new SelectListItem {Text="25", Value="25" },
                   new SelectListItem {Text="月底", Value="月底" },
                   new SelectListItem {Text="21", Value="21" },
                   new SelectListItem {Text="31", Value="31" },
                };

            ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

            ViewBag.phaseList = await _context.StockTypes.Where(x => x.TYPE_ISOPEN == true).OrderBy(x => x.TYPE_ORDER).Select(x => new Phase20List
            {
                WhereUser = "",
                Phase = x.TYPE_NAME,
                SerNo = x.TYPE_ORDER.ToString(),
            }).ToListAsync();


            var unit = _context.Phase20.Where(x => x.Whereuse == "單位").Select(x => x.Phase).ToList();

            var unitString = "";

            for (var i = 0; i < unit.Count(); i++)
            {
                unitString += i == 0 ? unit[i] : "," + unit[i];
            }

            ViewBag.UnitList = unitString;

            ViewBag.TaxTypeList = "應稅,免稅";


            // ViewBag.TaxTypeList

            EditViewModel vm = new EditViewModel();

            var outData = await _context.Out10.FindAsync(OutNo);

            if (outData == null) return NotFound();

            vm.OutNo = OutNo ?? "";
            vm.CoNo = outData.CoNo ?? "";
            vm.KeyinDate = !String.IsNullOrEmpty(outData.KeyinDate) ? DateTime.ParseExact((outData.KeyinDate).PadRight(12, '0'), "yyyyMMddHHmm", null).ToString("yyyy-MM-dd") : null;
            vm.OutDate = !String.IsNullOrEmpty(outData.OutDate) ? DateTime.ParseExact(outData.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null;
            vm.OutType = outData.OutType ?? "";
            vm.Ntus = outData.Ntus ?? "";
            vm.Paymonth = !String.IsNullOrEmpty(outData.Paymonth) ? DateTime.ParseExact(outData.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null;
            vm.Memo = outData.Memo ?? "";
            vm.PeNo = outData.PeNo ?? "";
            vm.DriveNo = outData.DriveNo ?? "";
            vm.Total1 = outData.Total1 ?? 0;
            vm.Total0 = outData.Total0 ?? 0;
            vm.Total = outData.Total ?? 0;
            vm.YesGet = outData.YesGet ?? 0;
            vm.CashDis = outData.CashDis ?? 0;
            vm.Discount = outData.Discount ?? 0;
            vm.SubTot = outData.SubTot ?? 0;
            vm.NotGet = outData.NotGet ?? 0;
            vm.Userid = _context.Users.Where(y => y.Userid == outData.Userid).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == outData.Userid).FirstOrDefault().Username : "";
            vm.Kg = outData.Kg ?? 0;
            vm.StockPass = outData.StockPass ?? null;
            vm.YnClose = outData.YnClose ?? null;
            vm.TallyState = outData.TallyState ?? "2";
            vm.Dlien = !String.IsNullOrEmpty(vm.CoNo) ? _context.Cstm10.Find(vm.CoNo)?.CoNo : "";
            vm.Editid = _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault() != null ? _context.Users.Where(y => y.Userid == HttpContext.Session.GetString("UserAc")).FirstOrDefault().Username : "";

            // 上筆訂單
            var lastOUT = await _context.Out10.Where(x => x.CoNo == outData.CoNo && string.Compare(x.OutDate, outData.OutDate) <= 0 && x.OutNo != OutNo && x.OutType == "1").OrderByDescending(x => x.OutNo).FirstOrDefaultAsync();
            // 所有訂購過的產品編號
            var LastOutPart = _context.Out20.Join(_context.Out10.Where(x => x.CoNo == outData.CoNo && String.Compare(x.OutNo, OutNo) <= 0 && x.OutNo != OutNo && x.OutType == "1")
                    , x => x.OutNo, y => y.OutNo, (x, y) => new { PartNo = x.PartNo, Qty = x.Qty }).Where(x => x.Qty > 0).Select(x => x.PartNo).Distinct().ToList();
            //var lastOut20 = lastOUT != null ? _context.Out20.Where(x => x.OutNo == lastOUT.OutNo).Select(x => x.PartNo).ToList() : new List<string>();
            vm.out20List = await _context.Out20.Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new Out20List()
            {
                Serno = x.Serno,
                IsSale = !String.IsNullOrEmpty(x.IsPromise),
                IsState = LastOutPart.Contains(x.PartNo),
                PartNo = x.PartNo ?? "",
                Qty = x.Qty ?? 0,
                Unit = x.Unit ?? "",
                TaxType = y.TaxType ?? "",
                PPrice = x.PPrice ?? 0,
                Price = x.Price ?? 0,
                Discount = x.Discount ?? 0,
                Amount = x.Amount ?? 0,
                SPrice = x.SPrice ?? 0,
                STQty = y.StQty ?? 0,
                Memo = x.Memo ?? "",
                OutNo = x.OutNo ?? "",
                Kg = y.InitQty1 ?? 0,
                Spec = y.PartNo + y.Spec + y.Barcode,
                Bqty = x.BROKEN ?? 0,
                Eqty = x.EXPIRED ?? 0,
                Pqty = x.PERFECT ?? 0,
                Oqty = x.OTHER ?? 0,
            }).Where(z => z.OutNo == OutNo).OrderBy(z => z.Serno).ToListAsync();



            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(EditViewModel vm,
            List<string> out20SerNo, List<string> out20Sale, List<string> out20Status, List<string> out20PartNo, List<string> out20Qty, List<string> out20Unit, List<string> out20Tax, List<string> out20PPrice,
             List<string> out20Price, List<string> out20Discount, List<string> out20Amount, List<string> out20SPrice, List<string> out20Memo, List<string> out20BQty, List<string> out20EQty, List<string> out20PQty, List<string> out20OQty)
        {
            if (ModelState.IsValid)
            {
                var Out10Data = _context.Out10.Find(vm.OutNo);

                if (Out10Data != null)
                {
                    Out10Data.OutNo = vm.OutNo ?? "";
                    Out10Data.OutType = vm.OutType ?? "";
                    Out10Data.OutDate = !String.IsNullOrEmpty(vm.OutDate) ? vm.OutDate.Replace("-", "") : "";
                    Out10Data.Paymonth = !String.IsNullOrEmpty(vm.Paymonth) ? vm.Paymonth.Replace("-", "") : "";
                    Out10Data.Ntus = vm.Ntus ?? "";
                    Out10Data.CoNo = vm.CoNo ?? "";
                    Out10Data.Memo = vm.Memo ?? "";
                    Out10Data.Total1 = vm.Total1 != null ? (double)Math.Round((decimal)vm.Total1, 2) : 0;
                    //Out10Data.Userid = vm.Userid ?? "";
                    Out10Data.StockPass = null;
                    Out10Data.YnClose = null;
                    Out10Data.PeNo = vm.PeNo ?? "";
                    //Out10Data.KeyinDate = !String.IsNullOrEmpty(vm.KeyinDate) ? vm.KeyinDate.Replace("-", "") : "";
                    Out10Data.YesGet = vm.YesGet != null ? (double)Math.Round((decimal)vm.YesGet, 2) : 0;
                    Out10Data.CashDis = vm.CashDis != null ? (double)Math.Round((decimal)vm.CashDis, 2) : 0;
                    Out10Data.SubTot = vm.SubTot != null ? (double)Math.Round((decimal)vm.SubTot, 2) : 0;
                    Out10Data.NotGet = vm.NotGet != null ? (double)Math.Round((decimal)vm.NotGet, 2) : 0;
                    Out10Data.Total0 = vm.Total0 != null ? (double)Math.Round((decimal)vm.Total0, 2) : 0;
                    Out10Data.Total = vm.Total != null ? (double)Math.Round((decimal)vm.Total, 2) : 0;
                    Out10Data.DriveNo = vm.DriveNo ?? "";
                    Out10Data.Discount = vm.Discount ?? 0;
                    Out10Data.Tax = vm.Tax ?? null;
                    Out10Data.Kg = vm.Kg ?? 0;
                    Out10Data.Promotion_No = null;
                    //Out10Data.TallyState = "2";
                    Out10Data.EDITID = HttpContext.Session.GetString("UserAc") ?? "";
                    _context.Out10.Update(Out10Data);
                    _context.SaveChanges();

                    var removeOut20Data = _context.Out20.Where(x => x.OutNo == vm.OutNo).ToList();


                    if (removeOut20Data != null && removeOut20Data.Count() > 0)
                    {
                        // 重複資料下方出現錯誤( error : database operation expected to affect 1 row(s) but actually affected 0 row(s) )
                        // 暫定用上方方法


                        //var sqlstr = "DELETE FROM OUT20 WHERE OUT_NO ='" + vm.OutNo + "'";
                        //_context.Database.ExecuteSqlCommand(sqlstr);
                        //_context.SaveChanges();

                        _context.Out20.RemoveRange(removeOut20Data);
                        await _context.SaveChangesAsync();

                    }

                    for (var i = 0; i < out20SerNo.Count(); i++)
                    {
                        var insertOut20Data = new Out20()
                        {
                            OutNo = vm.OutNo ?? "",
                            Serno = i + 1,
                            PartNo = out20PartNo[i] ?? "",
                            Qty = !String.IsNullOrEmpty(out20Qty[i]) ? Convert.ToDouble(out20Qty[i]) : 0,
                            Price = !String.IsNullOrEmpty(out20Price[i]) ? Convert.ToDouble(out20Price[i]) : 0,
                            Amount = !String.IsNullOrEmpty(out20Amount[i]) ? Convert.ToDouble(out20Amount[i]) : 0,
                            Discount = !String.IsNullOrEmpty(out20Discount[i]) ? Convert.ToDouble(out20Discount[i]) : 0,
                            Unit = out20Unit[i] ?? "",
                            Memo = out20Memo[i] ?? "",
                            SPrice = !String.IsNullOrEmpty(out20SPrice[i]) ? Convert.ToDouble(out20SPrice[i]) : 0,
                            PPrice = !String.IsNullOrEmpty(out20PPrice[i]) ? Convert.ToDouble(out20PPrice[i]) : 0,
                            IsPromise = out20Sale.Contains(out20SerNo[i]) ? "1" : null,
                        };
                        if (vm.OutType == "2")
                        {
                            insertOut20Data.BROKEN = !String.IsNullOrEmpty(out20BQty[i]) ? Convert.ToInt32(out20BQty[i]) : 0;
                            insertOut20Data.EXPIRED = !String.IsNullOrEmpty(out20EQty[i]) ? Convert.ToInt32(out20EQty[i]) : 0;
                            insertOut20Data.PERFECT = !String.IsNullOrEmpty(out20PQty[i]) ? Convert.ToInt32(out20PQty[i]) : 0;
                            insertOut20Data.OTHER = !String.IsNullOrEmpty(out20OQty[i]) ? Convert.ToInt32(out20OQty[i]) : 0;

                        }

                        _context.Out20.Add(insertOut20Data);
                    }

                    await _context.SaveChangesAsync();

                    IndexViewModel result = new IndexViewModel { OutNo = vm.OutNo };

                    return RedirectToAction("Index", result);
                }

            }
            return View(vm);

        }

        ///<summary>
        /// 刪除
        ///</summary>        
        public async Task<IActionResult> Delete(string OutNo)
        {
            var OUTData = _context.Out10.Find(OutNo);

            if (OUTData == null) return NotFound();

            var OUT20DATA = _context.Out20.Where(x => x.OutNo == OutNo).ToList();

            if (OUT20DATA != null && OUT20DATA.Count() > 0)
            {
                // 重複資料下方出現錯誤( error : database operation expected to affect 1 row(s) but actually affected 0 row(s) )
                // 暫定用上方方法
                var sqlstr = "DELETE FROM OUT20 WHERE OUT_NO ='" + OutNo + "'";
                await _context.Database.ExecuteSqlCommandAsync(sqlstr);
                await _context.SaveChangesAsync();

                //_context.Out20.RemoveRange(OUT20DATA);
                //await _context.SaveChangesAsync();
            }

            _context.Out10.Remove(OUTData);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        ///<summary>
        /// 客戶編輯
        ///</summary>        
        public async Task<IActionResult> CSTMEdit(string CoNo, string OutNo)
        {
            if (String.IsNullOrEmpty(CoNo) || String.IsNullOrEmpty(OutNo)) return NotFound();

            var cstm = await _context.Cstm10.FindAsync(CoNo);

            if (cstm == null) return NotFound();

            ViewBag.OutNo = OutNo;

            //區域類別
            ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", cstm.AreaNo);

            //業務人員
            ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", cstm.PeNo);

            //司機人員
            ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", cstm.DriveNo);

            //客戶類別
            var orderStatsList = new List<SelectListItem>()
            {
                new SelectListItem {Text="請選擇", Value="" },
                new SelectListItem {Text="銷付", Value="1" },
                new SelectListItem {Text="進付", Value="2" },
            };
            ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", cstm.CusType);

            //結帳日期
            var dlienList = new List<SelectListItem>()
            {
                new SelectListItem {Text="請選擇", Value="" },
                new SelectListItem {Text="15", Value="15" },
                new SelectListItem {Text="20", Value="20" },
                new SelectListItem {Text="25", Value="25" },
                new SelectListItem {Text="月底", Value="月底" },
                new SelectListItem {Text="21", Value="21" },
                new SelectListItem {Text="31", Value="31" },
            };
            ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", cstm.Dlien);

            //稅別
            var taxTypeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="請選擇", Value="" },
                new SelectListItem {Text="零稅", Value="0" },
                new SelectListItem {Text="外加", Value="1" },
                new SelectListItem {Text="內含", Value="2" },
            };

            ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", cstm.TaxType);

            //單價列印
            var printPriceList = new List<SelectListItem>()
            {
                new SelectListItem {Text="請選擇", Value="" },
                new SelectListItem {Text="是", Value="Y" },
                new SelectListItem {Text="否", Value="N" },
            };

            ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", cstm.PrintPrice);

            //售價類別
            ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

            //計價幣別
            var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

            ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", cstm.Ntus);

            //付款銀行
            ViewBag.PayBankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", cstm.Paybank);

            return View(cstm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CSTMEdit(Cstm10 vm, string OutNo)
        {
            _context.Cstm10.Update(vm);
            await _context.SaveChangesAsync();

            return Json(true);
        }

        ///<summary>
        /// 全品項輸入
        ///</summary>        
        [HttpPost]
        public async Task<IActionResult> SearchPhase20Data(string[] Phase20Data)
        {
            return Json(await _context.Stock10.Where(x => Phase20Data.Contains(x.Type) && x.IsShow == true && x.IsOpen == true).Select(x => x.PartNo).OrderBy(x => x).ToListAsync());
        }

        ///<summary>
        /// 列印
        ///</summary>  

        public class PrintStockList
        {
            public string PartNo { get; set; }
            public string Spec { get; set; }
            public double Qty { get; set; }
            public string Unit { get; set; }
            public double Price { get; set; }
            public double Discount { get; set; }
            public double Amount { get; set; }
            public double PPrice { get; set; }
            public double Tax { get; set; }
            public string Memo { get; set; }
        }

        public async Task<IActionResult> Print(string OutNo)
        {
            if (!String.IsNullOrEmpty(OutNo))
            {
                var Out10Data = await _context.Out10.FindAsync(OutNo);
                if (Out10Data == null) return NotFound();

                var CSTM = await _context.Cstm10.FindAsync(Out10Data.CoNo ?? "");
                if (CSTM == null) return NotFound();

                var Area = await _context.STO_AREA.Where(x => x.AREA_NAME == CSTM.AreaNo).FirstOrDefaultAsync();

                var PENO = await _context.Pepo10.FindAsync(CSTM.PeNo ?? "");

                var Out20Data = await _context.Out20.Where(x => x.OutNo == OutNo).Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new
                {
                    PartNo = x.PartNo ?? "",
                    Barcode = y.Barcode ?? "",
                    Spec = y.Spec ?? "",
                    Qty = x.Qty ?? 0,
                    Unit = y.Unit ?? "",
                    Price = x.Price ?? 0,
                    Discount = x.Discount ?? 0,
                    Serno = x.Serno,
                    Amount = x.Amount ?? 0,
                    PPrice = x.PPrice ?? 0,
                    TaxType = y.TaxType ?? "",
                    Memo = x.Memo ?? "",
                    Broken = x.BROKEN ?? 0,
                    Perfect = x.PERFECT ?? 0,
                    Expired = x.EXPIRED ?? 0,
                    Other = x.OTHER ?? 0,
                }).OrderBy(x => x.Serno).ToListAsync();

                var OutType = Out10Data.OutType;
                int pageSum = 18; // 單頁總筆數
                int totalSerno = OutType == "1" ? 193 : 229;
                var filepath = OutType == "1" ? @"wwwroot/report/C209_01.html" : @"wwwroot/report/C209_02.html";

                #region 加入固定資料

                Dictionary<string, string> OutData = new Dictionary<string, string>()
                {
                    ["$$C01$$"] = "弘隆食品有限公司",
                    ["$$C02$$"] = "苗栗縣竹南鎮中美里10鄰52-18號",
                    ["$$C03$$"] = "(037)461-779",
                    ["$$C04$$"] = OutNo ?? "",
                    ["$$C05$$"] = (CSTM.Coname ?? "") + "　" + (Out10Data.CoNo ?? ""),
                    ["$$C06$$"] = CSTM.Compaddr ?? "",
                    ["$$C07$$"] = CSTM.Tel1 ?? "",
                    ["$$C08$$"] = CSTM.Uniform ?? "",
                    ["$$C09$$"] = CSTM.Fax ?? "",
                    ["$$C10$$"] = PENO != null ? (PENO.Name ?? "") : "",
                    ["$$C11$$"] = !String.IsNullOrEmpty(Out10Data.OutDate) ? DateTime.ParseExact(Out10Data.OutDate, "yyyyMMdd", null).ToString("yyyy/MM/dd") : "",
                    ["$$C13$$"] = Out20Data.Count() == 0 ? "1" : Math.Ceiling(Convert.ToDouble(Out20Data.Count()) / pageSum).ToString(),
                };

                if (OutType == "1")
                {
                    OutData["$$C194$$"] = Out10Data.Memo ?? "";
                    OutData["$$C195$$"] = (Out10Data.Kg ?? 0).ToString() + "Kg";
                    OutData["$$C196$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                    OutData["$$C197$$"] = Area != null ? (Area.AREA_ORDER.ToString() ?? "0") : "0";
                    OutData["$$C198$$"] = CSTM.AreaNo ?? "";
                }
                else
                {
                    OutData["$$C230$$"] = Out10Data.Memo ?? "";
                    OutData["$$C231$$"] = (Out10Data.Kg ?? 0).ToString() + "Kg";
                    OutData["$$C232$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                    OutData["$$C233$$"] = Area != null ? (Area.AREA_ORDER.ToString() ?? "0") : "0";
                    OutData["$$C234$$"] = CSTM.AreaNo ?? "";
                }

                #endregion 加入固定資料

                List<Dictionary<string, string>> OutDetails = new List<Dictionary<string, string>>();

                int page = 1;
                int serno = 13;

                var OutDetail = new Dictionary<string, string>();

                foreach (var out20 in Out20Data)
                {
                    #region 加入產品
                    if (OutType == "1")
                    {
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Barcode ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Spec.Length > 20 ? out20.Spec[..20] : out20.Spec;
                        serno++;
                        //OutDetail[$"$$SPEC_{serno}$$"] = "123";
                        OutDetail[$"$$C{serno}$$"] = (out20.Qty).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Unit ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Price).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Discount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Amount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.PPrice).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.TaxType.Replace("稅", "") ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Memo ?? "";
                    }
                    else
                    {
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Barcode ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Spec.Length > 20 ? out20.Spec[..20] : out20.Spec;
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Broken).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Expired).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Perfect).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Other).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Unit ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Price).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Discount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Amount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.TaxType.Replace("稅", "") ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Memo ?? "";
                    }
                    #endregion 加入產品

                    #region 單頁筆數滿後換頁

                    if (serno >= totalSerno)
                    {
                        OutDetail["$$C13$$"] = page.ToString();
                        OutDetails.Add(OutDetail);
                        serno = 13;
                        page++;
                        OutDetail = new Dictionary<string, string>();
                    }

                    #endregion 單頁筆數滿後換頁
                }

                #region 補齊同頁剩餘資料

                if ((Out20Data.Count() > 0 && Out20Data != null && serno > 0) || (Out20Data == null || Out20Data.Count() == 0))
                {
                    for (serno = serno + 1; serno <= totalSerno; serno++)
                    {
                        OutDetail[$"$$C{serno}$$"] = "";
                    }
                    OutDetail["$$C13$$"] = page.ToString();
                    OutDetails.Add(OutDetail);

                }


                #endregion 補齊同頁剩餘資料

                return File(await _ajax.Export(OutData, OutDetails, filepath), "applicatuin/pdf", $"{OutNo}.pdf");
                //return null;

            }

            return NotFound();
        }

        public async Task<IActionResult> BatchPrint(BatchPrintVM vm)
        {
            var orders = GetOrders(vm, vm.FormType);
            var OutDetails = ExportFile(orders, out double sum);

            return File(await _ajax.BatchExport(OutDetails, sum), "applicatuin/pdf", $"TestBatch.pdf");
        }

        private List<Dictionary<string, string>> ExportFile(List<string> orders, out double sum)
        {
            sum = 0;
            var OutDetails = new List<Dictionary<string, string>>();

            string filepath = null;

            foreach (var outNo in orders)
            {
                var Out10Data = _context.Out10.Find(outNo);
                int page = 1;
                int serno = 13;

                var OutDetail = GetNewOutData(outNo, out string outType, out double total);
                sum += total;
                //var OutDetail = GetNewOutData(Out10Data, out string outType);

                //int totalSerno = outType == "1" ? 193 : 229;
                int totalSerno = outType == "1" ? 213 : 229;

                filepath = outType == "1" ? @"wwwroot/report/C209_01.html" : @"wwwroot/report/C209_02.html";

                var Out20Data = _context.Out20.Where(x => x.OutNo == outNo).Join(_context.Stock10, x => x.PartNo, y => y.PartNo, (x, y) => new
                {
                    PartNo = x.PartNo ?? "",
                    Barcode = y.Barcode ?? "",
                    Spec = y.Spec ?? "",
                    Qty = x.Qty ?? 0,
                    Unit = y.Unit ?? "",
                    Price = x.Price ?? 0,
                    Discount = x.Discount ?? 0,
                    Serno = x.Serno,
                    Amount = x.Amount ?? 0,
                    PPrice = x.PPrice ?? 0,
                    TaxType = y.TaxType ?? "",
                    Memo = x.Memo ?? "",
                    Broken = x.BROKEN ?? 0,
                    Perfect = x.PERFECT ?? 0,
                    Expired = x.EXPIRED ?? 0,
                    Other = x.OTHER ?? 0,
                }).OrderBy(x => x.Serno).ToList();

                //int pageSum = 18; // 單頁總筆數
                int pageSum = 20; // 單頁總筆數
                //double pagePriceSum = 0;

                foreach (var out20 in Out20Data)
                {
                    #region 加入產品
                    if (outType == "1")
                    {
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Barcode ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Spec.Length > 20 ? out20.Spec[..20] : out20.Spec;
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Qty).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Unit ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Price).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Discount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Amount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.PPrice).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.TaxType.Replace("稅", "") ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Memo ?? "";
                    }
                    else
                    {
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Barcode ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Spec.Length > 20 ? out20.Spec[..20] : out20.Spec;
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Broken).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Expired).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Perfect).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Other).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Unit ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Price).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Discount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = (out20.Amount).ToString();
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.TaxType.Replace("稅", "") ?? "";
                        serno++;
                        OutDetail[$"$$C{serno}$$"] = out20.Memo ?? "";
                    }
                    #endregion 加入產品

                    #region 單頁筆數滿後換頁

                    if (serno >= totalSerno)
                    {
                        OutDetail["$$C12$$"] = page.ToString();
                        OutDetail["filePath"] = filepath;
                        OutDetail["$$C13$$"] = Out20Data.Count() == 0 ? "1" : Math.Ceiling(Convert.ToDouble(Out20Data.Count()) / pageSum).ToString();

                        //OutDetail["$$C216$$"] = String.Format("{0:N}", OutDetail.Total ?? 0);
                        //if (outType == "1")
                        //{
                        //    OutDetail["$$C216$$"] = String.Format("{0:N}", pagePriceSum);
                        //}
                        //else
                        //{
                        //    OutDetail["$$C232$$"] = String.Format("{0:N}", pagePriceSum);
                        //}

                        OutDetails.Add(OutDetail);
                        serno = 13;
                        page++;
                        //pagePriceSum = 0;
                        OutDetail = GetNewOutData(outNo, out outType, out total);
                        //OutDetail = GetNewOutData(Out10Data, out outType);
                    }

                    #endregion 單頁筆數滿後換頁
                }

                #region 補齊同頁剩餘資料

                if ((Out20Data.Count() > 0 && Out20Data != null && serno > 0) || (Out20Data == null || Out20Data.Count() == 0))
                {
                    for (serno++; serno <= totalSerno; serno++)
                    {
                        OutDetail[$"$$C{serno}$$"] = "";
                    }
                    OutDetail["$$C12$$"] = page.ToString();
                    OutDetail["filePath"] = filepath;
                    OutDetail["$$C13$$"] = Out20Data.Count() == 0 ? "1" : Math.Ceiling(Convert.ToDouble(Out20Data.Count()) / pageSum).ToString();
                    //if (outType == "1")
                    //{
                    //    OutDetail["$$C216$$"] = String.Format("{0:N}", pagePriceSum);
                    //    OutDetail["$$C217$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                    //}
                    //else
                    //{
                    //    OutDetail["$$C232$$"] = String.Format("{0:N}", pagePriceSum);
                    //    OutDetail["$$C233$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                    //}

                    OutDetails.Add(OutDetail);
                }

                #endregion 補齊同頁剩餘資料
            }
            return OutDetails;
        }

        private List<string> GetOrders(BatchPrintVM vm, string type)
        {
            var query = _context.Out10.AsNoTracking();

            //過濾日期
            if (!string.IsNullOrEmpty(vm.StartDate) && !string.IsNullOrEmpty(vm.EndDate))
            {
                var formatStart = vm.StartDate.Replace("-", "");
                var formatEnd = vm.EndDate.Replace("-", "");

                query = query.Where(x => string.Compare(x.OutDate, formatStart) >= 0 && string.Compare(x.OutDate, formatEnd) <= 0);
            }

            if (string.IsNullOrEmpty(vm.CusId))
            {
                //過濾訂單編號
                if (!string.IsNullOrEmpty(vm.StartOrderNo) && !string.IsNullOrEmpty(vm.EndOrderNo))
                {
                    query = query.Where(x => string.Compare(x.OutNo, vm.StartOrderNo) >= 0 && string.Compare(x.OutNo, vm.EndOrderNo) <= 0);
                }

                //過濾稅務
                if (!vm.IsTaxIncluded)
                {
                    query = query.Join(_context.Cstm10, o => o.CoNo, c => c.CoNo, (o, c) => new { Out10 = o, Cstm10 = c })
                    .Where(x => x.Cstm10.TaxType != "1" && x.Cstm10.TaxType != "2")
                    .Select(x => x.Out10);
                }

                if (!vm.IsTaxExcluded)
                {
                    query = query.Join(_context.Cstm10, o => o.CoNo, c => c.CoNo, (o, c) => new { Out10 = o, Cstm10 = c })
                     .Where(x => x.Cstm10.TaxType != "0")
                     .Select(x => x.Out10);
                }

                //過濾區域
                var selectedSalesId = vm.Salespersons.Where(x => x.IsChecked).Select(x => x.Id);

                query = query.Join(_context.Pepo10, o => o.PeNo, p => p.PeNo, (o, p) => new { o, p })
                    .Where(x => selectedSalesId.Contains(x.p.PeNo))
                    .Select(x => x.o);

                query = query.OrderBy(x => x.PeNo).ThenBy(x => x.OutNo);
            }
            else
            {
                query = query.Where(x => x.CoNo == vm.CusId);
            }

            //判斷表單種類
            query = query.Where(x => x.OutType == type);

            return query.Select(x => x.OutNo).ToList();
        }

        private Dictionary<string, string> GetNewOutData(string outNo, out string outType, out double sum)
        //private Dictionary<string, string> GetNewOutData(Out10 Out10Data, out string outType)
        {
            var Out10Data = _context.Out10.Find(outNo);
            var CSTM = _context.Cstm10.Find(Out10Data.CoNo ?? "");

            sum = Out10Data.Total.Value;

            if (CSTM == null)
            {
                outType = null;

                return null;
            }

            var Area = _context.STO_AREA.Where(x => x.AREA_NAME == CSTM.AreaNo).FirstOrDefault();
            var PENO = _context.Pepo10.Find(CSTM.PeNo ?? "");

            outType = Out10Data.OutType;

            Dictionary<string, string> OutData = new Dictionary<string, string>()
            {
                ["$$C01$$"] = "弘隆食品有限公司",
                ["$$C02$$"] = "苗栗縣竹南鎮中美里10鄰52-18號",
                ["$$C03$$"] = "(037)461-779",

                //["$$C04$$"] = outNo ?? "",
                ["$$C04$$"] = Out10Data.OutNo ?? "",

                ["$$C05$$"] = (CSTM.Coname ?? "") + "　" + (Out10Data.CoNo ?? ""),
                ["$$C06$$"] = CSTM.Compaddr ?? "",
                ["$$C07$$"] = CSTM.Tel1 ?? "",
                ["$$C08$$"] = CSTM.Uniform ?? "",
                ["$$C09$$"] = CSTM.Fax ?? "",
                ["$$C10$$"] = PENO != null ? (PENO.Name ?? "") : "",
                ["$$C11$$"] = !String.IsNullOrEmpty(Out10Data.OutDate) ? DateTime.ParseExact(Out10Data.OutDate, "yyyyMMdd", null).ToString("yyyy/MM/dd") : "",
            };

            if (outType == "1")
            {
                //OutData["$$C194$$"] = Out10Data.Memo ?? "";
                //OutData["$$C195$$"] = (Out10Data.Kg ?? 0).ToString() + "Kg";
                //OutData["$$C196$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                //OutData["$$C197$$"] = Area != null ? (Area.AREA_ORDER.ToString() ?? "0") : "0";
                //OutData["$$C198$$"] = CSTM.AreaNo ?? "";
                OutData["$$C214$$"] = Out10Data.Memo ?? "";
                OutData["$$C215$$"] = (Out10Data.Kg ?? 0).ToString() + "Kg";
                OutData["$$C216$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                OutData["$$C217$$"] = Area != null ? (Area.AREA_ORDER.ToString() ?? "0") : "0";
                OutData["$$C218$$"] = CSTM.AreaNo ?? "";
            }
            else
            {
                OutData["$$C230$$"] = Out10Data.Memo ?? "";
                OutData["$$C231$$"] = (Out10Data.Kg ?? 0).ToString() + "Kg";
                OutData["$$C232$$"] = String.Format("{0:N}", Out10Data.Total ?? 0);
                OutData["$$C233$$"] = Area != null ? (Area.AREA_ORDER.ToString() ?? "0") : "0";
                OutData["$$C234$$"] = CSTM.AreaNo ?? "";
            }
            return OutData;
        }
    }
}
