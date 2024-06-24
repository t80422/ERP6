using ERP6.Models;
using ERP6.Services;
using ERP6.ViewModels.Out30VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ERP6.Controllers
{
    /// <summary>
    /// C210_寄賣客戶庫存檔
    /// </summary>
    public class Out30Controller : Controller
    {
        private readonly EEPEF01Context _context;
        private IOut3040Service _out3040Service;

        public Out30Controller(EEPEF01Context context, IOut3040Service out3040Service)
        {
            _context = context;
            _out3040Service = out3040Service;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            if (!vm.IsSearch)
            {
                //如果有資料的話，帶出來
                if (!string.IsNullOrEmpty(vm.CoNo) && !string.IsNullOrEmpty(vm.Paymonth))
                {
                    vm = await (from out30 in _context.Out30
                                where out30.CoNo == vm.CoNo && out30.Paymonth == vm.Paymonth
                                select new IndexViewModel
                                {
                                    CoNo = out30.CoNo,
                                    Paymonth = !string.IsNullOrEmpty(out30.Paymonth) ?
                                        DateTime.ParseExact(out30.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                    Memo = out30.Memo,
                                    Userid = out30.Userid,
                                    StockPass = out30.StockPass,
                                    Total0 = out30.Total0,
                                    Total1 = out30.Total1,
                                    Tax = out30.Tax,
                                    Total2 = out30.Total2,
                                    TaxType = out30.TaxType,
                                    CashDis = out30.CashDis,
                                    SubTot = out30.SubTot,
                                    NotGet = out30.NotGet,
                                    Discount = out30.Discount,
                                }).FirstOrDefaultAsync();

                    //確認資料
                    var checkData = _context.Out40.Where(x => x.CoNo == vm.CoNo && x.Paymonth == vm.Paymonth).Any();

                    //找出子表清單
                    if (checkData)
                    {
                        vm.out40List = await (from out40 in _context.Out40
                                              join stock10 in _context.Stock10
                                              on out40.PartNo equals stock10.PartNo
                                              where out40.CoNo == vm.CoNo && out40.Paymonth == vm.Paymonth
                                              select new Out40List
                                              {
                                                  CoNo = out40.CoNo,
                                                  Paymonth = out40.Paymonth,
                                                  Barcode = out40.Barcode,
                                                  PartNo = out40.PartNo,
                                                  Price = out40.Price,
                                                  Unit = out40.Unit,
                                                  LQty = out40.LQty,
                                                  InQty = out40.InQty,
                                                  InretQty = out40.InretQty,
                                                  OutQty = out40.OutQty,
                                                  StQty = out40.StQty,
                                                  Amount = out40.Amount,
                                                  Discount = out40.Discount,
                                                  Spec = stock10.Spec,
                                                  TaxType = stock10.TaxType,
                                              }).ToListAsync();
                    }
                }
            }

            vm.out30List = await (from out30 in _context.Out30
                                  select new Out30List
                                  {
                                      CoNo = out30.CoNo,
                                      Paymonth = out30.Paymonth,
                                      Memo = out30.Memo,
                                      Userid = out30.Userid,
                                      StockPass = out30.StockPass,
                                      Total0 = out30.Total0,
                                      Total1 = out30.Total1,
                                      Tax = out30.Tax,
                                      Total2 = out30.Total2,
                                      TaxType = out30.TaxType,
                                      CashDis = out30.CashDis,
                                      SubTot = out30.SubTot,
                                      NotGet = out30.NotGet,
                                      Discount = out30.Discount,
                                  }).ToListAsync();

            if (vm.IsSearch)
            {
                //客戶編號
                if (!string.IsNullOrEmpty(vm.CoNo))
                {
                    vm.out30List = await vm.out30List.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();
                }

                //統計月份
                if (!string.IsNullOrEmpty(vm.Paymonth))
                {
                    vm.Paymonth = vm.Paymonth.Replace("-", "");

                    vm.out30List = await vm.out30List.Where(x => x.Paymonth == vm.Paymonth).ToListAsync();
                }
            }
            else
            {
                vm.out30List = await vm.out30List.Take(100).ToListAsync();
            }

            ViewBag.TaxTypeList = new List<SelectListItem>()
            {
                new SelectListItem{Text="免稅",Value="0"},
                new SelectListItem{Text="外加",Value="1"},
                new SelectListItem{Text="內含",Value="2"},
            };
            return View(vm);
        }

        /// <summary>
        /// 搜尋
        /// </summary>
        /// <param name="CoNo"></param>
        /// <param name="Paymonth"></param>
        /// <returns></returns>
        public async Task<IActionResult> Search(string CoNo, string Paymonth)
        {
            //先找出主表資料
            IndexViewModel vm = await (from out30 in _context.Out30
                                       where out30.CoNo == CoNo && out30.Paymonth == Paymonth
                                       select new IndexViewModel
                                       {
                                           CoNo = out30.CoNo,
                                           Paymonth = !string.IsNullOrEmpty(out30.Paymonth) ?
                                            DateTime.ParseExact(out30.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null,
                                           Memo = out30.Memo,
                                           Userid = out30.Userid,
                                           StockPass = out30.StockPass,
                                           Total0 = out30.Total0,
                                           Total1 = out30.Total1,
                                           Tax = out30.Tax,
                                           Total2 = out30.Total2,
                                           TaxType = out30.TaxType,
                                           CashDis = out30.CashDis,
                                           SubTot = out30.SubTot,
                                           NotGet = out30.NotGet,
                                           Discount = out30.Discount,
                                       }).FirstOrDefaultAsync();

            //子表列表資料
            var out40List = await (from out40 in _context.Out40
                                   join stock10 in _context.Stock10
                                   on out40.PartNo equals stock10.PartNo
                                   where out40.CoNo == CoNo && out40.Paymonth == Paymonth
                                   select new Out40List
                                   {
                                       CoNo = out40.CoNo,
                                       Paymonth = out40.Paymonth,
                                       Barcode = out40.Barcode,
                                       PartNo = out40.PartNo,
                                       Price = out40.Price,
                                       Unit = out40.Unit,
                                       LQty = out40.LQty ?? 0,
                                       InQty = out40.InQty ?? 0,
                                       InretQty = out40.InretQty ?? 0,
                                       OutQty = out40.OutQty ?? 0,
                                       StQty = out40.StQty ?? 0,
                                       Amount = out40.Amount ?? 0,
                                       Discount = out40.Discount ?? 0,
                                       Spec = stock10.Spec,
                                       TaxType = stock10.TaxType,
                                   }).OrderBy(x => x.Barcode).ToListAsync();

            if (out40List != null && out40List.Count > 0)
            {
                vm.out40List = out40List;
            }

            return Json(vm);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            var vm = new Out30VM()
            {
                Out30 = new Out30()
                {
                    Userid = HttpContext.Session.GetString("UserAc"),
                },
                Out40List = new List<Out30Detail>()
            };

            return View(vm);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Out30VM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _out3040Service.AddAsync(vm);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vm);
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="CoNo"></param>
        /// <param name="Paymonth"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string coNo, string paymonth)
        {
            var out30 = await _out3040Service.GetOut30ByIdAsync(coNo, paymonth);

            if (out30 == null)
                return NotFound();

            var out40List = await _out3040Service.GetOut40ListsByCoNoPaymonthAsync(coNo, paymonth);
            var vm = new Out30VM()
            {
                Out30 = out30,
                Out40List = out40List.ToList()
            };

            return View(vm);
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Out30VM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _out3040Service.UpdateAsync(vm);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vm);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="coNo"></param>
        /// <param name="payMonth"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string coNo, string payMonth)
        {
            try
            {
                await _out3040Service.DeleteAsync(coNo, payMonth);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "刪除過程中發生錯誤: " + ex.Message });
            }
        }

        /// <summary>
        /// 所有當月客戶資料重新排序
        /// </summary>
        [HttpPost]
        public async Task ReSort(string PAYMONTH)
        {
            PAYMONTH = !string.IsNullOrEmpty(PAYMONTH) ? DateTime.ParseExact(PAYMONTH, "yyyy-MM", null).ToString("yyyyMM") : null;
            //  https://localhost:44368/Out30/ReSort?CONO=90068&PAYMONTH=202305
            if (!String.IsNullOrEmpty(PAYMONTH))
            {
                // 建立臨時資料表
                var sqlstr = @"CREATE TABLE #out40_tmp (
				    CO_NO VARCHAR(8) NOT NULL,
				    PAYMONTH VARCHAR(6) NOT NULL , 
				    BARCODE VARCHAR(13) NOT NULL , 
				    PART_NO VARCHAR(20) NOT NULL , 
				    PRICE FLOAT NOT NULL , 
				    UNIT VARCHAR(4) NOT NULL , 
				    L_QTY FLOAT NULL , 
				    IN_QTY FLOAT NULL , 
				    INRET_QTY FLOAT NULL , 
				    OUT_QTY FLOAT NULL ,
				    ST_QTY FLOAT NULL , 
				    AMOUNT FLOAT NULL,
				    DISCOUNT FLOAT NULL,
				    SORTID BIGINT NULL);";
                // 將要重新排序的資料抓進臨時表
                sqlstr += @$"INSERT INTO #out40_tmp SELECT *,ROW_NUMBER() OVER (ORDER BY CO_NO,BARCODE) FROM OUT40 WHERE PAYMONTH = '{PAYMONTH}';";
                // 刪除臨時表排序列
                sqlstr += @"ALTER TABLE #out40_tmp DROP COLUMN SORTID;";
                // 刪除原表要重新排序的資料
                sqlstr += @$"DELETE FROM OUT40 WHERE PAYMONTH = '{PAYMONTH}';";
                // 將重新排好的資料抓進原表
                sqlstr += @"INSERT INTO OUT40 SELECT * FROM #out40_tmp ;";
                // 刪除臨時表
                sqlstr += @"DROP TABLE #out40_tmp;";

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlstr;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task<IActionResult> GetProducts()
        {
            var data = await _out3040Service.GetAllProductsAsync();

            if (data == null)
                return Json(null);

            return Json(data.Select(x => new
            {
                id = x.PartNo,
                text = x.PartNo + x.Spec
            }));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(string partNo, string coNo, string paymonth)
        {

            var partDetails = await _out3040Service.GetOut40Stock10Async(coNo, partNo, paymonth.Replace("-", ""));

            if (partDetails == null)
                return NotFound();

            return Json(partDetails);
        }

        /// <summary>
        /// 匯出檔案
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Export()
        {
            var customers = await _out3040Service.GetAllCustomersAsync();
            var products = await _out3040Service.GetAllProductsAsync();
            var cusList = customers.Select(x => new SelectListItem()
            {
                Value = x.CoNo,
                Text = x.CoNo + " " + x.Coname
            }).ToList();
            var prodList = products.Select(x => new SelectListItem()
            {
                Value = x.PartNo,
                Text = x.PartNo + " " + x.Spec
            }).ToList();
            var vm = new ExportVM()
            {
                CustomerList = new SelectList(cusList, "Value", "Text"),
                ProductList = new SelectList(prodList, "Value", "Text"),
                PrintType="盤點表"
            };
            return View(vm);
        }

        /// <summary>
        /// 匯出檔案
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Export(ExportVM vm)
        {
            if (!string.IsNullOrEmpty(vm.BillingMonthStart))
            {
                vm.BillingMonthStart = DateTime.ParseExact(vm.BillingMonthStart, "yyyy-MM", CultureInfo.InvariantCulture).ToString("yyyyMM");
            }

            if (!string.IsNullOrEmpty(vm.BillingMonthEnd))
            {
                vm.BillingMonthEnd = DateTime.ParseExact(vm.BillingMonthEnd, "yyyy-MM", CultureInfo.InvariantCulture).ToString("yyyyMM");
            }

            object filteredData;
            switch (vm.PrintType)
            {
                case "盤點表":
                     filteredData = await _out3040Service.GetConsignInventoryChecksDataAsync(vm);
                    return View("PrintConsignInventoryChecks", filteredData);

                case "對帳表":
                     filteredData = await _out3040Service.GetConsignReconciliationsAsync(vm);
                    return View("PrintConsignReconciliations",filteredData);

                default:
                    return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStockPass(string coNo, string payMonth, string stockPass)
        {
            try
            {
                var out30 = await _out3040Service.GetOut30ByIdAsync(coNo, payMonth);

                if (out30 == null)
                    return Json(new { success = false, message = "資料未找到" });

                out30.StockPass = stockPass;
                _out3040Service.UpdateStockPass(out30);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
