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
using ERP6.ViewModels.Stock20;
using Microsoft.AspNetCore.Http;

namespace ERP6.Controllers
{
    public class Stock20Controller : Controller
    {
        private readonly EEPEF01Context _context;

        public Stock20Controller(EEPEF01Context context)
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
                if (!vm.IsSearch)
                {
                    //如果有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.SpNo))
                    {
                        var stock20Data = await (from stock20 in _context.Stock20
                                                 where stock20.SpNo == vm.SpNo
                                                 select new IndexViewModel
                                                 {
                                                     SpNo = stock20.SpNo,
                                                     Bdate = !string.IsNullOrEmpty(stock20.Bdate) ?
                                                       DateTime.ParseExact(stock20.Bdate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                                     Edate = !string.IsNullOrEmpty(stock20.Edate) ?
                                                       DateTime.ParseExact(stock20.Edate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                                     CoNo = stock20.CoNo,
                                                     Memo = stock20.Memo,
                                                     Userid = stock20.Userid
                                                 }).FirstOrDefaultAsync();

                        //裝載資料
                        vm = new IndexViewModel
                        {
                            SpNo = stock20Data.SpNo,
                            Bdate = stock20Data.Bdate,
                            Edate = stock20Data.Edate,
                            CoNo = stock20Data.CoNo,
                            Memo = stock20Data.Memo,
                            Userid = stock20Data.Userid
                        };

                        //確認資料
                        var checkStock21Data = _context.Stock21.Where(x => x.SpNo == vm.SpNo).Any();

                        if (checkStock21Data)
                        {
                            vm.stock21List = await (from stock21 in _context.Stock21
                                                    join stock10 in _context.Stock10
                                                    on stock21.PartNo equals stock10.PartNo
                                                    where stock21.SpNo == vm.SpNo
                                                    select new Stock21List
                                                    {
                                                        SpNo = stock21.SpNo,
                                                        Serno = stock21.Serno,
                                                        PartNo = stock21.PartNo,
                                                        Oldprice = stock21.Oldprice,
                                                        Oldsprice = stock21.Oldsprice,
                                                        Newprice = stock21.Newprice,
                                                        Newsprice = stock21.Newsprice,
                                                        Memo = stock21.Memo,
                                                        Qty = stock21.Qty,
                                                        Unit = stock21.Unit,
                                                        Spec = stock10.Spec,
                                                        TaxType = stock10.TaxType,
                                                    }).ToListAsync();
                        }
                    }
                }

                //取得資料
                var stock20List = await (from stock20 in _context.Stock20
                                         select new Stock20List
                                         {
                                             SpNo = stock20.SpNo,
                                             Bdate = !string.IsNullOrEmpty(stock20.Bdate) ?
                                                          DateTime.ParseExact(stock20.Bdate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                             Edate = !string.IsNullOrEmpty(stock20.Edate) ?
                                                          DateTime.ParseExact(stock20.Edate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                             CoNo = stock20.CoNo,
                                             Memo = stock20.Memo,
                                             Userid = stock20.Userid,
                                         }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        stock20List = await stock20List.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();
                    }

                    //有效日期begin
                    if (!string.IsNullOrEmpty(vm.Bdate))
                    {
                        vm.Bdate = vm.Bdate.Replace("-", "");

                        stock20List = await stock20List.Where(x => x.Bdate.Contains(vm.Bdate)).ToListAsync();
                    }

                    //有效日期end
                    if (!string.IsNullOrEmpty(vm.Edate))
                    {
                        vm.Edate = vm.Edate.Replace("-", "");

                        stock20List = await stock20List.Where(x => x.Edate.Contains(vm.Edate)).ToListAsync();
                    }

                    //調價單號
                    if (!string.IsNullOrEmpty(vm.SpNo))
                    {
                        stock20List = await stock20List.Where(x => x.SpNo.Contains(vm.SpNo)).ToListAsync();
                    }
                }

                vm.stock20List = stock20List;

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> Search(string SpNo)
        {
            //先找出主表資料
            var stock20Data = await (from stock20 in _context.Stock20
                                     where stock20.SpNo == SpNo
                                     select new IndexViewModel
                                     {
                                         SpNo = stock20.SpNo,
                                         Bdate = !string.IsNullOrEmpty(stock20.Bdate) ?
                                           DateTime.ParseExact(stock20.Bdate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                         Edate = !string.IsNullOrEmpty(stock20.Edate) ?
                                           DateTime.ParseExact(stock20.Edate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                         CoNo = stock20.CoNo,
                                         Memo = stock20.Memo,
                                         Userid = stock20.Userid
                                     }).FirstOrDefaultAsync();

            //裝載資料
            IndexViewModel vm = new IndexViewModel
            {
                SpNo = stock20Data.SpNo,
                Bdate = stock20Data.Bdate,
                Edate = stock20Data.Edate,
                CoNo = stock20Data.CoNo,
                Memo = stock20Data.Memo,
                Userid = stock20Data.Userid
            };

            //主表列表資料
            var stock20List = await (from stock20 in _context.Stock20
                                     select new Stock20List
                                     {
                                         SpNo = stock20.SpNo,
                                         //Bdate = !string.IsNullOrEmpty(stock20.Bdate) ?
                                         //             DateTime.ParseExact(stock20.Bdate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                         //Edate = !string.IsNullOrEmpty(stock20.Edate) ?
                                         //             DateTime.ParseExact(stock20.Edate, "yyyyMMdd", null).ToString("yyyy-MM-dd") : null,
                                         //CoNo = stock20.CoNo,
                                         //Memo = stock20.Memo,
                                         //Userid = stock20.Userid,
                                     }).ToListAsync();

            if (stock20List != null)
            {
                vm.stock20List = stock20List;
            }

            if (stock20Data != null)
            {
                //找出子表資料
                var stock21Data = await (from stock21 in _context.Stock21
                                         join stock10 in _context.Stock10
                                         on stock21.PartNo equals stock10.PartNo
                                         where stock21.SpNo == vm.SpNo
                                         select new Stock21List
                                         {
                                             SpNo = stock21.SpNo,
                                             Serno = stock21.Serno,
                                             PartNo = stock21.PartNo,
                                             Oldprice = stock21.Oldprice,
                                             Oldsprice = stock21.Oldsprice,
                                             Newprice = stock21.Newprice,
                                             Newsprice = stock21.Newsprice,
                                             Memo = string.IsNullOrEmpty(stock21.Memo) ? string.Empty : stock21.Memo,
                                             Qty = stock21.Qty,
                                             Unit = stock21.Unit,
                                             Spec = stock10.Spec,
                                             TaxType = stock10.TaxType,
                                         }).OrderByDescending(x => x.Serno).ToListAsync();

                vm.stock21List = stock21Data;
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
                AddViewModel vm = new AddViewModel();

                //取得資料
                var stock20List = await (from stock20 in _context.Stock20
                                         select new Stock20List
                                         {
                                             SpNo = stock20.SpNo,
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

                vm.stock20List = stock20List;

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
                var strDate = DateTime.Now.ToString("yyyyMM");
                var autoSpNo = string.Empty;

                var stock20DataCount = _context.Stock20.Where(x => x.SpNo.Contains(strDate)).Count();

                if (stock20DataCount == 0)
                {
                    //等於0代表當月沒資料 直接新增
                    autoSpNo = strDate + "0001";
                }
                else
                {
                    stock20DataCount = stock20DataCount + 1;
                    autoSpNo = strDate + stock20DataCount.ToString().PadLeft(4, '0');
                }

                //新增所需資料(廠商)
                Stock20 insertData = new Stock20
                {
                    SpNo = autoSpNo,
                    Bdate = string.IsNullOrEmpty(vm.Bdate) ? null : vm.Bdate.Replace("-", ""),
                    Edate = string.IsNullOrEmpty(vm.Edate) ? null : vm.Edate.Replace("-", ""),
                    CoNo = vm.CoNo,
                    Memo = vm.Memo,
                    Userid = vm.Userid
                };

                _context.Stock20.Add(insertData);
                await _context.SaveChangesAsync();

                //設定預設資料
                vm.SpNo = autoSpNo;
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
        public async Task<IActionResult> Edit(string SpNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                if (string.IsNullOrEmpty(SpNo))
                {
                    return NotFound();
                }

                //資料
                var stock20Data = _context.Stock20
                    .Where(x => x.SpNo == SpNo).FirstOrDefault();

                if (stock20Data == null)
                {
                    return NotFound();
                }

                vm.SpNo = stock20Data.SpNo;
                vm.Bdate = string.IsNullOrEmpty(stock20Data.Bdate) ?
                    null : DateTime.ParseExact(stock20Data.Bdate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.Edate = string.IsNullOrEmpty(stock20Data.Edate) ?
                    null : DateTime.ParseExact(stock20Data.Edate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.CoNo = stock20Data.CoNo;
                vm.Memo = stock20Data.Memo;

                //只顯示不修改
                vm.Userid = stock20Data.Userid;

                //清單資料
                var stock20List = await _context.Stock20.Select(x => new Stock20List
                {
                    SpNo = x.SpNo,
                }).ToListAsync();

                if (stock20List != null)
                {
                    stock20List = stock20List.Where(x => x.SpNo == SpNo).ToList();
                    vm.stock20List = stock20List;

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
                    Stock20 updateData = new Stock20
                    {
                        SpNo = vm.SpNo,
                        Bdate = string.IsNullOrEmpty(vm.Bdate) ? null : vm.Bdate.Replace("-", ""),
                        Edate = string.IsNullOrEmpty(vm.Edate) ? null : vm.Edate.Replace("-", ""),
                        CoNo = vm.CoNo,
                        Memo = vm.Memo,
                        Userid = HttpContext.Session.GetString("UserAc"),
                    };

                    _context.Stock20.Update(updateData);

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
        public async Task<IActionResult> Delete(string SpNo)
        {
            var checkStock20Data = await _context.Stock20.Where(x => x.SpNo == SpNo).FirstOrDefaultAsync();

            if (checkStock20Data == null)
            {
                return NotFound();
            }

            //先清除子表資料
            var removeStock21Data = _context.Stock21.Where(x => x.SpNo == SpNo).ToList();

            if (removeStock21Data != null)
            {
                _context.Stock21.RemoveRange(removeStock21Data);
                await _context.SaveChangesAsync();
            }

            //再清除主表資料
            _context.Stock20.Remove(checkStock20Data);
            await _context.SaveChangesAsync();

            return Json(true);
        }

        /// <summary>
        /// 複製
        /// </summary>
        /// <param name="SpNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Copy(string SpNo)
        {
            var strDate = DateTime.Now.ToString("yyyyMM");
            var autoSpNo = string.Empty;

            if (string.IsNullOrEmpty(SpNo))
            {
                return NotFound();
            }

            //找出主表資料並複製
            var stock20Data = _context.Stock20.Where(x => x.SpNo == SpNo).FirstOrDefault();

            if (stock20Data == null)
            {
                return NotFound();
            }

            //找出此筆當月份資料
            var lastDataNum = int.Parse(_context.Stock20.Where(x => x.SpNo.Contains(strDate))
                        .OrderByDescending(x => x.SpNo).FirstOrDefault().SpNo) + 1;

            autoSpNo = lastDataNum.ToString();

            Stock20 insertData = new Stock20
            {
                SpNo = autoSpNo,
                CoNo = stock20Data.CoNo,
                Bdate = stock20Data.Bdate,
                Edate = stock20Data.Edate,
                Userid = HttpContext.Session.GetString("UserAc"),
                Memo = stock20Data.Memo,
            };

            _context.Stock20.Add(insertData);
            await _context.SaveChangesAsync();

            //取出子表資料
            var stock21Data = await _context.Stock21.Where(x => x.SpNo == SpNo).ToListAsync();

            if (stock21Data != null && stock21Data.Count > 0)
            {
                //塞資料
                foreach (var item in stock21Data)
                {
                    Stock21 stock21InsertData = new Stock21
                    {
                        SpNo = insertData.SpNo,
                        Memo = item.Memo,
                        Newprice = item.Newprice,
                        Newsprice = item.Newsprice,
                        Oldprice = item.Oldprice,
                        Oldsprice = item.Oldsprice,
                        PartNo = item.PartNo,
                        Qty = item.Qty,
                        Serno = item.Serno,
                        Unit = item.Unit,
                    };

                    _context.Stock21.Add(stock21InsertData);
                    await _context.SaveChangesAsync();
                }

            }

            return Json(insertData.SpNo);
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
                    if (!string.IsNullOrEmpty(vm.SpNo))
                    {
                        var purData = (from stock21 in _context.Stock21
                                       join stock10 in _context.Stock10
                                       on stock21.PartNo equals stock10.PartNo
                                       where stock21.SpNo == vm.SpNo && stock21.Serno == vm.Serno
                                       select new
                                       {
                                           stock21,
                                           stock10
                                       }).FirstOrDefault();

                        if (purData != null)
                        {
                            //裝載資料
                            vm = new IndexDetailViewModel
                            {
                                SpNo = purData.stock21.SpNo,
                                Serno = purData.stock21.Serno,
                                PartNo = purData.stock21.PartNo,
                                Oldprice = purData.stock21.Oldprice,
                                Oldsprice = purData.stock21.Oldsprice,
                                Newprice = purData.stock21.Newprice,
                                Newsprice = purData.stock21.Newsprice,
                                Memo = purData.stock21.Memo,
                                Qty = purData.stock21.Qty,
                                Unit = purData.stock21.Unit,
                                Spec = purData.stock10.Spec,
                                TaxType = purData.stock10.TaxType,
                            };
                        }

                        //取得Stock21List資料
                        var checkStock21Data = _context.Stock21.Where(x => x.SpNo == vm.SpNo).Any();

                        if (checkStock21Data)
                        {
                            vm.stock21List = (from stock21 in _context.Stock21
                                              join stock10 in _context.Stock10
                                              on stock21.PartNo equals stock10.PartNo
                                              where stock21.SpNo == vm.SpNo
                                              select new Stock21List
                                              {
                                                  PartNo = stock21.PartNo,
                                                  Serno = stock21.Serno,
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
                        vm.stock21List = await vm.stock21List.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
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
                var data = await (from stock21 in _context.Stock21
                                  join stock10 in _context.Stock10
                                  on stock21.PartNo equals stock10.PartNo
                                  where stock21.PartNo == PartNo && stock21.Serno == SerNo
                                  select new
                                  {
                                      serNo = stock21.Serno,
                                      partNo = stock21.PartNo,
                                      spec = stock10.Spec,
                                      qty = stock21.Qty,
                                      unit = stock21.Unit,
                                      taxType = stock10.TaxType,
                                      oldPrice = stock21.Oldprice,
                                      oldSPrice = stock21.Oldsprice,
                                      newPrice = stock21.Newprice,
                                      newSPrice = stock21.Newsprice,
                                      memo = stock21.Memo,
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
        public async Task<IActionResult> AddDetail(string SpNo)
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

                vm.stock21List = await (from stock21 in _context.Stock21
                                        join stock10 in _context.Stock10
                                        on stock21.PartNo equals stock10.PartNo
                                        where stock21.SpNo == SpNo
                                        select new Stock21List
                                        {
                                            PartNo = stock21.PartNo,
                                            Serno = stock21.Serno,
                                        }).ToListAsync();

                //預設資料
                vm.SpNo = SpNo;

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

                var checkData = _context.Stock21.Where(x => x.SpNo == vm.SpNo).Any();

                if (!checkData)
                {
                    serNo = 1;
                }
                else
                {
                    var lastSerNo = _context.Stock21.Where(x => x.SpNo == vm.SpNo).OrderByDescending(x => x.Serno);

                    serNo = lastSerNo.First().Serno + 1;
                }

                //新增所需資料(廠商)
                Stock21 insertData = new Stock21
                {
                    SpNo = vm.SpNo,
                    Serno = serNo,
                    PartNo = vm.PartNo,
                    Qty = vm.Qty,
                    Unit = vm.Unit,
                    Oldprice = vm.Oldprice,
                    Oldsprice = vm.Oldsprice,
                    Newprice = vm.Newprice,
                    Newsprice = vm.Newsprice,
                    Memo = vm.Memo,
                };

                _context.Stock21.Add(insertData);
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
        /// <param name="BankNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditDetail(string SpNo, int SerNo)
        {
            EditDetailViewModel vm = new EditDetailViewModel();

            try
            {
                if (string.IsNullOrEmpty(SpNo) || SerNo <= 0)
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
                var stock21Data = (from stock21 in _context.Stock21
                                   join stock10 in _context.Stock10
                                   on stock21.PartNo equals stock10.PartNo
                                   where stock21.SpNo == SpNo && stock21.Serno == SerNo
                                   select new
                                   {
                                       SpNo = stock21.SpNo,
                                       Serno = stock21.Serno,
                                       PartNo = stock21.PartNo,
                                       Qty = stock21.Qty,
                                       Unit = stock21.Unit,
                                       Oldprice = stock21.Oldprice,
                                       Oldsprice = stock21.Oldsprice,
                                       Newprice = stock21.Newprice,
                                       Newsprice = stock21.Newsprice,
                                       Memo = stock21.Memo,
                                   }).FirstOrDefault();

                if (stock21Data == null)
                {
                    return NotFound();
                }

                vm.SpNo = stock21Data.SpNo;
                vm.Serno = stock21Data.Serno;
                vm.PartNo = stock21Data.PartNo;
                vm.Qty = stock21Data.Qty;
                vm.Unit = stock21Data.Unit;
                vm.Oldprice = stock21Data.Oldprice;
                vm.Oldsprice = stock21Data.Oldsprice;
                vm.Newprice = stock21Data.Newprice;
                vm.Newsprice = stock21Data.Newsprice;
                vm.Memo = stock21Data.Memo;

                //清單資料
                var stock21List = await _context.Stock21.Select(x => new Stock21List
                {
                    SpNo = x.SpNo,
                    PartNo = x.PartNo,
                    Serno = x.Serno,
                }).ToListAsync();

                if (stock21List != null)
                {
                    stock21List = stock21List.Where(x => x.SpNo == SpNo && x.Serno == SerNo).ToList();
                    vm.stock21List = stock21List;

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

                var checkData = _context.Stock21.Where(x => x.SpNo == vm.SpNo && x.Serno == vm.Serno).FirstOrDefault();

                if (checkData != null)
                {
                    checkData.PartNo = vm.PartNo;
                    checkData.Qty = vm.Qty;
                    checkData.Unit = vm.Unit;
                    checkData.Oldprice = vm.Oldprice;
                    checkData.Oldsprice = vm.Oldsprice;
                    checkData.Newprice = vm.Newprice;
                    checkData.Newsprice = vm.Newsprice;
                    checkData.Memo = vm.Memo;

                    await _context.SaveChangesAsync();

                    vm.IsTrue = true;
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
        public async Task<IActionResult> DeleteDetail(string SpNo, int SerNo)
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

            var deleteData = _context.Stock21.Where(x => x.SpNo == SpNo && x.Serno == SerNo).FirstOrDefault();

            if (deleteData == null)
            {
                return NotFound();
            }

            _context.Stock21.Remove(deleteData);
            await _context.SaveChangesAsync();

            DeleteResponse vm = new DeleteResponse();

            vm.IsTrue = true;
            vm.SpNo = SpNo;

            return Json(vm);
        }

        #endregion 子表
    }
}
