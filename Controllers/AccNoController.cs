using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using ERP6.ViewModels.Vendor;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using ERP6.ViewModels.AccNo;

namespace ERP6.Controllers
{
    public class AccNoController : Controller
    {
        private readonly EEPEF01Context _context;

        public AccNoController(EEPEF01Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <param name="vm">IndexViewModel</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            try
            {
                if (!vm.IsSearch)
                {
                    //如果vendorNo有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.Accno))
                    {
                        var accNoData = _context.Accno10.Where(x => x.Accno == vm.Accno).FirstOrDefault();

                        if (accNoData != null)
                        {
                            vm = new IndexViewModel
                            {
                                Accno = accNoData.Accno,
                                Accname = accNoData.Accname,
                                Acctype = accNoData.Acctype,
                                Midtype = accNoData.Midtype,
                            };
                        }
                    }
                }

                //取得會計科目代號檔資料
                var accNoList = await _context.Accno10.Select(x => new AccNoList
                {
                    Accno = x.Accno,
                    Accname = x.Accname,
                    Acctype = x.Acctype,
                    Midtype = x.Midtype,
                }).ToListAsync();

                //搜尋條件
                if (vm.IsSearch)
                {
                    //會計科目
                    if (!string.IsNullOrEmpty(vm.Accno))
                    {
                        accNoList = await accNoList.Where(x => x.Accno.Contains(vm.Accno)).ToListAsync();
                    }

                    //科目名稱
                    if (!string.IsNullOrEmpty(vm.Accname))
                    {
                        accNoList = await accNoList.Where(x => x.Accname.Contains(vm.Accname)).ToListAsync();
                    }

                    //會計大類
                    if (!string.IsNullOrEmpty(vm.Acctype))
                    {
                        accNoList = await accNoList.Where(x => x.Acctype.Contains(vm.Acctype)).ToListAsync();
                    }

                    //會計中類
                    if (!string.IsNullOrEmpty(vm.Midtype))
                    {
                        accNoList = await accNoList.Where(x => x.Midtype.Contains(vm.Midtype)).ToListAsync();
                    }
                }

                vm.accNoList = accNoList;

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
        /// <param name="AccNo">會計代碼編號</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(string AccNo)
        {
            try
            {
                AddViewModel vm = new AddViewModel();

                //取得預設資料
                if (!string.IsNullOrEmpty(AccNo))
                {
                    var accNoData = await _context.Accno10.Where(x => x.Accno == AccNo).FirstOrDefaultAsync();

                    vm.Accname = accNoData.Accname;
                    vm.Acctype = accNoData.Acctype;
                    vm.Midtype = accNoData.Midtype;
                }


                //取得清單資料
                var accNoList = await _context.Accno10.Select(x => new AccNoList
                {
                    Accno = x.Accno,
                    Accname = x.Accname,
                    Acctype = x.Acctype,
                    Midtype = x.Midtype,
                }).ToListAsync();

                vm.accNoList = accNoList;

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
        /// <param name="vm">AddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddViewModel vm)
        {
            try
            {
                //檢查accNo是否唯一
                var checkAccNo = _context.Accno10.Where(x => x.Accno == vm.Accno).FirstOrDefault();

                if (checkAccNo != null)
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "會計代碼已經存在";

                    return Json(vm);
                }

                //新增所需資料(廠商)
                Accno10 insertData = new Accno10
                {
                    Accno = vm.Accno,
                    Accname = vm.Accname,
                    Acctype = vm.Acctype,
                    Midtype = vm.Midtype,
                };

                _context.Accno10.Add(insertData);
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
        /// <param name="AccNo">會計科目代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string AccNo)
        {
            EditViewModel vm = new EditViewModel();

            try
            {
                if (string.IsNullOrEmpty(AccNo))
                {
                    return NotFound();
                }

                //資料
                var AccNoData = _context.Accno10
                    .Where(x => x.Accno == AccNo).FirstOrDefault();

                if (AccNoData == null)
                {
                    return NotFound();
                }

                vm.Accno = AccNoData.Accno;
                vm.Accname = AccNoData.Accname;
                vm.Acctype = AccNoData.Acctype;
                vm.Midtype = AccNoData.Midtype;

                //廠商清單資料
                var accNoList = await _context.Accno10.Select(x => new AccNoList
                {
                    Accno = x.Accno,
                    Accname = x.Accname,
                    Acctype = x.Acctype,
                    Midtype = x.Midtype,
                }).ToListAsync();

                if (accNoList != null)
                {
                    vm.accNoList = accNoList;

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
        /// <param name="vm">EditViewModel</param>
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
                    Accno10 updateData = new Accno10
                    {
                        Accno = vm.Accno,
                        Accname = vm.Accname,
                        Acctype = vm.Acctype,
                        Midtype = vm.Midtype,
                    };

                    _context.Accno10.Update(updateData);
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
        /// <param name="AccNo">會計科目代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string AccNo)
        {
            try
            {
                if (string.IsNullOrEmpty(AccNo))
                {
                    return NotFound();
                }

                var deleteData = await _context.Accno10
                    .Where(x => x.Accno == AccNo).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.Accno10.Remove(deleteData);
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
        public IActionResult Export(string StartVendorNo, string EndVendorNo, string VendorType)
        {
            #region SelectList

            //會計科目清單資料
            var accNoList = from accno10 in _context.Accno10
                            select new
                            {
                                Accno = accno10.Accno,
                                Accname = accno10.Accno + string.Empty + accno10.Accname
                            };

            //廠商清單
            ViewBag.AccNoList = new SelectList(accNoList.ToList(), "Accno", "Accname", string.Empty);

            #endregion

            ExportViewModel vm = new ExportViewModel();

            return View(vm);
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Export(ExportViewModel vm)
        {
            var accNoListData = await _context.Accno10.Select(x => new AccNoList
            {
                Accno = x.Accno,
                Accname = x.Accname,
                Acctype = x.Acctype,
                Midtype = x.Midtype,
            }).ToListAsync();

            //第一筆
            var firstData = _context.Accno10.OrderBy(x => x.Accno).First().Accno;

            //最後一筆
            var lastData = _context.Accno10.OrderByDescending(x => x.Accno).First().Accno;

            if (accNoListData == null)
            {
                return View(new VendorExportViewModel());
            }

            if (string.IsNullOrEmpty(vm.StartAccno) && string.IsNullOrEmpty(vm.EndAccno))
            {
                accNoListData = await accNoListData
                    .Where(x => x.Accno.CompareTo(firstData) >= 0 && x.Accno.CompareTo(lastData) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(vm.StartAccno))
            {
                accNoListData = await accNoListData
                    .Where(x => x.Accno.CompareTo(firstData) >= 0 && x.Accno.CompareTo(vm.EndAccno) <= 0).ToListAsync();
            }
            else if (string.IsNullOrEmpty(vm.EndAccno))
            {
                accNoListData = await accNoListData
                    .Where(x => x.Accno.CompareTo(vm.StartAccno) >= 0 && x.Accno.CompareTo(lastData) <= 0).ToListAsync();
            }
            else
            {
                accNoListData = await accNoListData
                    .Where(x => x.Accno.CompareTo(vm.StartAccno) >= 0 && x.Accno.CompareTo(vm.EndAccno) <= 0).ToListAsync();
            }

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
            sheet.GetRow(0).CreateCell(0).SetCellValue("科目代號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("科目名稱");
            sheet.GetRow(0).CreateCell(2).SetCellValue("會計大類");
            sheet.GetRow(0).CreateCell(3).SetCellValue("會計中類");

            //sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            //sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            //sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");
            //sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式

            //填入資料
            int rowIndex = 1;
            for (int row = 0; row < accNoListData.Count(); row++)
            {
                sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(accNoListData[row].Accno);
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(accNoListData[row].Accname);
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(accNoListData[row].Acctype);
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(accNoListData[row].Midtype);

                rowIndex++;
            }

            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);

            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"會計科目代號表.xls"));
        }
    }
}
