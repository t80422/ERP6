using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using Microsoft.AspNetCore.Http;
using ERP6.ViewModels.Sales;
using X.PagedList;

namespace ERP6.Controllers
{
    public class SalesController : Controller
    {
        private readonly EEPEF01Context _context;

        public SalesController(EEPEF01Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Area()
        {
            var area = _context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList();
            ViewBag.Area = area;

            return View(await _context.Stock10.Take(30).ToListAsync());
        }

        public async Task<IActionResult> Order(OrderViewModel vm, int? page = 1)
        {

            #region new

            var orderData = _context.Out10
              .Where(x => x.CoNo == vm.CustomerId)
              .OrderByDescending(x => x.OutDate);

            if (orderData == null)
            {
                return NotFound();
            }

            var data = new OrderViewModel
            {
                Shipped = orderData.Where(x => x.OutType == "1").Count(),
                NotShipped = orderData.Where(x => x.OutType == "2").Count(),
                //OrderStatus 預設給4(全部訂單)
                OrderState = string.IsNullOrEmpty(vm.OrderState) ? "4" : vm.OrderState,
                Area = vm.Area,
                CustomerId = vm.CustomerId,
            };

            #region selected

            ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", data.Area);
            ViewBag.Client = new SelectList(_context.Cstm10.Where(x => x.AreaNo == vm.Area).ToList(), "CoNo", "Company", data.CustomerId);

            var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="全部訂單", Value="4" },
                   new SelectListItem {Text="未出貨訂單", Value="2" },
                   new SelectListItem {Text="已出貨訂單", Value="1" },
                };

            ViewBag.StateSList = new SelectList(orderStatsList, "Value", "Text", data.OrderState);

            //業務人員
            ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", data.Business);

            //司機人員
            ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", data.Driver);

            #endregion

            //沒有資料時退回選擇區域客戶畫面
            if (string.IsNullOrEmpty(vm.CustomerId))
            {
                return RedirectToAction("Area", "Sales");
            }

            var orderList = orderData.Select(x => new OrderList
            {
                OrderNo = x.OutNo,
                OrderState = x.OutType,
                OrderTime = DateTime.ParseExact(x.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
                OrderAmount = x.Total,
                KeyInDate = DateTime.ParseExact(x.KeyinDate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
                Business = _context.Pepo10.Where(y => y.PeNo == x.PeNo).FirstOrDefault().Name,
                Driver = _context.Pepo10.Where(y => y.PeNo == x.DriveNo).FirstOrDefault().Name,
            }).ToList();

            //搜尋條件-狀態
            if (!string.IsNullOrEmpty(data.OrderState) && data.OrderState != "4")
            {
                orderList = orderList.Where(x => x.OrderState == vm.OrderState).ToList();
            }

            //搜尋條件-訂單時間
            if (!string.IsNullOrEmpty(vm.OrderTime))
            {
                orderList = orderList.Where(x => x.OrderTime.Contains(vm.OrderTime)).ToList();
            }

            //搜尋條件-訂單編號
            if (!string.IsNullOrEmpty(vm.OrderNo))
            {
                orderList = orderList.Where(x => x.OrderNo == vm.OrderNo).ToList();
            }

            //搜尋條件-建檔日期
            if (!string.IsNullOrEmpty(vm.KeyInDate))
            {
                orderList = orderList.Where(x => x.KeyInDate.Contains(vm.KeyInDate)).ToList();
            }

            //搜尋條件-業務人員
            if (!string.IsNullOrEmpty(vm.Business))
            {
                orderList = orderList.Where(x => x.Business.Contains(vm.Business)).ToList();
            }

            //搜尋條件-司機人員
            if (!string.IsNullOrEmpty(vm.Driver))
            {
                orderList = orderList.Where(x => x.Driver.Contains(vm.Driver)).ToList();
            }

            data.orderList = await orderList.ToPagedListAsync((int)page, 10);

            if (orderData == null)
            {
                return View(vm);
            }

            #endregion

            return View(data);
        }

        [HttpGet]
        // GET: Sales
        //public async Task<IActionResult> OrderEdit(string id, string SAREA, string SCLIENT)
        public async Task<IActionResult> OrderEdit(string OutNo, string Area, string CustomerId)
        {
            #region new

            var vm = new OrderEditViewModel();

            vm.OutNo = OutNo;
            vm.Area = Area;
            vm.CustomerId = CustomerId;

            //取得公司資料
            //var companyData = await _context.Cstm10.Where(x => x.CoNo == CustomerId).FirstOrDefaultAsync();

            //if (companyData == null)
            //{
            //    return NotFound();
            //}

            ////取得業務人員資料
            //if (!string.IsNullOrEmpty(companyData.PeNo))
            //{
            //    vm.Business = _context.Pepo10
            //        .Where(x => x.PeNo == companyData.PeNo).FirstOrDefault().Name;
            //}

            ////取得司機人員資料
            //if (!string.IsNullOrEmpty(companyData.DriveNo))
            //{
            //    vm.Driver = _context.Pepo10
            //    .Where(x => x.PeNo == companyData.DriveNo).FirstOrDefault().Name;
            //}

            //先取分類列表
            var title = _context.Stock10.Where(x => x.IsOpen == true).Select(x => new StockTitle
            {
                Name = x.Type
            }).Distinct().OrderBy(x => x.Name).ToList();

            vm.StockTitleList = title;

            //取出出貨單主表
            var outOrderMain = _context.Out10.Where(x => x.OutNo == vm.OutNo).FirstOrDefault();

            //TODO 訂單紀錄(單筆)By Alex
            #region 訂單紀錄(單筆)

            var outOrderDetailData = _context.Out20
                .Where(x => x.OutNo == (vm != null ? vm.OutNo : string.Empty)).ToList();

            vm.OutDetail = new List<OutOrderDetail>();

            foreach (var item in outOrderDetailData)
            {
                var OutDetail = new OutOrderDetail();

                OutDetail.OutDetailNo = item.OutNo;
                OutDetail.PartNo = item.PartNo;
                OutDetail.Sale = false;
                OutDetail.Stats = false;
                //品名
                OutDetail.Name = _context.Stock10.Where(x => x.PartNo == item.PartNo).Select(x => x.Spec).ToString();
                OutDetail.Qty = item.Qty;
                OutDetail.Memo = item.Memo;

                vm.OutDetail.Add(OutDetail);
            }

            #endregion 訂單紀錄(單筆)

            #region 新增/修改

            var promotionData = new Stock20();
            var checkTime = false;

            if (string.IsNullOrEmpty(vm.OutNo)) //新增
            {
                ViewBag.FUNNAME = "新增訂單";

                vm.OrderTime = DateTime.Now.ToString("yyyy-MM-dd");

                //檢查是否有促銷資料
                promotionData = _context.Stock20
                        .Where(x => x.CoNo == CustomerId)
                        .OrderByDescending(x => x.SpNo).FirstOrDefault();

                if (promotionData != null)
                {
                    //如果有在比對日期是否在期限內
                    checkTime = (int.Parse(promotionData.Bdate) <= int.Parse(DateTime.Now.ToString("yyyyMMdd"))
                        && int.Parse(promotionData.Edate) >= int.Parse(DateTime.Now.ToString("yyyyMMdd")));
                }
            }
            else //修改
            {
                ViewBag.FUNNAME = "修改訂單";

                vm.OrderTime = DateTime.ParseExact(outOrderMain.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.Memo = outOrderMain.Memo;

                //檢查是否有促銷資料
                if (outOrderMain.Promotion_No != null)
                {
                    checkTime = true;
                }
            }

            #endregion

            //TODO 促銷 販售&狀態待確認 By Alex
            #region 寫入資料

            //先找出所有訂單
            var allOut10Data = await _context.Out10.Where(x => x.CoNo == CustomerId).Select(x => x.OutNo).ToListAsync();

            var allData = (from out10 in _context.Out10
                           join out20 in _context.Out20 on out10.OutNo equals out20.OutNo
                           where out10.CoNo.Equals(CustomerId)
                           select new { out20.PartNo }).Distinct().ToList();

            Dictionary<int, string> dict = new Dictionary<int, string>();

            if (allData.Count() > 0)
            {
                for (int i = 0; i < allData.Count(); i++)
                {
                    dict.Add(i, allData[i].PartNo);
                }
            }

            vm.StockList = new List<StockInfo>();

            foreach (var titleItem in title)
            {
                var stockData = await _context.Stock10
                    .Where(x => x.Type == titleItem.Name)
                    .Select(x => new StockInfo
                    {
                        PartNo = x.PartNo,
                        Spec = x.Spec,
                        StockType = x.Type
                    }).OrderBy(x => x.PartNo).ToListAsync();

                foreach (var stock in stockData)
                {
                    if (dict.Where(x => x.Value == stock.PartNo).Any())
                    {
                        stock.IsSalesStatus = true;
                    }

                    //塞入促銷單對應資料
                    if (outOrderMain != null)
                    {
                        if (checkTime)
                        {
                            var promotionDataDetail = _context.Stock21.Where(x => x.SpNo == outOrderMain.Promotion_No).ToList();

                            if (promotionDataDetail != null)
                            {
                                foreach (var promotionDataDetailItem in promotionDataDetail)
                                {
                                    if (promotionDataDetailItem.PartNo == stock.PartNo)
                                    {
                                        stock.IsPromotion = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (checkTime)
                        {
                            var promotionDataDetail = _context.Stock21.Where(x => x.SpNo == outOrderMain.Promotion_No).ToList();

                            if (promotionDataDetail != null)
                            {
                                foreach (var promotionDataDetailItem in promotionDataDetail)
                                {
                                    if (promotionDataDetailItem.PartNo == stock.PartNo)
                                    {
                                        stock.IsPromotion = true;
                                    }
                                }
                            }
                        }
                    }

                    //20221224 item2.PackQty = null;
                    if (stock.PackQty == null)//數量
                        stock.PackQty = null;
                    else
                        stock.PackQty = null;

                    stock.StPs = "";

                    if (outOrderDetailData != null)
                    {
                        if (vm.OutDetail != null)
                        {
                            foreach (var outDetailItem in vm.OutDetail.Where(x => x.PartNo == stock.PartNo))
                            {
                                stock.PackQty = outDetailItem.Qty;
                                stock.StPs = outDetailItem.Memo;
                            }
                        }
                    }
                }

                vm.StockList.AddRange(stockData);
            }

            if (vm.StockList != null)
            {
                vm.StockList = vm.StockList
                    .OrderByDescending(x => x.IsPromotion)
                    .OrderByDescending(x => x.IsSalesStatus)
                    .ToList();
            }

            #endregion 寫入資料

            vm.StockTitleList = title;

            return View(vm);

            #endregion new
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> OrderEdit(string id, List<string> SALE, List<string> PART_NO, List<string> SPEC, List<string> PORDNUM, List<string> PS, [Bind("ORD_ID,ORD_TIME,ORD_STATE,ORD_FTIME,ORD_CODE,ORD_PAYNONEY,ORD_RECMONEY,ORD_RECTIME,ORD_AREA,ORD_CLIENT,ORD_USERID,ORD_USERNAME,ORD_ORDER,ORD_LINKID,ORD_PS,ORD_PTIME")] STO_ORDER sTO_ORDER)
        public async Task<IActionResult> OrderEdit(OrderEditViewModel vm, List<string> SALE, List<string> PART_NO, List<string> SPEC, List<string> PORDNUM, List<string> PS)
        {
            //新增訂貨單(出貨單)主表
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vm.IsCopy) && vm.IsCopy.Equals("true"))
                {
                    vm.OutNo = string.Empty;
                }

                //取得公司資料
                var companyData = await _context.Cstm10.Where(x => x.CoNo == vm.CustomerId).FirstOrDefaultAsync();

                if (companyData == null)
                {
                    return NotFound();
                }

                //新增
                if (string.IsNullOrEmpty(vm.OutNo))
                {
                    var getLastNum = _context.Out10.Where(x => x.CoNo == vm.CustomerId)
                         .OrderByDescending(x => x.OutNo).FirstOrDefault();

                    var pKey = (int.Parse(getLastNum.OutNo) + 1).ToString();

                    //先新增出貨單主表
                    //TODO 補其他欄位資料
                    Out10 insertData = new Out10
                    {
                        OutNo = DateTime.Now.ToString("yyyyMM") + pKey.Substring(pKey.Length - 4),
                        //經由ipad產出的訂單狀態先給未出貨
                        OutType = "2",
                        OutDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
                        Userid = HttpContext.Session.GetString("UserAc"),
                        Paymonth = DateTime.Now.AddDays(25).ToString("yyyyMM"),
                        Ntus = "NTD",
                        CoNo = vm.CustomerId,
                        Memo = vm.Memo,
                        KeyinDate = DateTime.Now.ToString("yyyyMMdd"),
                        PeNo = companyData.PeNo,
                        DriveNo = companyData.DriveNo,
                    };

                    //主表資料
                    var out10MainData = _context.Out10.Add(insertData);
                    await _context.SaveChangesAsync();

                    //判斷有無促銷
                    var promotionData = _context.Stock20
                        .Where(x => x.CoNo == out10MainData.Entity.CoNo)
                        .OrderByDescending(x => x.SpNo).FirstOrDefault();

                    var checkTime = false;

                    if (promotionData != null)
                    {
                        //如果有在比對日期是否在期限內
                        checkTime = (int.Parse(promotionData.Bdate) <= int.Parse(out10MainData.Entity.OutDate)
                            && int.Parse(promotionData.Edate) >= int.Parse(out10MainData.Entity.OutDate));

                        if (checkTime)
                        {
                            out10MainData.Entity.Promotion_No = promotionData.SpNo;
                        }

                    }

                    int fus = 0;
                    var serNo = 1;

                    //再新增出貨單明細
                    //TODO 促銷&販售狀況&其他欄位
                    foreach (var partNo in PART_NO)
                    {
                        if (Convert.ToInt32(PORDNUM[fus]) > 0)
                        {
                            //取出產品資料處理
                            var stockData = _context.Stock10.Where(x => x.PartNo == partNo).FirstOrDefault();

                            var out20InsertData = new Out20
                            {
                                OutNo = insertData.OutNo,
                                Serno = serNo,
                                PartNo = partNo,
                                Qty = Convert.ToInt32(PORDNUM[fus]),
                                Price = stockData.Price1,
                                Memo = PS[fus],
                                Unit = stockData.Unit,
                                Amount = stockData.Price1 * Convert.ToInt32(PORDNUM[fus])
                            };

                            serNo++;

                            var out20MainData = _context.Out20.Add(out20InsertData);
                            //await _context.SaveChangesAsync();

                            //判斷有無促銷
                            if (checkTime)
                            {
                                var promotionDataDetail = _context.Stock21
                                    .Where(x => x.SpNo == promotionData.SpNo).ToList();

                                if (promotionDataDetail != null)
                                {
                                    foreach (var promotionDataDetailItem in promotionDataDetail)
                                    {
                                        if (promotionDataDetailItem.PartNo == partNo)
                                        {
                                            out20MainData.Entity.Price = promotionDataDetailItem.Newprice;
                                        }
                                    }
                                }
                            }

                            //最後取出主表欄位做計算
                            out10MainData.Entity.Total += out20InsertData.Qty * out20InsertData.Price;

                            //免稅金額(total0)
                            if (stockData.TaxType == "免稅")
                            {
                                out10MainData.Entity.Total += out20InsertData.Qty * out20InsertData.Price;

                            }

                            //主表重量相加
                            out10MainData.Entity.Kg += out20InsertData.Qty * stockData.InitQty2;

                            _context.SaveChanges();
                        }

                        fus++;
                    }

                    //如果有公斤資料的話，轉換單位
                    if (out10MainData.Entity.Kg != 0)
                    {

                        var kg = out10MainData.Entity.Kg ?? 0;

                        out10MainData.Entity.Kg = (double?)decimal.Round((decimal)kg / 1000, 2);
                    }
                    else
                    {
                        out10MainData.Entity.Kg = 0;
                    }

                    //合計金額減掉免稅金額等於應稅金額
                    out10MainData.Entity.Total1 = out10MainData.Entity.Total - out10MainData.Entity.Total0;

                    _context.SaveChanges();

                    HttpContext.Session.SetString("msg", "新增成功");

                    //清掉不必要的參數
                    string area = vm.Area;
                    string customerId = vm.CustomerId;
                    vm = new OrderEditViewModel
                    {
                        Area = area,
                        CustomerId = customerId,
                        OrderState = "4",
                    };

                    return RedirectToAction("Order", "Sales", vm);
                }
                else
                {

                    #region 修改

                    //TODO 欄位相關
                    //先抓出主表
                    var out10MainData = _context.Out10.Where(x => x.OutNo == vm.OutNo).FirstOrDefault();

                    int fus = 0;
                    var serNo = 1;
                    //先刪除out20相關資料

                    var deleteOut20Data = _context.Out20.Where(x => x.OutNo == out10MainData.OutNo).ToList();

                    //因為先刪除detail 所以先將主表總價歸零
                    out10MainData.Total = 0;
                    _context.Out20.RemoveRange(deleteOut20Data);

                    _context.SaveChanges();

                    foreach (var partNo in PART_NO)
                    {
                        if (Convert.ToInt32(PORDNUM[fus]) > 0)
                        {
                            //取出產品資料處理
                            var stockData = _context.Stock10.Where(x => x.PartNo == partNo).FirstOrDefault();

                            var insertOut20Data = new Out20
                            {
                                OutNo = out10MainData.OutNo,
                                Serno = serNo,
                                PartNo = partNo,
                                Qty = Convert.ToInt32(PORDNUM[fus]),
                                Price = stockData.Price1,
                                Memo = PS[fus],
                                Unit = stockData.Unit,
                                Amount = stockData.Price1 * Convert.ToInt32(PORDNUM[fus])
                            };
                            serNo++;

                            var out20MainData = _context.Out20.Add(insertOut20Data);

                            //判斷有無促銷
                            if (out10MainData.Promotion_No != null)
                            {
                                var promotionDataDetail = _context.Stock21
                                    .Where(x => x.SpNo == out10MainData.Promotion_No).ToList();

                                if (promotionDataDetail != null)
                                {
                                    foreach (var promotionDataDetailItem in promotionDataDetail)
                                    {
                                        if (promotionDataDetailItem.PartNo == partNo)
                                        {
                                            out20MainData.Entity.Price = promotionDataDetailItem.Newprice;
                                        }
                                    }
                                }
                            }

                            //最後取出主表欄位做計算(合計金額)
                            out10MainData.Total += insertOut20Data.Qty * insertOut20Data.Price;

                            //免稅金額(total0)
                            if (stockData.TaxType == "免稅")
                            {
                                out10MainData.Total0 += insertOut20Data.Qty * insertOut20Data.Price;

                            }

                            //主表重量相加
                            out10MainData.Kg += insertOut20Data.Qty * stockData.InitQty2;

                            _context.SaveChanges();
                        }
                        fus++;
                    }

                    out10MainData.Userid = HttpContext.Session.GetString("UserAc");

                    //如果有公斤資料的話，轉換單位
                    if (out10MainData.Kg != 0)
                    {

                        var kg = out10MainData.Kg ?? 0;

                        out10MainData.Kg = (double?)decimal.Round((decimal)kg / 1000, 2);
                    }
                    else
                    {
                        out10MainData.Kg = 0;
                    }

                    //合計金額減掉免稅金額等於應稅金額
                    out10MainData.Total1 = out10MainData.Total - out10MainData.Total0;

                    _context.SaveChanges();

                    #endregion

                    //清掉不必要的參數
                    string area = vm.Area;
                    string customerId = vm.CustomerId;
                    vm = new OrderEditViewModel
                    {
                        Area = area,
                        CustomerId = customerId,
                        OrderState = "4",
                    };

                    HttpContext.Session.SetString("msg", "修改成功");

                    return RedirectToAction("Order", "Sales", vm);
                }


            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> OrderCopy(string OutNo, string Area, string CustomerId, string IsCopy)
        {
            if (string.IsNullOrEmpty(OutNo))
            {
                return NotFound();
            }

            OrderEditViewModel vm = new OrderEditViewModel();

            vm.Area = Area;
            vm.CustomerId = CustomerId;
            vm.IsCopy = IsCopy;

            //取得公司資料
            var companyData = await _context.Cstm10.Where(x => x.CoNo == CustomerId).FirstOrDefaultAsync();

            //取得業務人員資料
            if (!string.IsNullOrEmpty(companyData.PeNo))
            {
                vm.Business = _context.Pepo10
                    .Where(x => x.PeNo == companyData.PeNo).FirstOrDefault().Name;
            }

            //取得司機人員資料
            if (!string.IsNullOrEmpty(companyData.DriveNo))
            {
                vm.Driver = _context.Pepo10
                .Where(x => x.PeNo == companyData.DriveNo).FirstOrDefault().Name;
            }

            //先取分類列表
            var title = _context.Stock10.Where(x => x.IsOpen == true).Select(x => new StockTitle
            {
                Name = x.Type
            }).Distinct().OrderBy(x => x.Name).ToList();

            vm.StockTitleList = title;

            //取出出貨單主表
            var outOrderMain = _context.Out10.Where(x => x.OutNo == OutNo).FirstOrDefault();

            //TODO 訂單紀錄(單筆)By Alex
            #region 訂單紀錄(單筆)

            var outOrderDetailData = _context.Out20
                .Where(x => x.OutNo == (vm != null ? OutNo : string.Empty)).ToList();

            vm.OutDetail = new List<OutOrderDetail>();

            foreach (var item in outOrderDetailData)
            {
                var OutDetail = new OutOrderDetail();

                OutDetail.OutDetailNo = item.OutNo;
                OutDetail.PartNo = item.PartNo;
                OutDetail.Sale = false;
                OutDetail.Stats = false;
                //品名
                OutDetail.Name = _context.Stock10.Where(x => x.PartNo == item.PartNo).Select(x => x.Spec).ToString();
                OutDetail.Qty = item.Qty;
                OutDetail.Memo = item.Memo;

                vm.OutDetail.Add(OutDetail);
            }

            #endregion 訂單紀錄(單筆)

            #region 新增/修改

            var promotionData = new Stock20();
            var checkTime = false;

            //複製新訂單
            ViewBag.FUNNAME = "複製新訂單";

            vm.OrderTime = DateTime.ParseExact(outOrderMain.OutDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            vm.Memo = outOrderMain.Memo;

            //檢查是否有促銷資料
            if (outOrderMain.Promotion_No != null)
            {
                checkTime = true;
            }

            #endregion

            //TODO 促銷 販售&狀態待確認 By Alex
            #region 寫入資料

            //先找出所有訂單
            var allOut10Data = await _context.Out10.Where(x => x.CoNo == CustomerId).Select(x => x.OutNo).ToListAsync();

            var allData = (from out10 in _context.Out10
                           join out20 in _context.Out20 on out10.OutNo equals out20.OutNo
                           where out10.CoNo.Equals(CustomerId)
                           select new { out20.PartNo }).Distinct().ToList();

            Dictionary<int, string> dict = new Dictionary<int, string>();

            if (allData.Count() > 0)
            {
                for (int i = 0; i < allData.Count(); i++)
                {
                    dict.Add(i, allData[i].PartNo);
                }
            }

            vm.StockList = new List<StockInfo>();

            foreach (var titleItem in title)
            {
                var stockData = await _context.Stock10
                    .Where(x => x.Type == titleItem.Name)
                    .Select(x => new StockInfo
                    {
                        PartNo = x.PartNo,
                        Spec = x.Spec,
                        StockType = x.Type
                    }).OrderBy(x => x.PartNo).ToListAsync();

                foreach (var stock in stockData)
                {
                    if (dict.Where(x => x.Value == stock.PartNo).Any())
                    {
                        stock.IsSalesStatus = true;
                    }

                    //塞入促銷單對應資料
                    if (checkTime)
                    {
                        var promotionDataDetail = _context.Stock21.Where(x => x.SpNo == outOrderMain.Promotion_No).ToList();

                        if (promotionDataDetail != null)
                        {
                            foreach (var promotionDataDetailItem in promotionDataDetail)
                            {
                                if (promotionDataDetailItem.PartNo == stock.PartNo)
                                {
                                    stock.IsPromotion = true;
                                }
                            }
                        }
                    }

                    //20221224 item2.PackQty = null;
                    if (stock.PackQty == null)//數量
                        stock.PackQty = null;
                    else
                        stock.PackQty = null;

                    stock.StPs = "";

                    if (outOrderDetailData != null)
                    {
                        if (vm.OutDetail != null)
                        {
                            foreach (var outDetailItem in vm.OutDetail.Where(x => x.PartNo == stock.PartNo))
                            {
                                stock.PackQty = outDetailItem.Qty;
                                stock.StPs = outDetailItem.Memo;
                            }
                        }
                    }
                }

                vm.StockList.AddRange(stockData);
            }

            if (vm.StockList != null)
            {
                vm.StockList = vm.StockList
                    .OrderByDescending(x => x.IsPromotion)
                    .OrderByDescending(x => x.IsSalesStatus)
                    .ToList();
            }

            #endregion 寫入資料

            return View("OrderEdit", vm);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> OrderDel(string id, string Area, string CustomerId)
        {
            if (id == null)
            {
                return NotFound();
            }

            //先找出主表資料
            var deleteData = _context.Out10.Where(x => x.OutNo == id).FirstOrDefault();

            if (deleteData == null)
            {
                return NotFound();
            }

            //已出貨不能刪除
            if (deleteData.OutType == "2")
            {
                //再找出明細清單
                var deleteOut20Data = _context.Out20.Where(x => x.OutNo == deleteData.OutNo).ToList();

                if (deleteOut20Data != null)
                {
                    //先刪除明細資烙
                    _context.Out20.RemoveRange(deleteOut20Data);

                    //再刪除主表資料
                    _context.Out10.Remove(deleteData);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Order", "Sales", new OrderViewModel
                {
                    Area = Area,
                    CustomerId = CustomerId,
                    OrderState = "4",
                });
            }

            //設定初始資料

            var vm = new OrderViewModel
            {
                Area = Area,
                CustomerId = CustomerId,
                OrderState = "4",
            };

            return RedirectToAction("Order", "Sales", vm);
        }
    }
}
