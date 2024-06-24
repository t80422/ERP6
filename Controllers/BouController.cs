using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using ERP6.ViewModels.Bou;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ERP6.Controllers
{
    public class BouController : Controller
    {
        private readonly EEPEF01Context _context;

        public BouController(EEPEF01Context context)
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

                //付款方式清單資料
                ViewBag.PaymentList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款方式").ToList(), "Phase", "Phase", string.Empty);

                #endregion SelectList

                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.QuNo))
                    {
                        vm = await (from bou10 in _context.Bou10
                                    where bou10.QuNo == vm.QuNo
                                    select new IndexViewModel
                                    {
                                        QuNo = bou10.QuNo,
                                        QuDate = !string.IsNullOrEmpty(bou10.QuDate) ?
                                            DateTime.ParseExact(bou10.QuDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                        CoNo = bou10.CoNo,
                                        Corp = bou10.Corp,
                                        Fax = bou10.Fax,
                                        Atte = bou10.Atte,
                                        Tel = bou10.Tel,
                                        SendAddr = bou10.SendAddr,
                                        Payment = bou10.Payment,
                                        SendDate = !string.IsNullOrEmpty(bou10.SendDate) ?
                                            DateTime.ParseExact(bou10.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                        Memo = bou10.Memo,
                                        Sales = bou10.Sales,//業務人員
                                        Ntus = bou10.Ntus,
                                        Total = bou10.Total,
                                        Userid = bou10.Userid,
                                        PaperTail = bou10.PaperTail,
                                        Total1 = bou10.Total1,
                                        Total0 = bou10.Total0,
                                        YnPass = bou10.YnPass,
                                    }).FirstOrDefaultAsync();

                        //確認資料
                        var checkData = _context.Bou20.Where(x => x.QuNo == vm.QuNo).Any();

                        //找出子表清單
                        if (checkData)
                        {
                            vm.bou20List = await (from bou20 in _context.Bou20
                                                  join stock10 in _context.Stock10
                                                  on bou20.PartNo equals stock10.PartNo
                                                  where bou20.QuNo == vm.QuNo
                                                  select new Bou20List
                                                  {
                                                      QuNo = bou20.QuNo,
                                                      Serno = bou20.Serno,
                                                      PartNo = bou20.PartNo,
                                                      Spec = bou20.Spec,
                                                      Qty = bou20.Qty,
                                                      Unit = bou20.Unit,
                                                      Price = bou20.Price,
                                                      Amount = bou20.Amount,
                                                      Discount = bou20.Discount,
                                                      Memo = bou20.Memo,
                                                      SPrice = bou20.SPrice,
                                                      Profit = bou20.Profit,
                                                      TaxType = stock10.TaxType,
                                                  }).ToListAsync();
                        }
                    }
                }

                vm.bou10List = await (from bou10 in _context.Bou10
                                      select new Bou10List
                                      {
                                          QuNo = bou10.QuNo,
                                          QuDate = !string.IsNullOrEmpty(bou10.QuDate) ?
                                              DateTime.ParseExact(bou10.QuDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                          CoNo = bou10.CoNo,
                                          Corp = bou10.Corp,
                                          Fax = bou10.Fax,
                                          Atte = bou10.Atte,
                                          Tel = bou10.Tel,
                                          SendAddr = bou10.SendAddr,
                                          Payment = bou10.Payment,
                                          SendDate = !string.IsNullOrEmpty(bou10.SendDate) ?
                                              DateTime.ParseExact(bou10.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                          Memo = bou10.Memo,
                                          Sales = bou10.Sales,//業務人員
                                          Ntus = bou10.Ntus,
                                          Total = bou10.Total,
                                          Userid = bou10.Userid,
                                          PaperTail = bou10.PaperTail,
                                          Total1 = bou10.Total1,
                                          Total0 = bou10.Total0,
                                          YnPass = bou10.YnPass,
                                      }).ToListAsync();

                if (vm.IsSearch)
                {
                    //報價日期
                    if (!string.IsNullOrEmpty(vm.QuDate))
                    {
                        vm.QuDate = vm.QuDate.Replace("-", "");

                        vm.bou10List = await vm.bou10List.Where(x => x.QuDate == vm.QuDate).ToListAsync();
                    }

                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.bou10List = await vm.bou10List.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();
                    }

                    //報價單號
                    if (!string.IsNullOrEmpty(vm.QuNo))
                    {
                        vm.bou10List = await vm.bou10List.Where(x => x.QuNo.Contains(vm.QuNo)).ToListAsync();
                    }

                    //公司名稱
                    if (!string.IsNullOrEmpty(vm.Corp))
                    {
                        vm.bou10List = await vm.bou10List.Where(x => x.QuNo.Contains(vm.Corp)).ToListAsync();
                    }

                    //聯絡人
                    if (!string.IsNullOrEmpty(vm.Atte))
                    {
                        vm.bou10List = await vm.bou10List.Where(x => x.Atte.Contains(vm.Atte)).ToListAsync();
                    }

                    //聯絡電話
                    if (!string.IsNullOrEmpty(vm.Tel))
                    {
                        vm.bou10List = await vm.bou10List.Where(x => x.Tel.Contains(vm.Tel)).ToListAsync();
                    }
                }

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> Search(string QuNo)
        {
            //先找出主表資料
            IndexViewModel vm = await (from bou10 in _context.Bou10
                                       join pepo10 in _context.Pepo10
                                       on bou10.Sales equals pepo10.PeNo into ps
                                       from pepo10 in ps.DefaultIfEmpty()
                                       where bou10.QuNo == QuNo
                                       select new IndexViewModel
                                       {
                                           QuNo = bou10.QuNo,
                                           QuDate = !string.IsNullOrEmpty(bou10.QuDate) ?
                                               DateTime.ParseExact(bou10.QuDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           CoNo = bou10.CoNo,
                                           Corp = bou10.Corp,
                                           Fax = bou10.Fax,
                                           Atte = bou10.Atte,
                                           Tel = bou10.Tel,
                                           SendAddr = bou10.SendAddr,
                                           Payment = bou10.Payment,
                                           SendDate = !string.IsNullOrEmpty(bou10.SendDate) ?
                                               DateTime.ParseExact(bou10.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           Memo = bou10.Memo,
                                           Sales = bou10.Sales,//業務人員
                                           Ntus = bou10.Ntus,
                                           Total = bou10.Total,
                                           Userid = bou10.Userid,
                                           PaperTail = bou10.PaperTail,
                                           Total1 = bou10.Total1,
                                           Total0 = bou10.Total0,
                                           YnPass = bou10.YnPass,
                                       }).FirstOrDefaultAsync();

            //子表列表資料
            var bou20List = await (from bou20 in _context.Bou20
                                   join stock10 in _context.Stock10
                                   on bou20.PartNo equals stock10.PartNo
                                   where bou20.QuNo == QuNo
                                   select new Bou20List
                                   {
                                       QuNo = bou20.QuNo,
                                       Serno = bou20.Serno,
                                       PartNo = bou20.PartNo,
                                       Spec = bou20.Spec,
                                       Qty = bou20.Qty,
                                       Unit = bou20.Unit,
                                       Price = bou20.Price,
                                       Amount = bou20.Amount,
                                       Discount = bou20.Discount,
                                       Memo = bou20.Memo,
                                       SPrice = bou20.SPrice,
                                       Profit = bou20.Profit,
                                       TaxType = stock10.TaxType,
                                   }).ToListAsync();

            if (bou20List != null)
            {
                vm.bou20List = bou20List;
            }

            return Json(vm);
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

                //付款方式清單資料
                ViewBag.PaymentList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款方式").ToList(), "Phase", "Phase", string.Empty);

                #endregion SelectList

                AddViewModel vm = new AddViewModel();

                //取得資料
                var bou10List = await (from bou10 in _context.Bou10
                                       select new Bou10List
                                       {
                                           QuNo = bou10.QuNo,
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
                                       }).Take(100).ToListAsync();

                vm.bou10List = bou10List;

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
                #region SelectList

                //付款方式清單資料
                ViewBag.PaymentList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款方式").ToList(), "Phase", "Phase", string.Empty);

                #endregion SelectList

                var strDate = DateTime.Now.ToString("yyyyMM");
                var autoQuNo = string.Empty;

                var bou10DataCount = _context.Bou10.Where(x => x.QuNo.Contains(strDate)).Count();

                if (bou10DataCount == 0)
                {
                    //等於0代表當月沒資料 直接新增
                    autoQuNo = strDate + "0001";
                }
                else
                {
                    bou10DataCount = bou10DataCount + 1;
                    autoQuNo = strDate + bou10DataCount.ToString().PadLeft(4, '0');
                }

                //新增所需資料(廠商)
                Bou10 insertData = new Bou10
                {
                    QuNo = autoQuNo,
                    QuDate = string.IsNullOrEmpty(vm.QuDate) ? null : vm.QuDate.Replace("-", ""),
                    CoNo = vm.CoNo,
                    Corp = vm.Corp,
                    Fax = vm.Fax,
                    Atte = vm.Atte,
                    Tel = vm.Tel,
                    SendAddr = vm.SendAddr,
                    Payment = vm.Payment,
                    SendDate = string.IsNullOrEmpty(vm.SendDate) ? null : vm.SendDate.Replace("-", ""),
                    Memo = vm.Memo,
                    Sales = vm.Sales,
                    Ntus = vm.Ntus,
                    Userid = vm.Userid,
                    PaperTail = vm.PaperTail,
                    YnPass = vm.YnPass,
                };

                _context.Bou10.Add(insertData);
                await _context.SaveChangesAsync();

                //設定預設資料
                vm.QuNo = autoQuNo;
                vm.IsTrue = true;

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
        /// <param name="InNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string QuNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                #region SelectList

                //付款方式清單資料
                ViewBag.PaymentList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款方式").ToList(), "Phase", "Phase", string.Empty);

                #endregion SelectList

                if (string.IsNullOrEmpty(QuNo))
                {
                    return NotFound();
                }

                //資料
                var bou10Data = _context.Bou10
                    .Where(x => x.QuNo == QuNo).FirstOrDefault();

                if (bou10Data == null)
                {
                    return NotFound();
                }

                vm.QuNo = bou10Data.QuNo;
                vm.QuDate = string.IsNullOrEmpty(bou10Data.QuDate) ?
                                    null : DateTime.ParseExact(bou10Data.QuDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.CoNo = bou10Data.CoNo;
                vm.Corp = bou10Data.Corp;
                vm.Fax = bou10Data.Fax;
                vm.Atte = bou10Data.Atte;
                vm.Tel = bou10Data.Tel;
                vm.SendAddr = bou10Data.SendAddr;
                vm.Payment = bou10Data.Payment;
                vm.SendDate = string.IsNullOrEmpty(bou10Data.SendDate) ?
                                    null : DateTime.ParseExact(bou10Data.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.Memo = bou10Data.Memo;
                vm.Sales = bou10Data.Sales;
                vm.Ntus = bou10Data.Ntus;
                vm.PaperTail = bou10Data.PaperTail;

                //只顯示不修改
                vm.Userid = bou10Data.Userid;
                vm.YnPass = vm.YnPass;

                //清單資料
                var bou10List = await _context.Bou10.Select(x => new Bou10List
                {
                    QuNo = x.QuNo,
                }).ToListAsync();

                if (bou10List != null)
                {
                    bou10List = bou10List.Where(x => x.QuNo == QuNo).ToList();
                    vm.bou10List = bou10List;

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
                #region SelectList

                //付款方式清單資料
                ViewBag.PaymentList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款方式").ToList(), "Phase", "Phase", string.Empty);

                #endregion SelectList

                vm.IsTrue = false;

                if (vm != null)
                {
                    Bou10 updateData = new Bou10
                    {
                        QuNo = vm.QuNo,
                        QuDate = string.IsNullOrEmpty(vm.QuDate) ? null : vm.QuDate.Replace("-", ""),
                        CoNo = vm.CoNo,
                        Corp = vm.Corp,
                        Fax = vm.Fax,
                        Atte = vm.Atte,
                        Tel = vm.Tel,
                        SendAddr = vm.SendAddr,
                        Payment = vm.Payment,
                        SendDate = string.IsNullOrEmpty(vm.SendDate) ? null : vm.SendDate.Replace("-", ""),
                        Memo = vm.Memo,
                        Sales = vm.Sales,
                        Ntus = vm.Ntus,
                        Userid = vm.Userid,
                        PaperTail = vm.PaperTail,
                        YnPass = vm.YnPass,
                    };

                    _context.Bou10.Update(updateData);

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
        public async Task<IActionResult> Delete(string QuNo)
        {
            var checkBou10Data = await _context.Bou10.Where(x => x.QuNo == QuNo).FirstOrDefaultAsync();

            if (checkBou10Data == null)
            {
                return NotFound();
            }

            //先清除子表資料
            var removeBou20Data = _context.Bou20.Where(x => x.QuNo == QuNo).ToList();

            if (removeBou20Data != null)
            {
                _context.Bou20.RemoveRange(removeBou20Data);
                await _context.SaveChangesAsync();
            }

            //再清除主表資料
            _context.Bou10.Remove(checkBou10Data);
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
                    if (!string.IsNullOrEmpty(vm.QuNo))
                    {
                        var bouData = (from bou20 in _context.Bou20
                                       join stock10 in _context.Stock10
                                       on bou20.PartNo equals stock10.PartNo
                                       where bou20.QuNo == vm.QuNo && bou20.Serno == vm.Serno
                                       select new
                                       {
                                           bou20,
                                           stock10
                                       }).FirstOrDefault();

                        if (bouData != null)
                        {
                            //裝載資料
                            vm = new IndexDetailViewModel
                            {
                                QuNo = bouData.bou20.QuNo,
                                Serno = bouData.bou20.Serno,
                                PartNo = bouData.bou20.PartNo,
                                Spec = bouData.bou20.Spec,
                                Qty = bouData.bou20.Qty,
                                Unit = bouData.bou20.Unit,
                                Price = bouData.bou20.Price,
                                Amount = bouData.bou20.Amount,
                                Discount = bouData.bou20.Discount,
                                Memo = bouData.bou20.Memo,
                                SPrice = bouData.bou20.SPrice,
                                Profit = bouData.bou20.Profit,
                                TaxType = bouData.stock10.TaxType,
                            };
                        }

                        //取得Stock21List資料
                        var checkBou20Data = _context.Bou20.Where(x => x.QuNo == vm.QuNo).Any();

                        if (checkBou20Data)
                        {
                            vm.bou20List = (from bou20 in _context.Bou20
                                            join stock10 in _context.Stock10
                                            on bou20.PartNo equals stock10.PartNo
                                            where bou20.QuNo == vm.QuNo
                                            select new Bou20List
                                            {
                                                PartNo = bou20.PartNo,
                                                Serno = bou20.Serno,
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
                        vm.bou20List = await vm.bou20List.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
                    }
                }

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> SearchDetail(string PartNo, int SerNo)
        {
            try
            {
                var data = await (from bou20 in _context.Bou20
                                  join stock10 in _context.Stock10
                                  on bou20.PartNo equals stock10.PartNo
                                  where bou20.PartNo == PartNo && bou20.Serno == SerNo
                                  select new
                                  {
                                      QuNo = bou20.QuNo,
                                      Serno = bou20.Serno,
                                      PartNo = bou20.PartNo,
                                      Spec = bou20.Spec,
                                      Qty = bou20.Qty,
                                      Unit = bou20.Unit,
                                      Price = bou20.Price,
                                      Amount = bou20.Amount,
                                      Discount = bou20.Discount,
                                      Memo = bou20.Memo,
                                      SPrice = bou20.SPrice,
                                      Profit = bou20.Profit,
                                      TaxType = stock10.TaxType,
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
        public async Task<IActionResult> AddDetail(string QuNo)
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

                vm.bou20List = await (from bou20 in _context.Bou20
                                      join stock10 in _context.Stock10
                                      on bou20.PartNo equals stock10.PartNo
                                      where bou20.QuNo == QuNo
                                      select new Bou20List
                                      {
                                          PartNo = bou20.PartNo,
                                          Serno = bou20.Serno,
                                      }).ToListAsync();

                //預設資料
                vm.QuNo = QuNo;

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

                //先取得序號
                var serNo = 0;

                var checkData = _context.Bou20.Where(x => x.QuNo == vm.QuNo).Any();

                if (!checkData)
                {
                    serNo = 1;
                }
                else
                {
                    var lastSerNo = _context.Bou20.Where(x => x.QuNo == vm.QuNo).OrderByDescending(x => x.Serno);

                    serNo = lastSerNo.First().Serno + 1;
                }

                //品名規格
                var specName = _context.Stock10.Where(x => x.PartNo == vm.PartNo).FirstOrDefault().Spec;

                //新增所需資料(廠商)
                Bou20 insertData = new Bou20
                {
                    QuNo = vm.QuNo,
                    Serno = serNo,
                    PartNo = vm.PartNo,
                    Spec = specName,
                    Qty = vm.Qty ?? 0,
                    Unit = vm.Unit,
                    Price = vm.Price ?? 0,
                    Amount = vm.Amount ?? 0,
                    Discount = vm.Discount ?? 0,
                    Memo = vm.Memo ?? string.Empty,
                    SPrice = vm.SPrice ?? 0,
                    Profit = vm.Profit ?? 0,
                };

                _context.Bou20.Add(insertData);
                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                if (vm.IsTrue)
                {
                    var bou10Data = _context.Bou10.Where(x => x.QuNo == vm.QuNo).FirstOrDefault();
                    var bou20Data = (from bou20 in _context.Bou20
                                     join stock10 in _context.Stock10
                                     on bou20.PartNo equals stock10.PartNo
                                     where bou20.QuNo == vm.QuNo
                                     select new
                                     {
                                         QuNo = bou20.QuNo,
                                         Serno = bou20.Serno,
                                         PartNo = bou20.PartNo,
                                         Spec = bou20.Spec,
                                         Qty = bou20.Qty,
                                         Unit = bou20.Unit,
                                         Price = bou20.Price,
                                         Amount = bou20.Amount,
                                         Discount = bou20.Discount,
                                         Memo = bou20.Memo,
                                         SPrice = bou20.SPrice,
                                         Profit = bou20.Profit,
                                         TaxType = stock10.TaxType,
                                     }).ToList();

                    double? total0 = 0;
                    double? total1 = 0;

                    foreach (var item in bou20Data)
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
                    }

                    bou10Data.Total1 = total1;
                    bou10Data.Total0 = total0;
                    bou10Data.Total = total1 + total0;

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
        /// <param name="BankNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditDetail(string QuNo, int SerNo)
        {
            EditDetailViewModel vm = new EditDetailViewModel();

            try
            {
                if (string.IsNullOrEmpty(QuNo) || SerNo <= 0)
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
                var bou20Data = (from bou20 in _context.Bou20
                                 join stock10 in _context.Stock10
                                 on bou20.PartNo equals stock10.PartNo
                                 where bou20.QuNo == QuNo && bou20.Serno == SerNo
                                 select new
                                 {
                                     QuNo = bou20.QuNo,
                                     Serno = bou20.Serno,
                                     PartNo = bou20.PartNo,
                                     Spec = bou20.Spec,
                                     Qty = bou20.Qty,
                                     Unit = bou20.Unit,
                                     Price = bou20.Price,
                                     Amount = bou20.Amount,
                                     Discount = bou20.Discount,
                                     Memo = bou20.Memo,
                                     SPrice = bou20.SPrice,
                                     Profit = bou20.Profit,
                                     TaxType = stock10.TaxType,
                                 }).FirstOrDefault();

                if (bou20Data == null)
                {
                    return NotFound();
                }

                vm.QuNo = bou20Data.QuNo;
                vm.Serno = bou20Data.Serno;
                vm.PartNo = bou20Data.PartNo;
                vm.Spec = bou20Data.Spec;
                vm.Qty = bou20Data.Qty;
                vm.Unit = bou20Data.Unit;
                vm.Price = bou20Data.Price;
                vm.Amount = bou20Data.Amount;
                vm.Discount = bou20Data.Discount;
                vm.Memo = bou20Data.Memo;
                vm.SPrice = bou20Data.SPrice;
                vm.Profit = bou20Data.Profit;
                vm.TaxType = bou20Data.TaxType;

                //清單資料
                var bou20List = await _context.Bou20.Select(x => new Bou20List
                {
                    QuNo = x.QuNo,
                    PartNo = x.PartNo,
                    Serno = x.Serno,
                }).ToListAsync();

                if (bou20List != null)
                {
                    bou20List = bou20List.Where(x => x.QuNo == QuNo && x.Serno == SerNo).ToList();
                    vm.bou20List = bou20List;

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

            try
            {
                vm.IsTrue = false;

                var checkData = _context.Bou20.Where(x => x.QuNo == vm.QuNo && x.Serno == vm.Serno).FirstOrDefault();

                var specName = _context.Stock10.Where(x => x.PartNo == vm.PartNo).FirstOrDefault().Spec;

                if (checkData != null)
                {
                    //checkData.Serno = vm.Serno;
                    checkData.PartNo = vm.PartNo;
                    checkData.Spec = specName;
                    checkData.Qty = vm.Qty ?? 0;
                    checkData.Unit = vm.Unit;
                    checkData.Price = vm.Price ?? 0;
                    checkData.Amount = vm.Amount ?? 0;
                    checkData.Discount = vm.Discount ?? 0;
                    checkData.Memo = vm.Memo ?? string.Empty;
                    checkData.SPrice = vm.SPrice ?? 0;
                    checkData.Profit = vm.Profit ?? 0;

                    await _context.SaveChangesAsync();

                    vm.IsTrue = true;

                    if (vm.IsTrue)
                    {
                        var bou10Data = _context.Bou10.Where(x => x.QuNo == vm.QuNo).FirstOrDefault();
                        var bou20Data = (from bou20 in _context.Bou20
                                         join stock10 in _context.Stock10
                                         on bou20.PartNo equals stock10.PartNo
                                         where bou20.QuNo == vm.QuNo
                                         select new
                                         {
                                             QuNo = bou20.QuNo,
                                             Serno = bou20.Serno,
                                             PartNo = bou20.PartNo,
                                             Spec = bou20.Spec,
                                             Qty = bou20.Qty,
                                             Unit = bou20.Unit,
                                             Price = bou20.Price,
                                             Amount = bou20.Amount,
                                             Discount = bou20.Discount,
                                             Memo = bou20.Memo,
                                             SPrice = bou20.SPrice,
                                             Profit = bou20.Profit,
                                             TaxType = stock10.TaxType,
                                         }).ToList();

                        double? total0 = 0;
                        double? total1 = 0;

                        foreach (var item in bou20Data)
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
                        }

                        bou10Data.Total1 = total1;
                        bou10Data.Total0 = total0;
                        bou10Data.Total = total1 + total0;

                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    return NotFound();
                }

                return Json(vm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="QuNo"></param>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteDetail(string QuNo, int SerNo)
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

            var deleteData = _context.Bou20.Where(x => x.QuNo == QuNo && x.Serno == SerNo).FirstOrDefault();

            if (deleteData == null)
            {
                return NotFound();
            }

            _context.Bou20.Remove(deleteData);
            await _context.SaveChangesAsync();

            DeleteResponse vm = new DeleteResponse();

            vm.IsTrue = true;
            vm.QuNo = QuNo;

            return Json(vm);
        }

        #endregion 子表
    }
}
