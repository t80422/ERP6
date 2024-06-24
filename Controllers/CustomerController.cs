using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using X.PagedList;
using ERP6.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ERP6.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EEPEF01Context _context;

        public CustomerController(EEPEF01Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 客戶首頁
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(CustomerIndexViewModel vm)
        {
            try
            {
                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", string.Empty);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", string.Empty);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", string.Empty);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", string.Empty);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", string.Empty);

                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", string.Empty);

                //付款銀行
                ViewBag.Paybank = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                var CSTMList = await _context.Cstm10.Select(x => new CustomerList
                {
                    CoNo = x.CoNo,
                    Coname = x.Coname ?? "",
                    Company = x.Company ?? "",
                    Invocomp = x.Invocomp ?? "",
                    Dlien = x.Dlien,
                    Fax = x.Fax ?? "",
                    PrintPrice = x.PrintPrice,
                    Boss = x.Boss ?? "",
                    Sales = x.Sales ?? "",
                    Memo = x.Memo ?? "",
                    TaxType = x.TaxType,
                    Tel1 = x.Tel1 ?? "",
                    Tel2 = x.Tel2 ?? "",
                    Uniform = x.Uniform ?? "",
                    Compaddr = x.Compaddr ?? "",
                    Sendaddr = x.Sendaddr ?? "",
                    Payaccount = x.Payaccount ?? "",
                    Paybank = x.Paybank,
                    Email = x.Email ?? "",
                    Www = x.Www ?? "",
                    Prenotget = x.Prenotget,
                    Total1 = x.Total1,
                    Tax = x.Tax,
                    Total2 = x.Total2,
                    YesGet = x.YesGet,
                    SubTot = x.SubTot,
                    NotGet = x.NotGet,
                    PeNo = x.PeNo,
                    Product = x.Product ?? "",
                    AreaNo = x.AreaNo,
                    Payment = x.Payment ?? "",
                    ChehkDay = x.ChehkDay,
                    Ntus = x.Ntus,
                    Taxrate = x.Taxrate,
                    Mobile = x.Mobile ?? "",
                    Discount = x.Discount,
                    CusType = x.CusType,
                    PriceType = x.PriceType,
                    DriveNo = x.DriveNo,
                    ParentId = x.ParentId ?? _context.Cstm10.Where(x => x.CoNo == x.ParentId).First().Coname,
                }).ToListAsync();

                //搜尋條件(客戶)
                if (vm.IsSearch)
                {
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                        CSTMList = await CSTMList.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();

                    //統一編號
                    if (!string.IsNullOrEmpty(vm.Uniform))
                        CSTMList = await CSTMList.Where(x => x.Uniform.Contains(vm.Uniform)).ToListAsync();

                    //客戶類別
                    if (!string.IsNullOrEmpty(vm.CusType))
                        CSTMList = await CSTMList.Where(x => x.CusType == vm.CusType).ToListAsync();

                    //客戶簡稱
                    if (!string.IsNullOrEmpty(vm.Coname))
                        CSTMList = await CSTMList.Where(x => x.Coname.Contains(vm.Coname)).ToListAsync();

                    //公司名稱
                    if (!string.IsNullOrEmpty(vm.Company))
                        CSTMList = await CSTMList.Where(x => x.Company.Contains(vm.Company)).ToListAsync();

                    //發票抬頭
                    if (!string.IsNullOrEmpty(vm.Invocomp))
                        CSTMList = await CSTMList.Where(x => x.Invocomp.Contains(vm.Invocomp)).ToListAsync();

                    //結帳日期
                    if (!string.IsNullOrEmpty(vm.Dlien))
                        CSTMList = await CSTMList.Where(x => x.Dlien == vm.Dlien).ToListAsync();

                    //電話一
                    if (!string.IsNullOrEmpty(vm.Tel1))
                        CSTMList = await CSTMList.Where(x => x.Tel1.Contains(vm.Tel1)).ToListAsync();

                    //負責人
                    if (!string.IsNullOrEmpty(vm.Boss))
                        CSTMList = await CSTMList.Where(x => x.Boss.Contains(vm.Boss)).ToListAsync();

                    //傳真
                    if (!string.IsNullOrEmpty(vm.Fax))
                        CSTMList = await CSTMList.Where(x => x.Fax.Contains(vm.Fax)).ToListAsync();

                    //負責業務
                    if (!string.IsNullOrEmpty(vm.PeNo))
                        CSTMList = await CSTMList.Where(x => x.PeNo == vm.PeNo).ToListAsync();

                    //電話二
                    if (!string.IsNullOrEmpty(vm.Tel2))
                        CSTMList = await CSTMList.Where(x => x.Tel2.Contains(vm.Tel2)).ToListAsync();

                    //聯絡人
                    if (!string.IsNullOrEmpty(vm.Sales))
                        CSTMList = await CSTMList.Where(x => x.Sales.Contains(vm.Sales)).ToListAsync();

                    //行動電話
                    if (!string.IsNullOrEmpty(vm.Mobile))
                        CSTMList = await CSTMList.Where(x => x.Mobile.Contains(vm.Mobile)).ToListAsync();

                    //司機
                    if (!string.IsNullOrEmpty(vm.DriveNo))
                        CSTMList = await CSTMList.Where(x => x.DriveNo == vm.DriveNo).ToListAsync();

                    //通訊地址
                    if (!string.IsNullOrEmpty(vm.Compaddr))
                        CSTMList = await CSTMList.Where(x => x.Compaddr.Contains(vm.Compaddr)).ToListAsync();

                    //稅別
                    if (!string.IsNullOrEmpty(vm.TaxType))
                        CSTMList = await CSTMList.Where(x => x.TaxType == vm.TaxType).ToListAsync();

                    //稅率
                    if (vm.Taxrate != null)
                        CSTMList = await CSTMList.Where(x => x.Taxrate == vm.Taxrate).ToListAsync();

                    //倉庫地址
                    if (!string.IsNullOrEmpty(vm.Sendaddr))
                        CSTMList = await CSTMList.Where(x => x.Sendaddr.Contains(vm.Sendaddr)).ToListAsync();

                    //區域類別
                    if (!string.IsNullOrEmpty(vm.AreaNo))
                        CSTMList = await CSTMList.Where(x => x.AreaNo == vm.AreaNo).ToListAsync();

                    //營業項目
                    if (!string.IsNullOrEmpty(vm.Product))
                        CSTMList = await CSTMList.Where(x => x.Product.Contains(vm.Product)).ToListAsync();

                    //單價列印
                    if (!string.IsNullOrEmpty(vm.PrintPrice))
                        CSTMList = await CSTMList.Where(x => x.PrintPrice == vm.PrintPrice).ToListAsync();

                    //售價類別
                    if (!string.IsNullOrEmpty(vm.PriceType))
                        CSTMList = await CSTMList.Where(x => x.PriceType == vm.PriceType).ToListAsync();

                    //現金扣%
                    if (vm.Discount != null)
                        CSTMList = await CSTMList.Where(x => x.Discount == vm.Discount).ToListAsync();

                    //計價幣別
                    if (!string.IsNullOrEmpty(vm.Ntus))
                        CSTMList = await CSTMList.Where(x => x.Ntus == vm.Ntus).ToListAsync();

                    //付款帳號
                    if (!string.IsNullOrEmpty(vm.Payaccount))
                        CSTMList = await CSTMList.Where(x => x.Payaccount.Contains(vm.Payaccount)).ToListAsync();

                    //付款銀行
                    if (!string.IsNullOrEmpty(vm.Paybank))
                        CSTMList = await CSTMList.Where(x => x.Paybank == vm.Paybank).ToListAsync();

                    //付款方式
                    if (!string.IsNullOrEmpty(vm.Payment))
                        CSTMList = await CSTMList.Where(x => x.Payment.Contains(vm.Payment)).ToListAsync();

                    //電子郵件
                    if (!string.IsNullOrEmpty(vm.Email))
                        CSTMList = await CSTMList.Where(x => x.Email.Contains(vm.Email)).ToListAsync();

                    //票期天數
                    if (vm.ChehkDay != null)
                        CSTMList = await CSTMList.Where(x => x.ChehkDay == vm.ChehkDay).ToListAsync();

                    //網址
                    if (!string.IsNullOrEmpty(vm.Www))
                        CSTMList = await CSTMList.Where(x => x.Www.Contains(vm.Www)).ToListAsync();

                    //備註
                    if (!string.IsNullOrEmpty(vm.Memo))
                        CSTMList = await CSTMList.Where(x => x.Memo.Contains(vm.Memo)).ToListAsync();
                }

                vm.customerList = await CSTMList.Select(x => new CSTMList
                {
                    CoName = x.Coname,
                    CoNo = x.CoNo
                }).ToListAsync();

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> IndexNotUse(CustomerIndexViewModel vm)
        {
            try
            {

                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", string.Empty);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", string.Empty);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", string.Empty);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", string.Empty);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", string.Empty);

                //售價類別
                //var priceTypeList = new List<SelectListItem>()
                //{
                //   new SelectListItem {Text="請選擇", Value="" },
                //   new SelectListItem {Text="批發價-北", Value="批發價" },
                //   new SelectListItem {Text="進貨付款-中", Value="門市價" },
                //   new SelectListItem {Text="進貨付款", Value="公教價" },
                //   new SelectListItem {Text="預設一", Value="預設一" },
                //   new SelectListItem {Text="預設二", Value="預設二" },
                //   new SelectListItem {Text="預設三", Value="預設三" },
                //   new SelectListItem {Text="預設四", Value="預設四" },
                //   new SelectListItem {Text="預設五", Value="預設五" },
                //};

                //ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", string.Empty);

                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);




                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", string.Empty);

                //付款銀行
                ViewBag.Paybank = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                if (!String.IsNullOrEmpty(vm.CoNo))
                {
                    vm = await _context.Cstm10.Where(x => x.CoNo == vm.CoNo).Select(x => new CustomerIndexViewModel
                    {
                        CoNo = x.CoNo,
                        Coname = x.Coname,
                        Company = x.Company,
                        Invocomp = x.Invocomp,
                        Dlien = x.Dlien,
                        Fax = x.Fax,
                        PrintPrice = x.PrintPrice,
                        Boss = x.Boss,
                        Sales = x.Sales,
                        Memo = x.Memo,
                        TaxType = x.TaxType,
                        Tel1 = x.Tel1,
                        Tel2 = x.Tel2,
                        Uniform = x.Uniform,
                        Compaddr = x.Compaddr,
                        Sendaddr = x.Sendaddr,
                        Payaccount = x.Payaccount,
                        Paybank = x.Paybank,
                        Email = x.Email,
                        Www = x.Www,
                        Prenotget = x.Prenotget,
                        Total1 = x.Total1,
                        Tax = x.Tax,
                        Total2 = x.Total2,
                        YesGet = x.YesGet,
                        SubTot = x.SubTot,
                        NotGet = x.NotGet,
                        PeNo = x.PeNo,
                        Product = x.Product,
                        AreaNo = x.AreaNo,
                        Payment = x.Payment,
                        ChehkDay = x.ChehkDay,
                        Ntus = x.Ntus,
                        Taxrate = x.Taxrate,
                        Mobile = x.Mobile,
                        Discount = x.Discount,
                        CusType = x.CusType,
                        PriceType = x.PriceType,
                        DriveNo = x.DriveNo,
                        ParentId = x.ParentId == null ? "" : (_context.Cstm10.Where(x => x.CoNo == x.ParentId).First() != null ? _context.Cstm10.Where(x => x.CoNo == x.ParentId).First().Coname : ""),
                        OldCoNo = x.CoNo
                    }).FirstOrDefaultAsync();
                }
                /////////////////////////////////////
                ///
                //取得客戶資料
                var CustomerList = await _context.Cstm10.Select(x => new CustomerList
                {
                    CoNo = x.CoNo,
                    Coname = x.Coname,
                    Company = x.Company,
                    Invocomp = x.Invocomp,
                    Dlien = x.Dlien,
                    Fax = x.Fax,
                    PrintPrice = x.PrintPrice,
                    Boss = x.Boss,
                    Sales = x.Sales,
                    Memo = x.Memo,
                    TaxType = x.TaxType,
                    Tel1 = x.Tel1,
                    Tel2 = x.Tel2,
                    Uniform = x.Uniform,
                    Compaddr = x.Compaddr,
                    Sendaddr = x.Sendaddr,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Email = x.Email,
                    Www = x.Www,
                    Prenotget = x.Prenotget,
                    Total1 = x.Total1,
                    Tax = x.Tax,
                    Total2 = x.Total2,
                    YesGet = x.YesGet,
                    SubTot = x.SubTot,
                    NotGet = x.NotGet,
                    PeNo = x.PeNo,
                    Product = x.Product,
                    AreaNo = x.AreaNo,
                    Payment = x.Payment,
                    ChehkDay = x.ChehkDay,
                    Ntus = x.Ntus,
                    Taxrate = x.Taxrate,
                    Mobile = x.Mobile,
                    Discount = x.Discount,
                    CusType = x.CusType,
                    PriceType = x.PriceType,
                    DriveNo = x.DriveNo,
                    ParentId = x.ParentId ?? _context.Cstm10.Where(x => x.CoNo == x.ParentId).First().Coname,
                }).ToListAsync();

                //搜尋條件(客戶)
                if (vm.IsSearch)
                {
                    //客戶編號
                    if (!string.IsNullOrEmpty(vm.CoNo))
                    {
                        CustomerList = await CustomerList.Where(x => x.CoNo.Contains(vm.CoNo)).ToListAsync();
                    }

                    //統一編號
                    if (!string.IsNullOrEmpty(vm.Uniform))
                    {
                        CustomerList = await CustomerList.Where(x => x.Uniform.Contains(vm.Uniform)).ToListAsync();
                    }

                    //客戶簡稱
                    if (!string.IsNullOrEmpty(vm.Coname))
                    {
                        CustomerList = await CustomerList.Where(x => x.Coname.Contains(vm.Coname)).ToListAsync();
                    }

                    //公司名稱
                    if (!string.IsNullOrEmpty(vm.Company))
                    {
                        CustomerList = await CustomerList.Where(x => x.Company.Contains(vm.Company)).ToListAsync();
                    }

                    //發票抬頭
                    if (!string.IsNullOrEmpty(vm.Invocomp))
                    {
                        CustomerList = await CustomerList.Where(x => x.Invocomp.Contains(vm.Invocomp)).ToListAsync();
                    }

                    //電話一
                    if (!string.IsNullOrEmpty(vm.Tel1))
                    {
                        CustomerList = await CustomerList.Where(x => x.Tel1.Contains(vm.Tel1)).ToListAsync();
                    }

                    //負責人
                    if (!string.IsNullOrEmpty(vm.Boss))
                    {
                        CustomerList = await CustomerList.Where(x => x.Boss.Contains(vm.Boss)).ToListAsync();
                    }

                    //傳真
                    if (!string.IsNullOrEmpty(vm.Fax))
                    {
                        CustomerList = await CustomerList.Where(x => x.Fax.Contains(vm.Fax)).ToListAsync();
                    }

                    //電話二
                    if (!string.IsNullOrEmpty(vm.Tel2))
                    {
                        CustomerList = await CustomerList.Where(x => x.Tel2.Contains(vm.Tel2)).ToListAsync();
                    }

                    //聯絡人
                    if (!string.IsNullOrEmpty(vm.Sales))
                    {
                        CustomerList = await CustomerList.Where(x => x.Sales.Contains(vm.Sales)).ToListAsync();
                    }

                    //行動電話
                    if (!string.IsNullOrEmpty(vm.Mobile))
                    {
                        CustomerList = await CustomerList.Where(x => x.Mobile.Contains(vm.Mobile)).ToListAsync();
                    }

                    //Email
                    if (!string.IsNullOrEmpty(vm.Email))
                    {
                        CustomerList = await CustomerList.Where(x => x.Email.Contains(vm.Email)).ToListAsync();
                    }
                }

                //vm.customerList = CustomerList;

                return View(vm);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add(bool? Back = false)
        {
            try
            {
                CustomerAddViewModel vm = new CustomerAddViewModel();

                vm.Back = (bool)Back;

                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", string.Empty);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", string.Empty);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", string.Empty);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", string.Empty);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", string.Empty);

                ////售價類別
                //var priceTypeList = new List<SelectListItem>()
                //{
                //   new SelectListItem {Text="請選擇", Value="" },
                //   new SelectListItem {Text="批發價-北", Value="批發價" },
                //   new SelectListItem {Text="進貨付款-中", Value="門市價" },
                //   new SelectListItem {Text="進貨付款", Value="公教價" },
                //   new SelectListItem {Text="預設一", Value="預設一" },
                //   new SelectListItem {Text="預設二", Value="預設二" },
                //   new SelectListItem {Text="預設三", Value="預設三" },
                //   new SelectListItem {Text="預設四", Value="預設四" },
                //   new SelectListItem {Text="預設五", Value="預設五" },
                //};

                //ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", string.Empty);

                //售價類別
                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", string.Empty);

                //付款銀行
                ViewBag.PaybankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                //取得客戶資料
                var CustomerList = await _context.Cstm10.Select(x => new CustomerList
                {
                    CoNo = x.CoNo,
                    Coname = x.Coname,
                    Company = x.Company,
                    Invocomp = x.Invocomp,
                    Dlien = x.Dlien,
                    Fax = x.Fax,
                    PrintPrice = x.PrintPrice,
                    Boss = x.Boss,
                    Sales = x.Sales,
                    Memo = x.Memo,
                    TaxType = x.TaxType,
                    Tel1 = x.Tel1,
                    Tel2 = x.Tel2,
                    Uniform = x.Uniform,
                    Compaddr = x.Compaddr,
                    Sendaddr = x.Sendaddr,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Email = x.Email,
                    Www = x.Www,
                    Prenotget = x.Prenotget,
                    Total1 = x.Total1,
                    Tax = x.Tax,
                    Total2 = x.Total2,
                    YesGet = x.YesGet,
                    SubTot = x.SubTot,
                    NotGet = x.NotGet,
                    PeNo = x.PeNo,
                    Product = x.Product,
                    AreaNo = x.AreaNo,
                    Payment = x.Payment,
                    ChehkDay = x.ChehkDay,
                    Ntus = x.Ntus,
                    Taxrate = x.Taxrate,
                    Mobile = x.Mobile,
                    Discount = x.Discount,
                    CusType = x.CusType,
                    PriceType = x.PriceType,
                    DriveNo = x.DriveNo
                }).ToListAsync();

                vm.customerList = CustomerList;

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="vm">CustomerAddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CustomerAddViewModel vm)
        {
            try
            {
                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", string.Empty);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", string.Empty);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", string.Empty);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", string.Empty);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", string.Empty);

                ////售價類別
                //var priceTypeList = new List<SelectListItem>()
                //{
                //   new SelectListItem {Text="請選擇", Value="" },
                //   new SelectListItem {Text="批發價-北", Value="批發價" },
                //   new SelectListItem {Text="進貨付款-中", Value="門市價" },
                //   new SelectListItem {Text="進貨付款", Value="公教價" },
                //   new SelectListItem {Text="預設一", Value="預設一" },
                //   new SelectListItem {Text="預設二", Value="預設二" },
                //   new SelectListItem {Text="預設三", Value="預設三" },
                //   new SelectListItem {Text="預設四", Value="預設四" },
                //   new SelectListItem {Text="預設五", Value="預設五" },
                //};

                //ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", string.Empty);

                //售價類別
                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", string.Empty);

                //付款銀行
                ViewBag.PaybankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                //檢查CoNo是否唯一
                var checkCoNo = _context.Cstm10.Where(x => x.CoNo == vm.CoNo).FirstOrDefault();

                if (checkCoNo != null)
                {
                    vm.IsTrue = false;
                    vm.ErrorMessage = "客戶編號已經存在，請重新編輯";

                    return View(vm);
                    //return Json(vm);
                }

                //TODO 新增所需資料(客戶)
                Cstm10 insertData = new Cstm10
                {
                    CoNo = vm.CoNo,
                    Uniform = vm.Uniform,
                    CusType = vm.CusType,
                    Company = vm.Company,
                    Coname = vm.Coname,
                    Invocomp = vm.Invocomp,
                    Dlien = vm.Dlien,
                    Tel1 = vm.Tel1,
                    Boss = vm.Boss,
                    Fax = vm.Fax,
                    PeNo = vm.PeNo,
                    Tel2 = vm.Tel2,
                    Sales = vm.Sales,
                    Mobile = vm.Mobile,
                    DriveNo = vm.DriveNo,
                    Compaddr = vm.Compaddr,
                    TaxType = vm.TaxType,
                    Taxrate = vm.Taxrate,
                    Sendaddr = vm.Sendaddr,
                    AreaNo = vm.AreaNo,
                    Product = vm.Product,
                    PrintPrice = vm.PrintPrice,
                    PriceType = vm.PriceType,
                    Discount = vm.Discount,
                    Ntus = vm.Ntus,
                    ChehkDay = vm.ChehkDay,
                    Payaccount = vm.Payaccount,
                    Paybank = vm.Paybank,
                    Email = vm.Email,
                    Www = vm.Www,
                    Memo = vm.Memo,
                };

                _context.Cstm10.Add(insertData);
                await _context.SaveChangesAsync();

                vm.IsTrue = true;

                return RedirectToAction("Index", vm);
                //return Json(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 編輯客戶資料
        /// </summary>
        /// <param name="CustomerId">客戶代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string CustomerId, bool? Back = false)
        {
            CustomerEditViewModel vm = new CustomerEditViewModel();
            vm.Back = (bool)Back;
            try
            {
                if (string.IsNullOrEmpty(CustomerId))
                {
                    return NotFound();
                }

                //廠商資料
                var customerData = _context.Cstm10
                    .Where(x => x.CoNo == CustomerId).FirstOrDefault();

                if (customerData == null)
                {
                    return NotFound();
                }

                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", customerData.AreaNo);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", customerData.PeNo);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", customerData.DriveNo);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", customerData.CusType);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", customerData.Dlien);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", customerData.TaxType);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", customerData.PrintPrice);

                ////售價類別
                //var priceTypeList = new List<SelectListItem>()
                //{
                //   new SelectListItem {Text="請選擇", Value="" },
                //   new SelectListItem {Text="批發價-北", Value="批發價" },
                //   new SelectListItem {Text="進貨付款-中", Value="門市價" },
                //   new SelectListItem {Text="進貨付款", Value="公教價" },
                //   new SelectListItem {Text="預設一", Value="預設一" },
                //   new SelectListItem {Text="預設二", Value="預設二" },
                //   new SelectListItem {Text="預設三", Value="預設三" },
                //   new SelectListItem {Text="預設四", Value="預設四" },
                //   new SelectListItem {Text="預設五", Value="預設五" },
                //};

                //ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", string.Empty);

                //售價類別
                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", customerData.Ntus);

                //付款銀行
                ViewBag.PayBankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", customerData.Paybank);

                #endregion

                vm.CoNo = customerData.CoNo;
                vm.Coname = customerData.Coname;
                vm.Company = customerData.Company;
                vm.Invocomp = customerData.Invocomp;
                vm.Dlien = customerData.Dlien;
                vm.Fax = customerData.Fax;
                vm.PrintPrice = customerData.PrintPrice;
                vm.Boss = customerData.Boss;
                vm.Sales = customerData.Sales;
                vm.Memo = customerData.Memo;
                vm.TaxType = customerData.TaxType;
                vm.Tel1 = customerData.Tel1;
                vm.Tel2 = customerData.Tel2;
                vm.Uniform = customerData.Uniform;
                vm.Compaddr = customerData.Compaddr;
                vm.Sendaddr = customerData.Sendaddr;
                vm.Payaccount = customerData.Payaccount;
                vm.Paybank = customerData.Paybank;
                vm.Email = customerData.Email;
                vm.Www = customerData.Www;
                vm.Prenotget = customerData.Prenotget;
                vm.Total1 = customerData.Total1;
                vm.Tax = customerData.Tax;
                vm.Total2 = customerData.Total2;
                vm.YesGet = customerData.YesGet;
                vm.SubTot = customerData.SubTot;
                vm.NotGet = customerData.NotGet;
                vm.PeNo = customerData.PeNo;
                vm.Product = customerData.Product;
                vm.AreaNo = customerData.AreaNo;
                vm.Payment = customerData.Payment;
                vm.ChehkDay = customerData.ChehkDay;
                vm.Ntus = customerData.Ntus;
                vm.Taxrate = customerData.Taxrate;
                vm.Mobile = customerData.Mobile;
                vm.Discount = customerData.Discount;
                vm.CusType = customerData.CusType;
                vm.PriceType = customerData.PriceType;
                vm.DriveNo = customerData.DriveNo;
                vm.ParentId = customerData.ParentId == null ? "" : (_context.Cstm10.Where(x => x.CoNo == customerData.ParentId).First() != null ? _context.Cstm10.Where(x => x.CoNo == customerData.ParentId).First().Coname : "");
                //vm.ParentId = customerData.ParentId ?? _context.Cstm10.Where(x => x.CoNo == customerData.ParentId).First().Coname;

                //廠商清單資料
                var customerList = await _context.Cstm10.Select(x => new CustomerList
                {
                    CoNo = x.CoNo,
                    Coname = x.Coname,
                    Company = x.Company,
                    Invocomp = x.Invocomp,
                    Dlien = x.Dlien,
                    Fax = x.Fax,
                    PrintPrice = x.PrintPrice,
                    Boss = x.Boss,
                    Sales = x.Sales,
                    Memo = x.Memo,
                    TaxType = x.TaxType,
                    Tel1 = x.Tel1,
                    Tel2 = x.Tel2,
                    Uniform = x.Uniform,
                    Compaddr = x.Compaddr,
                    Sendaddr = x.Sendaddr,
                    Payaccount = x.Payaccount,
                    Paybank = x.Paybank,
                    Email = x.Email,
                    Www = x.Www,
                    Prenotget = x.Prenotget,
                    Total1 = x.Total1,
                    Tax = x.Tax,
                    Total2 = x.Total2,
                    YesGet = x.YesGet,
                    SubTot = x.SubTot,
                    NotGet = x.NotGet,
                    PeNo = x.PeNo,
                    Product = x.Product,
                    AreaNo = x.AreaNo,
                    Payment = x.Payment,
                    ChehkDay = x.ChehkDay,
                    Ntus = x.Ntus,
                    Taxrate = x.Taxrate,
                    Mobile = x.Mobile,
                    Discount = x.Discount,
                    CusType = x.CusType,
                    PriceType = x.PriceType,
                    DriveNo = x.DriveNo
                }).ToListAsync();

                vm.customerList = customerList;

                return View(vm);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 編輯客戶資料
        /// </summary>
        /// <param name="vm">CustomerEditViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerEditViewModel vm)
        {
            try
            {
                #region SelectList

                //區域類別
                ViewBag.Area = new SelectList(_context.STO_AREA.Where(x => x.AREA_STATE == 1).ToList(), "AREA_NAME", "AREA_NAME", string.Empty);

                //業務人員
                ViewBag.BusinessList = new SelectList(_context.Pepo10.Where(x => x.Dep == "業務部").ToList(), "PeNo", "Name", string.Empty);

                //司機人員
                ViewBag.DriverList = new SelectList(_context.Pepo10.Where(x => x.Posi == "司機").ToList(), "PeNo", "Name", string.Empty);

                //客戶類別
                var orderStatsList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="銷付", Value="1" },
                   new SelectListItem {Text="進付", Value="2" },
                };

                ViewBag.CusTypeList = new SelectList(orderStatsList, "Value", "Text", string.Empty);

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

                ViewBag.DlienList = new SelectList(dlienList, "Value", "Text", string.Empty);

                //稅別
                var taxTypeList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="零稅", Value="0" },
                   new SelectListItem {Text="外加", Value="1" },
                   new SelectListItem {Text="內含", Value="2" },
                };

                ViewBag.TaxTypeList = new SelectList(taxTypeList, "Value", "Text", string.Empty);

                //單價列印
                var printPriceList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="是", Value="Y" },
                   new SelectListItem {Text="否", Value="N" },
                };

                ViewBag.PrintPriceList = new SelectList(printPriceList, "Value", "Text", string.Empty);

                ////售價類別
                //var priceTypeList = new List<SelectListItem>()
                //{
                //   new SelectListItem {Text="請選擇", Value="" },
                //   new SelectListItem {Text="批發價-北", Value="批發價" },
                //   new SelectListItem {Text="進貨付款-中", Value="門市價" },
                //   new SelectListItem {Text="進貨付款", Value="公教價" },
                //   new SelectListItem {Text="預設一", Value="預設一" },
                //   new SelectListItem {Text="預設二", Value="預設二" },
                //   new SelectListItem {Text="預設三", Value="預設三" },
                //   new SelectListItem {Text="預設四", Value="預設四" },
                //   new SelectListItem {Text="預設五", Value="預設五" },
                //};

                //ViewBag.PriceTypeList = new SelectList(priceTypeList, "Value", "Text", string.Empty);

                //售價類別
                ViewBag.PriceTypeList = new SelectList(_context.PriceType.OrderBy(x => x.PT_ID).ToList(), "PT_VALUE", "PT_TEXT", string.Empty);

                //計價幣別
                var ntusList = new List<SelectListItem>()
                {
                   new SelectListItem {Text="請選擇", Value="" },
                   new SelectListItem {Text="NTD", Value="NTD" },
                };

                ViewBag.NtusList = new SelectList(ntusList, "Value", "Text", string.Empty);

                //付款銀行
                ViewBag.PaybankList = new SelectList(_context.Phase20.Where(x => x.Whereuse == "付款銀行").ToList(), "Phase", "Phase", string.Empty);

                #endregion

                vm.IsTrue = false;

                if (vm != null)
                {
                    Cstm10 updateData = new Cstm10();

                    updateData.CoNo = vm.CoNo;
                    updateData.Coname = vm.Coname;
                    updateData.Company = vm.Company;
                    updateData.Invocomp = vm.Invocomp;
                    updateData.Dlien = vm.Dlien;
                    updateData.Fax = vm.Fax;
                    updateData.PrintPrice = vm.PrintPrice;
                    updateData.Boss = vm.Boss;
                    updateData.Sales = vm.Sales;
                    updateData.Memo = vm.Memo;
                    updateData.TaxType = vm.TaxType;
                    updateData.Tel1 = vm.Tel1;
                    updateData.Tel2 = vm.Tel2;
                    updateData.Uniform = vm.Uniform;
                    updateData.Compaddr = vm.Compaddr;
                    updateData.Sendaddr = vm.Sendaddr;
                    updateData.Payaccount = vm.Payaccount;
                    updateData.Paybank = vm.Paybank;
                    updateData.Email = vm.Email;
                    updateData.Www = vm.Www;
                    updateData.Prenotget = vm.Prenotget;
                    updateData.Total1 = vm.Total1;
                    updateData.Tax = vm.Tax;
                    updateData.Total2 = vm.Total2;
                    updateData.YesGet = vm.YesGet;
                    updateData.SubTot = vm.SubTot;
                    updateData.NotGet = vm.NotGet;
                    updateData.PeNo = vm.PeNo;
                    updateData.Product = vm.Product;
                    updateData.AreaNo = vm.AreaNo;
                    updateData.Payment = vm.Payment;
                    updateData.ChehkDay = vm.ChehkDay;
                    updateData.Ntus = vm.Ntus;
                    updateData.Taxrate = vm.Taxrate;
                    updateData.Mobile = vm.Mobile;
                    updateData.Discount = vm.Discount;
                    updateData.CusType = vm.CusType;
                    updateData.PriceType = vm.PriceType;
                    updateData.DriveNo = vm.DriveNo;

                    _context.Cstm10.Update(updateData);

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
        /// 刪除客戶資料
        /// </summary>
        /// <param name="CustomerId">客戶代碼</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string CustomerId)
        {
            try
            {
                if (string.IsNullOrEmpty(CustomerId))
                {
                    return NotFound();
                }

                var deleteData = await _context.Cstm10
                    .Where(x => x.CoNo == CustomerId).FirstOrDefaultAsync();

                if (deleteData != null)
                {
                    _context.Cstm10.Remove(deleteData);
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
        /// 匯出廠商資料
        /// </summary>
        /// <param name="vendorNo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Export(string StartCoNo, string EndCoNo)
        {
            try
            {
                #region SelectList

                //廠商清單資料
                var customerList = from cstm10 in _context.Cstm10
                                   select new
                                   {
                                       CoNo = cstm10.CoNo,
                                       Coname = cstm10.CoNo + string.Empty + cstm10.Coname
                                   };

                //廠商清單
                ViewBag.CustomerList = new SelectList(customerList.ToList(), "CoNo", "Coname", string.Empty);

                #endregion

                var customerListData = await _context.Cstm10.Select(x => new CustomerList
                {
                    CoNo = x.CoNo,
                    Coname = x.Coname,
                    Uniform = x.Uniform,
                    Fax = x.Fax,
                    Boss = x.Boss,
                    Sales = x.Sales,
                    Tel1 = x.Tel1,
                    Tel2 = x.Tel2,
                    Compaddr = x.Compaddr,
                    Sendaddr = x.Sendaddr,
                }).ToListAsync();

                if (customerListData == null)
                {
                    return View(new StockExportViewModel());
                }

                //第一筆
                var firstData = _context.Cstm10.OrderBy(x => x.CoNo).First().CoNo;

                //最後一筆
                var lastData = _context.Cstm10.OrderByDescending(x => x.CoNo).First().CoNo;

                if (string.IsNullOrEmpty(StartCoNo) && string.IsNullOrEmpty(EndCoNo))
                {
                    customerListData = await customerListData
                        .Where(x => x.CoNo.CompareTo(firstData) >= 0 && x.CoNo.CompareTo(lastData) <= 0).ToListAsync();
                }
                else if (string.IsNullOrEmpty(StartCoNo))
                {
                    customerListData = await customerListData
                        .Where(x => x.CoNo.CompareTo(firstData) >= 0 && x.CoNo.CompareTo(EndCoNo) <= 0).ToListAsync();
                }
                else if (string.IsNullOrEmpty(EndCoNo))
                {
                    customerListData = await customerListData
                        .Where(x => x.CoNo.CompareTo(StartCoNo) >= 0 && x.CoNo.CompareTo(lastData) <= 0).ToListAsync();
                }
                else
                {
                    customerListData = await customerListData
                        .Where(x => x.CoNo.CompareTo(StartCoNo) >= 0 && x.CoNo.CompareTo(EndCoNo) <= 0).ToListAsync();
                }

                StockExportViewModel vm = new StockExportViewModel();

                vm.customerList = customerListData;

                return View(vm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<string> ChangeCoNo(string CoNo, string NewCoNo)
        {
            if (!String.IsNullOrEmpty(CoNo) && !String.IsNullOrEmpty(NewCoNo))
            {
                var NewCustomer = await _context.Cstm10.Where(x => x.CoNo == NewCoNo).AnyAsync();

                if (NewCustomer) { return "客戶編號已存在、請重新輸入"; }

                var Customer = await _context.Cstm10.FindAsync(CoNo);

                if (Customer == null) { return "找無資料"; }

                try
                {
                    Customer.CoNo = NewCoNo;

                    _context.Cstm10.Add(Customer);
                    await _context.SaveChangesAsync();

                    Customer = await _context.Cstm10.FindAsync(CoNo);

                    _context.Cstm10.Remove(Customer);
                    await _context.SaveChangesAsync();


                    return "success";
                }
                catch (Exception ex)
                {
                    return "發生錯誤";
                }
            }

            return "資料不足";
        }

        #region 區域管理

        public async Task<IActionResult> Area(string AreaNo)
        {
            try
            {
                #region SelectList

                var areaListData = from stoArea in _context.STO_AREA
                                   select new
                                   {
                                       AreaId = stoArea.AREA_ID,
                                       AreaName = stoArea.AREA_ID + string.Empty + stoArea.AREA_NAME,
                                   };

                ViewBag.AreaList = new SelectList(areaListData.ToList(), "AreaId", "AreaName", string.Empty);

                #endregion

                //找出區域資料
                var areaData = await _context.STO_AREA.Select(x => new AreaList
                {
                    AREA_ID = x.AREA_ID,
                    AREA_NAME = x.AREA_NAME,
                    AREA_ORDER = x.AREA_ORDER
                }).ToListAsync();

                AreaIndexViewModel vm = new AreaIndexViewModel();

                if (!string.IsNullOrEmpty(AreaNo))
                {
                    areaData = await areaData.Where(x => x.AREA_NAME.Contains(AreaNo)).ToListAsync();
                }
                vm.areaLst = await areaData.OrderBy(x => x.AREA_ORDER).ToListAsync();

                return View(vm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> AreaEdit(string AreaNo, string AreaName)
        {
            try
            {
                var intAreaNo = 0;

                if (string.IsNullOrEmpty(AreaNo))
                {
                    return NotFound();
                }

                intAreaNo = int.Parse(AreaNo);

                var areaEditData = await _context.STO_AREA.Where(x => x.AREA_ID == intAreaNo).FirstOrDefaultAsync();

                if (areaEditData != null)
                {
                    areaEditData.AREA_NAME = AreaName;
                    areaEditData.AREA_TIME = DateTime.Now;
                    areaEditData.AREA_CODE = DateTime.Now.ToString("yyyyMMddhhmmss");
                }

                _context.STO_AREA.Update(areaEditData);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> AreaAdd(string AddAreaName)
        {
            try
            {
                var insertData = new STO_AREA
                {
                    AREA_NAME = AddAreaName,
                    AREA_TIME = DateTime.Now,
                    AREA_CODE = DateTime.Now.ToString("yyyyMMddhhmmss"),
                    AREA_ORDER = 0,
                    AREA_STATE = 1,
                };

                _context.STO_AREA.Add(insertData);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IActionResult> DelArea(string AreaNo)
        {
            try
            {
                var intAreaNo = 0;

                if (string.IsNullOrEmpty(AreaNo))
                {
                    return NotFound();
                }

                intAreaNo = int.Parse(AreaNo);

                var delData = await _context.STO_AREA.Where(x => x.AREA_ID == intAreaNo).FirstOrDefaultAsync();

                _context.STO_AREA.Remove(delData);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> OrderArea(string[] AreaNos)
        {
            try
            {
                for (var i = 0; i < AreaNos.Length; i++)
                {
                    var areano = Convert.ToInt32(AreaNos[i]);
                    var data = _context.STO_AREA.Where(x => x.AREA_ID == areano).FirstOrDefault();
                    data.AREA_ORDER = i + 1;
                }

                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch
            {
                throw;
            }
        }

        #endregion 區域管理

        #region 子公司

        public async Task<IActionResult> AddNewItem(CustomerIndexViewModel vm)
        {
            try
            {
                //檢查是否有母公司編號
                if (string.IsNullOrEmpty(vm.OldCoNo))
                {
                    return Json(0);
                }

                //檢查子公司編號是否重複
                var checkData = _context.Cstm10.Where(x => x.CoNo == vm.NewItemCoNo).Any();

                if (checkData)
                {
                    return Json(0);
                }

                var insertData = new Cstm10
                {
                    CoNo = vm.NewItemCoNo,
                    Uniform = vm.NewItemUniform,
                    CusType = vm.NewItemCusType,
                    Company = vm.NewItemCompany,
                    Coname = vm.NewItemConame,
                    Invocomp = vm.NewItemInvocomp,
                    Dlien = vm.NewItemDlien,
                    Tel1 = vm.NewItemTel1,
                    Boss = vm.NewItemBoss,
                    Fax = vm.NewItemFax,
                    PeNo = vm.NewItemPeNo,
                    Tel2 = vm.NewItemTel2,
                    Sales = vm.NewItemSales,
                    Mobile = vm.NewItemMobile,
                    DriveNo = vm.NewItemDriveNo,
                    Compaddr = vm.NewItemCompaddr,
                    TaxType = vm.NewItemTaxType,
                    Taxrate = vm.NewItemTaxrate,
                    Sendaddr = vm.NewItemSendaddr,
                    AreaNo = vm.NewItemAreaNo,
                    Product = vm.NewItemProduct,
                    PrintPrice = vm.NewItemPrintPrice,
                    PriceType = vm.NewItemPriceType,
                    Discount = vm.NewItemDiscount,
                    Ntus = vm.NewItemNtus,

                    Payaccount = vm.NewItemPayaccount,
                    Paybank = vm.NewItemPaybank,
                    Email = vm.NewItemEmail,
                    Www = vm.NewItemWww,
                    Memo = vm.NewItemMemo,
                    ParentId = vm.OldCoNo,
                };

                _context.Cstm10.Add(insertData);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #endregion 子公司
    }
}
