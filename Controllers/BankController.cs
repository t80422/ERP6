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
using ERP6.ViewModels.Bank;

namespace ERP6.Controllers
{
    public class BankController : Controller
    {
        private readonly EEPEF01Context _context;

        public BankController(EEPEF01Context context)
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
                    if (!string.IsNullOrEmpty(vm.BankNo))
                    {
                        var oGetData = (from bank10 in _context.Bank10
                                        select new IndexViewModel
                                        {
                                            BankNo = bank10.BankNo,
                                            BankName = bank10.BankName,
                                            Initamount = bank10.Initamount,
                                            Accno = bank10.Accno,
                                            Initdate = bank10.Initdate,
                                            Ntus = bank10.Ntus,
                                        }).FirstOrDefault();
                    }
                }

                //取得資料
                var bank10List = await (from bank10 in _context.Bank10
                                        select new Bank10List
                                        {
                                            BankNo = bank10.BankNo,
                                            BankName = bank10.BankName,
                                            Initamount = bank10.Initamount,
                                            Accno = bank10.Accno,
                                            Initdate = bank10.Initdate,
                                            Ntus = bank10.Ntus,
                                        }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //帳戶
                    if (!string.IsNullOrEmpty(vm.BankNo))
                    {
                        bank10List = await bank10List.Where(x => x.BankNo.Contains(vm.BankNo)).ToListAsync();
                    }

                    //初期日期
                    if (!string.IsNullOrEmpty(vm.Initdate))
                    {
                        vm.Initdate = vm.Initdate.Replace("-", "");

                        bank10List = await bank10List.Where(x => x.Initdate == vm.Initdate).ToListAsync();
                    }

                    //對應的會計科目
                    if (!string.IsNullOrEmpty(vm.AccnoDDL))
                    {
                        bank10List = await bank10List.Where(x => x.Accno == vm.AccnoDDL).ToListAsync();
                    }
                }

                vm.bank10List = bank10List;

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
                AddViewModel vm = new AddViewModel();

                //取得資料
                var bank10List = await (from bank10 in _context.Bank10
                                        select new Bank10List
                                        {
                                            BankNo = bank10.BankNo,
                                            BankName = bank10.BankName,
                                            Initamount = bank10.Initamount,
                                            Accno = bank10.Accno,
                                            Initdate = bank10.Initdate,
                                            Ntus = bank10.Ntus,
                                        }).ToListAsync();

                vm.bank10List = bank10List;

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
                //檢查AccNo是否唯一
                var checkNo = _context.Bank10.Where(x => x.Accno == vm.Accno).FirstOrDefault();

                if (checkNo != null)
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "資料已經存在，請重新編輯";

                    return Json(vm);
                }

                //新增所需資料(廠商)
                Bank10 insertData = new Bank10
                {
                    BankNo = vm.BankNo,
                    BankName = vm.BankName,
                    Initdate = string.IsNullOrEmpty(vm.Initdate) ? null : vm.Initdate.Replace("-", ""),
                    Initamount = vm.Initamount,
                    Accno = vm.Accno,
                    Ntus = vm.Ntus,
                };

                _context.Bank10.Add(insertData);
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
        public async Task<IActionResult> Edit(string BankNo)
        {
            EditViewModel vm = new EditViewModel();

            //會計科目清單資料
            var accNoListData = from accNo10 in _context.Accno10
                                select new
                                {
                                    AccNo = accNo10.Accno,
                                    AccName = accNo10.Accno + string.Empty + accNo10.Accname
                                };

            //會計科目清單
            ViewBag.AccNoListData = new SelectList(accNoListData.ToList(), "AccNo", "AccName", string.Empty);

            try
            {
                if (string.IsNullOrEmpty(BankNo))
                {
                    return NotFound();
                }


                //資料
                var bankData = _context.Bank10
                    .Where(x => x.BankNo == BankNo).FirstOrDefault();

                if (bankData == null)
                {
                    return NotFound();
                }

                vm.BankNo = bankData.BankNo;
                vm.BankName = bankData.BankName;
                vm.Ntus = bankData.Ntus;
                vm.Initdate = DateTime.ParseExact(bankData.Initdate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                vm.Initamount = bankData.Initamount;
                vm.Accno = bankData.Accno;

                //清單資料
                var bankList = await _context.Bank10.Select(x => new Bank10List
                {
                    BankNo = x.BankNo,
                    BankName = x.BankName,
                    Ntus = x.Ntus,
                    Initdate = DateTime.ParseExact(bankData.Initdate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
                    Initamount = x.Initamount,
                    Accno = x.Accno,
                }).ToListAsync();

                if (bankList != null)
                {
                    bankList = bankList.Where(x => x.BankNo == BankNo).ToList();
                    vm.bank10List = bankList;

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
                    Bank10 updateData = new Bank10
                    {
                        BankNo = vm.BankNo,
                        BankName = vm.BankName,
                        Initdate = string.IsNullOrEmpty(vm.Initdate) ? null : vm.Initdate.Replace("-", ""),
                        Initamount = vm.Initamount,
                        Accno = vm.Accno,
                        Ntus = vm.Ntus,
                    };

                    _context.Bank10.Update(updateData);

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
        /// <param name="BankNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string BankNo)
        {
            try
            {
                if (string.IsNullOrEmpty(BankNo))
                {
                    return NotFound();
                }

                var deleteData = await _context.Bank10
                    .Where(x => x.BankNo == BankNo).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.Bank10.Remove(deleteData);

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
