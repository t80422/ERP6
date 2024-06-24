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
using ERP6.ViewModels.In;
using Microsoft.AspNetCore.Http;

namespace ERP6.Controllers
{
    public class InController : Controller
    {
        private readonly EEPEF01Context _context;
        private readonly AjaxsController _ajax;

        public InController(EEPEF01Context context, AjaxsController ajax)
        {
            _context = context;
            _ajax = ajax;
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

                //進貨類別資料
                var inTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="進貨", Value="1" },
                   new SelectListItem {Text="進貨退出", Value="2" },
                   new SelectListItem {Text="樣品", Value="3" },
                   new SelectListItem {Text="寄庫進貨", Value="4" },
                };

                ViewBag.InTypeList = new SelectList(inTypeList, "Value", "Text", string.Empty);

                //備註資料
                ViewBag.MemoList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "明細備註").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.InNo))
                    {
                        var inData = await (from in10 in _context.In10
                                            where in10.InNo == vm.InNo
                                            select new IndexViewModel
                                            {
                                                InNo = in10.InNo,
                                                InDate = !string.IsNullOrEmpty(in10.InDate) ?
                                                  DateTime.ParseExact(in10.InDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                                VendorNo = in10.VendorNo,
                                                Memo = in10.Memo,
                                                Ntus = in10.Ntus,
                                                Total1 = in10.Total1,
                                                Userid = in10.Userid,
                                                Paymonth = !string.IsNullOrEmpty(in10.Paymonth) ?
                                                  DateTime.ParseExact(in10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                                StockPass = in10.StockPass,
                                                YnClose = in10.YnClose,
                                                InvoiceNo = in10.InvoiceNo,
                                                InType = in10.InType,
                                                Total = in10.Total,
                                                YesGet = in10.YesGet,
                                                CashDis = in10.CashDis,
                                                SubTot = in10.SubTot,
                                                NotGet = in10.NotGet,
                                                Total0 = in10.Total0,
                                                Discount = in10.Discount
                                            }).FirstOrDefaultAsync();

                        //裝載資料
                        vm = new IndexViewModel
                        {
                            InNo = inData.InNo,
                            InDate = inData.InDate,
                            VendorNo = inData.VendorNo,
                            Memo = inData.Memo,
                            Ntus = inData.Ntus,
                            Total1 = inData.Total1,
                            Userid = inData.Userid,
                            Paymonth = inData.Paymonth,
                            StockPass = inData.StockPass,
                            YnClose = inData.YnClose,
                            InvoiceNo = inData.InvoiceNo,
                            InType = inData.InType,
                            Total = inData.Total,
                            YesGet = inData.YesGet,
                            CashDis = inData.CashDis,
                            SubTot = inData.SubTot,
                            NotGet = inData.NotGet,
                            Total0 = inData.Total0,
                            Discount = inData.Discount
                        };

                        //取得Pur20List資料
                        var checkIn20Data = _context.In20.Where(x => x.InNo == vm.InNo).Any();

                        if (checkIn20Data)
                        {
                            vm.in20List = await (from in20 in _context.In20
                                                 join stock10 in _context.Stock10
                                                 on in20.PartNo equals stock10.PartNo
                                                 where in20.InNo == vm.InNo
                                                 select new In20List
                                                 {
                                                     InNo = in20.InNo,
                                                     Serno = in20.Serno,
                                                     PurNo = in20.PurNo,
                                                     PartNo = in20.PartNo,
                                                     Qty = in20.Qty,
                                                     Price = in20.Price,
                                                     Amount = in20.Amount,
                                                     Memo = in20.Memo,
                                                     Discount = in20.Discount,
                                                     Unit = in20.Unit,
                                                     Spec = stock10.Spec,
                                                     TaxType = stock10.TaxType,
                                                 }).ToListAsync();
                        }
                    }
                }

                //取得Pur10List資料
                var in10List = await (from in10 in _context.In10
                                      select new In10List
                                      {
                                          InNo = in10.InNo,
                                          InDate = !string.IsNullOrEmpty(in10.InDate) ?
                                            DateTime.ParseExact(in10.InDate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                          VendorNo = in10.VendorNo,
                                          Memo = in10.Memo,
                                          Ntus = in10.Ntus,
                                          Total1 = in10.Total1,
                                          Userid = in10.Userid,
                                          Paymonth = !string.IsNullOrEmpty(in10.Paymonth) ?
                                            DateTime.ParseExact(in10.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                          StockPass = in10.StockPass,
                                          YnClose = in10.YnClose,
                                          InvoiceNo = in10.InvoiceNo,
                                          InType = in10.InType,
                                          Total = in10.Total,
                                          YesGet = in10.YesGet,
                                          CashDis = in10.CashDis,
                                          SubTot = in10.SubTot,
                                          NotGet = in10.NotGet,
                                          Total0 = in10.Total0,
                                          Discount = in10.Discount
                                      }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //廠商編號號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        in10List = await in10List.Where(x => x.VendorNo.Contains(vm.VendorNo)).ToListAsync();
                    }

                    //進貨日期
                    if (!string.IsNullOrEmpty(vm.InDate))
                    {
                        vm.InDate = vm.InDate.Replace("-", "");

                        in10List = await in10List.Where(x => x.InDate == vm.InDate).ToListAsync();
                    }

                    //進貨單號
                    if (!string.IsNullOrEmpty(vm.InNo))
                    {
                        in10List = await in10List.Where(x => x.InNo.Contains(vm.InNo)).ToListAsync();
                    }

                    //進貨類別
                    if (!string.IsNullOrEmpty(vm.InType))
                    {
                        in10List = await in10List.Where(x => x.InType == vm.InType).ToListAsync();
                    }

                    //發票號碼
                    if (!string.IsNullOrEmpty(vm.InvoiceNo))
                    {
                        in10List = await in10List.Where(x => x.InvoiceNo == vm.InvoiceNo).ToListAsync();
                    }

                    //帳款月份
                    if (!string.IsNullOrEmpty(vm.Paymonth))
                    {
                        vm.Paymonth = vm.InDate.Replace("-", "");

                        in10List = await in10List.Where(x => x.Paymonth == vm.Paymonth).ToListAsync();
                    }

                }

                vm.in10List = in10List;

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

                //進貨類別資料
                var inTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="進貨", Value="1" },
                   new SelectListItem {Text="進貨退出", Value="2" },
                   new SelectListItem {Text="樣品", Value="3" },
                   new SelectListItem {Text="寄庫進貨", Value="4" },
                };

                ViewBag.InTypeList = new SelectList(inTypeList, "Value", "Text", string.Empty);

                //備註資料
                ViewBag.MemoList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "明細備註").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                AddViewModel vm = new AddViewModel();

                //取得資料
                var in10List = await (from in10 in _context.In10
                                      select new In10List
                                      {
                                          InNo = in10.InNo,
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

                vm.in10List = in10List;

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

                //進貨類別資料
                var inTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="進貨", Value="1" },
                   new SelectListItem {Text="進貨退出", Value="2" },
                   new SelectListItem {Text="樣品", Value="3" },
                   new SelectListItem {Text="寄庫進貨", Value="4" },
                };

                ViewBag.InTypeList = new SelectList(inTypeList, "Value", "Text", string.Empty);

                //備註資料
                ViewBag.MemoList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "明細備註").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                var strDate = DateTime.Now.ToString("yyyyMM");
                var autoInNo = string.Empty;

                var in10DataCount = _context.In10.Where(x => x.InNo.Contains(strDate)).Count();

                if (in10DataCount == 0)
                {
                    //等於0代表當月沒資料 直接新增
                    autoInNo = strDate + "0001";

                }
                else
                {
                    in10DataCount = in10DataCount + 1;
                    autoInNo = strDate + in10DataCount.ToString().PadLeft(4, '0');
                }

                //新增所需資料(廠商)
                In10 insertData = new In10
                {
                    InNo = autoInNo,
                    InDate = string.IsNullOrEmpty(vm.InDate) ? null : vm.InDate.Replace("-", ""),
                    VendorNo = vm.VendorNo,
                    Memo = vm.Memo,
                    Ntus = string.IsNullOrEmpty(vm.Ntus) ? "NTD" : vm.Ntus,
                    Userid = HttpContext.Session.GetString("UserAc"),
                    Paymonth = string.IsNullOrEmpty(vm.Paymonth) ? null : vm.Paymonth.Replace("-", "").Substring(0, 6),
                    StockPass = "Y",
                    YnClose = null,
                    InvoiceNo = vm.InvoiceNo,
                    InType = vm.InType,
                    Discount = vm.Discount,
                };

                _context.In10.Add(insertData);
                await _context.SaveChangesAsync();

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
        public async Task<IActionResult> Edit(string InNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                #region SelectList

                //進貨類別資料
                var inTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="進貨", Value="1" },
                   new SelectListItem {Text="進貨退出", Value="2" },
                   new SelectListItem {Text="樣品", Value="3" },
                   new SelectListItem {Text="寄庫進貨", Value="4" },
                };

                ViewBag.InTypeList = new SelectList(inTypeList, "Value", "Text", string.Empty);

                //備註資料
                ViewBag.MemoList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "明細備註").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                if (string.IsNullOrEmpty(InNo))
                {
                    return NotFound();
                }

                //資料
                var inData = _context.In10
                    .Where(x => x.InNo == InNo).FirstOrDefault();

                if (inData == null)
                {
                    return NotFound();
                }

                vm.InDate = string.IsNullOrEmpty(inData.InDate) ?
                null : DateTime.ParseExact(inData.InDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.VendorNo = inData.VendorNo;
                vm.Memo = inData.Memo;
                vm.Ntus = inData.Ntus;
                vm.Paymonth = string.IsNullOrEmpty(inData.Paymonth) ?
                null : DateTime.ParseExact(inData.Paymonth, "yyyyMM", null).ToString("yyyy-MM");
                vm.InvoiceNo = inData.InvoiceNo;
                vm.InType = inData.InType;
                vm.Discount = inData.Discount;

                //只顯示不修改
                vm.Userid = inData.Userid;
                vm.StockPass = inData.StockPass;
                vm.YnClose = inData.YnClose;

                //清單資料
                var in10List = await _context.In10.Select(x => new In10List
                {
                    InNo = x.InNo,
                }).ToListAsync();

                if (in10List != null)
                {
                    in10List = in10List.Where(x => x.InNo == InNo).ToList();
                    vm.in10List = in10List;

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

                //進貨類別資料
                var inTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="進貨", Value="1" },
                   new SelectListItem {Text="進貨退出", Value="2" },
                   new SelectListItem {Text="樣品", Value="3" },
                   new SelectListItem {Text="寄庫進貨", Value="4" },
                };

                ViewBag.InTypeList = new SelectList(inTypeList, "Value", "Text", string.Empty);

                //備註資料
                ViewBag.MemoList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "明細備註").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                vm.IsTrue = false;

                if (vm != null)
                {
                    In10 updateData = new In10
                    {
                        InNo = vm.InNo,
                        InDate = string.IsNullOrEmpty(vm.InDate) ? null : vm.InDate.Replace("-", ""),
                        VendorNo = vm.VendorNo,
                        Memo = vm.Memo,
                        Paymonth = string.IsNullOrEmpty(vm.Paymonth) ? null : vm.InDate.Replace("-", ""),
                        InvoiceNo = vm.InvoiceNo,
                        InType = vm.InType,
                        Discount = vm.Discount,
                        Userid = HttpContext.Session.GetString("UserAc"),
                    };

                    _context.In10.Update(updateData);

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


        public async Task<IActionResult> Print(string InNo)
        {
            if (!String.IsNullOrEmpty(InNo))
            {
                var In10Data = await _context.In10.FindAsync(InNo);                
                if (In10Data == null) return NotFound();

                var Vendor = await _context.Vendor10.FindAsync(In10Data.VendorNo ?? "");                
                
                if (Vendor == null) return NotFound();

                Pepo10 PENO = null;

                var In20Data = await _context.In20.Where(x => x.InNo == InNo).OrderBy(x => x.Serno).ToListAsync();                

                int pageSum = 13; // 單頁總筆數


                #region 加入固定資料

                Dictionary<string, string> InData = new Dictionary<string, string>()
                {
                    ["$$BUSINESS_NAME$$"] = "弘隆食品有限公司",
                    ["$$BUSINESS_ADDRESS$$"] = "苗栗縣竹南鎮中美里10鄰52-18號",
                    ["$$BUSINESS_TEL$$"] = "(037)461-779",
                    ["$$OUT_NO$$"] = InNo ?? "",
                    ["$$CSTM_NAME$$"] = (Vendor.Vename ?? "") + "　" + (In10Data.VendorNo ?? ""),
                    ["$$CSTM_ADDRESS$$"] = Vendor.Invoaddr ?? "",
                    ["$$CSTM_TEL$$"] = Vendor.Tel1 ?? "",
                    ["$$CSTM_UNIFORM$$"] = Vendor.Uniform ?? "",
                    ["$$CSTM_FAX$$"] = Vendor.Fax ?? "",
                    ["$$CSTM_PENO$$"] = PENO != null ? (PENO.Name ?? "") : "",
                    ["$$OUT_DATE$$"] = !String.IsNullOrEmpty(In10Data.InDate) ? DateTime.ParseExact(In10Data.InDate, "yyyyMMdd", null).ToString("yyyy/MM/dd") : "",
                    ["$$TOTAL_PAGE$$"] = In20Data.Count() == 0 ? "1" : Math.Ceiling(Convert.ToDouble(In20Data.Count()) / pageSum).ToString(),
                    ["$$OUT_KG$$"] = "0" + "Kg",
                    ["$$OUT_TOTAL$$"] = String.Format("{0:N}", In10Data.Total ?? 0),
                };

                #endregion 加入固定資料

                List<Dictionary<string, string>> OutDetails = new List<Dictionary<string, string>>();

                int page = 1;
                int serno = 0;

                var OutDetail = new Dictionary<string, string>();

                foreach (var In20 in In20Data)
                {
                    #region 加入產品

                    var StockData = await _context.Stock10.FindAsync(In20.PartNo ?? "");

                    if (StockData != null)
                    {
                        serno++;
                        OutDetail[$"$$PARTNO_{serno}$$"] = In20.PartNo ?? "";
                        OutDetail[$"$$SPEC_{serno}$$"] = !String.IsNullOrEmpty(StockData.Spec) ? (StockData.Spec.Length > 20 ? StockData.Spec[..20] : StockData.Spec) : "";
                        //OutDetail[$"$$SPEC_{serno}$$"] = "123";
                        OutDetail[$"$$QTY_{serno}$$"] = (In20.Qty ?? 0).ToString();
                        OutDetail[$"$$UNIT_{serno}$$"] = In20.Unit ?? "";
                        OutDetail[$"$$PRICE_{serno}$$"] = (In20.Price ?? 0).ToString();
                        OutDetail[$"$$DISCOUNT_{serno}$$"] = (In20.Discount ?? 0).ToString();
                        OutDetail[$"$$AMOUNT_{serno}$$"] = (In20.Amount ?? 0).ToString();
                        OutDetail[$"$$PPRICE_{serno}$$"] = (0).ToString();
                        OutDetail[$"$$TAXTYPE_{serno}$$"] = StockData.TaxType.Replace("稅", "") ?? "";
                        OutDetail[$"$$MEMO_{serno}$$"] = In20.Memo ?? "";
                    }

                    #endregion 加入產品

                    #region 單頁筆數滿後換頁

                    if (serno % pageSum == 0)
                    {
                        OutDetail["$$CURRENT_PAGE$$"] = page.ToString();
                        OutDetails.Add(OutDetail);
                        serno = 0;
                        page++;
                        OutDetail = new Dictionary<string, string>();
                    }

                    #endregion 單頁筆數滿後換頁
                }

                #region 補齊同頁剩餘資料

                if ((In20Data.Count() > 0 && In20Data != null && serno > 0) || (In20Data == null || In20Data.Count() == 0))
                {
                    for (serno = serno + 1; serno <= pageSum; serno++)
                    {
                        OutDetail[$"$$PARTNO_{serno}$$"] = "";
                        OutDetail[$"$$SPEC_{serno}$$"] = "";
                        OutDetail[$"$$QTY_{serno}$$"] = "";
                        OutDetail[$"$$UNIT_{serno}$$"] = "";
                        OutDetail[$"$$PRICE_{serno}$$"] = "";
                        OutDetail[$"$$DISCOUNT_{serno}$$"] = "";
                        OutDetail[$"$$AMOUNT_{serno}$$"] = "";
                        OutDetail[$"$$PPRICE_{serno}$$"] = "";
                        OutDetail[$"$$TAXTYPE_{serno}$$"] = "";
                        OutDetail[$"$$MEMO_{serno}$$"] = "";
                    }
                    OutDetail["$$CURRENT_PAGE$$"] = page.ToString();
                    OutDetails.Add(OutDetail);

                }


                #endregion 補齊同頁剩餘資料

                return File(await _ajax.Export(InData, OutDetails, @"wwwroot/report/C205_01.html"), "applicatuin/pdf", $"{InNo}.pdf");
                //return null;

            }

            return NotFound();
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> Delete(string PurNo)
        //{
        //    var checkPur10Data = await _context.Pur10.Where(x => x.PurNo == PurNo).FirstOrDefaultAsync();

        //    if (checkPur10Data == null)
        //    {
        //        return NotFound();
        //    }

        //    //先清除子表資料
        //    var removePur20Data = _context.Pur20.Where(x => x.PurNo == PurNo).ToList();

        //    if (removePur20Data != null)
        //    {
        //        _context.Pur20.RemoveRange(removePur20Data);
        //        await _context.SaveChangesAsync();
        //    }

        //    //再清除主表資料
        //    _context.Pur10.Remove(checkPur10Data);
        //    await _context.SaveChangesAsync();

        //    return Json(true);
        //}

        #endregion 主表

        #region 子表

        /// <summary>
        /// 子表首頁
        /// </summary>
        /// <returns></returns>
        //public async Task<IActionResult> IndexDetail(IndexDetailViewModel vm)
        //{
        //    try
        //    {
        //        #region SelectList

        //        //產品清單資料
        //        var stock10ListData = from stock10 in _context.Stock10
        //                              select new
        //                              {
        //                                  id = stock10.PartNo,
        //                                  text = stock10.PartNo + stock10.Spec,
        //                                  unit = stock10.Unit,
        //                                  taxType = stock10.TaxType,
        //                              };

        //        //產品清單資料
        //        ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //        //單位
        //        ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //        //稅別
        //        var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //        ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //        #endregion

        //        if (!vm.IsSearch)
        //        {
        //            //如果有資料的話，帶出來
        //            if (!string.IsNullOrEmpty(vm.PurNo))
        //            {
        //                var purData = (from pur20 in _context.Pur20
        //                               where pur20.PurNo == vm.PurNo && pur20.Serno == vm.Serno
        //                               select new IndexDetailViewModel
        //                               {
        //                                   PurNo = pur20.PurNo,
        //                                   Serno = pur20.Serno,
        //                                   PartNo = pur20.PartNo,
        //                                   Qty = pur20.Qty,
        //                                   Price = pur20.Price,
        //                                   Amount = pur20.Amount,
        //                                   Memo = pur20.Memo,
        //                                   InQty = pur20.InQty,
        //                                   Discount = pur20.Discount,
        //                                   Unit = pur20.Unit,
        //                               }).FirstOrDefault();

        //                if (purData != null)
        //                {
        //                    //裝載資料
        //                    vm = new IndexDetailViewModel
        //                    {
        //                        PurNo = purData.PurNo,
        //                        Serno = purData.Serno,
        //                        PartNo = purData.PartNo,
        //                        Qty = purData.Qty,
        //                        Price = purData.Price,
        //                        Amount = purData.Amount,
        //                        Memo = purData.Memo,
        //                        InQty = purData.InQty,
        //                        Discount = purData.Discount,
        //                        Unit = purData.Unit,
        //                    };
        //                }

        //                //取得Pur20List資料
        //                var checkPur20Data = _context.Pur20.Where(x => x.PurNo == vm.PurNo).Any();

        //                if (checkPur20Data)
        //                {
        //                    vm.pur20List = (from pur20 in _context.Pur20
        //                                    join stock10 in _context.Stock10
        //                                    on pur20.PartNo equals stock10.PartNo
        //                                    where pur20.PurNo == vm.PurNo
        //                                    select new Pur20List
        //                                    {
        //                                        PurNo = pur20.PurNo,
        //                                        Serno = pur20.Serno,
        //                                        PartNo = pur20.PartNo,
        //                                        Spec = stock10.Spec,
        //                                        Qty = pur20.Qty,
        //                                        Unit = pur20.Unit,
        //                                        TaxType = stock10.TaxType,
        //                                        Price = pur20.Price,
        //                                        Discount = pur20.Discount,
        //                                        Amount = pur20.Amount,
        //                                        Memo = pur20.Memo,
        //                                    }).ToList();
        //                }
        //                else
        //                {
        //                    vm.pur20List = new List<Pur20List>();
        //                }
        //            }
        //        }
        //        //搜尋條件
        //        if (vm.IsSearch)
        //        {
        //            //採購單號
        //            if (!string.IsNullOrEmpty(vm.PurNo))
        //            {
        //                vm.pur20List = await vm.pur20List.Where(x => x.PurNo.Contains(vm.PurNo)).ToListAsync();
        //            }


        //            //產品編號
        //            if (!string.IsNullOrEmpty(vm.PartNo))
        //            {
        //                vm.pur20List = await vm.pur20List.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
        //            }
        //        }

        //        return View(vm);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 新增詳細資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> AddDetail(string PurNo)
        //{
        //    try
        //    {
        //        AddDetailViewModel vm = new AddDetailViewModel();

        //        #region SelectList

        //        //產品清單資料
        //        var stock10ListData = from stock10 in _context.Stock10
        //                              select new
        //                              {
        //                                  id = stock10.PartNo,
        //                                  text = stock10.PartNo + stock10.Spec,
        //                                  unit = stock10.Unit,
        //                                  taxType = stock10.TaxType,
        //                              };

        //        //產品清單資料
        //        ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //        //單位
        //        ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //        //稅別
        //        var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //        ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //        #endregion

        //        vm.pur20List = await (from pur20 in _context.Pur20
        //                              join stock10 in _context.Stock10
        //                              on pur20.PartNo equals stock10.PartNo
        //                              where pur20.PurNo == PurNo
        //                              select new Pur20List
        //                              {
        //                                  PurNo = pur20.PurNo,
        //                                  Serno = pur20.Serno,
        //                                  PartNo = pur20.PartNo,
        //                                  Spec = stock10.Spec,
        //                                  Qty = pur20.Qty,
        //                                  Unit = pur20.Unit,
        //                                  TaxType = stock10.TaxType,
        //                                  Price = pur20.Price,
        //                                  Discount = pur20.Discount,
        //                                  Amount = pur20.Amount,
        //                                  Memo = pur20.Memo,
        //                              }).ToListAsync();

        //        return View(vm);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 新增詳細資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddDetail(AddDetailViewModel vm)
        //{
        //    var serNo = 0;

        //    try
        //    {
        //        vm.IsTrue = false;

        //        #region SelectList

        //        //產品清單資料
        //        var stock10ListData = from stock10 in _context.Stock10
        //                              select new
        //                              {
        //                                  id = stock10.PartNo,
        //                                  text = stock10.PartNo + stock10.Spec,
        //                                  unit = stock10.Unit,
        //                                  taxType = stock10.TaxType,
        //                              };

        //        //產品清單資料
        //        ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //        //單位
        //        ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //        //稅別
        //        var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //        ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //        #endregion

        //        //先取得序號
        //        var checkData = _context.Pur20.Where(x => x.PurNo == vm.PurNo).Any();

        //        if (!checkData)
        //        {
        //            serNo = 1;
        //        }
        //        else
        //        {
        //            var lastSerNo = _context.Pur20.Where(x => x.PurNo == vm.PurNo).OrderByDescending(x => x.Serno);

        //            serNo = lastSerNo.First().Serno + 1;
        //        }

        //        //新增所需資料(廠商)
        //        Pur20 insertData = new Pur20
        //        {
        //            PurNo = vm.PurNo,
        //            Serno = serNo,
        //            PartNo = vm.PartNo,
        //            Qty = vm.Qty,
        //            Price = vm.Price,
        //            Amount = vm.Amount,
        //            Memo = vm.Memo,
        //            InQty = vm.InQty,
        //            Discount = vm.Discount,
        //            Unit = vm.Unit,
        //        };

        //        _context.Pur20.Add(insertData);
        //        await _context.SaveChangesAsync();

        //        vm.IsTrue = true;

        //        if (vm.IsTrue)
        //        {
        //            var pur10Data = _context.Pur10.Where(x => x.PurNo == vm.PurNo).FirstOrDefault();
        //            var pur20Data = (from pur20 in _context.Pur20
        //                             join stock10 in _context.Stock10
        //                             on pur20.PartNo equals stock10.PartNo
        //                             where pur20.PurNo == vm.PurNo
        //                             select new
        //                             {
        //                                 purNo = pur20.PurNo,
        //                                 partNo = pur20.PartNo,
        //                                 price = pur20.Price,
        //                                 qty = pur20.Qty,
        //                                 amount = pur20.Amount,
        //                                 taxType = stock10.TaxType
        //                             }).ToList();

        //            double? total0 = 0;
        //            double? total1 = 0;

        //            foreach (var item in pur20Data)
        //            {
        //                //total1 = 應稅 total0 = 免稅

        //                switch (item.taxType)
        //                {
        //                    case "應稅":
        //                        total1 += item.amount;
        //                        break;
        //                    case "免稅":
        //                        total0 += item.amount;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }

        //            pur10Data.Total1 = total1;
        //            pur10Data.Total0 = total0;
        //            pur10Data.Total = total1 + total0;

        //            await _context.SaveChangesAsync();
        //        }

        //        return Json(vm);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="BankNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> EditDetail(string PurNo, int SerNo)
        //{
        //    EditDetailViewModel vm = new EditDetailViewModel();

        //    try
        //    {
        //        if (string.IsNullOrEmpty(PurNo))
        //        {
        //            return NotFound();
        //        }

        //        #region SelectList

        //        //產品清單資料
        //        var stock10ListData = from stock10 in _context.Stock10
        //                              select new
        //                              {
        //                                  id = stock10.PartNo,
        //                                  text = stock10.PartNo + stock10.Spec,
        //                                  unit = stock10.Unit,
        //                                  taxType = stock10.TaxType,
        //                              };

        //        //產品清單資料
        //        ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //        //單位
        //        ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //        //稅別
        //        var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //        ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //        #endregion

        //        //資料
        //        var pur20Data = (from pur20 in _context.Pur20
        //                         join stock10 in _context.Stock10
        //                         on pur20.PartNo equals stock10.PartNo
        //                         where pur20.PurNo == PurNo && pur20.Serno == SerNo
        //                         select new
        //                         {
        //                             purNo = pur20.PurNo,
        //                             serNo = pur20.Serno,
        //                             partNo = pur20.PartNo,
        //                             qty = pur20.Qty,
        //                             unit = pur20.Unit,
        //                             taxType = stock10.TaxType,
        //                             price = pur20.Price,
        //                             discount = pur20.Discount,
        //                             amount = pur20.Amount,
        //                             memo = pur20.Memo,
        //                             inQty = pur20.InQty,
        //                         }).FirstOrDefault();

        //        if (pur20Data == null)
        //        {
        //            return NotFound();
        //        }

        //        vm.PurNo = pur20Data.purNo;
        //        vm.Serno = pur20Data.serNo;
        //        vm.PartNo = pur20Data.partNo;
        //        vm.Qty = pur20Data.qty;
        //        vm.Unit = pur20Data.unit;
        //        vm.TaxType = pur20Data.taxType;
        //        vm.Price = pur20Data.price;
        //        vm.Discount = pur20Data.discount;
        //        vm.Amount = pur20Data.amount;
        //        vm.Memo = pur20Data.memo;
        //        vm.InQty = pur20Data.inQty;

        //        //清單資料
        //        var pur20List = await _context.Pur20.Select(x => new Pur20List
        //        {
        //            PurNo = x.PurNo,
        //            Serno = x.Serno,
        //            PartNo = x.PartNo,
        //            Qty = x.Qty,
        //            Unit = x.Unit,
        //            Price = x.Price,
        //            Discount = x.Discount,
        //            Amount = x.Amount,
        //            Memo = x.Memo,
        //            InQty = x.InQty,
        //        }).ToListAsync();

        //        if (pur20List != null)
        //        {
        //            pur20List = pur20List.Where(x => x.PurNo == PurNo && x.Serno == SerNo).ToList();
        //            vm.pur20List = pur20List;

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
        /// <param name="vm"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditDetail(EditDetailViewModel vm)
        //{
        //    #region SelectList

        //    //產品清單資料
        //    var stock10ListData = from stock10 in _context.Stock10
        //                          select new
        //                          {
        //                              id = stock10.PartNo,
        //                              text = stock10.PartNo + stock10.Spec,
        //                              unit = stock10.Unit,
        //                              taxType = stock10.TaxType,
        //                          };

        //    //產品清單資料
        //    ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //    //單位
        //    ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //    //稅別
        //    var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //    ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //    #endregion

        //    try
        //    {
        //        vm.IsTrue = false;

        //        var checkData = _context.Pur20.Where(x => x.PurNo == vm.PurNo && x.Serno == vm.Serno).FirstOrDefault();

        //        if (checkData != null)
        //        {
        //            checkData.Amount = vm.Amount;
        //            checkData.Discount = vm.Discount;
        //            checkData.InQty = vm.InQty;
        //            checkData.Memo = vm.Memo;
        //            checkData.PartNo = vm.PartNo;
        //            checkData.Price = vm.Price;
        //            checkData.Qty = vm.Qty;
        //            checkData.Unit = vm.Unit;

        //            await _context.SaveChangesAsync();

        //            vm.IsTrue = true;

        //            if (vm.IsTrue)
        //            {
        //                var pur10Data = _context.Pur10.Where(x => x.PurNo == vm.PurNo).FirstOrDefault();
        //                var pur20Data = (from pur20 in _context.Pur20
        //                                 join stock10 in _context.Stock10
        //                                 on pur20.PartNo equals stock10.PartNo
        //                                 where pur20.PurNo == vm.PurNo
        //                                 select new
        //                                 {
        //                                     purNo = pur20.PurNo,
        //                                     partNo = pur20.PartNo,
        //                                     price = pur20.Price,
        //                                     qty = pur20.Qty,
        //                                     amount = pur20.Amount,
        //                                     taxType = stock10.TaxType
        //                                 }).ToList();

        //                double? total0 = 0;
        //                double? total1 = 0;

        //                foreach (var item in pur20Data)
        //                {
        //                    //total1 = 應稅 total0 = 免稅
        //                    switch (item.taxType)
        //                    {
        //                        case "應稅":
        //                            total1 += item.amount;
        //                            break;
        //                        case "免稅":
        //                            total0 += item.amount;
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }

        //                pur10Data.Total1 = total1;
        //                pur10Data.Total0 = total0;
        //                pur10Data.Total = total1 + total0;

        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }

        //        return Json(vm);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="PurNo"></param>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        //public async Task<IActionResult> DeleteDetail(string PurNo, int SerNo)
        //{
        //    #region SelectList

        //    //產品清單資料
        //    var stock10ListData = from stock10 in _context.Stock10
        //                          select new
        //                          {
        //                              id = stock10.PartNo,
        //                              text = stock10.PartNo + stock10.Spec,
        //                              unit = stock10.Unit,
        //                              taxType = stock10.TaxType,
        //                          };

        //    //產品清單資料
        //    ViewBag.Stock10ListData = new SelectList(stock10ListData.ToList(), "id", "text", string.Empty);

        //    //單位
        //    ViewBag.UnitList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "單位").ToList(), "Phase", "Phase", string.Empty);

        //    //稅別
        //    var taxTypeList = new List<SelectListItem>()
        //        {
        //           new SelectListItem {Text="請選擇", Value="" },
        //           new SelectListItem {Text="應稅", Value="應稅" },
        //           new SelectListItem {Text="免稅", Value="免稅" },
        //        };

        //    ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

        //    #endregion

        //    var deleteData = _context.Pur20.Where(x => x.PurNo == PurNo && x.Serno == SerNo).FirstOrDefault();

        //    if (deleteData == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Pur20.Remove(deleteData);
        //    await _context.SaveChangesAsync();

        //    var pur10Data = _context.Pur10.Where(x => x.PurNo == PurNo).FirstOrDefault();
        //    var pur20Data = (from pur20 in _context.Pur20
        //                     join stock10 in _context.Stock10
        //                     on pur20.PartNo equals stock10.PartNo
        //                     where pur20.PurNo == PurNo
        //                     select new
        //                     {
        //                         purNo = pur20.PurNo,
        //                         partNo = pur20.PartNo,
        //                         price = pur20.Price,
        //                         qty = pur20.Qty,
        //                         amount = pur20.Amount,
        //                         taxType = stock10.TaxType
        //                     }).ToList();

        //    double? total0 = 0;
        //    double? total1 = 0;

        //    foreach (var item in pur20Data)
        //    {
        //        //total1 = 應稅 total0 = 免稅
        //        switch (item.taxType)
        //        {
        //            case "應稅":
        //                total1 += item.amount;
        //                break;
        //            case "免稅":
        //                total0 += item.amount;
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    pur10Data.Total1 = total1;
        //    pur10Data.Total0 = total0;
        //    pur10Data.Total = total1 + total0;

        //    await _context.SaveChangesAsync();

        //    DeleteResponse vm = new DeleteResponse();

        //    vm.IsTrue = true;
        //    vm.PurNo = PurNo;

        //    return Json(vm);
        //}

        #endregion 子表
    }
}
