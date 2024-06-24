using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using ERP6.ViewModels.Stock;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ERP6.Controllers
{
    public class StockController : Controller
    {
        private readonly EEPEF01Context _context;
        private readonly AjaxsController _ajax;        

        public StockController(EEPEF01Context context , AjaxsController ajax)
        {
            _context = context;
            _ajax = ajax;
        }

        public class PriceTypeList
        {
            public int PT_ID { get; set; }
            public string PT_TEXT { get; set; }
            public string PT_VALUE { get; set; }
        }

        /// <summary>
        /// 產品首頁
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(StockIndexViewModel vm, int? page)
        {
            try
            {
                #region SelectList

                //產品分類
                ViewBag.TypeList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "產品分類").ToList(), "Phase", "Phase", string.Empty);

                //庫存異動
                var YnCountList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.YnCountList = new SelectList(YnCountList, "Value", "Text", string.Empty);

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

                // 可否退貨
                var ReturnList123 = new List<SelectListItem>()
                {
                    new SelectListItem {Text="請選擇", Value="" },
                    new SelectListItem {Text="可退貨" , Value="1"},
                    new SelectListItem {Text="不可退貨" , Value="2"}
                };
                ViewBag.ReturnList123 = new SelectList(ReturnList123, "Value", "Text", string.Empty);

                #endregion

                //foreach(var it in In20List)
                //{
                //    if (!In20Datas.ContainsKey(it.PartNo))
                //    {
                //        In20Datas.Add(it.PartNo, new In20Data() { LastCost = it.Price ?? 0, LastIn = _context.In10.Find(it.InNo)?.InDate });
                //    }
                //}

                //TODO 取得產品資料
                var StockList = _context.Stock10.Select(x => new StockList
                {
                    PartNo = x.PartNo,
                    Barcode = x.Barcode,
                    Spec = x.Spec,
                    Unit = x.Unit,
                    Cost1 = x.Cost1 ?? 0,
                    Cost2 = x.Cost2 ?? 0,
                    Cost3 = x.Cost3 ?? 0,
                    SalesCost = x.SalesCost ?? 0,
                    Price1 = x.Price1 ?? 0,
                    Price2 = x.Price2 ?? 0,
                    Price3 = x.Price3 ?? 0,
                    TaxType = x.TaxType,
                    Type = x.Type,
                    Atti = x.Atti,
                    LastIn = x.LastIn ?? "",
                    //LastIn = In20List.Where(y => y.Key == x.PartNo).FirstOrDefault().Value != null ?
                    //                _context.In10.Where(z=>z.InNo == (In20List.Where(y => y.Key == x.PartNo).FirstOrDefault().Value.InNo ?? "")).FirstOrDefault().InDate : "",
                    //LastCost = In20Datas.ContainsKey(x.PartNo) ? (In20Datas[x.PartNo].LastCost ?? 0) : 0,
                    //LastCost = In20List.ContainsKey(x.PartNo) ? (In20List[x.PartNo].Price ?? 0) : 0,
                    LastCost = x.LastCost ?? 0,
                    LastDiscount = x.LastDiscount ?? 0,
                    LastOut = x.LastOut,
                    SafeQty = x.SafeQty ?? 0,
                    StQty = x.StQty ?? 0,
                    Location = x.Location,
                    TranPara1 = x.TranPara1 ?? 0,
                    TranPara2 = x.TranPara2 ?? 0,
                    TranPara3 = x.TranPara3 ?? 0,
                    SPrice1 = x.SPrice1 ?? 0,
                    SPrice2 = x.SPrice2 ?? 0,
                    SPrice3 = x.SPrice3 ?? 0,
                    TUnit1 = x.TUnit1,
                    TUnit2 = x.TUnit2,
                    TUnit3 = x.TUnit3,
                    YnCount = x.YnCount,
                    PackQty = x.PackQty ?? 0,
                    InitQty1 = x.InitQty1 ?? 0,
                    InitQty2 = x.InitQty2 ?? 0,
                    InitCost1 = x.InitCost1 ?? 0,
                    InitCost2 = x.InitCost2 ?? 0,
                    InitCost3 = x.InitCost3 ?? 0,
                    InitCost4 = x.InitCost4 ?? 0,
                    InitCost5 = x.InitCost5 ?? 0,
                    CompCost = x.CompCost ?? 0,
                    L = x.L ?? 0,
                    W = x.W ?? 0,
                    H = x.H ?? 0,
                    Cuft = x.Cuft ?? 0,
                    LastModidate = x.LastModidate,
                    LastModiuser = x.LastModiuser,
                    ST_PS = x.ST_PS,
                    IsOpen = (bool)x.IsOpen,
                    IsShow = (bool)x.IsShow,
                    UnitPara1 = x.UnitPara1,
                    UnitPara2 = x.UnitPara2,
                    UnitPara3 = x.UnitPara3,
                    SPrice4 = x.SPrice4 ?? 0,
                    SPrice5 = x.SPrice5 ?? 0,
                    SPrice6 = x.SPrice6 ?? 0,
                    SPrice7 = x.SPrice7 ?? 0,
                    SPrice8 = x.SPrice8 ?? 0,
                    DefaultPrice1 = x.DefaultPrice1 ?? 0,
                    DefaultPrice2 = x.DefaultPrice2 ?? 0,
                    DefaultPrice3 = x.DefaultPrice3 ?? 0,
                    DefaultPrice4 = x.DefaultPrice4 ?? 0,
                    DefaultPrice5 = x.DefaultPrice5 ?? 0,
                    IsReturn = x.IsReturn != null ? ((bool)x.IsReturn ? "1" : "2") : "",
                }).ToList();

                //// 取得進貨資料
                //// 所有進貨單
                //var INData = await _context.In10.Where(x => x.InType == "1").OrderByDescending(x => x.InNo).Select(x => x.InNo).Distinct().ToListAsync();
                //var In20List = _context.In20.Where(x => INData.Contains(x.InNo)).ToList().GroupBy(x => x.PartNo)
                //    .ToDictionary(y => y.Key, y => y.OrderByDescending(z => z.InNo).FirstOrDefault());


                //搜尋條件(商品)
                if (vm.IsSearch)
                {
                    //商品編號
                    if (!string.IsNullOrEmpty(vm.PartNo))
                    {
                        StockList = await StockList.Where(x => x.PartNo.Contains(vm.PartNo)).ToListAsync();
                    }

                    //品名規格
                    if (!string.IsNullOrEmpty(vm.Spec))
                    {
                        StockList = await StockList.Where(x => x.Spec.Contains(vm.Spec)).ToListAsync();
                    }

                    //商品條碼
                    if (!string.IsNullOrEmpty(vm.Barcode))
                    {
                        StockList = await StockList.Where(x => x.Barcode.Contains(vm.Barcode)).ToListAsync();
                    }

                    //商品分類
                    if (!string.IsNullOrEmpty(vm.Type))
                    {
                        StockList = await StockList.Where(x => x.Type == vm.Type).ToListAsync();
                    }
                    // 可否退貨
                    if (!string.IsNullOrEmpty(vm.IsReturn))
                    {
                        StockList = await StockList.Where(x => x.IsReturn == vm.IsReturn).ToListAsync();
                    }
                }

                vm.stockList = StockList;

                //毛利率
                vm.GPM1 = ((vm.SPrice1 ?? 0 - vm.Price1 ?? 0) - (vm.Price1 ?? 0)) / vm.SPrice1 ?? 0 * 100;
                vm.GPM2 = ((vm.SPrice2 ?? 0 - vm.Price2 ?? 0) - (vm.Price2 ?? 0)) / vm.SPrice2 ?? 0 * 100;
                vm.GPM3 = ((vm.SPrice3 ?? 0 - vm.Price3 ?? 0) - (vm.Price3 ?? 0)) / vm.SPrice3 ?? 0 * 100;

                // 價格名稱
                var PriceTypeList = await _context.PriceType.OrderBy(x => x.PT_ID).Select(x=>new PriceTypeList 
                {
                    PT_ID = x.PT_ID ,
                    PT_TEXT = x.PT_TEXT ,
                    PT_VALUE = x.PT_VALUE
                }).ToListAsync();

                ViewBag.PriceTypeList = PriceTypeList;
                
                var PriceTypes = new Dictionary<string, string>();
                PriceTypes["First"] = PriceTypeList.Where(x => x.PT_ID == 1).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Second"] = PriceTypeList.Where(x => x.PT_ID == 2).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Third"] = PriceTypeList.Where(x => x.PT_ID == 3).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Forth"] = PriceTypeList.Where(x => x.PT_ID == 4).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Fifth"] = PriceTypeList.Where(x => x.PT_ID == 5).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Sixth"] = PriceTypeList.Where(x => x.PT_ID == 6).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Seventh"] = PriceTypeList.Where(x => x.PT_ID == 7).FirstOrDefault()?.PT_VALUE;
                PriceTypes["Eighth"] = PriceTypeList.Where(x => x.PT_ID == 8).FirstOrDefault()?.PT_VALUE;
                ViewBag.PriceTypes = PriceTypes;

                return View(vm);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 新增產品資料
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            try
            {
                StockAddViewModel vm = new StockAddViewModel();

                #region SelectList

                //產品分類
                ViewBag.TypeList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "產品分類").ToList(), "Phase", "Phase", string.Empty);

                //庫存異動
                var YnCountList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.YnCountList = new SelectList(YnCountList, "Value", "Text", string.Empty);

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

                // 可否退貨
                var ReturnList123 = new List<SelectListItem>()
                {
                    new SelectListItem {Text="可退貨" , Value="1"},
                    new SelectListItem {Text="不可退貨" , Value="2"}
                };
                ViewBag.ReturnList123 = new SelectList(ReturnList123, "Value", "Text", string.Empty);

                #endregion


                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增產品資料
        /// </summary>
        /// <param name="vm">StockAddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StockAddViewModel vm)
        {
            try
            {
                #region SelectList

                //產品分類
                ViewBag.TypeList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "產品分類").ToList(), "Phase", "Phase", string.Empty);

                //庫存異動
                var YnCountList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.YnCountList = new SelectList(YnCountList, "Value", "Text", string.Empty);

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

                if (String.IsNullOrEmpty(vm.PartNo))
                {

                    vm.IsTrue = false;
                    vm.ErrorMessage = "產品編號不得為空";

                    //return Json(vm);
                    return View(vm);

                }
                //檢查PartNo是否唯一
                var checkPartNo = _context.Stock10.Where(x => x.PartNo == vm.PartNo).FirstOrDefault();

                if (checkPartNo != null)
                {

                    vm.IsTrue = false;
                    vm.ErrorMessage = "產品編號已經存在，請重新編輯";

                    //return Json(vm);
                    return View(vm);
                }

                // 新增圖片
                var imageName = vm.Image != null && vm.Image.Length > 0 ? vm.Image.FileName : "";
                var imagePath = await _ajax.UploadFile(vm.Image, "/image/stock10");

                //TODO 新增所需資料(產品)
                Stock10 insertData = new Stock10
                {
                    PartNo = vm.PartNo ?? "",
                    Barcode = vm.Barcode ?? "",
                    Spec = vm.Spec ?? "",
                    Unit = vm.Unit ?? "",
                    Cost1 = vm.Cost1 ?? 0,
                    Cost2 = vm.Cost2 ?? 0,
                    Cost3 = vm.Cost3 ?? 0,
                    SalesCost = vm.SalesCost ?? 0,
                    Price1 = vm.Price1 ?? 0,
                    Price2 = vm.Price2 ?? 0,
                    Price3 = vm.Price3 ?? 0,
                    TaxType = vm.TaxType ?? "",
                    Type = vm.Type ?? "",
                    Atti = vm.Atti ?? "",
                    LastIn = !String.IsNullOrEmpty(vm.LastIn) ? vm.LastIn.Replace("-", "") : "",
                    LastCost = vm.LastCost ?? 0,
                    LastDiscount = vm.LastDiscount ?? 0,
                    LastOut = !String.IsNullOrEmpty(vm.LastOut) ? vm.LastOut.Replace("-", "") : "",
                    SafeQty = vm.SafeQty ?? 0,
                    StQty = vm.StQty ?? 0,
                    Location = vm.Location ?? "",
                    TranPara1 = vm.TranPara1 ?? 0,
                    TranPara2 = vm.TranPara2 ?? 0,
                    TranPara3 = vm.TranPara3 ?? 0,
                    SPrice1 = vm.SPrice1 ?? 0,
                    SPrice2 = vm.SPrice2 ?? 0,
                    SPrice3 = vm.SPrice3 ?? 0,
                    TUnit1 = vm.TUnit1 ?? "",
                    TUnit2 = vm.TUnit2 ?? "",
                    TUnit3 = vm.TUnit3 ?? "",
                    YnCount = vm.YnCount ?? "",
                    PackQty = vm.PackQty ?? 0,
                    InitQty1 = vm.InitQty1 ?? 0,
                    InitQty2 = vm.InitQty2 ?? 0,
                    InitCost1 = vm.InitCost1 ?? 0,
                    InitCost2 = vm.InitCost2 ?? 0,
                    InitCost3 = vm.InitCost3 ?? 0,
                    InitCost4 = vm.InitCost4 ?? 0,
                    InitCost5 = vm.InitCost5 ?? 0,
                    CompCost = vm.CompCost ?? 0,
                    L = vm.L ?? 0,
                    W = vm.W ?? 0,
                    H = vm.H ?? 0,
                    Cuft = vm.Cuft ?? 0,
                    LastModidate = DateTime.Now.ToString("yyyyMMdd"),
                    LastModiuser = HttpContext.Session.GetString("UserAc"),
                    ST_PS = vm.ST_PS ?? "",
                    IsOpen = vm.IsShow, // 預設同isshow //IsOpen = vm.IsOpen,
                    IsShow = vm.IsShow,
                    UnitPara1 = vm.UnitPara1 ?? "",
                    UnitPara2 = vm.UnitPara2 ?? "",
                    UnitPara3 = vm.UnitPara3 ?? "",
                    SPrice4 = vm.SPrice4 ?? 0,
                    SPrice5 = vm.SPrice5 ?? 0,
                    SPrice6 = vm.SPrice6 ?? 0,
                    SPrice7 = vm.SPrice7 ?? 0,
                    SPrice8 = vm.SPrice8 ?? 0,
                    DefaultPrice1 = vm.DefaultPrice1 ?? 0,
                    DefaultPrice2 = vm.DefaultPrice2 ?? 0,
                    DefaultPrice3 = vm.DefaultPrice3 ?? 0,
                    DefaultPrice4 = vm.DefaultPrice4 ?? 0,
                    DefaultPrice5 = vm.DefaultPrice5 ?? 0,

                    ImageName = imageName,
                    ImagePath = imagePath,

                    IsReturn = !String.IsNullOrEmpty(vm.IsReturn) ? vm.IsReturn == "1" : true,
                };

                _context.Stock10.Add(insertData);
                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                return RedirectToAction("Index", vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 編輯產品資料
        /// </summary>
        /// <param name="PartNo">產品代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string PartNo)
        {
            StockEditViewModel vm = new StockEditViewModel();

            try
            {
                if (string.IsNullOrEmpty(PartNo))
                {
                    return NotFound();
                }

                //TODO 編輯產品資料
                var updateData = await _context.Stock10.Where(x => x.PartNo == PartNo).FirstOrDefaultAsync();

                if (updateData == null)
                {
                    return NotFound();
                }
                vm.PartNo = updateData.PartNo;
                vm.Barcode = updateData.Barcode;
                vm.Spec = updateData.Spec;
                vm.Unit = updateData.Unit;
                vm.Cost1 = updateData.Cost1 ?? 0;
                vm.Cost2 = updateData.Cost2 ?? 0;
                vm.Cost3 = updateData.Cost3 ?? 0;
                vm.SalesCost = updateData.SalesCost ?? 0;
                vm.Price1 = updateData.Price1 ?? 0;
                vm.Price2 = updateData.Price2 ?? 0;
                vm.Price3 = updateData.Price3 ?? 0;
                vm.TaxType = updateData.TaxType;
                vm.Type = updateData.Type;
                vm.Atti = updateData.Atti;
                vm.LastIn = !String.IsNullOrEmpty(updateData.LastIn) ? DateTime.ParseExact(updateData.LastIn, "yyyyMMdd", null).ToString("yyyy-MM-dd") : "";
                vm.LastCost = updateData.LastCost ?? 0;
                vm.LastDiscount = updateData.LastDiscount;
                vm.LastOut = !String.IsNullOrEmpty(updateData.LastOut) ? DateTime.ParseExact(updateData.LastOut, "yyyyMMdd", null).ToString("yyyy-MM-dd") : "";
                vm.SafeQty = updateData.SafeQty ?? 0;
                vm.StQty = updateData.StQty ?? 0;
                vm.Location = updateData.Location;
                vm.TranPara1 = updateData.TranPara1 ?? 0;
                vm.TranPara2 = updateData.TranPara2 ?? 0;
                vm.TranPara3 = updateData.TranPara3 ?? 0;
                vm.SPrice1 = updateData.SPrice1 ?? 0;
                vm.SPrice2 = updateData.SPrice2 ?? 0;
                vm.SPrice3 = updateData.SPrice3 ?? 0;
                vm.TUnit1 = updateData.TUnit1;
                vm.TUnit2 = updateData.TUnit2;
                vm.TUnit3 = updateData.TUnit3;
                vm.YnCount = updateData.YnCount;
                vm.PackQty = updateData.PackQty ?? 0;
                vm.InitQty1 = updateData.InitQty1 ?? 0;
                vm.InitQty2 = updateData.InitQty2 ?? 0;
                vm.InitCost1 = updateData.InitCost1 ?? 0;
                vm.InitCost2 = updateData.InitCost2 ?? 0;
                vm.InitCost3 = updateData.InitCost3 ?? 0;
                vm.InitCost4 = updateData.InitCost4 ?? 0;
                vm.InitCost5 = updateData.InitCost5 ?? 0;
                vm.CompCost = updateData.CompCost ?? 0;
                vm.L = updateData.L ?? 0;
                vm.W = updateData.W ?? 0;
                vm.H = updateData.H ?? 0;
                vm.Cuft = updateData.Cuft ?? 0;
                vm.LastModidate = DateTime.ParseExact(updateData.LastModidate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.LastModiuser = updateData.LastModiuser;
                vm.ST_PS = updateData.ST_PS;
                vm.IsOpen = (bool)updateData.IsOpen;
                vm.IsShow = (bool)updateData.IsShow;
                vm.UnitPara1 = updateData.UnitPara1;
                vm.UnitPara2 = updateData.UnitPara2;
                vm.UnitPara3 = updateData.UnitPara3;
                vm.SPrice4 = updateData.SPrice4 ?? 0;
                vm.SPrice5 = updateData.SPrice5 ?? 0;
                vm.SPrice6 = updateData.SPrice6 ?? 0;
                vm.SPrice7 = updateData.SPrice7 ?? 0;
                vm.SPrice8 = updateData.SPrice8 ?? 0;
                vm.DefaultPrice1 = updateData.DefaultPrice1 ?? 0;
                vm.DefaultPrice2 = updateData.DefaultPrice2 ?? 0;
                vm.DefaultPrice3 = updateData.DefaultPrice3 ?? 0;
                vm.DefaultPrice4 = updateData.DefaultPrice4 ?? 0;
                vm.DefaultPrice5 = updateData.DefaultPrice5 ?? 0;
                vm.IsReturn = updateData.IsReturn == null ? "1" : ((bool)updateData.IsReturn ? "1" : "2");
                vm.ImagePath = !String.IsNullOrEmpty(updateData.ImagePath) ? $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/image/stock10/{updateData.ImagePath}"
                            : $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/image/imgfile.png";

                #region SelectList

                //產品分類
                ViewBag.TypeList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "產品分類").ToList(), "Phase", "Phase", string.Empty);

                //庫存異動
                var YnCountList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.YnCountList = new SelectList(YnCountList, "Value", "Text", string.Empty);

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

                // 可否退貨
                var ReturnList123 = new List<SelectListItem>()
                {
                    new SelectListItem {Text="可退貨" , Value="1"},
                    new SelectListItem {Text="不可退貨" , Value="2"}
                };
                ViewBag.ReturnList123 = new SelectList(ReturnList123, "Value", "Text", string.Empty);

                #endregion

                #region 公司毛利率

                //公司毛利率
                double? CGPM1 = 0;
                double? CGPM2 = 0;
                double? CGPM3 = 0;
                double? CGPM4 = 0;
                double? CGPM5 = 0;
                double? CGPM6 = 0;
                double? CGPM7 = 0;
                double? CGPM8 = 0;

                if (!string.IsNullOrEmpty(vm.CompCost.ToString()))
                {

                    CGPM1 = (((vm.Price1 ?? 0) - (vm.CompCost ?? 0)) / vm.Price1 ?? 0 * 100) * 100;

                    if (CGPM1 < 0)
                    {
                        CGPM1 = 0;
                    }
                    else if (CGPM1 > 0)
                    {
                        CGPM1 = (double?)Math.Round(Convert.ToDecimal(CGPM1), 2);
                    }
                    else
                    {
                        CGPM1 = 0;
                    }

                    CGPM2 = (((vm.Price2 ?? 0) - (vm.CompCost ?? 0)) / vm.Price2 ?? 0 * 100) * 100;

                    if (CGPM2 < 0)
                    {
                        CGPM2 = 0;
                    }
                    else if (CGPM2 > 0)
                    {
                        CGPM2 = (double?)Math.Round(Convert.ToDecimal(CGPM2), 2);
                    }
                    else
                    {
                        CGPM2 = 0;
                    }

                    CGPM3 = (((vm.Price3 ?? 0) - (vm.CompCost ?? 0)) / vm.Price3 ?? 0 * 100) * 100;

                    if (CGPM3 < 0)
                    {
                        CGPM3 = 0;
                    }
                    else if (CGPM3 > 0)
                    {
                        CGPM3 = (double?)Math.Round(Convert.ToDecimal(CGPM3), 2);
                    }
                    else
                    {
                        CGPM3 = 0;
                    }

                    CGPM4 = (((vm.DefaultPrice1 ?? 0) - (vm.CompCost ?? 0)) / vm.DefaultPrice1 ?? 0 * 100) * 100;

                    if (CGPM4 < 0)
                    {
                        CGPM4 = 0;
                    }
                    else if (CGPM4 > 0)
                    {
                        CGPM4 = (double?)Math.Round(Convert.ToDecimal(CGPM4), 2);
                    }
                    else
                    {
                        CGPM4 = 0;
                    }

                    CGPM5 = (((vm.DefaultPrice2 ?? 0) - (vm.CompCost ?? 0)) / vm.DefaultPrice2 ?? 0 * 100) * 100;

                    if (CGPM5 < 0)
                    {
                        CGPM5 = 0;
                    }
                    else if (CGPM5 > 0)
                    {
                        CGPM5 = (double?)Math.Round(Convert.ToDecimal(CGPM5), 2);
                    }
                    else
                    {
                        CGPM5 = 0;
                    }

                    CGPM6 = (((vm.DefaultPrice3 ?? 0) - (vm.CompCost ?? 0)) / vm.DefaultPrice3 ?? 0 * 100) * 100;

                    if (CGPM6 < 0)
                    {
                        CGPM6 = 0;
                    }
                    else if (CGPM6 > 0)
                    {
                        CGPM6 = (double?)Math.Round(Convert.ToDecimal(CGPM6), 2);
                    }
                    else
                    {
                        CGPM6 = 0;
                    }

                    CGPM7 = (((vm.DefaultPrice4 ?? 0) - (vm.CompCost ?? 0)) / vm.DefaultPrice4 ?? 0 * 100) * 100;

                    if (CGPM7 < 0)
                    {
                        CGPM7 = 0;
                    }
                    else if (CGPM7 > 0)
                    {
                        CGPM7 = (double?)Math.Round(Convert.ToDecimal(CGPM7), 2);
                    }
                    else
                    {
                        CGPM7 = 0;
                    }

                    CGPM8 = (((vm.DefaultPrice5 ?? 0) - (vm.CompCost ?? 0)) / vm.DefaultPrice5 ?? 0 * 100) * 100;

                    if (CGPM8 < 0)
                    {
                        CGPM8 = 0;
                    }
                    else if (CGPM8 > 0)
                    {
                        CGPM8 = (double?)Math.Round(Convert.ToDecimal(CGPM8), 2);
                    }
                    else
                    {
                        CGPM8 = 0;
                    }
                }

                ViewBag.CGPM1 = CGPM1;
                ViewBag.CGPM2 = CGPM2;
                ViewBag.CGPM3 = CGPM3;
                ViewBag.CGPM4 = CGPM4;
                ViewBag.CGPM5 = CGPM5;
                ViewBag.CGPM6 = CGPM6;
                ViewBag.CGPM7 = CGPM7;
                ViewBag.CGPM8 = CGPM8;


                #endregion 公司毛利率

                #region 客戶毛利率

                //客戶毛利率
                double? GPM1 = 0;
                double? GPM2 = 0;
                double? GPM3 = 0;
                double? GPM4 = 0;
                double? GPM5 = 0;
                double? GPM6 = 0;
                double? GPM7 = 0;
                double? GPM8 = 0;

                if (!string.IsNullOrEmpty(vm.CompCost.ToString()))
                {

                    GPM1 = (((vm.SPrice1 ?? 0) - (vm.Price1 ?? 0)) / vm.SPrice1 ?? 0 * 100) * 100;

                    if (GPM1 < 0)
                    {
                        GPM1 = 0;
                    }
                    else if (GPM1 > 0)
                    {
                        GPM1 = (double?)Math.Round(Convert.ToDecimal(GPM1), 2);
                    }
                    else
                    {
                        GPM1 = 0;
                    }

                    GPM2 = (((vm.SPrice2 ?? 0) - (vm.Price2 ?? 0)) / vm.SPrice2 ?? 0 * 100) * 100;

                    if (GPM2 < 0)
                    {
                        GPM2 = 0;
                    }
                    else if (GPM2 > 0)
                    {
                        GPM2 = (double?)Math.Round(Convert.ToDecimal(GPM2), 2);
                    }
                    else
                    {
                        GPM2 = 0;
                    }

                    GPM3 = (((vm.SPrice3 ?? 0) - (vm.Price3 ?? 0)) / vm.SPrice3 ?? 0 * 100) * 100;

                    if (GPM3 < 0)
                    {
                        GPM3 = 0;
                    }
                    else if (GPM3 > 0)
                    {
                        GPM3 = (double?)Math.Round(Convert.ToDecimal(GPM3), 2);
                    }
                    else
                    {
                        GPM3 = 0;
                    }

                    GPM4 = (((vm.SPrice4 ?? 0) - (vm.DefaultPrice1 ?? 0)) / vm.SPrice4 ?? 0 * 100) * 100;

                    if (GPM4 < 0)
                    {
                        GPM4 = 0;
                    }
                    else if (GPM4 > 0)
                    {
                        GPM4 = (double?)Math.Round(Convert.ToDecimal(GPM4), 2);
                    }
                    else
                    {
                        GPM4 = 0;
                    }

                    GPM5 = (((vm.SPrice5 ?? 0) - (vm.DefaultPrice2 ?? 0)) / vm.SPrice5 ?? 0 * 100) * 100;

                    if (GPM5 < 0)
                    {
                        GPM5 = 0;
                    }
                    else if (GPM5 > 0)
                    {
                        GPM5 = (double?)Math.Round(Convert.ToDecimal(GPM5), 2);
                    }
                    else
                    {
                        GPM5 = 0;
                    }

                    GPM6 = (((vm.SPrice6 ?? 0) - (vm.DefaultPrice3 ?? 0)) / vm.SPrice6 ?? 0 * 100) * 100;

                    if (GPM6 < 0)
                    {
                        GPM6 = 0;
                    }
                    else if (GPM6 > 0)
                    {
                        GPM6 = (double?)Math.Round(Convert.ToDecimal(GPM6), 2);
                    }
                    else
                    {
                        GPM6 = 0;
                    }

                    GPM7 = (((vm.SPrice7 ?? 0) - (vm.DefaultPrice4 ?? 0)) / vm.SPrice7 ?? 0 * 100) * 100;

                    if (GPM7 < 0)
                    {
                        GPM7 = 0;
                    }
                    else if (GPM7 > 0)
                    {
                        GPM7 = (double?)Math.Round(Convert.ToDecimal(GPM7), 2);
                    }
                    else
                    {
                        GPM7 = 0;
                    }

                    GPM8 = (((vm.SPrice8 ?? 0) - (vm.DefaultPrice5 ?? 0)) / vm.SPrice8 ?? 0 * 100) * 100;

                    if (GPM8 < 0)
                    {
                        GPM8 = 0;
                    }
                    else if (GPM8 > 0)
                    {
                        GPM8 = (double?)Math.Round(Convert.ToDecimal(GPM8), 2);
                    }
                    else
                    {
                        GPM8 = 0;
                    }
                }

                ViewBag.GPM1 = GPM1;
                ViewBag.GPM2 = GPM2;
                ViewBag.GPM3 = GPM3;
                ViewBag.GPM4 = GPM4;
                ViewBag.GPM5 = GPM5;
                ViewBag.GPM6 = GPM6;
                ViewBag.GPM7 = GPM7;
                ViewBag.GPM8 = GPM8;

                #endregion 客戶毛利率

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        /// <summary>
        /// 編輯產品資料
        /// </summary>
        /// <param name="vm">StockEditViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockEditViewModel vm)
        {
            vm.IsTrue = false;
            try
            {

                if (vm != null)
                {
                    var stockData = _context.Stock10.Find(vm.PartNo);

                    if(stockData != null)
                    {
                        string imagePath = stockData.ImagePath ?? "";
                        string imageName = stockData.ImageName ?? "";

                        // 更新圖片
                        if (vm.Image != null && vm.Image.Length > 0)
                        {                        
                            // 先儲存舊檔名
                            string OldFile = imagePath;
                            // 儲存圖片
                            imagePath = await _ajax.UploadFile(vm.Image, "image/stock10");
                            imageName = vm.Image.FileName.ToString();
                            // 刪除舊圖片
                            _ajax.DeleteFile(OldFile,"image/stock10");
                        }

                        stockData.Barcode = vm.Barcode ?? "";
                        stockData.Spec = vm.Spec ?? "";
                        stockData.Unit = vm.Unit ?? "";
                        stockData.Cost1 = vm.Cost1 ?? 0;
                        stockData.Cost2 = vm.Cost2 ?? 0;
                        stockData.Cost3 = vm.Cost3 ?? 0;
                        stockData.SalesCost = vm.SalesCost ?? 0;
                        stockData.Price1 = vm.Price1 ?? 0;
                        stockData.Price2 = vm.Price2 ?? 0;
                        stockData.Price3 = vm.Price3 ?? 0;
                        stockData.TaxType = vm.TaxType ?? "";
                        stockData.Type = vm.Type ?? "";
                        stockData.Atti = vm.Atti ?? "";
                        stockData.LastIn = !String.IsNullOrEmpty(vm.LastIn) ? vm.LastIn.Replace("-", "") : "";
                        stockData.LastCost = vm.LastCost ?? 0;
                        stockData.LastDiscount = vm.LastDiscount ?? 0;
                        stockData.LastOut = !String.IsNullOrEmpty(vm.LastOut) ? vm.LastOut.Replace("-", "") : "";
                        stockData.SafeQty = vm.SafeQty ?? 0;
                        stockData.StQty = vm.StQty ?? 0;
                        stockData.Location = vm.Location ?? "";
                        stockData.TranPara1 = vm.TranPara1 ?? 0;
                        stockData.TranPara2 = vm.TranPara2 ?? 0;
                        stockData.TranPara3 = vm.TranPara3 ?? 0;
                        stockData.SPrice1 = vm.SPrice1 ?? 0;
                        stockData.SPrice2 = vm.SPrice2 ?? 0;
                        stockData.SPrice3 = vm.SPrice3 ?? 0;
                        stockData.TUnit1 = vm.TUnit1 ?? "";
                        stockData.TUnit2 = vm.TUnit2 ?? "";
                        stockData.TUnit3 = vm.TUnit3 ?? "";
                        stockData.YnCount = vm.YnCount ?? "";
                        stockData.PackQty = vm.PackQty ?? 0;
                        stockData.InitQty1 = vm.InitQty1 ?? 0;
                        stockData.InitQty2 = vm.InitQty2 ?? 0;
                        stockData.InitCost1 = vm.InitCost1 ?? 0;
                        stockData.InitCost2 = vm.InitCost2 ?? 0;
                        stockData.InitCost3 = vm.InitCost3 ?? 0;
                        stockData.InitCost4 = vm.InitCost4 ?? 0;
                        stockData.InitCost5 = vm.InitCost5 ?? 0;
                        stockData.CompCost = vm.CompCost ?? 0;
                        stockData.L = vm.L ?? 0;
                        stockData.W = vm.W ?? 0;
                        stockData.H = vm.H ?? 0;
                        stockData.Cuft = vm.Cuft ?? 0;
                        stockData.LastModidate = DateTime.Now.ToString("yyyyMMdd") ?? "";
                        stockData.LastModiuser = HttpContext.Session.GetString("UserAc") ?? "";
                        stockData.ST_PS = vm.ST_PS ?? "";
                        stockData.IsOpen = vm.IsShow; // 預設同isshow //IsOpen = vm.IsOpen;
                        stockData.IsShow = vm.IsShow;
                        stockData.UnitPara1 = vm.UnitPara1 ?? "";
                        stockData.UnitPara2 = vm.UnitPara2 ?? "";
                        stockData.UnitPara3 = vm.UnitPara3 ?? "";
                        stockData.SPrice4 = vm.SPrice4 ?? 0;
                        stockData.SPrice5 = vm.SPrice5 ?? 0;
                        stockData.SPrice6 = vm.SPrice6 ?? 0;
                        stockData.SPrice7 = vm.SPrice7 ?? 0;
                        stockData.SPrice8 = vm.SPrice8 ?? 0;
                        stockData.DefaultPrice1 = vm.DefaultPrice1 ?? 0;
                        stockData.DefaultPrice2 = vm.DefaultPrice2 ?? 0;
                        stockData.DefaultPrice3 = vm.DefaultPrice3 ?? 0;
                        stockData.DefaultPrice4 = vm.DefaultPrice4 ?? 0;
                        stockData.DefaultPrice5 = vm.DefaultPrice5 ?? 0;
                        stockData.ImageName = imageName ?? "";
                        stockData.ImagePath = imagePath ?? "";

                        stockData.IsReturn = !String.IsNullOrEmpty(vm.IsReturn) ? vm.IsReturn == "1" : true;

                        _context.Stock10.Update(stockData);
                    }


                    await _context.SaveChangesAsync();

                    vm.IsTrue = true;
                }
                else
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "編輯失敗!";

                    return View(vm);
                }

                return RedirectToAction("Index", vm);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 刪除產品資料
        /// </summary>
        /// <param name="StockId">產品代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string PartNo)
        {
            try
            {
                if (string.IsNullOrEmpty(PartNo))
                {
                    return NotFound();
                }

                var deleteData = await _context.Stock10
                    .Where(x => x.PartNo == PartNo).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.Stock10.Remove(deleteData);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Export()
        {
            #region SelectList

            //產品清單資料
            var stockList = from stock10 in _context.Stock10
                            select new
                            {
                                PartNo = stock10.PartNo,
                                Spec = stock10.PartNo + string.Empty + stock10.Spec
                            };

            //產品清單
            ViewBag.StockList = new SelectList(stockList.ToList(), "PartNo", "Spec", string.Empty);

            //產品分類
            ViewBag.TypeList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "產品分類").ToList(), "Phase", "Phase", string.Empty);

            #endregion

            //var stockListData = await _context.Stock10.Select(x => new StockList
            //{
            //    PartNo = x.PartNo,
            //    Barcode = x.Barcode,
            //    Spec = x.Spec,
            //    Unit = x.Unit,
            //    Cost1 = x.Cost1 ?? 0,
            //    Cost2 = x.Cost2 ?? 0,
            //    Cost3 = x.Cost3 ?? 0,
            //    SalesCost = x.SalesCost ?? 0,
            //    Price1 = x.Price1 ?? 0,
            //    Price2 = x.Price2 ?? 0,
            //    Price3 = x.Price3 ?? 0,
            //    TaxType = x.TaxType,
            //    Type = x.Type,
            //    Atti = x.Atti,
            //    //LastIn = string.IsNullOrEmpty(x.LastIn) ? x.LastIn : DateTime.ParseExact(x.LastIn, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
            //    LastIn = x.LastIn,
            //    LastCost = x.LastCost ?? 0,
            //    LastDiscount = x.LastDiscount ?? 0,
            //    //LastOut = string.IsNullOrEmpty(x.LastOut) ? x.LastOut : DateTime.ParseExact(x.LastOut, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
            //    LastOut = x.LastOut,
            //    SafeQty = x.SafeQty ?? 0,
            //    StQty = x.StQty ?? 0,
            //    Location = x.Location,
            //    TranPara1 = x.TranPara1 ?? 0,
            //    TranPara2 = x.TranPara2 ?? 0,
            //    TranPara3 = x.TranPara3 ?? 0,
            //    SPrice1 = x.SPrice1 ?? 0,
            //    SPrice2 = x.SPrice2 ?? 0,
            //    SPrice3 = x.SPrice3 ?? 0,
            //    TUnit1 = x.TUnit1,
            //    TUnit2 = x.TUnit2,
            //    TUnit3 = x.TUnit3,
            //    YnCount = x.YnCount,
            //    PackQty = x.PackQty ?? 0,
            //    InitQty1 = x.InitQty1 ?? 0,
            //    InitQty2 = x.InitQty2 ?? 0,
            //    InitCost1 = x.InitCost1 ?? 0,
            //    InitCost2 = x.InitCost2 ?? 0,
            //    InitCost3 = x.InitCost3 ?? 0,
            //    InitCost4 = x.InitCost4 ?? 0,
            //    InitCost5 = x.InitCost5 ?? 0,
            //    CompCost = x.CompCost ?? 0,
            //    L = x.L ?? 0,
            //    W = x.W ?? 0,
            //    H = x.H ?? 0,
            //    Cuft = x.Cuft ?? 0,
            //    //LastModidate = DateTime.ParseExact(x.LastModidate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
            //    LastModidate = x.LastModidate,
            //    LastModiuser = x.LastModiuser,
            //    ST_PS = x.ST_PS,
            //    IsOpen = x.IsOpen,
            //    IsShow = x.IsShow,
            //    UnitPara1 = x.UnitPara1,
            //    UnitPara2 = x.UnitPara2,
            //    UnitPara3 = x.UnitPara3,
            //    SPrice4 = x.SPrice4 ?? 0,
            //    SPrice5 = x.SPrice5 ?? 0,
            //    SPrice6 = x.SPrice6 ?? 0,
            //    SPrice7 = x.SPrice7 ?? 0,
            //    SPrice8 = x.SPrice8 ?? 0,
            //    DefaultPrice1 = x.DefaultPrice1 ?? 0,
            //    DefaultPrice2 = x.DefaultPrice2 ?? 0,
            //    DefaultPrice3 = x.DefaultPrice3 ?? 0,
            //    DefaultPrice4 = x.DefaultPrice4 ?? 0,
            //    DefaultPrice5 = x.DefaultPrice5 ?? 0,
            //}).ToListAsync();

            //if (stockListData == null)
            //{
            //    return View(new StockExportViewModel());
            //}

            ////第一筆
            //var firstData = _context.Stock10.OrderBy(x => x.PartNo).First().PartNo;

            ////最後一筆
            //var lastData = _context.Stock10.OrderByDescending(x => x.PartNo).First().PartNo;

            //if (string.IsNullOrEmpty(StartPartNo) && string.IsNullOrEmpty(EndPartNo))
            //{
            //    stockListData = await stockListData
            //        .Where(x => x.PartNo.CompareTo(firstData) >= 0 && x.PartNo.CompareTo(lastData) <= 0).ToListAsync();
            //}
            //else if (string.IsNullOrEmpty(StartPartNo))
            //{
            //    stockListData = await stockListData
            //        .Where(x => x.PartNo.CompareTo(firstData) >= 0 && x.PartNo.CompareTo(EndPartNo) <= 0).ToListAsync();
            //}
            //else if (string.IsNullOrEmpty(EndPartNo))
            //{
            //    stockListData = await stockListData
            //        .Where(x => x.PartNo.CompareTo(StartPartNo) >= 0 && x.PartNo.CompareTo(lastData) <= 0).ToListAsync();
            //}
            //else
            //{
            //    stockListData = await stockListData
            //        .Where(x => x.PartNo.CompareTo(StartPartNo) >= 0 && x.PartNo.CompareTo(EndPartNo) <= 0).ToListAsync();
            //}

            StockExportViewModel vm = new StockExportViewModel();

            //vm.stockList = stockListData;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Export(StockExportViewModel vm)
        {
            //TODO 標籤尚未作業
            try
            {
                if (vm.IsBarcode == "0")
                {
                    #region getData

                    //找出產品所需資料
                    var stockListData = await _context.Stock10.ToListAsync();

                    //第一筆
                    var firstData = _context.Stock10.OrderBy(x => x.PartNo).First().PartNo;

                    //最後一筆
                    var lastData = _context.Stock10.OrderByDescending(x => x.PartNo).First().PartNo;

                    if (string.IsNullOrEmpty(vm.StartPartNo) && string.IsNullOrEmpty(vm.EndPartNo))
                    {
                        stockListData = await stockListData
                            .Where(x => x.PartNo.CompareTo(firstData) >= 0 && x.PartNo.CompareTo(lastData) <= 0).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(vm.StartPartNo))
                    {
                        stockListData = await stockListData
                            .Where(x => x.PartNo.CompareTo(firstData) >= 0 && x.PartNo.CompareTo(vm.EndPartNo) <= 0).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(vm.EndPartNo))
                    {
                        stockListData = await stockListData
                            .Where(x => x.PartNo.CompareTo(vm.StartPartNo) >= 0 && x.PartNo.CompareTo(lastData) <= 0).ToListAsync();
                    }
                    else
                    {
                        stockListData = await stockListData
                            .Where(x => x.PartNo.CompareTo(vm.StartPartNo) >= 0 && x.PartNo.CompareTo(vm.EndPartNo) <= 0).ToListAsync();
                    }

                    if (!string.IsNullOrEmpty(vm.StockType))
                    {
                        stockListData = await stockListData.Where(x => x.Type == vm.StockType).ToListAsync();
                    }

                    if (!string.IsNullOrEmpty(vm.ReportOrder))
                    {
                        switch (vm.ReportOrder)
                        {
                            case "1":
                                stockListData = await stockListData.OrderBy(x => x.PartNo).ToListAsync();
                                break;
                            case "2":
                                stockListData = await stockListData.OrderBy(x => x.Barcode).ToListAsync();
                                break;
                            case "3":
                                stockListData = await stockListData
                                    .OrderBy(x => x.Type)
                                    .ThenBy(x => x.PartNo).ToListAsync();
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion getData

                    //建立Excel
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
                    ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

                    //新增標題列
                    sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
                                        //sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
                    sheet.GetRow(0).CreateCell(0).SetCellValue("產品分類");
                    sheet.GetRow(0).CreateCell(1).SetCellValue("產品編號");
                    sheet.GetRow(0).CreateCell(2).SetCellValue("商品條碼");
                    sheet.GetRow(0).CreateCell(3).SetCellValue("品名規格");
                    sheet.GetRow(0).CreateCell(4).SetCellValue("庫存量");
                    sheet.GetRow(0).CreateCell(5).SetCellValue("單位");
                    if (!string.IsNullOrEmpty(vm.ReportFormat))
                    {
                        switch (vm.ReportFormat)
                        {
                            //庫存表
                            case "1":
                                sheet.GetRow(0).CreateCell(6).SetCellValue("儲位");
                                sheet.GetRow(0).CreateCell(7).SetCellValue("稅別");
                                break;
                            //庫存成本表
                            case "2":
                                sheet.GetRow(0).CreateCell(6).SetCellValue("成本");
                                sheet.GetRow(0).CreateCell(7).SetCellValue("庫存金額");
                                break;
                            default:
                                break;
                        }
                    }

                    //填入資料
                    int rowIndex = 1;
                    for (int row = 0; row < stockListData.Count(); row++)
                    {
                        sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(stockListData[row].Type);
                        sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(stockListData[row].PartNo);
                        sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(stockListData[row].Barcode);
                        sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(stockListData[row].Spec);
                        sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(stockListData[row].StQty ?? 0);
                        sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(stockListData[row].Unit);
                        switch (vm.ReportFormat)
                        {
                            case "1":
                                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(stockListData[row].Location);
                                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(stockListData[row].TaxType);
                                break;
                            case "2":
                                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(stockListData[row].CompCost ?? 0);
                                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(stockListData[row].StQty ?? 0 * stockListData[row].CompCost ?? 0);
                                break;
                            default:
                                break;
                        }

                        rowIndex++;
                    }

                    var excelDatas = new MemoryStream();
                    hssfworkbook.Write(excelDatas);

                    return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"產品庫存明細表.xls"));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 變更編號
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> ChangePartNo(string PartNo , string NewPartNo)
        {
            string result = "";
            if (!String.IsNullOrEmpty(PartNo) && !String.IsNullOrEmpty(NewPartNo))
            {
                var NewProduct = await _context.Stock10.FindAsync(NewPartNo);
                if(NewProduct != null) { return "產品編號已存在，請重新輸入"; }

                //var Product = await _context.Stock10.Where(x => x.PartNo == PartNo).FirstOrDefaultAsync();
                var Product = await _context.Stock10.FindAsync(PartNo);

                if (Product != null)
                {
                    try
                    {
                        _context.Stock10.Remove(Product);
                        await _context.SaveChangesAsync();

                        Product.PartNo = NewPartNo;
                        _context.Stock10.Add(Product);
                        await _context.SaveChangesAsync();

                        return "success";
                    }
                    catch(Exception e)
                    {
                        return "發生錯誤";
                    }

                }
                else
                {
                    return "找無資料";
                }
            }
            return result;
        }

        /// <summary>
        /// 產品分類畫面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> StockType(string type)
        {
            var StockType = await _context.StockTypes.ToListAsync();

            if (!String.IsNullOrEmpty(type))
            {
                StockType = await StockType.Where(x => (x.TYPE_NAME ?? "").Contains(type)).ToListAsync();
            }

            StockType = await StockType.OrderBy(x => x.TYPE_ORDER == null).ThenBy(x => x.TYPE_ORDER).ToListAsync();

            ViewBag.type = type ?? "";

            return View(StockType);
        }

        [HttpPost]
        public async Task<IActionResult> AddType(string AddTypeName)
        {
            try
            {
                var insertData = new STOCK_TYPE
                {
                    TYPE_ISOPEN = true,
                    TYPE_NAME = AddTypeName,
                    TYPE_ORDER = null,
                    TYPE_STATE = "",
                    TYPE_TIME = DateTime.Now
                };

                _context.StockTypes.Add(insertData);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch(Exception e)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditType(int TypeNo , string TypeName , bool TypeStatus)
        {
            try
            {
                if (TypeNo > 0 && !String.IsNullOrEmpty(TypeName))
                {
                    var type = await _context.StockTypes.FindAsync(TypeNo);
                    type.TYPE_NAME = TypeName ?? "";
                    type.TYPE_ISOPEN = TypeStatus;
                    await _context.SaveChangesAsync();
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch(Exception e)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DelType(int TypeNo)
        {
            try
            {
                if (TypeNo > 0)
                {
                    var Type = await _context.StockTypes.FindAsync(TypeNo);
                    if(Type != null)
                    {
                        _context.StockTypes.Remove(Type);
                        await _context.SaveChangesAsync();
                    }

                    return Json(1);
                }
                else
                {
                    return Json(0);
                }                
            }
            catch(Exception e)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OrderType(int[] TypeNos)
        {
            try
            {
                if(TypeNos.Length > 0)
                {
                    for(var i = 0; i < TypeNos.Length; i++)
                    {
                        var Type = await _context.StockTypes.FindAsync(TypeNos[i]);
                        Type.TYPE_ORDER = i + 1;
                    }
                    await _context.SaveChangesAsync();                    
                }
                return Json(1);
            }
            catch(Exception e)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePriceName(int[] PT_ID , string[] PT_TEXT , string[] PT_VALUE)
        {
            if(PT_ID.Length > 0)
            {
                for(var i = 0; i < PT_ID.Length; i++)
                {
                    if (PT_ID[i] > 0)
                    {
                        var PriceType = _context.PriceType.Find(PT_ID[i]);
                        if(PriceType != null)
                        {
                            PriceType.PT_TEXT = PT_TEXT[i] ?? "";
                            PriceType.PT_VALUE = PT_VALUE[i] ?? "";

                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }

}
