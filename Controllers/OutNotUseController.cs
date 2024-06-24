using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using ERP6.ViewModels.Out;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using ERP6.ViewModels.Stock;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace ERP6.Controllers
{
    public class OutNotUseController : Controller
    {
        private readonly EEPEF01Context _context;

        public OutNotUseController(EEPEF01Context context)
        {
            _context = context;
        }

        #region 主表

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            try
            {
                #region SelectList

                //出貨類別
                var outTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="出貨", Value="1" },
                   new SelectListItem {Text="出貨退回", Value="2" },
                };

                ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

                #endregion SelectList

                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.OutNo))
                    {
                        vm = await (from out10 in _context.Out10
                                    where out10.OutNo == vm.OutNo
                                    select new IndexViewModel
                                    {
                                        OutNo = out10.OutNo,
                                        OutType = out10.OutType,
                                        OutDate = !string.IsNullOrEmpty(out10.OutDate) ?
                                            DateTime.ParseExact(out10.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                        Paymonth = !string.IsNullOrEmpty(out10.Paymonth) ?
                                            DateTime.ParseExact(out10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                        Ntus = out10.Ntus,
                                        CoNo = out10.CoNo,
                                        Memo = out10.Memo,
                                        Total1 = (double)Math.Round((decimal)out10.Total1, 2),
                                        Userid = out10.Userid,
                                        StockPass = out10.StockPass,
                                        YnClose = out10.YnClose,
                                        PeNo = out10.PeNo,
                                        KeyinDate = !string.IsNullOrEmpty(out10.KeyinDate) ?
                                            DateTime.ParseExact(out10.KeyinDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                        YesGet = (double)Math.Round((decimal)out10.YesGet, 2),
                                        CashDis = (double)Math.Round((decimal)out10.CashDis, 2),
                                        SubTot = (double)Math.Round((decimal)out10.SubTot, 2),
                                        NotGet = (double)Math.Round((decimal)out10.NotGet, 2),
                                        Total0 = (double)Math.Round((decimal)out10.Total0, 2),
                                        Total = (double)Math.Round((decimal)out10.Total, 2),
                                        DriveNo = out10.DriveNo,
                                        Discount = out10.Discount,
                                        Tax = out10.Tax,
                                        Kg = out10.Kg,
                                        Promotion_No = out10.Promotion_No,
                                    }).FirstOrDefaultAsync();

                        //確認資料
                        var checkData = _context.Out20.Where(x => x.OutNo == vm.OutNo).Any();

                        //找出子表清單
                        if (checkData)
                        {
                            vm.out20List = await (from Out20 in _context.Out20
                                                  join stock10 in _context.Stock10
                                                  on Out20.PartNo equals stock10.PartNo
                                                  where Out20.OutNo == vm.OutNo
                                                  select new Out20List
                                                  {
                                                      OutNo = Out20.OutNo,
                                                      Serno = Out20.Serno,
                                                      PartNo = Out20.PartNo,
                                                      Qty = Out20.Qty,
                                                      Price = Out20.Price,
                                                      Amount = (double)Math.Round((decimal)Out20.Amount,2),
                                                      Discount = Out20.Discount,
                                                      Unit = Out20.Unit,
                                                      Memo = Out20.Memo,
                                                      SPrice = Out20.SPrice,
                                                      PPrice = Out20.PPrice,
                                                      STQty = stock10.StQty ?? 0,
                                                  }).ToListAsync();
                        }
                    }
                }

                // 先獲取依照區域排的客戶名單
                var CSTMLIST = await _context.Cstm10.Join(_context.STO_AREA, x => x.AreaNo, y => y.AREA_NAME, (x, y) => new { AREAID = x.AreaNo, CONO = x.CoNo, AREA = y.AREA_NAME, ORDER = y.AREA_ORDER })
                                    .OrderBy(z => z.ORDER).OrderBy(z=>z.AREAID).ToListAsync();
                var OUT10LIST = await _context.Out10.OrderByDescending(x => x.OutNo).ToListAsync();

                vm.out10List= await OUT10LIST.Join(CSTMLIST, x => x.CoNo, y => y.CONO, (x, y) => new Out10List
                                {
                                    OutNo = x.OutNo ?? "",
                                    Area_Order = y.ORDER ?? 0,
                                    Area = y.AREA ?? "",    
                                    OutDate = x.OutDate,
                                }).OrderByDescending(z=>z.OutDate).ThenBy(z=>z.Area_Order).ThenBy(z=>z.Area).ThenByDescending(z=>z.OutNo).ToListAsync();

                //vm.out10List = await (from out10 in _context.Out10
                //                      select new Out10List
                //                      {
                //                          OutNo = out10.OutNo,
                //                          OutType = out10.OutType,
                //                          OutDate = !string.IsNullOrEmpty(out10.OutDate) ?
                //                            DateTime.ParseExact(out10.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                //                          Paymonth = !string.IsNullOrEmpty(out10.Paymonth) ?
                //                            DateTime.ParseExact(out10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                //                          Ntus = out10.Ntus,
                //                          CoNo = out10.CoNo,
                //                          Memo = out10.Memo,
                //                          Total1 = (double)Math.Round((decimal)out10.Total1, 2),
                //                          Userid = out10.Userid,
                //                          StockPass = out10.StockPass,
                //                          YnClose = out10.YnClose,
                //                          PeNo = out10.PeNo,
                //                          KeyinDate = !string.IsNullOrEmpty(out10.KeyinDate) ?
                //                            DateTime.ParseExact(out10.KeyinDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                //                          YesGet = (double)Math.Round((decimal)out10.YesGet, 2),
                //                          CashDis = (double)Math.Round((decimal)out10.CashDis, 2),
                //                          SubTot = (double)Math.Round((decimal)out10.SubTot, 2),
                //                          NotGet = (double)Math.Round((decimal)out10.NotGet, 2),
                //                          Total0 = (double)Math.Round((decimal)out10.Total0, 2),
                //                          Total = (double)Math.Round((decimal)out10.Total, 2),
                //                          DriveNo = out10.DriveNo,
                //                          Discount = out10.Discount,
                //                          Tax = out10.Tax,
                //                          Kg = out10.Kg,
                //                          Promotion_No = out10.Promotion_No,                                          
                //                      }).OrderBy(x=>x.Area).OrderByDescending(x => x.OutNo).ToListAsync();

                if (vm.IsSearch)
                {
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();
                    }

                    //建檔日期
                    if (!string.IsNullOrEmpty(vm.KeyinDate))
                    {
                        vm.KeyinDate = vm.KeyinDate.Replace("-", "");

                        vm.out10List = await vm.out10List.Where(x => x.KeyinDate == vm.KeyinDate).ToListAsync();
                    }

                    //出貨單號
                    if (!string.IsNullOrEmpty(vm.OutNo))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.OutNo.Contains(vm.OutNo)).ToListAsync();
                    }

                    //出貨類別
                    if (!string.IsNullOrEmpty(vm.OutType))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.OutType == vm.OutType).ToListAsync();
                    }

                    //出貨日期
                    if (!string.IsNullOrEmpty(vm.OutDate))
                    {
                        vm.OutDate = vm.OutDate.Replace("-", "");

                        vm.out10List = await vm.out10List.Where(x => x.OutDate == vm.OutDate).ToListAsync();
                    }

                    //帳款月份
                    if (!string.IsNullOrEmpty(vm.Paymonth))
                    {
                        vm.OutDate = vm.Paymonth.Replace("-", "");

                        vm.out10List = await vm.out10List.Where(x => x.Paymonth == vm.Paymonth).ToListAsync();
                    }

                    //備註
                    if (!string.IsNullOrEmpty(vm.Memo))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.Memo.Contains(vm.Memo)).ToListAsync();
                    }

                    //業務人員
                    if (!string.IsNullOrEmpty(vm.PeNo))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.PeNo == vm.PeNo).ToListAsync();
                    }

                    //司機人員
                    if (!string.IsNullOrEmpty(vm.DriveNo))
                    {
                        vm.out10List = await vm.out10List.Where(x => x.DriveNo == vm.DriveNo).ToListAsync();
                    }
                }
                else
                {
                    vm.out10List = await vm.out10List.Take(100).ToListAsync();
                }

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> Search(string OutNo)
        {
            //先找出主表資料
            IndexViewModel vm = await (from out10 in _context.Out10
                                       join pepo10 in _context.Pepo10
                                       on out10.PeNo equals pepo10.PeNo into ps
                                       from pepo10 in ps.DefaultIfEmpty()
                                       where out10.OutNo == OutNo
                                       select new IndexViewModel
                                       {
                                           OutNo = out10.OutNo,
                                           OutType = out10.OutType,
                                           OutDate = !string.IsNullOrEmpty(out10.OutDate) ?
                                            DateTime.ParseExact(out10.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           Paymonth = !string.IsNullOrEmpty(out10.Paymonth) ?
                                            DateTime.ParseExact(out10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                           Ntus = out10.Ntus,
                                           CoNo = out10.CoNo,
                                           Memo = out10.Memo,
                                           Total1 = (double)Math.Round((decimal)out10.Total1, 2),
                                           Userid = out10.Userid,
                                           StockPass = out10.StockPass,
                                           YnClose = out10.YnClose,
                                           PeNo = out10.PeNo,
                                           KeyinDate = !string.IsNullOrEmpty(out10.KeyinDate) ?
                                            DateTime.ParseExact(out10.KeyinDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           YesGet = (double)Math.Round((decimal)out10.YesGet, 2),
                                           CashDis = (double)Math.Round((decimal)out10.CashDis, 2),
                                           SubTot = (double)Math.Round((decimal)out10.SubTot, 2),
                                           NotGet = (double)Math.Round((decimal)out10.NotGet, 2),
                                           Total0 = (double)Math.Round((decimal)out10.Total0,2),
                                           Total = (double)Math.Round((decimal)out10.Total, 2),
                                           DriveNo = out10.DriveNo,
                                           Discount = out10.Discount,
                                           Tax = out10.Tax,
                                           Kg = out10.Kg,
                                           Promotion_No = out10.Promotion_No,
                                       }).FirstOrDefaultAsync();

            // 上一筆訂單
            var LastOut = _context.Out10.Where(x => x.CoNo == vm.CoNo && x.OutNo != OutNo).OrderByDescending(x => x.OutNo).FirstOrDefault();
            // 上筆所有產品編號
            var LastOutPart = LastOut != null ? _context.Out20.Where(x => x.OutNo == LastOut.OutNo).OrderBy(x => x.Serno).Select(x => x.PartNo).ToList() : new List<string>();

            //子表列表資料
            var Out20List = await (from out20 in _context.Out20
                                   join stock10 in _context.Stock10
                                   on out20.PartNo equals stock10.PartNo
                                   where out20.OutNo == OutNo
                                   orderby out20.Serno
                                   select new Out20List
                                   {
                                       OutNo = out20.OutNo,
                                       Serno = out20.Serno,
                                       PartNo = out20.PartNo,
                                       Qty = out20.Qty ?? 0,
                                       Price = out20.Price ?? 0,
                                       Amount = (double)Math.Round((decimal)out20.Amount,2),
                                       Discount = out20.Discount ?? 0,
                                       Unit = out20.Unit,
                                       Memo = out20.Memo ?? string.Empty,
                                       SPrice = out20.SPrice ?? 0,
                                       PPrice = out20.PPrice ?? 0,
                                       Spec = stock10.Spec,
                                       TaxType = stock10.TaxType,
                                       STQty = stock10.StQty ?? 0,
                                       IsSale = !String.IsNullOrEmpty(out20.IsPromise),
                                       IsState = LastOutPart.Contains(out20.PartNo),
                                   }).ToListAsync();

            if (Out20List != null)
            {
                vm.out20List = Out20List;
            }

            return Json(vm);
        }

        public async Task<IActionResult> SearchPhase20Data(string OutNo, List<string> Phase20ListData)
        {
            try
            {
                AddViewModel vm = new AddViewModel();

                vm.IsTrue = false;

                if (Phase20ListData == null)
                {
                    vm.out20List = new List<Out20List>();
                }

                //找出相對應資料
                var checkOut20Data = _context.Out20.Where(x => x.OutNo == OutNo).Any();
                var autoSerNo = 1;
                var addNo = 0;
                if (checkOut20Data)
                {
                    //如果有資料 找出最後一筆序號
                    var out20Data = _context.Out20.Where(x => x.OutNo == OutNo).OrderByDescending(x => x.Serno).First().Serno;
                    autoSerNo = out20Data + 1;
                }

                foreach (var item in Phase20ListData)
                {
                    var stockData = await _context.Stock10.Where(x => x.Type == item).ToListAsync();

                    foreach (var stockItem in stockData)
                    {
                        Out20 out20Data = new Out20
                        {
                            OutNo = OutNo,
                            Serno = autoSerNo + addNo,
                            PartNo = stockItem.PartNo,
                            Price = stockItem.Price1,
                            //Spec = stockItem.Spec,
                            Unit = stockItem.Unit,
                            //TaxType = stockItem.TaxType,
                            SPrice = stockItem.SPrice1,
                        };

                        _context.Out20.Add(out20Data);
                        await _context.SaveChangesAsync();

                        addNo++;
                    }
                }

                vm.IsTrue = true;
                vm.OutNo = OutNo;

                if (vm.IsTrue)
                {
                    var Out10Data = _context.Out10.Where(x => x.OutNo == vm.OutNo).FirstOrDefault();
                    var Out20Data = (from Out20 in _context.Out20
                                     join stock10 in _context.Stock10
                                     on Out20.PartNo equals stock10.PartNo
                                     where Out20.OutNo == vm.OutNo
                                     select new
                                     {
                                         OutNo = Out20.OutNo,
                                         Serno = Out20.Serno,
                                         PartNo = Out20.PartNo,
                                         Spec = stock10.Spec,
                                         Qty = Out20.Qty,
                                         Unit = Out20.Unit,
                                         PPrice = Out20.PPrice,
                                         Price = Out20.Price,
                                         Amount = Out20.Amount,
                                         Discount = Out20.Discount,
                                         Memo = Out20.Memo,
                                         SPrice = Out20.SPrice,
                                         TaxType = stock10.TaxType,
                                         Kg = stock10.InitQty1,
                                     }).ToList();

                    double? total0 = 0;
                    double? total1 = 0;
                    double? Kg = 0;
                    foreach (var item in Out20Data)
                    {
                        //total1 = 應稅 total0 = 免稅

                        switch (item.TaxType)
                        {
                            case "應稅":
                                total1 += item.Amount;
                                break;
                            case "免稅":
                                total0 += item.Amount;
                                break;
                            default:
                                break;
                        }

                        if (string.IsNullOrEmpty(item.Kg.ToString()))
                        {
                            Kg += item.Kg;
                        }
                    }

                    Out10Data.Total1 = total1;
                    Out10Data.Total0 = total0;
                    Out10Data.Total = total1 + total0;
                    Out10Data.Kg = Kg;

                    if (Out10Data.Discount != null && Out10Data.Discount > 0)
                    {
                        Out10Data.CashDis = (Out10Data.Total * Out10Data.Discount / 100);
                        Out10Data.Total = Out10Data.Total - Out10Data.CashDis;
                        Out10Data.NotGet = Out10Data.Total;
                    }
                    else
                    {
                        Out10Data.NotGet = Out10Data.Total;
                    }

                    await _context.SaveChangesAsync();
                }

                return Json(vm);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            try
            {
                #region SelectList

                //出貨類別
                var outTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="出貨", Value="1" },
                   new SelectListItem {Text="出貨退回", Value="2" },
                };

                ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

                #endregion SelectList

                AddViewModel vm = new AddViewModel();

                //取得資料
                var Out10List = await (from out10 in _context.Out10
                                       select new Out10List
                                       {
                                           OutNo = out10.OutNo,
                                           //InDate = !string.IsNullOrEmpty(in10.InDate) ?
                                           // DateTime.ParseExact(in10.InDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           //VendorNo = in10.VendorNo,
                                           //Memo = in10.Memo,
                                           //Ntus = in10.Ntus,
                                           //Total1 = in10.Total1,
                                           //Userid = in10.Userid,
                                           //Paymonth = !string.IsNullOrEmpty(in10.Paymonth) ?
                                           // DateTime.ParseExact(in10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                           //StockPass = in10.StockPass,
                                           //YnClose = in10.YnClose,
                                           //InvoiceNo = in10.InvoiceNo,
                                           //InType = in10.InType,
                                           //Total = in10.Total,
                                           //YesGet = in10.YesGet,
                                           //CashDis = in10.CashDis,
                                           //SubTot = in10.SubTot,
                                           //NotGet = in10.NotGet,
                                           //Total0 = in10.Total0,
                                           //Discount = in10.Discount
                                       }).Take(100).OrderByDescending(x => x.OutNo).ToListAsync();

                vm.out10List = Out10List;

                //載入預設資料
                vm.Userid = HttpContext.Session.GetString("UserAc");

                


                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var autoOutNo = string.Empty;
                    var strDate = DateTime.Now.ToString("yyyyMM") + "5";
                    var Out10DataCount = _context.Out10.Where(x => x.OutNo.Contains(strDate)).Count();
                    if(Out10DataCount == 0)
                    { 
                        autoOutNo = strDate + "001";
                    }
                    else
                    {
                        var lastDataNum = int.Parse(_context.Out10.Where(x => x.OutNo.Contains(strDate)).OrderByDescending(x => x.OutNo).FirstOrDefault().OutNo) + 1;
                        autoOutNo = lastDataNum.ToString();
                    }

                    vm.OutNo = autoOutNo;

                    

                    // 計算總金額
                    //vm.Total = (vm.Total0 ?? 0) + (vm.Total1 ?? 0);

                    vm.Total = (double)Math.Round(Convert.ToDecimal(vm.Total0 ?? 0 + vm.Total1 ?? 0), 2);

                    // 計算折扣金額
                    var cst = _context.Cstm10.Find(vm.CoNo);
                    vm.Discount = vm.Discount != null ? vm.Discount : (cst != null ? cst.Discount : 0);
                    //vm.CashDis = Convert.ToDouble(((vm.Total ?? 0) * vm.Discount) / 100);
                    vm.CashDis = (double)Math.Round(Convert.ToDecimal((vm.Total * vm.Discount) / 100), 2);
                    // 計算未收金額
                    vm.NotGet = (double)Math.Round(Convert.ToDecimal(vm.Total - (vm.YesGet ?? 0) - vm.CashDis - (vm.SubTot ?? 0)),2);

                    //設定預設資料
                    vm.OutNo = autoOutNo;
                    vm.IsTrue = true;

                    //新增所需資料(廠商)
                    Out10 insertData = new Out10
                    {
                        OutNo = autoOutNo,
                        OutType = vm.OutType,
                        OutDate = string.IsNullOrEmpty(vm.OutDate) ? null : vm.OutDate.Replace("-", ""),
                        Paymonth = string.IsNullOrEmpty(vm.Paymonth) ? null : vm.Paymonth.Replace("-", "").Substring(0, 6),
                        Ntus = vm.Ntus,
                        CoNo = vm.CoNo,
                        Memo = vm.Memo,
                        Total1 = vm.Total1 ?? 0,
                        Userid = HttpContext.Session.GetString("UserAc"),
                        StockPass = vm.StockPass,
                        YnClose = vm.YnClose,
                        PeNo = vm.PeNo,
                        KeyinDate = string.IsNullOrEmpty(vm.KeyinDate) ? null : vm.KeyinDate.Replace("-", ""),
                        YesGet = vm.YesGet ?? 0,
                        CashDis = vm.CashDis ?? 0,
                        SubTot = vm.SubTot ?? 0,
                        NotGet = vm.NotGet,
                        Total0 = vm.Total0 ?? 0,
                        Total = vm.Total ?? 0,
                        DriveNo = vm.DriveNo,
                        Discount = vm.Discount ?? 0,
                        //Discount = vm.Discount,
                        Tax = vm.Tax ?? 0,
                        Kg = vm.Kg ?? 0,                        
                        Promotion_No = vm.Promotion_No,
                        TallyState = "2",
                    };

                    _context.Out10.Add(insertData);
                    await _context.SaveChangesAsync();


                }

                return Json(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="OutNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string OutNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                #region SelectList

                //出貨類別
                var outTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="出貨", Value="1" },
                   new SelectListItem {Text="出貨退回", Value="2" },
                };

                ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

                #endregion SelectList

                if (string.IsNullOrEmpty(OutNo))
                {
                    return NotFound();
                }

                //資料
                var Out10Data = _context.Out10
                    .Where(x => x.OutNo == OutNo).FirstOrDefault();

                if (Out10Data == null)
                {
                    return NotFound();
                }

                vm.OutNo = Out10Data.OutNo;
                vm.OutType = Out10Data.OutType;
                vm.OutDate = !string.IsNullOrEmpty(Out10Data.OutDate) ?
                                            DateTime.ParseExact(Out10Data.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null;
                vm.Paymonth = !string.IsNullOrEmpty(Out10Data.Paymonth) ?
                                            DateTime.ParseExact(Out10Data.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null;
                vm.Ntus = Out10Data.Ntus;
                vm.CoNo = Out10Data.CoNo;
                vm.Memo = Out10Data.Memo;
                vm.StockPass = Out10Data.StockPass;
                vm.YnClose = Out10Data.YnClose;
                vm.PeNo = Out10Data.PeNo;
                vm.KeyinDate = !string.IsNullOrEmpty(Out10Data.KeyinDate) ?
                                            DateTime.ParseExact(Out10Data.KeyinDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null;
                vm.DriveNo = Out10Data.DriveNo;
                vm.Discount = Out10Data.Discount;
                vm.Tax = Out10Data.Tax;
                vm.Promotion_No = Out10Data.Promotion_No;

                // 下方金額資料
                vm.Total1 = Out10Data.Total1;
                vm.Total0 = Out10Data.Total0;
                vm.Total = Out10Data.Total;
                vm.YesGet = Out10Data.YesGet;
                vm.CashDis = Out10Data.CashDis;
                vm.SubTot = Out10Data.SubTot;
                vm.NotGet = Out10Data.NotGet;

                //只顯示不修改
                vm.Userid = Out10Data.Userid;

                //清單資料
                var out10List = await _context.Out10.Select(x => new Out10List
                {
                    OutNo = x.OutNo,
                }).ToListAsync();

                if (out10List != null)
                {
                    out10List = out10List.Where(x => x.OutNo == OutNo).ToList();
                    vm.out10List = out10List;

                    //子表列表資料
                    vm.out20List = await (from out20 in _context.Out20
                                          join stock10 in _context.Stock10
                                          on out20.PartNo equals stock10.PartNo
                                          where out20.OutNo == OutNo
                                          select new Out20List
                                          {
                                              OutNo = out20.OutNo,
                                              Serno = out20.Serno,
                                              PartNo = out20.PartNo,
                                              Qty = out20.Qty,
                                              Price = out20.Price,
                                              Amount = out20.Amount,
                                              Discount = out20.Discount,
                                              Unit = out20.Unit,
                                              Memo = out20.Memo,
                                              SPrice = out20.SPrice,
                                              PPrice = out20.PPrice,
                                              Spec = stock10.Spec,
                                              TaxType = stock10.TaxType,
                                          }).ToListAsync();

                    return View(vm);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel vm)
        {
            try
            {
                vm.IsTrue = false;

                if (vm != null)
                {
                    //預防資料消失重新計算一遍
                    if (vm.NotGet == 0 || vm.NotGet == null)
                    {
                        vm.NotGet = (vm.Total ?? 0) - (vm.YesGet ?? 0) - (vm.CashDis ?? 0) - (vm.SubTot ?? 0);
                    }

                    Out10 updateData = new Out10
                    {
                        OutNo = vm.OutNo,
                        OutType = vm.OutType,
                        OutDate = string.IsNullOrEmpty(vm.OutDate) ? null : vm.OutDate.Replace("-", ""),
                        Paymonth = string.IsNullOrEmpty(vm.Paymonth) ? null : vm.Paymonth.Replace("-", "").Substring(0, 6),
                        Ntus = vm.Ntus,
                        CoNo = vm.CoNo,
                        Memo = vm.Memo,
                        Userid = HttpContext.Session.GetString("UserAc"),
                        StockPass = vm.StockPass,
                        YnClose = vm.YnClose,
                        PeNo = vm.PeNo,
                        KeyinDate = string.IsNullOrEmpty(vm.KeyinDate) ? null : vm.KeyinDate.Replace("-", ""),
                        DriveNo = vm.DriveNo,
                        Discount = vm.Discount,
                        Tax = vm.Tax,
                        Promotion_No = vm.Promotion_No,

                        // 加入未儲存欄位
                        YesGet = vm.YesGet , 
                        SubTot = vm.SubTot ,
                        
                        Total1 = vm.Total1 , 
                        CashDis = vm.CashDis ,
                        NotGet = vm.NotGet,
                        Total0 = vm.Total0 , 
                        Total = vm.Total,

                    };

                    _context.Out10.Update(updateData);

                    await _context.SaveChangesAsync();

                    vm.IsTrue = true;
                }
                else
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "編輯失敗!";

                    return Json(vm);
                }

                return Json(vm);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string OutNo)
        {
            var checkOut10Data = await _context.Out10.Where(x => x.OutNo == OutNo).FirstOrDefaultAsync();

            if (checkOut10Data == null)
            {
                return NotFound();
            }

            //先清除子表資料
            var removeOut20Data = _context.Out20.Where(x => x.OutNo == OutNo).ToList();

            if (removeOut20Data != null)
            {
                _context.Out20.RemoveRange(removeOut20Data);
                await _context.SaveChangesAsync();
            }

            //再清除主表資料
            _context.Out10.Remove(checkOut10Data);
            await _context.SaveChangesAsync();

            return Json(true);
        }

        #endregion 主表

        #region 子表

        /// <summary>
        /// 子表首頁
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexDetail(IndexDetailViewModel vm)
        {
            try
            {
                #region SelectList

                //產品清單資料
                var stock10ListData = from stock10 in _context.Stock10
                                      select new
                                      {
                                          id = stock10.PartNo,
                                          text = stock10.PartNo + stock10.Spec,
                                          unit = stock10.Unit,
                                          taxType = stock10.TaxType,
                                      };

                //產品清單資料
                ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

                //單位
                ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="應稅", Value="應稅" },
                   new SelectListItem {Text="免稅", Value="免稅" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                #endregion

                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.OutNo))
                    {
                        var OutData = (from Out20 in _context.Out20
                                       join stock10 in _context.Stock10
                                       on Out20.PartNo equals stock10.PartNo
                                       where Out20.OutNo == vm.OutNo && Out20.Serno == vm.Serno
                                       select new
                                       {
                                           Out20,
                                           stock10
                                       }).FirstOrDefault();

                        if (OutData != null)
                        {
                            //裝載資料
                            vm = new IndexDetailViewModel
                            {
                                OutNo = OutData.Out20.OutNo,
                                Serno = OutData.Out20.Serno,
                                PartNo = OutData.Out20.PartNo,
                                Spec = OutData.stock10.Spec,
                                Qty = OutData.Out20.Qty,
                                Unit = OutData.Out20.Unit,
                                PPrice = OutData.Out20.PPrice,
                                Price = OutData.Out20.Price,
                                Amount = OutData.Out20.Amount,
                                Discount = OutData.Out20.Discount,
                                Memo = OutData.Out20.Memo,
                                SPrice = OutData.Out20.SPrice,
                                TaxType = OutData.stock10.TaxType,
                            };
                        }

                        //取得Stock21List資料
                        var checkOut20Data = _context.Out20.Where(x => x.OutNo == vm.OutNo).Any();

                        if (checkOut20Data)
                        {
                            vm.out20List = (from Out20 in _context.Out20
                                            join stock10 in _context.Stock10
                                            on Out20.PartNo equals stock10.PartNo
                                            where Out20.OutNo == vm.OutNo
                                            select new Out20List
                                            {
                                                PartNo = Out20.PartNo,
                                                Serno = Out20.Serno,
                                            }).ToList();
                        }
                    }
                }

                //搜尋條件
                if (vm.IsSearch)
                {
                    //產品編號
                    if (!string.IsNullOrEmpty(vm.PartNo))
                    {
                        vm.out20List = await vm.out20List.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
                    }
                }

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> SearchDetail(string PartNo, int SerNo , string outNo)
        {
            try
            {
                //var data = await (from Out20 in _context.Out20
                //                  join stock10 in _context.Stock10
                //                  on Out20.PartNo equals stock10.PartNo
                //                  where Out20.PartNo == PartNo && Out20.Serno == SerNo
                //                  select new
                //                  {
                //                      OutNo = Out20.OutNo,
                //                      Serno = Out20.Serno,
                //                      PartNo = Out20.PartNo,
                //                      Spec = stock10.Spec,
                //                      Qty = Out20.Qty,
                //                      Unit = Out20.Unit,
                //                      PPrice = Out20.PPrice,
                //                      Price = Out20.Price,
                //                      Amount = Out20.Amount,
                //                      Discount = Out20.Discount,
                //                      Memo = Out20.Memo,
                //                      SPrice = Out20.SPrice,
                //                      TaxType = stock10.TaxType,
                //                  }).FirstOrDefaultAsync();

                var data = await (from Out20 in _context.Out20
                                  join stock10 in _context.Stock10
                                  on Out20.PartNo equals stock10.PartNo
                                  where Out20.PartNo == PartNo && Out20.Serno == SerNo && Out20.OutNo == outNo
                                  select new
                                  {
                                      OutNo = Out20.OutNo,
                                      Serno = Out20.Serno,
                                      PartNo = Out20.PartNo,
                                      Spec = stock10.Spec,
                                      Qty = Out20.Qty,
                                      Unit = Out20.Unit,
                                      PPrice = Out20.PPrice,
                                      Price = Out20.Price,
                                      Amount = Out20.Amount,
                                      Discount = Out20.Discount,
                                      Memo = Out20.Memo,
                                      SPrice = Out20.SPrice,
                                      TaxType = stock10.TaxType,
                                      stQty = stock10.StQty,
                                  }).FirstOrDefaultAsync();

                return Json(data);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 新增詳細資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddDetail(string OutNo)
        {
            try
            {
                AddDetailViewModel vm = new AddDetailViewModel();

                #region SelectList

                //產品清單資料
                var stock10ListData = from stock10 in _context.Stock10
                                      select new
                                      {
                                          id = stock10.PartNo,
                                          text = stock10.PartNo + stock10.Spec,
                                          unit = stock10.Unit,
                                          taxType = stock10.TaxType,
                                      };

                //產品清單資料
                ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

                //單位
                ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="應稅", Value="應稅" },
                   new SelectListItem {Text="免稅", Value="免稅" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                #endregion

                vm.out20List = await (from Out20 in _context.Out20
                                      join stock10 in _context.Stock10
                                      on Out20.PartNo equals stock10.PartNo
                                      where Out20.OutNo == OutNo
                                      select new Out20List
                                      {
                                          PartNo = Out20.PartNo,
                                          Serno = Out20.Serno,
                                      }).ToListAsync();

                ////品項資料
                //vm.phaseList = await _context.Phase20.Where(x => x.Whereuse == "產品分類").Select(x => new Phase20List
                //{
                //    WhereUser = x.Whereuse,
                //    Phase = x.Phase,
                //    SerNo = x.Serno
                //}).ToListAsync();

                // 品項資料
                vm.phaseList = await _context.StockTypes.Where(x => x.TYPE_ISOPEN == true).OrderBy(x => x.TYPE_ORDER).Select(x => new Phase20List
                {
                    WhereUser = "",
                    Phase = x.TYPE_NAME,
                    SerNo = x.TYPE_ORDER.ToString(),
                }).ToListAsync();

                //預設資料
                vm.OutNo = OutNo;
                vm.CoNo = _context.Out10.Find(OutNo)?.CoNo;
                var cst_pricetype = _context.Cstm10.Find(_context.Out10.Find(OutNo).CoNo).PriceType;
                vm.PriceType = _context.PriceType.Where(x => x.PT_VALUE == cst_pricetype).First().PT_STOCKCOL;
                

                return View(vm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 新增詳細資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetail(AddDetailViewModel vm)
        {
            try
            {
                vm.IsTrue = false;

                //先取得序號
                var serNo = 0;

                var checkData = _context.Out20.Where(x => x.OutNo == vm.OutNo).Any();

                if (!checkData)
                {
                    serNo = 1;
                }
                else
                {
                    var lastSerNo = _context.Out20.Where(x => x.OutNo == vm.OutNo).OrderByDescending(x => x.Serno);

                    serNo = lastSerNo.First().Serno + 1;
                }

                //品名規格
                var specName = _context.Stock10.Where(x => x.PartNo == vm.PartNo).FirstOrDefault().Spec;

                //新增所需資料(廠商)
                Out20 insertData = new Out20
                {
                    OutNo = vm.OutNo,
                    Serno = serNo,
                    PartNo = vm.PartNo,
                    Qty = vm.Qty ?? 0,
                    Price = vm.Price ?? 0,
                    Amount = vm.Amount ?? 0,
                    Discount = vm.Discount ?? 0,
                    Unit = vm.Unit,
                    Memo = vm.Memo ?? string.Empty,
                    SPrice = vm.SPrice ?? 0,
                    PPrice = vm.PPrice ?? 0,
                };

                _context.Out20.Add(insertData);
                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                if (vm.IsTrue)
                {
                    var Out10Data = _context.Out10.Where(x => x.OutNo == vm.OutNo).FirstOrDefault();
                    var Out20Data = (from Out20 in _context.Out20
                                     join stock10 in _context.Stock10
                                     on Out20.PartNo equals stock10.PartNo
                                     where Out20.OutNo == vm.OutNo
                                     select new
                                     {
                                         OutNo = Out20.OutNo,
                                         Serno = Out20.Serno,
                                         PartNo = Out20.PartNo,
                                         Spec = stock10.Spec,
                                         Qty = Out20.Qty,
                                         Unit = Out20.Unit,
                                         PPrice = Out20.PPrice,
                                         Price = Out20.Price,
                                         Amount = Out20.Amount,
                                         Discount = Out20.Discount,
                                         Memo = Out20.Memo,
                                         SPrice = Out20.SPrice,
                                         TaxType = stock10.TaxType,
                                         Kg = stock10.InitQty1,
                                     }).ToList();

                    // 需要重新計算的資料
                    double? total0 = 0; // 應稅合計
                    double? total1 = 0; // 免稅合計
                    double? Kg = 0; // 重量合計
                    double? cashdis = 0; // 折扣金額                    
                    double? notget = 0; // 未收金額
                    double? total = 0;

                    foreach (var item in Out20Data)
                    {
                        //total1 = 應稅 total0 = 免稅

                        switch (item.TaxType)
                        {
                            case "應稅":
                                total1 += (double)Math.Round((decimal)item.Amount, 2);
                                break;
                            case "免稅":
                                total0 += (double)Math.Round((decimal)item.Amount, 2);
                                break;
                            default:
                                break;
                        }

                        if (string.IsNullOrEmpty(item.Kg.ToString()))
                        {
                            Kg += item.Kg;
                        }
                    }

                    Out10Data.Total1 = total1;
                    Out10Data.Total0 = total0;
                    Out10Data.Kg = Kg;

                    // 總金額
                    total = (double)Math.Round((decimal)(total0 + total1),2);
                    Out10Data.Total = total;
                    // 折扣金額
                    cashdis = (double)Math.Round(Convert.ToDecimal((total * (Out10Data.Discount ?? 0)) / 100), 2);
                    Out10Data.CashDis = cashdis ?? 0;
                    // 已收
                    var yesget = Out10Data.YesGet ?? 0;
                    var subtot = Out10Data.SubTot ?? 0;
                    //未收金額
                    notget = (double)Math.Round(Convert.ToDecimal(total - yesget - subtot - cashdis),2);
                    Out10Data.NotGet = notget;

                    await _context.SaveChangesAsync();
                }

                return Json(vm);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="QuNo"></param>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditDetail(string OutNo, int SerNo)
        {
            EditDetailViewModel vm = new EditDetailViewModel();

            try
            {
                if (string.IsNullOrEmpty(OutNo) || SerNo <= 0)
                {
                    return NotFound();
                }

                #region SelectList

                //產品清單資料
                var stock10ListData = from stock10 in _context.Stock10
                                      select new
                                      {
                                          id = stock10.PartNo,
                                          text = stock10.PartNo + stock10.Spec,
                                          unit = stock10.Unit,
                                          taxType = stock10.TaxType,
                                      };

                //產品清單資料
                ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

                //單位
                ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="應稅", Value="應稅" },
                   new SelectListItem {Text="免稅", Value="免稅" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                #endregion

                //資料
                var Out20Data = (from Out20 in _context.Out20
                                 join stock10 in _context.Stock10
                                 on Out20.PartNo equals stock10.PartNo
                                 where Out20.OutNo == OutNo && Out20.Serno == SerNo
                                 select new
                                 {
                                     OutNo = Out20.OutNo,
                                     Serno = Out20.Serno,
                                     PartNo = Out20.PartNo,
                                     Spec = stock10.Spec,
                                     Qty = Out20.Qty,
                                     Unit = Out20.Unit,
                                     PPrice = Out20.PPrice,
                                     Price = Out20.Price,
                                     Amount = Out20.Amount,
                                     Discount = Out20.Discount,
                                     Memo = Out20.Memo,
                                     SPrice = Out20.SPrice,
                                     TaxType = stock10.TaxType,
                                     stQty = stock10.StQty,
                                 }).FirstOrDefault();

                if (Out20Data == null)
                {
                    return NotFound();
                }
                vm.CONO = _context.Out10.Find(OutNo)?.CoNo;
                vm.OutNo = Out20Data.OutNo;
                vm.Serno = Out20Data.Serno;
                vm.PartNo = Out20Data.PartNo;
                vm.Spec = Out20Data.Spec;
                vm.Qty = Out20Data.Qty;
                vm.Unit = Out20Data.Unit;
                vm.PPrice = Out20Data.PPrice;
                vm.Price = Out20Data.Price;
                vm.Amount = Out20Data.Amount;
                vm.Discount = Out20Data.Discount;
                vm.Memo = Out20Data.Memo;
                vm.SPrice = Out20Data.SPrice;
                vm.TaxType = Out20Data.TaxType;
                var cstm_pricetype = _context.Cstm10.Find(_context.Out10.Find(vm.OutNo).CoNo).PriceType;
                vm.PriceType = _context.PriceType.Where(x => x.PT_VALUE == cstm_pricetype).FirstOrDefault().PT_STOCKCOL;
                vm.stQty = Out20Data.stQty;
                //清單資料
                var Out20List = await _context.Out20.Select(x => new Out20List
                {
                    OutNo = x.OutNo,
                    PartNo = x.PartNo,
                    Serno = x.Serno,
                }).ToListAsync();

                if (Out20List != null)
                {
                    Out20List = Out20List.Where(x => x.OutNo == OutNo && x.Serno == SerNo).ToList();
                    vm.out20List = Out20List;

                    return View(vm);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetail(EditDetailViewModel vm)
        {
            vm.IsTrue = false;

            var checkData = _context.Out20.Where(x => x.OutNo == vm.OutNo && x.Serno == vm.Serno).FirstOrDefault();

            if (checkData != null)
            {
                //checkData.PartNo = vm.PartNo;
                checkData.Qty = vm.Qty ?? 0;
                checkData.Price = vm.Price ?? 0;
                checkData.Amount = vm.Amount ?? 0;
                checkData.Discount = vm.Discount ?? 0;
                checkData.Unit = vm.Unit;
                checkData.Memo = vm.Memo ?? string.Empty;
                checkData.SPrice = vm.SPrice ?? 0;
                checkData.PPrice = vm.PPrice ?? 0;

                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                if (vm.IsTrue)
                {
                    var Out10Data = _context.Out10.Where(x => x.OutNo == vm.OutNo).FirstOrDefault();
                    var Out20Data = (from Out20 in _context.Out20
                                     join stock10 in _context.Stock10
                                     on Out20.PartNo equals stock10.PartNo
                                     where Out20.OutNo == vm.OutNo
                                     select new
                                     {
                                         OutNo = Out20.OutNo,
                                         Serno = Out20.Serno,
                                         PartNo = Out20.PartNo,
                                         Spec = stock10.Spec,
                                         Qty = Out20.Qty,
                                         Unit = Out20.Unit,
                                         PPrice = Out20.PPrice,
                                         Price = Out20.Price,
                                         Amount = Out20.Amount,
                                         Discount = Out20.Discount,
                                         Memo = Out20.Memo,
                                         SPrice = Out20.SPrice,
                                         TaxType = stock10.TaxType,
                                         Kg = stock10.InitQty1,
                                     }).ToList();

                    // 需要重新計算的資料
                    double? total0 = 0; // 應稅合計
                    double? total1 = 0; // 免稅合計
                    double? Kg = 0; // 重量合計
                    double? cashdis = 0; // 折扣金額                    
                    double? notget = 0; // 未收金額
                    double? total = 0;

                    foreach (var item in Out20Data)
                    {
                        //total1 = 應稅 total0 = 免稅

                        switch (item.TaxType)
                        {
                            case "應稅":
                                total1 += (double)Math.Round((decimal)item.Amount, 2);
                                break;
                            case "免稅":
                                total0 += (double)Math.Round((decimal)item.Amount, 2);
                                break;
                            default:
                                break;
                        }

                        if (string.IsNullOrEmpty(item.Kg.ToString()))
                        {
                            Kg += item.Kg;
                        }
                    }

                    Out10Data.Total1 = total1;
                    Out10Data.Total0 = total0;
                    Out10Data.Kg = Kg;

                    // 總金額
                    total = (double)Math.Round((decimal)(total0 + total1), 2); ;
                    Out10Data.Total = total;
                    // 折扣金額
                    cashdis = (double)Math.Round(Convert.ToDecimal((total * (Out10Data.Discount ?? 0)) / 100), 2);
                    Out10Data.CashDis = cashdis ?? 0;
                    // 已收
                    var yesget = Out10Data.YesGet ?? 0;
                    var subtot = Out10Data.SubTot ?? 0;
                    //未收金額
                    notget = (double)Math.Round(Convert.ToDecimal(total - yesget - subtot - cashdis), 2);
                    Out10Data.NotGet = notget;

                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                return NotFound();
            }

            return Json(vm);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteDetail(string OutNo, int SerNo)
        {

            var deleteData = _context.Out20.Where(x => x.OutNo == OutNo && x.Serno == SerNo).FirstOrDefault();

            if (deleteData == null)
            {
                return NotFound();
            }

            _context.Out20.Remove(deleteData);
            await _context.SaveChangesAsync();

            DeleteResponse vm = new DeleteResponse();

            vm.IsTrue = true;

            if (vm.IsTrue)
            {
                var Out10Data = _context.Out10.Where(x => x.OutNo == OutNo).FirstOrDefault();
                var Out20Data = (from Out20 in _context.Out20
                                 join stock10 in _context.Stock10
                                 on Out20.PartNo equals stock10.PartNo
                                 where Out20.OutNo == OutNo
                                 select new
                                 {
                                     OutNo = Out20.OutNo,
                                     Serno = Out20.Serno,
                                     PartNo = Out20.PartNo,
                                     Spec = stock10.Spec,
                                     Qty = Out20.Qty,
                                     Unit = Out20.Unit,
                                     PPrice = Out20.PPrice,
                                     Price = Out20.Price,
                                     Amount = Out20.Amount,
                                     Discount = Out20.Discount,
                                     Memo = Out20.Memo,
                                     SPrice = Out20.SPrice,
                                     TaxType = stock10.TaxType,
                                     Kg = stock10.InitQty1,
                                 }).ToList();

                // 需要重新計算的資料
                double? total0 = 0; // 應稅合計
                double? total1 = 0; // 免稅合計
                double? Kg = 0; // 重量合計
                double? cashdis = 0; // 折扣金額                    
                double? notget = 0; // 未收金額
                double? total = 0;

                foreach (var item in Out20Data)
                {
                    //total1 = 應稅 total0 = 免稅

                    switch (item.TaxType)
                    {
                        case "應稅":
                            total1 += item.Amount;
                            break;
                        case "免稅":
                            total0 += item.Amount;
                            break;
                        default:
                            break;
                    }

                    if (string.IsNullOrEmpty(item.Kg.ToString()))
                    {
                        Kg += item.Kg;
                    }
                }

                Out10Data.Total1 = total1;
                Out10Data.Total0 = total0;
                Out10Data.Total = total1 + total0;
                Out10Data.Kg = Kg;

                // 總金額
                total = total0 + total1;
                Out10Data.Total = total;
                // 折扣金額
                cashdis = (double)Math.Round(Convert.ToDecimal((total * (Out10Data.Discount ?? 0)) / 100), 2);
                Out10Data.CashDis = cashdis ?? 0;
                // 已收
                var yesget = Out10Data.YesGet ?? 0;
                var subtot = Out10Data.SubTot ?? 0;
                //未收金額
                notget = (double)Math.Round(Convert.ToDecimal(total - yesget - subtot - cashdis), 2);
                Out10Data.NotGet = notget;


                await _context.SaveChangesAsync();
            }

            vm.OutNo = OutNo;

            return Json(vm);
        }

        #endregion 子表

        #region 其他

        /// <summary>
        /// 過帳
        /// </summary>
        /// <param name="OutNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> StockPass(string OutNo)
        {
            var checkData = await _context.Out10.Where(x => x.OutNo == OutNo).FirstOrDefaultAsync();

            if (checkData != null)
            {
                checkData.StockPass = "Y";
            }

            await _context.SaveChangesAsync();

            return Json(null);
        }

        /// <summary>
        /// 預覽C208
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Preview(C208ExportViewModel vm)
        {
            #region SelectList

            //客戶清單資料
            var coNoList = from cstm10 in _context.Cstm10
                           select new
                           {
                               CoNo = cstm10.CoNo,
                               Coname = cstm10.CoNo + string.Empty + cstm10.Coname
                           };

            //客戶清單
            ViewBag.CoNoList = new SelectList(coNoList.ToList(), "CoNo", "Coname", string.Empty);

            //業務人員清單
            var peNoList = from pepo10 in _context.Pepo10
                           where pepo10.Posi == "業務員"
                           select new
                           {
                               PeNo = pepo10.PeNo,
                               Name = pepo10.PeNo + string.Empty + pepo10.Name
                           };

            //業務人員清單
            ViewBag.PeNoList = new SelectList(peNoList.ToList(), "PeNo", "Name", string.Empty);

            //司機人員清單
            var driveNoList = from pepo10 in _context.Pepo10
                              where pepo10.Posi == "司機"
                              select new
                              {
                                  DriveNo = pepo10.PeNo,
                                  Name = pepo10.PeNo + string.Empty + pepo10.Name
                              };

            //司機人員清單
            ViewBag.DriveNoList = new SelectList(driveNoList.ToList(), "DriveNo", "Name", string.Empty);

            //出貨類別
            var outTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="出貨", Value="1" },
                   new SelectListItem {Text="出貨退回", Value="2" },
                };

            ViewBag.OutTypeList = new SelectList(outTypeList, "Value", "Text", string.Empty);

            //稅別
            var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="應稅", Value="應稅" },
                   new SelectListItem {Text="免稅", Value="免稅" },
                };

            ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

            //產品清單資料
            var stock10List = from stock10 in _context.Stock10
                              select new
                              {
                                  PartNo = stock10.PartNo,
                                  Spec = stock10.PartNo + string.Empty + stock10.Spec
                              };

            //產品清單
            ViewBag.StockList = new SelectList(stock10List.ToList(), "PartNo", "Spec", string.Empty);

            #endregion

            return View(vm);
        }

        /// <summary>
        /// 匯出C208資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> C208Export(C208ExportViewModel vm)
        {
            //var out10Data = await _context.Out10.ToListAsync();
            var out10Data = await (from out10 in _context.Out10
                                   join out20 in _context.Out20
                                   on out10.OutNo equals out20.OutNo
                                   join stock10 in _context.Stock10
                                   on out20.PartNo equals stock10.PartNo
                                   select new
                                   {
                                       out10 = out10,
                                       out20 = out20,
                                       stock10 = stock10
                                   }).ToListAsync();

            //第一筆
            var firstData = _context.Out10.OrderBy(x => x.OutNo).First().OutNo;

            //最後一筆
            var lastData = _context.Out10.OrderByDescending(x => x.OutNo).First().OutNo;

            if (out10Data == null)
            {
                return View(new C208ExportViewModel());
            }

            string bOutDate = vm.BeginOutDate.Replace("-", "");
            string eOutDate = vm.EndOutDate.Replace("-", "");

            //先找出時間區段資料
            out10Data = await out10Data.Where(x => x.out10.OutDate.CompareTo(bOutDate) >= 0 && x.out10.OutDate.CompareTo(eOutDate) <= 0).ToListAsync();

            if (!string.IsNullOrEmpty(vm.BeginCoNo))
            {
                var bCoNo = vm.BeginCoNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.CoNo.CompareTo(bCoNo) >= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.EndCoNo))
            {
                var eCoNo = vm.EndCoNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.CoNo.CompareTo(eCoNo) <= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.BeginPeNo))
            {
                var bPeNo = vm.BeginPeNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.PeNo.CompareTo(bPeNo) >= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.EndPeNo))
            {
                var bCoNo = vm.EndPeNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.PeNo.CompareTo(bCoNo) <= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.BeginDriveNo))
            {
                var bDriveNo = vm.BeginDriveNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.DriveNo.CompareTo(bDriveNo) >= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.EndDriveNo))
            {
                var bDriveNo = vm.EndDriveNo.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.DriveNo.CompareTo(bDriveNo) <= 0).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.PayMonth))
            {
                var payMonth = vm.PayMonth.Replace("-", "");

                out10Data = await out10Data.Where(x => x.out10.Paymonth.Contains(payMonth)).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.OutType))
            {
                out10Data = await out10Data.Where(x => x.out10.OutType.Contains(vm.OutType)).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.TaxType))
            {
                out10Data = await out10Data.Where(x => x.stock10.TaxType.Contains(vm.OutType)).ToListAsync();
            }

            if (!string.IsNullOrEmpty(vm.PartNo))
            {
                out10Data = await out10Data.Where(x => x.stock10.TaxType.Contains(vm.OutType)).ToListAsync();
            }

            //if (string.IsNullOrEmpty(vm.StartVendorNo) && string.IsNullOrEmpty(vm.EndVendorNo))
            //{
            //    vendorListData = await vendorListData
            //        .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            //}
            //else if (string.IsNullOrEmpty(vm.StartVendorNo))
            //{
            //    vendorListData = await vendorListData
            //        .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
            //}
            //else if (string.IsNullOrEmpty(vm.EndVendorNo))
            //{
            //    vendorListData = await vendorListData
            //        .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            //}
            //else
            //{
            //    vendorListData = await vendorListData
            //        .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
            //}

            //vm.vendorList = vendorListData;

            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("出貨資料統計表"); //建立sheet

            //設定樣式
            //ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            //IFont headerfont = hssfworkbook.CreateFont();
            //headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
            //headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
            //headerfont.FontName = "微軟正黑體";
            //headerfont.FontHeightInPoints = 20;
            //headerfont.Boldweight = (short)FontBoldWeight.Bold;
            //headerStyle.SetFont(headerfont);

            //新增標題列
            sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
            //sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
            sheet.GetRow(0).CreateCell(0).SetCellValue("出貨日期範圍");
            sheet.GetRow(0).CreateCell(2).SetCellValue("至");
            sheet.GetRow(0).CreateCell(5).SetCellValue("司機");
            sheet.GetRow(0).CreateCell(6).SetCellValue("幣別");
            sheet.GetRow(0).CreateCell(7).SetCellValue("客戶");
            sheet.GetRow(0).CreateCell(9).SetCellValue("至");
            //sheet.GetRow(0).CreateCell(6).SetCellValue("電話二");
            //sheet.GetRow(0).CreateCell(7).SetCellValue("通訊地址一");
            //sheet.GetRow(0).CreateCell(8).SetCellValue("倉庫地址");
            //sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            //sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            //sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");
            //sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式

            //填入資料
            int rowIndex = 1;
            //for (int row = 0; row < out10Data.Count(); row++)
            //{
            //    sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(vendorListData[row].VendorNo);
            //    sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(vendorListData[row].Vename);
            //    sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(vendorListData[row].Uniform);
            //    sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(vendorListData[row].Fax);
            //    sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(vendorListData[row].Boss);
            //    sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(vendorListData[row].Tel1);
            //    sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(vendorListData[row].Tel2);
            //    sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(vendorListData[row].Invoaddr);
            //    sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(vendorListData[row].Factaddr);

            //    rowIndex++;
            //}

            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);

            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"廠商通訊錄.xls"));
        }

        #endregion 其他


        //// 調整正式站未收金額資料
        //public void debugNotGet()
        //{
        //    int start = 387;

        //    var outList = _context.Out10.Where(x => x.OutNo.StartsWith("2023045")).ToList();

        //    foreach(var outlist in outList){
        //        var outNumber = outlist.OutNo.Replace("2023045", "");
        //        if(Convert.ToInt32(outNumber) >= start)
        //        {
        //            // 金額總計
        //            var total = outlist.Total ?? 0;

        //            // -現金折扣
        //            var cstDis = _context.Cstm10.Find(outlist.CoNo).Discount;
        //            var disSum = (total * cstDis) / 100;
        //            total  = total - Convert.ToDouble(disSum);

        //            // -折讓金額
        //            var subTot = outlist.SubTot ?? 0;
        //            total -= subTot;

        //            // -已收
        //            var yesGet = outlist.YesGet ?? 0;
        //            total -= yesGet;

        //            // =未收金額
        //            var notGet = total ;

        //            // 儲存
        //            var outData = _context.Out10.Find(outlist.OutNo);
        //            outData.NotGet = notGet;
        //            //outData.Discount = cstDis;
                    
        //            _context.SaveChanges();
        //        }                
        //    }

        //}
    }
}
