using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using ERP6.ViewModels.Pur;
using Microsoft.AspNetCore.Http;

namespace ERP6.Controllers
{
    public class PurController : Controller
    {
        private readonly EEPEF01Context _context;

        public PurController(EEPEF01Context context)
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

                //表尾
                var paperTailList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="C204-1", Value="C204-1" },
                   new SelectListItem {Text="C204-2", Value="C204-2" },
                };

                ViewBag.PaperTailList = new SelectList(paperTailList, "Value", "Text", string.Empty);

                #endregion SelectList

                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.PurNo))
                    {
                        var purData = (from pur10 in _context.Pur10
                                       where pur10.PurNo == vm.PurNo
                                       select new IndexViewModel
                                       {
                                           PurNo = pur10.PurNo,
                                           PurDate = !string.IsNullOrEmpty(pur10.PurDate) ?
                                           DateTime.ParseExact(pur10.PurDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           SendDate = !string.IsNullOrEmpty(pur10.SendDate) ?
                                           DateTime.ParseExact(pur10.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                           VendorNo = pur10.VendorNo,
                                           Ntus = pur10.Ntus,
                                           PeNo = pur10.PeNo,
                                           Memo = pur10.Memo,
                                           Total1 = pur10.Total1,
                                           Total0 = pur10.Total0,
                                           Total = pur10.Total,
                                           PaperTail = pur10.PaperTail,
                                           Userid = pur10.Userid,
                                       }).FirstOrDefault();

                        //裝載資料
                        vm = new IndexViewModel
                        {
                            PurNo = purData.PurNo,
                            PurDate = purData.PurDate,
                            SendDate = purData.SendDate,
                            VendorNo = purData.VendorNo,
                            Ntus = purData.Ntus,
                            PeNo = purData.PeNo,
                            Memo = purData.Memo,
                            Total1 = purData.Total1,
                            Total0 = purData.Total0,
                            Total = purData.Total,
                            PaperTail = purData.PaperTail,
                            Userid = purData.Userid,
                        };

                        //取得Pur20List資料
                        var checkPur20Data = _context.Pur20.Where(x => x.PurNo == vm.PurNo).Any();

                        if (checkPur20Data)
                        {
                            vm.pur20List = (from pur20 in _context.Pur20
                                            join stock10 in _context.Stock10
                                            on pur20.PartNo equals stock10.PartNo
                                            where pur20.PurNo == vm.PurNo
                                            select new Pur20List
                                            {
                                                Serno = pur20.Serno,
                                                PartNo = pur20.PartNo,
                                                Spec = stock10.Spec,
                                                Qty = pur20.Qty,
                                                Unit = pur20.Unit,
                                                TaxType = stock10.TaxType,
                                                Price = pur20.Price,
                                                Discount = pur20.Discount,
                                                Amount = pur20.Amount,
                                                Memo = pur20.Memo,
                                            }).ToList();
                        }
                    }
                }

                //取得Pur10List資料
                var pur10List = await (from pur10 in _context.Pur10
                                       select new Pur10List
                                       {
                                           PurNo = pur10.PurNo,
                                           PurDate = pur10.PurDate,
                                           SendDate = pur10.SendDate,
                                           VendorNo = pur10.VendorNo,
                                           Ntus = pur10.Ntus,
                                           PeNo = pur10.PeNo,
                                           Memo = pur10.Memo,
                                           Total1 = pur10.Total1,
                                           Total0 = pur10.Total0,
                                           Total = pur10.Total,
                                           PaperTail = pur10.PaperTail,
                                           Userid = pur10.Userid,
                                       }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //採購單號
                    if (!string.IsNullOrEmpty(vm.PurNo))
                    {
                        pur10List = await pur10List.Where(x => x.PurNo.Contains(vm.PurNo)).ToListAsync();
                    }

                    //採購日期
                    if (!string.IsNullOrEmpty(vm.PurDate))
                    {
                        vm.PurDate = vm.PurDate.Replace("-", "");

                        pur10List = await pur10List.Where(x => x.PurDate == vm.PurDate).ToListAsync();
                    }

                    //交貨日期
                    if (!string.IsNullOrEmpty(vm.SendDate))
                    {
                        vm.SendDate = vm.SendDate.Replace("-", "");

                        pur10List = await pur10List.Where(x => x.SendDate == vm.SendDate).ToListAsync();
                    }

                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        pur10List = await pur10List.Where(x => x.VendorNo == vm.VendorNo).ToListAsync();
                    }

                    //採購人員
                    if (!string.IsNullOrEmpty(vm.PeNo))
                    {
                        pur10List = await pur10List.Where(x => x.PeNo == vm.PeNo).ToListAsync();
                    }
                }

                vm.pur10List = pur10List;

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
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            try
            {
                #region SelectList

                //表尾
                var paperTailList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="C204-1", Value="C204-1" },
                   new SelectListItem {Text="C204-2", Value="C204-2" },
                };

                ViewBag.PaperTailList = new SelectList(paperTailList, "Value", "Text", string.Empty);

                #endregion SelectList

                AddViewModel vm = new AddViewModel();

                //取得資料
                var pur10List = await (from pur10 in _context.Pur10
                                       select new Pur10List
                                       {
                                           PurNo = pur10.PurNo,
                                           PurDate = pur10.PurDate,
                                           SendDate = pur10.SendDate,
                                           VendorNo = pur10.VendorNo,
                                           Ntus = pur10.Ntus,
                                           PeNo = pur10.PeNo,
                                           Memo = pur10.Memo,
                                           Total1 = pur10.Total1,
                                           Total0 = pur10.Total0,
                                           Total = pur10.Total,
                                           PaperTail = pur10.PaperTail,
                                           Userid = pur10.Userid,
                                       }).ToListAsync();

                vm.pur10List = pur10List;

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
                var strDate = DateTime.Now.ToString("yyyyMM");
                var autoPurNo = string.Empty;

                var pur10DataCount = _context.Pur10.Where(x => x.PurNo.Contains(strDate)).Count();

                if (pur10DataCount == 0)
                {
                    //等於0代表當月沒資料 直接新增
                    autoPurNo = strDate + "0001";

                }
                else
                {
                    pur10DataCount = pur10DataCount + 1;
                    autoPurNo = strDate + pur10DataCount.ToString().PadLeft(4, '0');
                }

                ////檢查AccNo是否唯一
                //var checkNo = _context.Pur10.Where(x => x.PurNo == vm.PurNo).FirstOrDefault();

                //if (checkNo != null)
                //{
                //    vm.IsTrue = false;
                //    vm.ErrorMessage = "資料已經存在，請重新編輯";

                //    return Json(vm);
                //}

                //新增所需資料(廠商)
                Pur10 insertData = new Pur10
                {
                    PurNo = autoPurNo,
                    PurDate = string.IsNullOrEmpty(vm.PurDate) ? null : vm.PurDate.Replace("-", ""),
                    SendDate = string.IsNullOrEmpty(vm.SendDate) ? null : vm.SendDate.Replace("-", ""),
                    VendorNo = vm.VendorNo,
                    Ntus = vm.Ntus,
                    PeNo = vm.PeNo,
                    Memo = vm.Memo,
                    Total1 = vm.Total1,
                    Total0 = vm.Total0,
                    Total = vm.Total,
                    PaperTail = vm.PaperTail,
                    Userid = HttpContext.Session.GetString("UserAc"),
                };

                _context.Pur10.Add(insertData);
                await _context.SaveChangesAsync();

                //回傳所需資料
                vm.IsTrue = true;
                vm.PurNo = autoPurNo;

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
        /// <param name="PurNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string PurNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                #region SelectList

                //表尾
                var paperTailList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="C204-1", Value="C204-1" },
                   new SelectListItem {Text="C204-2", Value="C204-2" },
                };

                ViewBag.PaperTailList = new SelectList(paperTailList, "Value", "Text", string.Empty);

                #endregion SelectList

                if (string.IsNullOrEmpty(PurNo))
                {
                    return NotFound();
                }

                //資料
                var purData = _context.Pur10
                    .Where(x => x.PurNo == PurNo).FirstOrDefault();

                if (purData == null)
                {
                    return NotFound();
                }

                vm.PurNo = purData.PurNo;
                vm.PurDate = string.IsNullOrEmpty(purData.PurDate) ?
                    null : DateTime.ParseExact(purData.PurDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.SendDate = string.IsNullOrEmpty(purData.SendDate) ?
                    null : DateTime.ParseExact(purData.SendDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.VendorNo = purData.VendorNo;
                vm.Ntus = purData.Ntus;
                vm.PeNo = purData.PeNo;
                vm.Memo = purData.Memo;
                vm.Total1 = purData.Total1;
                vm.Total0 = purData.Total0;
                vm.Total = purData.Total;
                vm.PaperTail = purData.PaperTail;
                vm.Userid = HttpContext.Session.GetString("UserAc");

                //清單資料
                var purList = await _context.Pur10.Select(x => new Pur10List
                {
                    PurNo = x.PurNo,
                    PurDate = x.PurDate,
                    SendDate = x.SendDate,
                    VendorNo = x.VendorNo,
                    Ntus = x.Ntus,
                    PeNo = x.PeNo,
                    Memo = x.Memo,
                    Total1 = x.Total1,
                    Total0 = x.Total0,
                    Total = x.Total,
                    PaperTail = x.PaperTail,
                    Userid = x.Userid,
                }).ToListAsync();

                if (purList != null)
                {
                    purList = purList.Where(x => x.PurNo == PurNo).ToList();
                    vm.pur10List = purList;

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
                    Pur10 updateData = new Pur10
                    {
                        PurNo = vm.PurNo,
                        PurDate = string.IsNullOrEmpty(vm.PurDate) ? null : vm.PurDate.Replace("-", ""),
                        SendDate = string.IsNullOrEmpty(vm.SendDate) ? null : vm.SendDate.Replace("-", ""),
                        VendorNo = vm.VendorNo,
                        Ntus = vm.Ntus,
                        PeNo = vm.PeNo,
                        Memo = vm.Memo,
                        Total1 = vm.Total1,
                        Total0 = vm.Total0,
                        Total = vm.Total,
                        PaperTail = vm.PaperTail,
                        Userid = HttpContext.Session.GetString("UserAc"),
                    };

                    _context.Pur10.Update(updateData);

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
        public async Task<IActionResult> Delete(string PurNo)
        {
            var checkPur10Data = await _context.Pur10.Where(x => x.PurNo == PurNo).FirstOrDefaultAsync();

            if (checkPur10Data == null)
            {
                return NotFound();
            }

            //先清除子表資料
            var removePur20Data = _context.Pur20.Where(x => x.PurNo == PurNo).ToList();

            if (removePur20Data != null)
            {
                _context.Pur20.RemoveRange(removePur20Data);
                await _context.SaveChangesAsync();
            }

            //再清除主表資料
            _context.Pur10.Remove(checkPur10Data);
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
                    if (!string.IsNullOrEmpty(vm.PurNo))
                    {
                        var purData = (from pur20 in _context.Pur20
                                       where pur20.PurNo == vm.PurNo && pur20.Serno == vm.Serno
                                       select new IndexDetailViewModel
                                       {
                                           PurNo = pur20.PurNo,
                                           Serno = pur20.Serno,
                                           PartNo = pur20.PartNo,
                                           Qty = pur20.Qty,
                                           Price = pur20.Price,
                                           Amount = pur20.Amount,
                                           Memo = pur20.Memo,
                                           InQty = pur20.InQty,
                                           Discount = pur20.Discount,
                                           Unit = pur20.Unit,
                                       }).FirstOrDefault();

                        if (purData != null)
                        {
                            //裝載資料
                            vm = new IndexDetailViewModel
                            {
                                PurNo = purData.PurNo,
                                Serno = purData.Serno,
                                PartNo = purData.PartNo,
                                Qty = purData.Qty,
                                Price = purData.Price,
                                Amount = purData.Amount,
                                Memo = purData.Memo,
                                InQty = purData.InQty,
                                Discount = purData.Discount,
                                Unit = purData.Unit,
                            };
                        }

                        //取得Pur20List資料
                        var checkPur20Data = _context.Pur20.Where(x => x.PurNo == vm.PurNo).Any();

                        if (checkPur20Data)
                        {
                            vm.pur20List = (from pur20 in _context.Pur20
                                            join stock10 in _context.Stock10
                                            on pur20.PartNo equals stock10.PartNo
                                            where pur20.PurNo == vm.PurNo
                                            select new Pur20List
                                            {
                                                PurNo = pur20.PurNo,
                                                Serno = pur20.Serno,
                                                PartNo = pur20.PartNo,
                                                Spec = stock10.Spec,
                                                Qty = pur20.Qty,
                                                Unit = pur20.Unit,
                                                TaxType = stock10.TaxType,
                                                Price = pur20.Price,
                                                Discount = pur20.Discount,
                                                Amount = pur20.Amount,
                                                Memo = pur20.Memo,
                                            }).ToList();
                        }
                        else
                        {
                            vm.pur20List = new List<Pur20List>();
                        }
                    }
                }
                //搜尋條件
                if (vm.IsSearch)
                {
                    //採購單號
                    if (!string.IsNullOrEmpty(vm.PurNo))
                    {
                        vm.pur20List = await vm.pur20List.Where(x => x.PurNo.Contains(vm.PurNo)).ToListAsync();
                    }


                    //產品編號
                    if (!string.IsNullOrEmpty(vm.PartNo))
                    {
                        vm.pur20List = await vm.pur20List.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
                    }
                }

                return View(vm);
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
        public async Task<IActionResult> AddDetail(string PurNo)
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

                vm.pur20List = await (from pur20 in _context.Pur20
                                      join stock10 in _context.Stock10
                                      on pur20.PartNo equals stock10.PartNo
                                      where pur20.PurNo == PurNo
                                      select new Pur20List
                                      {
                                          PurNo = pur20.PurNo,
                                          Serno = pur20.Serno,
                                          PartNo = pur20.PartNo,
                                          Spec = stock10.Spec,
                                          Qty = pur20.Qty,
                                          Unit = pur20.Unit,
                                          TaxType = stock10.TaxType,
                                          Price = pur20.Price,
                                          Discount = pur20.Discount,
                                          Amount = pur20.Amount,
                                          Memo = pur20.Memo,
                                      }).ToListAsync();

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
            var serNo = 0;

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
                var checkData = _context.Pur20.Where(x => x.PurNo == vm.PurNo).Any();

                if (!checkData)
                {
                    serNo = 1;
                }
                else
                {
                    var lastSerNo = _context.Pur20.Where(x => x.PurNo == vm.PurNo).OrderByDescending(x => x.Serno);

                    serNo = lastSerNo.First().Serno + 1;
                }

                //新增所需資料(廠商)
                Pur20 insertData = new Pur20
                {
                    PurNo = vm.PurNo,
                    Serno = serNo,
                    PartNo = vm.PartNo,
                    Qty = vm.Qty,
                    Price = vm.Price,
                    Amount = vm.Amount,
                    Memo = vm.Memo,
                    InQty = vm.InQty,
                    Discount = vm.Discount,
                    Unit = vm.Unit,
                };

                _context.Pur20.Add(insertData);
                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                if (vm.IsTrue)
                {
                    var pur10Data = _context.Pur10.Where(x => x.PurNo == vm.PurNo).FirstOrDefault();
                    var pur20Data = (from pur20 in _context.Pur20
                                     join stock10 in _context.Stock10
                                     on pur20.PartNo equals stock10.PartNo
                                     where pur20.PurNo == vm.PurNo
                                     select new
                                     {
                                         purNo = pur20.PurNo,
                                         partNo = pur20.PartNo,
                                         price = pur20.Price,
                                         qty = pur20.Qty,
                                         amount = pur20.Amount,
                                         taxType = stock10.TaxType
                                     }).ToList();

                    double? total0 = 0;
                    double? total1 = 0;

                    foreach (var item in pur20Data)
                    {
                        //total1 = 應稅 total0 = 免稅

                        switch (item.taxType)
                        {
                            case "應稅":
                                total1 += item.amount;
                                break;
                            case "免稅":
                                total0 += item.amount;
                                break;
                            default:
                                break;
                        }
                    }

                    pur10Data.Total1 = total1;
                    pur10Data.Total0 = total0;
                    pur10Data.Total = total1 + total0;

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
        public async Task<IActionResult> EditDetail(string PurNo, int SerNo)
        {
            EditDetailViewModel vm = new EditDetailViewModel();

            try
            {
                if (string.IsNullOrEmpty(PurNo))
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
                var pur20Data = (from pur20 in _context.Pur20
                                 join stock10 in _context.Stock10
                                 on pur20.PartNo equals stock10.PartNo
                                 where pur20.PurNo == PurNo && pur20.Serno == SerNo
                                 select new
                                 {
                                     purNo = pur20.PurNo,
                                     serNo = pur20.Serno,
                                     partNo = pur20.PartNo,
                                     qty = pur20.Qty,
                                     unit = pur20.Unit,
                                     taxType = stock10.TaxType,
                                     price = pur20.Price,
                                     discount = pur20.Discount,
                                     amount = pur20.Amount,
                                     memo = pur20.Memo,
                                     inQty = pur20.InQty,
                                 }).FirstOrDefault();

                if (pur20Data == null)
                {
                    return NotFound();
                }

                vm.PurNo = pur20Data.purNo;
                vm.Serno = pur20Data.serNo;
                vm.PartNo = pur20Data.partNo;
                vm.Qty = pur20Data.qty;
                vm.Unit = pur20Data.unit;
                vm.TaxType = pur20Data.taxType;
                vm.Price = pur20Data.price;
                vm.Discount = pur20Data.discount;
                vm.Amount = pur20Data.amount;
                vm.Memo = pur20Data.memo;
                vm.InQty = pur20Data.inQty;

                //清單資料
                var pur20List = await _context.Pur20.Select(x => new Pur20List
                {
                    PurNo = x.PurNo,
                    Serno = x.Serno,
                    PartNo = x.PartNo,
                    Qty = x.Qty,
                    Unit = x.Unit,
                    Price = x.Price,
                    Discount = x.Discount,
                    Amount = x.Amount,
                    Memo = x.Memo,
                    InQty = x.InQty,
                }).ToListAsync();

                if (pur20List != null)
                {
                    pur20List = pur20List.Where(x => x.PurNo == PurNo && x.Serno == SerNo).ToList();
                    vm.pur20List = pur20List;

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

                var checkData = _context.Pur20.Where(x => x.PurNo == vm.PurNo && x.Serno == vm.Serno).FirstOrDefault();

                if (checkData != null)
                {
                    checkData.Amount = vm.Amount;
                    checkData.Discount = vm.Discount;
                    checkData.InQty = vm.InQty;
                    checkData.Memo = vm.Memo;
                    checkData.PartNo = vm.PartNo;
                    checkData.Price = vm.Price;
                    checkData.Qty = vm.Qty;
                    checkData.Unit = vm.Unit;

                    await _context.SaveChangesAsync();

                    vm.IsTrue = true;

                    if (vm.IsTrue)
                    {
                        var pur10Data = _context.Pur10.Where(x => x.PurNo == vm.PurNo).FirstOrDefault();
                        var pur20Data = (from pur20 in _context.Pur20
                                         join stock10 in _context.Stock10
                                         on pur20.PartNo equals stock10.PartNo
                                         where pur20.PurNo == vm.PurNo
                                         select new
                                         {
                                             purNo = pur20.PurNo,
                                             partNo = pur20.PartNo,
                                             price = pur20.Price,
                                             qty = pur20.Qty,
                                             amount = pur20.Amount,
                                             taxType = stock10.TaxType
                                         }).ToList();

                        double? total0 = 0;
                        double? total1 = 0;

                        foreach (var item in pur20Data)
                        {
                            //total1 = 應稅 total0 = 免稅
                            switch (item.taxType)
                            {
                                case "應稅":
                                    total1 += item.amount;
                                    break;
                                case "免稅":
                                    total0 += item.amount;
                                    break;
                                default:
                                    break;
                            }
                        }

                        pur10Data.Total1 = total1;
                        pur10Data.Total0 = total0;
                        pur10Data.Total = total1 + total0;

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
        /// <param name="PurNo"></param>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteDetail(string PurNo, int SerNo)
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

            var deleteData = _context.Pur20.Where(x => x.PurNo == PurNo && x.Serno == SerNo).FirstOrDefault();

            if (deleteData == null)
            {
                return NotFound();
            }

            _context.Pur20.Remove(deleteData);
            await _context.SaveChangesAsync();

            var pur10Data = _context.Pur10.Where(x => x.PurNo == PurNo).FirstOrDefault();
            var pur20Data = (from pur20 in _context.Pur20
                             join stock10 in _context.Stock10
                             on pur20.PartNo equals stock10.PartNo
                             where pur20.PurNo == PurNo
                             select new
                             {
                                 purNo = pur20.PurNo,
                                 partNo = pur20.PartNo,
                                 price = pur20.Price,
                                 qty = pur20.Qty,
                                 amount = pur20.Amount,
                                 taxType = stock10.TaxType
                             }).ToList();

            double? total0 = 0;
            double? total1 = 0;

            foreach (var item in pur20Data)
            {
                //total1 = 應稅 total0 = 免稅
                switch (item.taxType)
                {
                    case "應稅":
                        total1 += item.amount;
                        break;
                    case "免稅":
                        total0 += item.amount;
                        break;
                    default:
                        break;
                }
            }

            pur10Data.Total1 = total1;
            pur10Data.Total0 = total0;
            pur10Data.Total = total1 + total0;

            await _context.SaveChangesAsync();

            DeleteResponse vm = new DeleteResponse();

            vm.IsTrue = true;
            vm.PurNo = PurNo;

            return Json(vm);
        }

        #endregion 子表

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="BankNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> Edit(string BankNo)
        //{
        //    EditViewModel vm = new EditViewModel();

        //    //會計科目清單資料
        //    var accNoListData = from accNo10 in _context.Accno10
        //                        select new
        //                        {
        //                            AccNo = accNo10.Accno,
        //                            AccName = accNo10.Accno + string.Empty + accNo10.Accname
        //                        };

        //    //會計科目清單
        //    ViewBag.AccNoListData = new SelectList(accNoListData.ToList(), "AccNo", "AccName", string.Empty);

        //    try
        //    {
        //        if (string.IsNullOrEmpty(BankNo))
        //        {
        //            return NotFound();
        //        }


        //        //資料
        //        var bankData = _context.Bank10
        //            .Where(x => x.BankNo == BankNo).FirstOrDefault();

        //        if (bankData == null)
        //        {
        //            return NotFound();
        //        }

        //        vm.BankNo = bankData.BankNo;
        //        vm.BankName = bankData.BankName;
        //        vm.Ntus = bankData.Ntus;
        //        vm.Initdate = DateTime.ParseExact(bankData.Initdate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
        //        vm.Initamount = bankData.Initamount;
        //        vm.Accno = bankData.Accno;

        //        //清單資料
        //        var bankList = await _context.Bank10.Select(x => new Bank10List
        //        {
        //            BankNo = x.BankNo,
        //            BankName = x.BankName,
        //            Ntus = x.Ntus,
        //            Initdate = DateTime.ParseExact(bankData.Initdate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
        //            Initamount = x.Initamount,
        //            Accno = x.Accno,
        //        }).ToListAsync();

        //        if (bankList != null)
        //        {
        //            bankList = bankList.Where(x => x.BankNo == BankNo).ToList();
        //            vm.bank10List = bankList;

        //            return View(vm);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //}

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="vm">VendorEditViewModel</param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(EditViewModel vm)
        //{
        //    try
        //    {
        //        vm.IsTrue = false;

        //        if (vm != null)
        //        {
        //            Bank10 updateData = new Bank10
        //            {
        //                BankNo = vm.BankNo,
        //                BankName = vm.BankName,
        //                Initdate = string.IsNullOrEmpty(vm.Initdate) ? null : vm.Initdate.Replace("-", ""),
        //                Initamount = vm.Initamount,
        //                Accno = vm.Accno,
        //                Ntus = vm.Ntus,
        //            };

        //            _context.Bank10.Update(updateData);

        //            await _context.SaveChangesAsync();

        //            vm.IsTrue = true;
        //        }
        //        else
        //        {
        //            vm.IsTrue = false;
        //            vm.ErrorMessage = "編輯失敗!";

        //            return Json(vm);
        //        }

        //        return Json(vm);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="BankNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> Delete(string BankNo)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(BankNo))
        //        {
        //            return NotFound();
        //        }

        //        var deleteData = await _context.Bank10
        //            .Where(x => x.BankNo == BankNo).FirstOrDefaultAsync();

        //        if (deleteData != null)
        //        {
        //            _context.Bank10.Remove(deleteData);

        //            await _context.SaveChangesAsync();
        //        }

        //        return Json(true);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> Export(string StartVendorNo, string EndVendorNo, string VendorType)
        //{
        //    #region SelectList

        //    //廠商清單資料
        //    var vendorList = from vendor10 in _context.Vendor10
        //                     select new
        //                     {
        //                         VendorNo = vendor10.VendorNo,
        //                         Vename = vendor10.VendorNo + string.Empty + vendor10.Vename
        //                     };

        //    //廠商清單
        //    ViewBag.VendorList = new SelectList(vendorList.ToList(), "VendorNo", "Vename", string.Empty);

        //    #endregion

        //    var vendorListData = await _context.Vendor10.Select(x => new VendorList
        //    {
        //        VendorNo = x.VendorNo,
        //        Vename = x.Vename,
        //        Uniform = x.Uniform,
        //        Fax = x.Fax,
        //        Boss = x.Boss,
        //        Sales = x.Sales,
        //        Tel1 = x.Tel1,
        //        Tel2 = x.Tel2,
        //        Invoaddr = x.Invoaddr,
        //        Factaddr = x.Factaddr,
        //    }).ToListAsync();

        //    if (vendorListData == null)
        //    {
        //        return View(new VendorExportViewModel());
        //    }

        //    //第一筆
        //    var firstData = _context.Stock10.OrderBy(x => x.PartNo).First().PartNo;

        //    //最後一筆
        //    var lastData = _context.Stock10.OrderByDescending(x => x.PartNo).First().PartNo;

        //    if (string.IsNullOrEmpty(StartVendorNo) && string.IsNullOrEmpty(EndVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
        //    }
        //    else if (string.IsNullOrEmpty(StartVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(EndVendorNo) <= 0).ToListAsync();
        //    }
        //    else if (string.IsNullOrEmpty(EndVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(StartVendorNo) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
        //    }
        //    else
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(StartVendorNo) >= 0 && x.VendorNo.CompareTo(EndVendorNo) <= 0).ToListAsync();
        //    }

        //    VendorExportViewModel vm = new VendorExportViewModel();

        //    vm.vendorList = vendorListData;

        //    return View(vm);
        //}

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Export(VendorExportViewModel vm)
        //{
        //    var vendorListData = await _context.Vendor10.Select(x => new VendorList
        //    {
        //        VendorNo = x.VendorNo,
        //        Vename = x.Vename,
        //        Uniform = x.Uniform,
        //        Fax = x.Fax,
        //        Boss = x.Boss,
        //        Sales = x.Sales,
        //        Tel1 = x.Tel1,
        //        Tel2 = x.Tel2,
        //        Invoaddr = x.Invoaddr,
        //        Factaddr = x.Factaddr,
        //    }).ToListAsync();

        //    //第一筆
        //    var firstData = _context.Vendor10.OrderBy(x => x.VendorNo).First().VendorNo;

        //    //最後一筆
        //    var lastData = _context.Vendor10.OrderByDescending(x => x.VendorNo).First().VendorNo;

        //    if (vendorListData == null)
        //    {
        //        return View(new VendorExportViewModel());
        //    }

        //    if (string.IsNullOrEmpty(vm.StartVendorNo) && string.IsNullOrEmpty(vm.EndVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
        //    }
        //    else if (string.IsNullOrEmpty(vm.StartVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
        //    }
        //    else if (string.IsNullOrEmpty(vm.EndVendorNo))
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
        //    }
        //    else
        //    {
        //        vendorListData = await vendorListData
        //            .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
        //    }

        //    vm.vendorList = vendorListData;

        //    //建立Excel
        //    HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
        //    ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

        //    //設定樣式
        //    //ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
        //    //IFont headerfont = hssfworkbook.CreateFont();
        //    //headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
        //    //headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
        //    //headerfont.FontName = "微軟正黑體";
        //    //headerfont.FontHeightInPoints = 20;
        //    //headerfont.Boldweight = (short)FontBoldWeight.Bold;
        //    //headerStyle.SetFont(headerfont);

        //    //新增標題列
        //    sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
        //    //sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
        //    sheet.GetRow(0).CreateCell(0).SetCellValue("廠商編號");
        //    sheet.GetRow(0).CreateCell(1).SetCellValue("廠商簡稱");
        //    sheet.GetRow(0).CreateCell(2).SetCellValue("統一編號");
        //    sheet.GetRow(0).CreateCell(3).SetCellValue("傳真");
        //    sheet.GetRow(0).CreateCell(4).SetCellValue("負責人");
        //    sheet.GetRow(0).CreateCell(5).SetCellValue("電話一");
        //    sheet.GetRow(0).CreateCell(6).SetCellValue("電話二");
        //    sheet.GetRow(0).CreateCell(7).SetCellValue("通訊地址一");
        //    sheet.GetRow(0).CreateCell(8).SetCellValue("倉庫地址");
        //    //sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
        //    //sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
        //    //sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");
        //    //sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式

        //    //填入資料
        //    int rowIndex = 1;
        //    for (int row = 0; row < vendorListData.Count(); row++)
        //    {
        //        sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(vendorListData[row].VendorNo);
        //        sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(vendorListData[row].Vename);
        //        sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(vendorListData[row].Uniform);
        //        sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(vendorListData[row].Fax);
        //        sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(vendorListData[row].Boss);
        //        sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(vendorListData[row].Tel1);
        //        sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(vendorListData[row].Tel2);
        //        sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(vendorListData[row].Invoaddr);
        //        sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(vendorListData[row].Factaddr);

        //        rowIndex++;
        //    }

        //    var excelDatas = new MemoryStream();
        //    hssfworkbook.Write(excelDatas);

        //    return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"廠商通訊錄.xls"));
        //}
    }
}
