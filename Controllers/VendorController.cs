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

namespace ERP6.Controllers
{
    public class VendorController : Controller
    {
        private readonly EEPEF01Context _context;

        public VendorController(EEPEF01Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 廠商首頁
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(VendorIndexViewModel vm)
        {
            try
            {
                #region SelectList

                //會計項目
                var accNoListData = from accno10 in _context.Accno10
                                    select new
                                    {
                                        AccNo = accno10.Accno,
                                        AccName = accno10.Accno + string.Empty + accno10.Accname
                                    };

                ViewBag.AccNoList = new SelectList(accNoListData.ToList(), "AccNo", "AccName", string.Empty);

                #endregion

                if (!vm.IsSearch)
                {
                    //如果vendorNo有資料的話，帶出來
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        var vendorData = _context.Vendor10.Where(x => x.VendorNo == vm.VendorNo).FirstOrDefault();

                        if (vendorData != null)
                        {
                            vm = new VendorIndexViewModel
                            {
                                VendorNo = vendorData.VendorNo,
                                Uniform = vendorData.Uniform,
                                Vename = vendorData.Vename,
                                Company = vendorData.Company,
                                Dlien = vendorData.Dlien,
                                Invocomp = vendorData.Invocomp,
                                Payment = vendorData.Payment,
                                Tel1 = vendorData.Tel1,
                                Boss = vendorData.Boss,
                                Fax = vendorData.Fax,
                                ChehkDay = vendorData.ChehkDay,
                                Tel2 = vendorData.Tel2,
                                Sales = vendorData.Sales,
                                Mobile = vendorData.Mobile,
                                TaxType = vendorData.TaxType,
                                Email = vendorData.Email,
                                Taxrate = vendorData.Taxrate,
                                Invoaddr = vendorData.Invoaddr,
                                Ntus = vendorData.Ntus,
                                Factaddr = vendorData.Factaddr,
                                Discount = vendorData.Discount,
                                Product = vendorData.Product,
                                PriceType = vendorData.PriceType,
                                Payaccount = vendorData.Payaccount,
                                Paybank = vendorData.Paybank,
                                Www = vendorData.Www,
                                Memo = vendorData.Memo,
                                LineId = vendorData.LineId,
                                Accno1 = vendorData.Accno1,
                            };
                        }
                    }
                }

                //取得廠商資料
                var vendorList = await _context.Vendor10.Select(x => new VendorList
                {
                    VendorNo = x.VendorNo,
                    Uniform = x.Uniform,
                    Vename = x.Vename,
                    Company = x.Company,
                    Dlien = x.Dlien,
                    Invocomp = x.Invocomp,
                    Payment = x.Payment,
                    Tel1 = x.Tel1,
                    Boss = x.Boss,
                    Fax = x.Fax,
                    ChehkDay = x.ChehkDay,
                    Tel2 = x.Tel2,
                    Sales = x.Sales,
                    Mobile = x.Mobile,
                    TaxType = x.TaxType,
                    Email = x.Email,
                    Taxrate = x.Taxrate,
                    Invoaddr = x.Invoaddr,
                    Ntus = x.Ntus,
                    Factaddr = x.Factaddr,
                    Discount = x.Discount,
                    Product = x.Product,
                    PriceType = x.PriceType,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Www = x.Www,
                    Memo = x.Memo,
                    LineId = x.LineId,
                    Accno1 = x.Accno1,
                }).ToListAsync();

                //搜尋條件(廠商)
                if (vm.IsSearch)
                {
                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        vendorList = await vendorList.Where(x => x.VendorNo.Contains(vm.VendorNo)).ToListAsync();
                    }

                    //統一編號
                    if (!string.IsNullOrEmpty(vm.Uniform))
                    {
                        vendorList = await vendorList.Where(x => x.Uniform.Contains(vm.Uniform)).ToListAsync();
                    }

                    //廠商簡稱
                    if (!string.IsNullOrEmpty(vm.Vename))
                    {
                        vendorList = await vendorList.Where(x => x.Vename.Contains(vm.Vename)).ToListAsync();
                    }

                    //公司名稱
                    if (!string.IsNullOrEmpty(vm.Company))
                    {
                        vendorList = await vendorList.Where(x => x.Company.Contains(vm.Company)).ToListAsync();
                    }

                    //發票抬頭
                    if (!string.IsNullOrEmpty(vm.Invocomp))
                    {
                        vendorList = await vendorList.Where(x => x.Invocomp.Contains(vm.Invocomp)).ToListAsync();
                    }

                    //電話一
                    if (!string.IsNullOrEmpty(vm.Tel1))
                    {
                        vendorList = await vendorList.Where(x => x.Tel1.Contains(vm.Tel1)).ToListAsync();
                    }

                    //負責人
                    if (!string.IsNullOrEmpty(vm.Boss))
                    {
                        vendorList = await vendorList.Where(x => x.Boss.Contains(vm.Boss)).ToListAsync();
                    }

                    //傳真
                    if (!string.IsNullOrEmpty(vm.Fax))
                    {
                        vendorList = await vendorList.Where(x => x.Fax.Contains(vm.Fax)).ToListAsync();
                    }

                    //電話二
                    if (!string.IsNullOrEmpty(vm.Tel2))
                    {
                        vendorList = await vendorList.Where(x => x.Tel2.Contains(vm.Tel2)).ToListAsync();
                    }

                    //聯絡人
                    if (!string.IsNullOrEmpty(vm.Sales))
                    {
                        vendorList = await vendorList.Where(x => x.Sales.Contains(vm.Sales)).ToListAsync();
                    }

                    //行動電話
                    if (!string.IsNullOrEmpty(vm.Mobile))
                    {
                        vendorList = await vendorList.Where(x => x.Mobile.Contains(vm.Mobile)).ToListAsync();
                    }

                    //LineId
                    if (!string.IsNullOrEmpty(vm.LineId))
                    {
                        vendorList = await vendorList.Where(x => x.LineId.Contains(vm.LineId)).ToListAsync();
                    }

                    //Email
                    if (!string.IsNullOrEmpty(vm.Email))
                    {
                        vendorList = await vendorList.Where(x => x.Email.Contains(vm.Email)).ToListAsync();
                    }
                }

                vm.vendorList = vendorList;

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增廠商資料
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            try
            {
                #region SelectList

                //會計項目
                var accNoListData = from accno10 in _context.Accno10
                                    select new
                                    {
                                        AccNo = accno10.Accno,
                                        AccName = accno10.Accno + string.Empty + accno10.Accname
                                    };

                ViewBag.AccNoList = new SelectList(accNoListData.ToList(), "AccNo", "AccName", string.Empty);

                #endregion

                VendorAddViewModel vm = new VendorAddViewModel();

                //取得廠商資料
                var vendorList = await _context.Vendor10.Select(x => new VendorList
                {
                    VendorNo = x.VendorNo,
                    Uniform = x.Uniform,
                    Vename = x.Vename,
                    Company = x.Company,
                    Dlien = x.Dlien,
                    Invocomp = x.Invocomp,
                    Payment = x.Payment,
                    Tel1 = x.Tel1,
                    Boss = x.Boss,
                    Fax = x.Fax,
                    ChehkDay = x.ChehkDay,
                    Tel2 = x.Tel2,
                    Sales = x.Sales,
                    Mobile = x.Mobile,
                    TaxType = x.TaxType,
                    Email = x.Email,
                    Taxrate = x.Taxrate,
                    Invoaddr = x.Invoaddr,
                    Ntus = x.Ntus,
                    Factaddr = x.Factaddr,
                    Discount = x.Discount,
                    Product = x.Product,
                    PriceType = x.PriceType,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Www = x.Www,
                    Memo = x.Memo,
                    LineId = x.LineId,
                    Accno1 = x.Accno1,
                }).ToListAsync();

                vm.vendorList = vendorList;

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增廠商資料
        /// </summary>
        /// <param name="vm">VendorAddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VendorAddViewModel vm)
        {
            try
            {
                //檢查vendorNo是否唯一
                var checkVendorNo = _context.Vendor10.Where(x => x.VendorNo == vm.VendorNo).FirstOrDefault();

                if (checkVendorNo != null)
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "廠商編號已經存在，請重新編輯";

                    return Json(vm);
                }

                //新增所需資料(廠商)
                Vendor10 insertData = new Vendor10
                {
                    VendorNo = vm.VendorNo,
                    Uniform = vm.Uniform,
                    Vename = vm.Vename,
                    Company = vm.Company,
                    Dlien = vm.Dlien,
                    Invocomp = vm.Invocomp,
                    Payment = vm.Payment,
                    Tel1 = vm.Tel1,
                    Boss = vm.Boss,
                    Fax = vm.Fax,
                    ChehkDay = vm.ChehkDay,
                    Tel2 = vm.Tel2,
                    Sales = vm.Sales,
                    Mobile = vm.Mobile,
                    TaxType = vm.TaxType,
                    Email = vm.Email,
                    Taxrate = vm.Taxrate,
                    Invoaddr = vm.Invoaddr,
                    Ntus = vm.Ntus,
                    Factaddr = vm.Factaddr,
                    Discount = vm.Discount,
                    Product = vm.Product,
                    PriceType = vm.PriceType,
                    Payaccount = vm.Payaccount,
                    Paybank = vm.Paybank,
                    Www = vm.Www,
                    Memo = vm.Memo,
                    LineId = vm.LineId,
                    Accno1 = vm.Accno1,
                };

                _context.Vendor10.Add(insertData);
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
        /// 編輯廠商資料
        /// </summary>
        /// <param name="vendorId">廠商代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string vendorNo)
        {
            VendorEditViewModel vm = new VendorEditViewModel();

            try
            {
                if (string.IsNullOrEmpty(vendorNo))
                {
                    return NotFound();
                }

                //廠商資料
                var vendorData = _context.Vendor10
                    .Where(x => x.VendorNo == vendorNo).FirstOrDefault();

                if (vendorData == null)
                {
                    return NotFound();
                }

                #region SelectList

                //會計項目
                var accNoListData = from accno10 in _context.Accno10
                                    select new
                                    {
                                        AccNo = accno10.Accno,
                                        AccName = accno10.Accno + string.Empty + accno10.Accname
                                    };

                ViewBag.AccNoList = new SelectList(accNoListData.ToList(), "AccNo", "AccName", string.Empty);

                //結帳日期
                var dlienList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="15", Value="15" },
                   new SelectListItem {Text="20", Value="20" },
                   new SelectListItem {Text="25", Value="25" },
                   new SelectListItem {Text="月底", Value="月底" },
                   new SelectListItem {Text="21", Value="21" },
                   new SelectListItem {Text="31", Value="31" },
                };

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", vendorData.Dlien);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", vendorData.TaxType);

                //進價類別
                var priceTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", vendorData.TaxType);

                //付款銀行
                ViewBag.PayBankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", vendorData.Paybank);

                #endregion

                vm.VendorNo = vendorData.VendorNo;
                vm.Uniform = vendorData.Uniform;
                vm.Vename = vendorData.Vename;
                vm.Company = vendorData.Company;
                vm.Dlien = vendorData.Dlien;
                vm.Invocomp = vendorData.Invocomp;
                vm.Payment = vendorData.Payment;
                vm.Tel1 = vendorData.Tel1;
                vm.Boss = vendorData.Boss;
                vm.Fax = vendorData.Fax;
                vm.ChehkDay = vendorData.ChehkDay;
                vm.Tel2 = vendorData.Tel2;
                vm.Sales = vendorData.Sales;
                vm.Mobile = vendorData.Mobile;
                vm.TaxType = vendorData.TaxType;
                vm.Email = vendorData.Email;
                vm.Taxrate = vendorData.Taxrate;
                vm.Invoaddr = vendorData.Invoaddr;
                vm.Ntus = vendorData.Ntus;
                vm.Factaddr = vendorData.Factaddr;
                vm.Discount = vendorData.Discount;
                vm.Product = vendorData.Product;
                vm.PriceType = vendorData.PriceType;
                vm.Payaccount = vendorData.Payaccount;
                vm.Paybank = vendorData.Paybank;
                vm.Www = vendorData.Www;
                vm.Memo = vendorData.Memo;
                vm.LineId = vendorData.LineId;
                vm.Accno1 = vendorData.Accno1;

                //廠商清單資料
                var vendorList = await _context.Vendor10.Select(x => new VendorList
                {
                    VendorNo = x.VendorNo,
                    Uniform = x.Uniform,
                    Vename = x.Vename,
                    Company = x.Company,
                    Dlien = x.Dlien,
                    Invocomp = x.Invocomp,
                    Payment = x.Payment,
                    Tel1 = x.Tel1,
                    Boss = x.Boss,
                    Fax = x.Fax,
                    ChehkDay = x.ChehkDay,
                    Tel2 = x.Tel2,
                    Sales = x.Sales,
                    Mobile = x.Mobile,
                    TaxType = x.TaxType,
                    Email = x.Email,
                    Taxrate = x.Taxrate,
                    Invoaddr = x.Invoaddr,
                    Ntus = x.Ntus,
                    Factaddr = x.Factaddr,
                    Discount = x.Discount,
                    Product = x.Product,
                    PriceType = x.PriceType,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Www = x.Www,
                    Memo = x.Memo,
                    LineId = x.LineId,
                    Accno1 = x.Accno1,
                }).ToListAsync();

                if (vendorList != null)
                {
                    vm.vendorList = vendorList;

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
        /// 編輯廠商資料
        /// </summary>
        /// <param name="vm">VendorEditViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VendorEditViewModel vm)
        {
            try
            {
                vm.IsTrue = false;

                if (vm != null)
                {
                    Vendor10 updateData = new Vendor10
                    {
                        VendorNo = vm.VendorNo,
                        Uniform = vm.Uniform,
                        Vename = vm.Vename,
                        Company = vm.Company,
                        Dlien = vm.Dlien,
                        Invocomp = vm.Invocomp,
                        Payment = vm.Payment,
                        Tel1 = vm.Tel1,
                        Boss = vm.Boss,
                        Fax = vm.Fax,
                        ChehkDay = vm.ChehkDay,
                        Tel2 = vm.Tel2,
                        Sales = vm.Sales,
                        Mobile = vm.Mobile,
                        TaxType = vm.TaxType,
                        Email = vm.Email,
                        Taxrate = vm.Taxrate,
                        Invoaddr = vm.Invoaddr,
                        Ntus = vm.Ntus,
                        Factaddr = vm.Factaddr,
                        Discount = vm.Discount,
                        Product = vm.Product,
                        PriceType = vm.PriceType,
                        Payaccount = vm.Payaccount,
                        Paybank = vm.Paybank,
                        Www = vm.Www,
                        Memo = vm.Memo,
                        LineId = vm.LineId,
                        Accno1 = vm.Accno1,
                    };

                    _context.Vendor10.Update(updateData);

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
        /// 相關資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(VendorDetailViewModel vm)
        {
            #region selectList

            //客戶清單資料
            var customerListData = from cstm10 in _context.Cstm10
                                   select new
                                   {
                                       CoNo = cstm10.CoNo,
                                       Coname = cstm10.CoNo + string.Empty + cstm10.Coname
                                   };

            //客戶清單
            ViewBag.CustomerList = new SelectList(customerListData.ToList(), "CoNo", "Coname", vm.CoNo ?? string.Empty);

            //廠商清單資料
            var vendorListData = from vendor10 in _context.Vendor10
                                 select new
                                 {
                                     VendorNo = vendor10.VendorNo,
                                     Vename = vendor10.VendorNo + string.Empty + vendor10.Vename
                                 };

            //廠商清單
            ViewBag.VendorList = new SelectList(vendorListData.ToList(), "VendorNo", "Vename", vm.VendorNo ?? string.Empty);

            //產品清單資料
            var stockListData = from stock10 in _context.Stock10
                                select new
                                {
                                    PartNo = stock10.PartNo,
                                    Spec = stock10.PartNo + string.Empty + stock10.Spec
                                };

            //產品清單
            ViewBag.StockList = new SelectList(stockListData.ToList(), "PartNo", "Spec", vm.StockNo ?? string.Empty);

            #endregion

            try
            {
                #region 進貨資料

                //進貨資料
                var allin10Data = await (from in10 in _context.In10
                                         join in20 in _context.In20 on in10.InNo equals in20.InNo
                                         join vendor in _context.Vendor10 on in10.VendorNo equals vendor.VendorNo //廠商資料
                                         join stock10 in _context.Stock10 on in20.PartNo equals stock10.PartNo //產品資料
                                         where in10.VendorNo == vm.VendorNo
                                         orderby in10.InNo descending
                                         select new
                                         {
                                             serNo = in20.Serno,
                                             inDate = in10.InDate,
                                             vendorNo = in10.VendorNo,
                                             veName = vendor.Vename,
                                             inNo = in10.InNo,
                                             partNo = stock10.PartNo,
                                             spec = stock10.Spec,
                                             qty = in20.Qty,
                                             unit = in20.Unit,
                                             price = in20.Price,
                                             discount = in20.Discount,
                                             amount = in20.Amount,
                                             memo = in20.Memo,
                                             stockPass = in10.StockPass
                                         }).Take(1000).ToListAsync();

                if (allin10Data != null)
                {
                    vm.in10DataList = await allin10Data.Select(x => new In10DataList
                    {
                        SerNo = x.serNo,
                        InDate = x.inDate,
                        VendorNo = x.vendorNo,
                        VeName = x.veName,
                        InNo = x.inNo,
                        PartNo = x.partNo,
                        Spec = x.spec,
                        Qty = x.qty,
                        Unit = x.unit,
                        Price = x.price,
                        Discount = x.discount,
                        Amount = x.amount,
                        Memo = x.memo,
                        StockPass = x.stockPass
                    }).ToListAsync();

                    //進貨資料 搜尋條件只有產品編號 廠商編號 日期範圍 有無過帳
                    //產品編號
                    if (!string.IsNullOrEmpty(vm.StockNo))
                    {
                        vm.in10DataList = await vm.in10DataList.Where(x => x.PartNo == vm.StockNo).ToListAsync();
                    }

                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        vm.in10DataList = await vm.in10DataList.Where(x => x.VendorNo == vm.VendorNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.in10DataList = await vm.in10DataList
                            .Where(x => int.Parse(x.InDate) >= int.Parse(bDate) && int.Parse(x.InDate) <= int.Parse(eDate)).ToListAsync();
                    }

                    //有無過帳
                    if (vm.OnlyShow)
                    {
                        vm.in10DataList = await vm.in10DataList.Where(x => x.StockPass == vm.OnlyShow.ToString()).ToListAsync();
                    }
                }

                #endregion 進貨資料

                #region 銷貨單資料

                //銷貨資料
                var allOutData = await (from out10 in _context.Out10
                                        join out20 in _context.Out20 on out10.OutNo equals out20.OutNo
                                        join cstm10 in _context.Cstm10 on out10.CoNo equals cstm10.CoNo
                                        join stock10 in _context.Stock10 on out20.PartNo equals stock10.PartNo
                                        orderby out10.OutNo descending
                                        select new
                                        {
                                            outDate = out10.OutDate,
                                            coNo = cstm10.CoNo,
                                            coName = cstm10.Coname,
                                            outNo = out10.OutNo,
                                            outType = out10.OutType,
                                            serNo = out20.Serno,
                                            partNo = stock10.PartNo,
                                            spec = stock10.Spec,
                                            qty = out20.Qty,
                                            unit = out20.Unit,
                                            price = out20.Price,
                                            discount = out20.Discount,
                                            amount = out20.Amount,
                                            memo = out20.Memo,
                                            stockPass = out10.StockPass
                                        }).Take(1000).ToListAsync();

                if (allOutData != null)
                {
                    vm.outDataList = await allOutData.Select(x => new OutDataList
                    {
                        OutDate = x.outDate,
                        CoNo = x.coNo,
                        CoName = x.coName,
                        OutNo = x.outNo,
                        OutType = x.outType,
                        SerNo = x.serNo,
                        PartNo = x.partNo,
                        Spec = x.spec,
                        Qty = x.qty,
                        Unit = x.unit,
                        Price = x.price,
                        Discount = x.discount,
                        Amount = x.amount,
                        Memo = x.memo,
                        StockPass = x.stockPass
                    }).ToListAsync();

                    //銷貨資料 搜尋條件只有客戶編號 產品編號 日期範圍 有無過帳
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.outDataList = await vm.outDataList.Where(x => x.CoNo == vm.CoNo).ToListAsync();
                    }

                    //產品編號
                    if (!string.IsNullOrEmpty(vm.StockNo))
                    {
                        vm.outDataList = await vm.outDataList.Where(x => x.PartNo == vm.StockNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.outDataList = await vm.outDataList
                            .Where(x => int.Parse(x.OutDate) >= int.Parse(bDate) && int.Parse(x.OutDate) <= int.Parse(eDate)).ToListAsync();
                    }

                    //有無過帳
                    if (vm.OnlyShow)
                    {
                        vm.outDataList = await vm.outDataList.Where(x => x.StockPass == vm.OnlyShow.ToString()).ToListAsync();
                    }
                }

                #endregion 銷貨單資料

                #region 應收帳款

                var allOGet10Data = await (from oGet10 in _context.OGet10
                                           join cstm10 in _context.Cstm10 on oGet10.CoNo equals cstm10.CoNo
                                           orderby oGet10.Paymonth descending
                                           select new
                                           {
                                               payMonth = oGet10.Paymonth,
                                               coNo = oGet10.CoNo,
                                               coName = cstm10.Coname,
                                               ntus = oGet10.Ntus,
                                               total0 = oGet10.Total0,
                                               total1 = oGet10.Total1,
                                               tax = oGet10.Tax,
                                               yesGet = oGet10.YesGet,
                                               subTot = oGet10.SubTot,
                                               notGet = oGet10.NotGet,
                                               lNotGet = oGet10.LnotGet,
                                               tNotGet = oGet10.TnotGet,
                                           }).Take(1000).ToListAsync();

                if (allOGet10Data != null)
                {
                    vm.oGetDataList = await allOGet10Data.Select(x => new OGetDataList
                    {
                        PayMonth = x.payMonth,
                        CoNo = x.coNo,
                        CoName = x.coName,
                        Ntus = x.ntus,
                        Total0 = x.total0,
                        Total1 = x.total1,
                        Tax = x.tax,
                        YesGet = x.yesGet,
                        SubTot = x.subTot,
                        NotGet = x.notGet,
                        LNotGet = x.lNotGet,
                        TNotGet = x.tNotGet,
                    }).ToListAsync();

                    //應收帳款 搜尋條件只有客戶編號 日期範圍
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.oGetDataList = await vm.oGetDataList.Where(x => x.CoNo == vm.CoNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "").Substring(0, 6);
                        var eDate = vm.EndDate.Replace("-", "").Substring(0, 6);

                        vm.oGetDataList = await vm.oGetDataList
                            .Where(x => int.Parse(x.PayMonth) >= int.Parse(bDate) && int.Parse(x.PayMonth) <= int.Parse(eDate)).ToListAsync();
                    }

                }

                #endregion

                #region 應付帳款

                var allIPay10Data = await (from iPay10 in _context.IPay10
                                           join vendor10 in _context.Vendor10 on iPay10.VendorNo equals vendor10.VendorNo
                                           orderby iPay10.Paymonth descending
                                           select new
                                           {
                                               payMonth = iPay10.Paymonth,
                                               vendorNo = vendor10.VendorNo,
                                               vename = vendor10.Vename,
                                               ntus = iPay10.Ntus,
                                               total0 = iPay10.Total0,
                                               total1 = iPay10.Total1,
                                               tax = iPay10.Tax,
                                               yesGet = iPay10.YesGet,
                                               subTot = iPay10.SubTot,
                                               notGet = iPay10.NotGet,
                                               lNotGet = iPay10.LnotGet,
                                               tNotGet = iPay10.TnotGet,
                                           }).Take(1000).ToListAsync();

                if (allIPay10Data != null)
                {
                    vm.iPayDataList = await allIPay10Data.Select(x => new IPayDataList
                    {
                        PayMonth = x.payMonth,
                        VendorNo = x.vendorNo,
                        VeName = x.vename,
                        Ntus = x.ntus,
                        Total0 = x.total0,
                        Total1 = x.total1,
                        Tax = x.tax,
                        YesGet = x.yesGet,
                        SubTot = x.subTot,
                        NotGet = x.notGet,
                        LNotGet = x.lNotGet,
                        TNotGet = x.tNotGet,
                    }).ToListAsync();

                    //應付帳款 搜尋條件只有廠商編號 日期範圍
                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        vm.iPayDataList = await vm.iPayDataList.Where(x => x.VendorNo == vm.VendorNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "").Substring(0, 6);
                        var eDate = vm.EndDate.Replace("-", "").Substring(0, 6);

                        vm.iPayDataList = await vm.iPayDataList
                            .Where(x => int.Parse(x.PayMonth) >= int.Parse(bDate) && int.Parse(x.PayMonth) <= int.Parse(eDate)).ToListAsync();
                    }

                }

                #endregion 應付帳款

                #region 報價資料

                var allBouData = await (from bou10 in _context.Bou10
                                        join bou20 in _context.Bou20 on bou10.QuNo equals bou20.QuNo
                                        join cstm10 in _context.Cstm10 on bou10.CoNo equals cstm10.CoNo
                                        join stock10 in _context.Stock10 on bou20.PartNo equals stock10.PartNo
                                        orderby bou10.QuDate descending
                                        select new
                                        {
                                            quDate = bou10.QuDate,
                                            coNo = cstm10.CoNo,
                                            company = cstm10.Company,
                                            quNo = bou10.QuNo,
                                            serNo = bou20.Serno,
                                            partNo = stock10.PartNo,
                                            spec = stock10.Spec,
                                            qty = bou20.Qty,
                                            unit = bou20.Unit,
                                            price = bou20.Price,
                                            discount = bou20.Discount,
                                            amount = bou20.Amount,
                                            memo = bou20.Memo,
                                        }).Take(1000).ToListAsync();

                if (allBouData != null)
                {
                    vm.bouDataList = await allBouData.Select(x => new BouDataList
                    {
                        QuDate = x.quDate,
                        CoNo = x.coNo,
                        Company = x.company,
                        QuNo = x.quNo,
                        SerNo = x.serNo,
                        PartNo = x.partNo,
                        Spec = x.spec,
                        Qty = x.qty,
                        Unit = x.unit,
                        Price = x.price,
                        Discount = x.discount,
                        Amount = x.amount,
                        Memo = x.memo,
                    }).ToListAsync();

                    //銷貨資料 搜尋條件只有客戶編號 產品編號 日期範圍
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.bouDataList = await vm.bouDataList.Where(x => x.CoNo == vm.CoNo).ToListAsync();
                    }

                    //產品編號
                    if (!string.IsNullOrEmpty(vm.StockNo))
                    {
                        vm.bouDataList = await vm.bouDataList.Where(x => x.PartNo == vm.StockNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.bouDataList = await vm.bouDataList
                            .Where(x => int.Parse(x.QuDate) >= int.Parse(bDate) && int.Parse(x.QuDate) <= int.Parse(eDate)).ToListAsync();
                    }
                }

                #endregion 報價資料

                #region 收款紀錄

                var allGCheckData = await (from gCheck10 in _context.Gcheck10
                                           join gCheck30 in _context.Gcheck30 on gCheck10.PayNo equals gCheck30.PayNo
                                           join cstm10 in _context.Cstm10 on gCheck10.CoNo equals cstm10.CoNo
                                           orderby gCheck10.PayDate descending
                                           select new
                                           {
                                               payDate = gCheck10.PayDate,
                                               coName = cstm10.Coname,
                                               payNo = gCheck10.PayNo,
                                               get_Type = gCheck10.GetType,
                                               payMonth = gCheck30.Paymonth,
                                               checkNo = gCheck10.CheckNo,
                                               dline = gCheck10.DLine,
                                               checkType = gCheck10.CheckType,
                                               pMoney = gCheck30.Pmoney,
                                               mMoney = gCheck30.Mmoney,
                                               ynPass = gCheck10.YnPass,
                                               coNo = cstm10.CoNo
                                           }).Take(1000).ToListAsync();

                if (allGCheckData != null)
                {
                    vm.gCheckDataList = await allGCheckData.Select(x => new GCheckDataList
                    {
                        PayDate = x.payDate,
                        CoName = x.coName,
                        PayNo = x.payNo,
                        Get_Type = x.get_Type,
                        PayMonth = x.payMonth,
                        CheckNo = x.checkNo,
                        Dline = x.dline,
                        CheckType = x.checkType,
                        PMoney = x.pMoney,
                        MMoney = x.mMoney,
                        YnPass = x.ynPass,
                        CoNo = x.coNo
                    }).ToListAsync();

                    //銷貨資料 搜尋條件只有客戶編號 日期範圍
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        vm.gCheckDataList = await vm.gCheckDataList.Where(x => x.CoNo == vm.CoNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.gCheckDataList = await vm.gCheckDataList
                            .Where(x => int.Parse(x.PayDate) >= int.Parse(bDate) && int.Parse(x.PayDate) <= int.Parse(eDate)).ToListAsync();
                    }
                }

                #endregion 收款紀錄

                #region 付款紀錄

                var allPCheckData = await (from pCheck10 in _context.Pcheck10
                                           join pCheck30 in _context.Pcheck30 on pCheck10.PayNo equals pCheck30.PayNo
                                           join vendor10 in _context.Vendor10 on pCheck10.VendorNo equals vendor10.VendorNo
                                           orderby pCheck10.PayDate descending
                                           select new
                                           {
                                               payDate = pCheck10.PayDate,
                                               venderNo = vendor10.VendorNo,
                                               veName = vendor10.Vename,
                                               payNo = pCheck10.PayNo,
                                               get_Type = pCheck10.GetType,
                                               payMonth = pCheck30.Paymonth,
                                               checkNo = pCheck10.CheckNo,
                                               dline = pCheck10.DLine,
                                               checkType = pCheck10.CheckType,
                                               pMoney = pCheck30.Pmoney,
                                               mMoney = pCheck30.Mmoney,
                                               ynPass = pCheck10.YnPass,
                                           }).Take(1000).ToListAsync();

                if (allPCheckData != null)
                {
                    vm.pCheckDataList = await allPCheckData.Select(x => new PCheckDataList
                    {
                        PayDate = x.payDate,
                        VeName = x.veName,
                        PayNo = x.payNo,
                        Get_Type = x.get_Type,
                        PayMonth = x.payMonth,
                        CheckNo = x.checkNo,
                        Dline = x.dline,
                        CheckType = x.checkType,
                        PMoney = x.pMoney,
                        MMoney = x.mMoney,
                        YnPass = x.ynPass,
                        VendorNo = x.venderNo
                    }).ToListAsync();

                    //銷貨資料 搜尋條件只有廠商編號 日期範圍
                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        vm.pCheckDataList = await vm.pCheckDataList.Where(x => x.VendorNo == vm.VendorNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.pCheckDataList = await vm.pCheckDataList
                            .Where(x => int.Parse(x.PayDate) >= int.Parse(bDate) && int.Parse(x.PayDate) <= int.Parse(eDate)).ToListAsync();
                    }
                }

                #endregion 付款紀錄

                #region 採購紀錄

                var allPurData = await (from pur10 in _context.Pur10
                                        join pur20 in _context.Pur20 on pur10.PurNo equals pur20.PurNo
                                        join vendor10 in _context.Vendor10 on pur10.VendorNo equals vendor10.VendorNo //廠商資料
                                        join stock10 in _context.Stock10 on pur20.PartNo equals stock10.PartNo //產品資料
                                        orderby pur10.PurDate descending
                                        select new
                                        {
                                            purDate = pur10.PurDate,
                                            vendorNo = vendor10.VendorNo,
                                            veName = vendor10.Vename,
                                            purNo = pur10.PurNo,
                                            serNo = pur20.Serno,
                                            partNo = pur20.PartNo,
                                            spec = stock10.Spec,
                                            qty = pur20.Qty,
                                            unit = pur20.Unit,
                                            price = pur20.Price,
                                            discount = pur20.Discount,
                                            inQty = pur20.InQty,
                                            memo = pur10.Memo
                                        }).Take(1000).ToListAsync();

                if (allPurData != null)
                {
                    vm.purDataList = await allPurData.Select(x => new PurDataList
                    {
                        PurDate = x.purDate,
                        VendorNo = x.vendorNo,
                        VeName = x.veName,
                        PurNo = x.purNo,
                        SerNo = x.serNo,
                        PartNo = x.partNo,
                        Spec = x.spec,
                        Qty = x.qty,
                        Unit = x.unit,
                        Price = x.price,
                        Discount = x.discount,
                        InQty = x.inQty,
                        Memo = x.memo
                    }).ToListAsync();

                    //銷貨資料 搜尋條件只有廠商編號 產品編號 日期範圍
                    //廠商編號
                    if (!string.IsNullOrEmpty(vm.VendorNo))
                    {
                        vm.purDataList = await vm.purDataList.Where(x => x.VendorNo == vm.VendorNo).ToListAsync();
                    }

                    //產品編號
                    if (!string.IsNullOrEmpty(vm.StockNo))
                    {
                        vm.purDataList = await vm.purDataList.Where(x => x.PartNo == vm.StockNo).ToListAsync();
                    }

                    //日期範圍
                    if (!string.IsNullOrEmpty(vm.BeginDate) && !string.IsNullOrEmpty(vm.EndDate))
                    {
                        var bDate = vm.BeginDate.Replace("-", "");
                        var eDate = vm.EndDate.Replace("-", "");

                        vm.purDataList = await vm.purDataList
                            .Where(x => int.Parse(x.PurDate) >= int.Parse(bDate) && int.Parse(x.PurDate) <= int.Parse(eDate)).ToListAsync();
                    }
                }

                #endregion 採購紀錄

                return View(vm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 刪除廠商資料
        /// </summary>
        /// <param name="vendorId">廠商代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string vendorNo)
        {
            try
            {
                if (string.IsNullOrEmpty(vendorNo))
                {
                    return NotFound();
                }

                var deleteData = await _context.Vendor10
                    .Where(x => x.VendorNo == vendorNo).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.Vendor10.Remove(deleteData);

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
        /// 匯出廠商資料
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
        /// 匯出廠商資料
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
