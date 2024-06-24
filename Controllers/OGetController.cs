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
using ERP6.ViewModels.Vendor;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using ERP6.ViewModels.OGet;

namespace ERP6.Controllers
{
    public class OGetController : Controller
    {
        private readonly EEPEF01Context _context;

        public OGetController(EEPEF01Context context)
        {
            _context = context;
        }

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
                    if (!string.IsNullOrEmpty(vm.Paymonth) &&
                        !string.IsNullOrEmpty(vm.CoNo))
                    {
                        var oGetData = (from oGet10 in _context.OGet10
                                        join cstm10 in _context.Cstm10 on oGet10.CoNo equals cstm10.CoNo
                                        select new IndexViewModel
                                        {
                                            Paymonth = DateTime.ParseExact(oGet10.Paymonth, "yyyyMM", null).ToString("yyyy/MM"),
                                            CoNo = oGet10.CoNo,
                                            Ntus = oGet10.Ntus,
                                            Total0 = oGet10.Total0,
                                            Total1 = oGet10.Total1,
                                            Tax = oGet10.Tax,
                                            Total2 = oGet10.Total2,
                                            YesGet = oGet10.YesGet,
                                            SubTot = oGet10.SubTot,
                                            NotGet = oGet10.NotGet,
                                            Accid = oGet10.Accid,
                                            LnotGet = oGet10.LnotGet,
                                            Memo = oGet10.Memo,
                                            TnotGet = oGet10.TnotGet,
                                            Total = oGet10.Total,
                                            RetTotal = oGet10.RetTotal,
                                            RetPercent = oGet10.RetPercent,
                                            CashDiscount = oGet10.CashDiscount,
                                            CoName = cstm10.Coname,
                                        }).FirstOrDefault();
                    }
                }

                //取得資料
                var oGetList = await (from oGet10 in _context.OGet10
                                      join cstm10 in _context.Cstm10 on oGet10.CoNo equals cstm10.CoNo
                                      select new OGetList
                                      {
                                          Paymonth = DateTime.ParseExact(oGet10.Paymonth, "yyyyMM", null).ToString("yyyy/MM"),
                                          CoNo = oGet10.CoNo,
                                          Ntus = oGet10.Ntus,
                                          Total0 = oGet10.Total0,
                                          Total1 = oGet10.Total1,
                                          Tax = oGet10.Tax,
                                          Total2 = oGet10.Total2,
                                          YesGet = oGet10.YesGet,
                                          SubTot = oGet10.SubTot,
                                          NotGet = oGet10.NotGet,
                                          Accid = oGet10.Accid,
                                          LnotGet = oGet10.LnotGet,
                                          Memo = oGet10.Memo,
                                          TnotGet = oGet10.TnotGet,
                                          Total = oGet10.Total,
                                          RetTotal = oGet10.RetTotal,
                                          RetPercent = oGet10.RetPercent,
                                          CashDiscount = oGet10.CashDiscount,
                                          CoName = cstm10.Coname,
                                      }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //帳款月份
                    if (!string.IsNullOrEmpty(vm.Paymonth))
                    {
                        vm.Paymonth = vm.Paymonth.Replace("/", "");

                        oGetList = await oGetList.Where(x => x.Paymonth.Contains(vm.Paymonth)).ToListAsync();
                    }

                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNoDDL))
                    {
                        oGetList = await oGetList.Where(x => x.CoNo == vm.CoNoDDL).ToListAsync();
                    }

                    //傳票編號
                    if (!string.IsNullOrEmpty(vm.Accid))
                    {
                        oGetList = await oGetList.Where(x => x.Accid.Contains(vm.Accid)).ToListAsync();
                    }
                }

                vm.oGetList = oGetList;

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

                //客戶編號
                var coListData = from cstm10 in _context.Cstm10
                                 select new
                                 {
                                     CoNo = cstm10.CoNo,
                                     CoName = cstm10.CoNo + string.Empty + cstm10.Coname,
                                 };

                ViewBag.CoList = new SelectList(coListData.ToList(), "CoNo", "CoName", string.Empty);

                #endregion

                AddViewModel vm = new AddViewModel();

                //取得資料
                var oGetList = await (from oGet10 in _context.OGet10
                                      join cstm10 in _context.Cstm10 on oGet10.CoNo equals cstm10.CoNo
                                      select new OGetList
                                      {
                                          Paymonth = DateTime.ParseExact(oGet10.Paymonth, "yyyyMM", null).ToString("yyyy/MM"),
                                          CoNo = oGet10.CoNo,
                                          Ntus = oGet10.Ntus,
                                          Total0 = oGet10.Total0,
                                          Total1 = oGet10.Total1,
                                          Tax = oGet10.Tax,
                                          Total2 = oGet10.Total2,
                                          YesGet = oGet10.YesGet,
                                          SubTot = oGet10.SubTot,
                                          NotGet = oGet10.NotGet,
                                          Accid = oGet10.Accid,
                                          LnotGet = oGet10.LnotGet,
                                          Memo = oGet10.Memo,
                                          TnotGet = oGet10.TnotGet,
                                          Total = oGet10.Total,
                                          RetTotal = oGet10.RetTotal,
                                          RetPercent = oGet10.RetPercent,
                                          CashDiscount = oGet10.CashDiscount,
                                      }).Take(20).ToListAsync();

                vm.Ntus = "NTD";
                vm.oGetList = oGetList;

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
        /// <param name="vm">VendorAddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddViewModel vm)
        {
            try
            {
                //檢查vendorNo是否唯一
                var checkNo = _context.OGet10.Where(x => x.Paymonth == vm.Paymonth && x.CoNo == vm.CoNo).FirstOrDefault();

                if (checkNo != null)
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "廠商編號已經存在，請重新編輯";

                    return Json(vm);
                }

                //新增所需資料(廠商)
                OGet10 insertData = new OGet10
                {
                    Paymonth = DateTime.Now.ToString("yyyyMM"),
                    CoNo = vm.CoNoDDL,
                    Ntus = vm.Ntus ?? "NTD",
                    Total0 = vm.Total0,
                    Total1 = vm.Total1,
                    Tax = vm.Tax,
                    Total2 = vm.Total2,
                    YesGet = vm.YesGet,
                    SubTot = vm.SubTot,
                    NotGet = vm.NotGet,
                    Accid = vm.Accid,
                    LnotGet = vm.LnotGet,
                    Memo = vm.Memo,
                    TnotGet = vm.TnotGet,
                    Total = vm.Total,
                    RetTotal = vm.RetTotal,
                    RetPercent = vm.RetPercent,
                    CashDiscount = vm.CashDiscount,
                };

                _context.OGet10.Add(insertData);
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
        /// <param name="vendorId">廠商代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string PayMonthNo, string CoNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                if (string.IsNullOrEmpty(PayMonthNo) || string.IsNullOrEmpty(CoNo))
                {
                    return NotFound();
                }

                var strPayMonth = PayMonthNo.Replace("/", "");

                //資料
                var oGetData = _context.OGet10
                    .Where(x => x.Paymonth == strPayMonth && x.CoNo == CoNo).FirstOrDefault();

                if (oGetData == null)
                {
                    return NotFound();
                }

                #region SelectList

                //客戶編號
                var coListData = from cstm10 in _context.Cstm10
                                 select new
                                 {
                                     CoNo = cstm10.CoNo,
                                     CoName = cstm10.CoNo + string.Empty + cstm10.Coname,
                                 };

                ViewBag.CoList = new SelectList(coListData.ToList(), "CoNo", "CoName", string.Empty);

                #endregion

                vm.Paymonth = DateTime.ParseExact(oGetData.Paymonth, "yyyyMM", null).ToString("yyyy-MM");
                vm.CoNo = oGetData.CoNo;
                vm.Ntus = oGetData.Ntus;
                vm.Total0 = oGetData.Total0;
                vm.Total1 = oGetData.Total1;
                vm.Tax = oGetData.Tax;
                vm.Total2 = oGetData.Total2;
                vm.YesGet = oGetData.YesGet;
                vm.SubTot = oGetData.SubTot;
                vm.NotGet = oGetData.NotGet;
                vm.Accid = oGetData.Accid;
                vm.LnotGet = oGetData.LnotGet;
                vm.Memo = oGetData.Memo;
                vm.TnotGet = oGetData.TnotGet;
                vm.Total = oGetData.Total;
                vm.RetTotal = oGetData.RetTotal;
                vm.RetPercent = oGetData.RetPercent;
                vm.CashDiscount = oGetData.CashDiscount;
                vm.CoName = _context.Cstm10.Where(x => x.CoNo == oGetData.CoNo).First().Coname;

                //清單資料
                var oGetList = await _context.OGet10.Select(x => new OGetList
                {
                    Paymonth = DateTime.ParseExact(x.Paymonth, "yyyyMM", null).ToString("yyyy/MM"),
                    CoNo = x.CoNo,
                    Ntus = x.Ntus,
                    Total0 = x.Total0,
                    Total1 = x.Total1,
                    Tax = x.Tax,
                    Total2 = x.Total2,
                    YesGet = x.YesGet,
                    SubTot = x.SubTot,
                    NotGet = x.NotGet,
                    Accid = x.Accid,
                    LnotGet = x.LnotGet,
                    Memo = x.Memo,
                    TnotGet = x.TnotGet,
                    Total = x.Total,
                    RetTotal = x.RetTotal,
                    RetPercent = x.RetPercent,
                    CashDiscount = x.CashDiscount,
                }).ToListAsync();

                if (oGetList != null)
                {
                    oGetList = oGetList.Where(x => x.CoNo == CoNo && x.Paymonth == PayMonthNo).ToList();
                    vm.oGetList = oGetList;

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
        /// <param name="vm">VendorEditViewModel</param>
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
                    OGet10 updateData = new OGet10
                    {
                        Paymonth = vm.Paymonth.Replace("/", ""),
                        CoNo = vm.CoNo,
                        Ntus = vm.Ntus,
                        Total0 = vm.Total0,
                        Total1 = vm.Total1,
                        Tax = vm.Tax,
                        Total2 = vm.Total2,
                        YesGet = vm.YesGet,
                        SubTot = vm.SubTot,
                        NotGet = vm.NotGet,
                        Accid = vm.Accid,
                        LnotGet = vm.LnotGet,
                        Memo = vm.Memo,
                        TnotGet = vm.TnotGet,
                        Total = vm.Total,
                        RetTotal = vm.RetTotal,
                        RetPercent = vm.RetPercent,
                        CashDiscount = vm.CashDiscount,
                    };

                    _context.OGet10.Update(updateData);

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
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="PayMonthNo"></param>
        /// <param name="CoNo">客戶編號</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string PayMonthNo, string CoNo)
        {
            try
            {
                if (string.IsNullOrEmpty(PayMonthNo) || string.IsNullOrEmpty(CoNo))
                {
                    return NotFound();
                }

                PayMonthNo = PayMonthNo.Replace("/", "");

                var deleteData = await _context.OGet10
                    .Where(x => x.Paymonth == PayMonthNo && x.CoNo == CoNo).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.OGet10.Remove(deleteData);

                    await _context.SaveChangesAsync();
                }

                return Json(true);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Export(string StartVendorNo, string EndVendorNo, string VendorType)
        {
            #region SelectList

            //廠商清單資料
            var vendorList = from vendor10 in _context.Vendor10
                             select new
                             {
                                 VendorNo = vendor10.VendorNo,
                                 Vename = vendor10.VendorNo + string.Empty + vendor10.Vename
                             };

            //廠商清單
            ViewBag.VendorList = new SelectList(vendorList.ToList(), "VendorNo", "Vename", string.Empty);

            #endregion

            var vendorListData = await _context.Vendor10.Select(x => new VendorList
            {
                VendorNo = x.VendorNo,
                Vename = x.Vename,
                Uniform = x.Uniform,
                Fax = x.Fax,
                Boss = x.Boss,
                Sales = x.Sales,
                Tel1 = x.Tel1,
                Tel2 = x.Tel2,
                Invoaddr = x.Invoaddr,
                Factaddr = x.Factaddr,
            }).ToListAsync();

            if (vendorListData == null)
            {
                return View(new VendorExportViewModel());
            }

            //第一筆
            var firstData = _context.Stock10.OrderBy(x => x.PartNo).First().PartNo;

            //最後一筆
            var lastData = _context.Stock10.OrderByDescending(x => x.PartNo).First().PartNo;

            if (string.IsNullOrEmpty(StartVendorNo) && string.IsNullOrEmpty(EndVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(StartVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(EndVendorNo) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(EndVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(StartVendorNo) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            }
            else
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(StartVendorNo) >= 0 && x.VendorNo.CompareTo(EndVendorNo) <= 0).ToListAsync();
            }

            VendorExportViewModel vm = new VendorExportViewModel();

            vm.vendorList = vendorListData;

            return View(vm);
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Export(VendorExportViewModel vm)
        {
            var vendorListData = await _context.Vendor10.Select(x => new VendorList
            {
                VendorNo = x.VendorNo,
                Vename = x.Vename,
                Uniform = x.Uniform,
                Fax = x.Fax,
                Boss = x.Boss,
                Sales = x.Sales,
                Tel1 = x.Tel1,
                Tel2 = x.Tel2,
                Invoaddr = x.Invoaddr,
                Factaddr = x.Factaddr,
            }).ToListAsync();

            //第一筆
            var firstData = _context.Vendor10.OrderBy(x => x.VendorNo).First().VendorNo;

            //最後一筆
            var lastData = _context.Vendor10.OrderByDescending(x => x.VendorNo).First().VendorNo;

            if (vendorListData == null)
            {
                return View(new VendorExportViewModel());
            }

            if (string.IsNullOrEmpty(vm.StartVendorNo) && string.IsNullOrEmpty(vm.EndVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(vm.StartVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(firstData) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(vm.EndVendorNo))
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(lastData) <= 0).ToListAsync();
            }
            else
            {
                vendorListData = await vendorListData
                    .Where(x => x.VendorNo.CompareTo(vm.StartVendorNo) >= 0 && x.VendorNo.CompareTo(vm.EndVendorNo) <= 0).ToListAsync();
            }

            vm.vendorList = vendorListData;

            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

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
            sheet.GetRow(0).CreateCell(0).SetCellValue("廠商編號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("廠商簡稱");
            sheet.GetRow(0).CreateCell(2).SetCellValue("統一編號");
            sheet.GetRow(0).CreateCell(3).SetCellValue("傳真");
            sheet.GetRow(0).CreateCell(4).SetCellValue("負責人");
            sheet.GetRow(0).CreateCell(5).SetCellValue("電話一");
            sheet.GetRow(0).CreateCell(6).SetCellValue("電話二");
            sheet.GetRow(0).CreateCell(7).SetCellValue("通訊地址一");
            sheet.GetRow(0).CreateCell(8).SetCellValue("倉庫地址");
            //sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            //sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            //sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");
            //sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式

            //填入資料
            int rowIndex = 1;
            for (int row = 0; row < vendorListData.Count(); row++)
            {
                sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(vendorListData[row].VendorNo);
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(vendorListData[row].Vename);
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(vendorListData[row].Uniform);
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(vendorListData[row].Fax);
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(vendorListData[row].Boss);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(vendorListData[row].Tel1);
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(vendorListData[row].Tel2);
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(vendorListData[row].Invoaddr);
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(vendorListData[row].Factaddr);

                rowIndex++;
            }

            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);

            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"廠商通訊錄.xls"));
        }
    }
}
