using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using Microsoft.AspNetCore.Http;
using ERP6.ViewModels.Ajax;
using NETCore.Encrypt;
using ERP6.ViewModels.OGet;
using Microsoft.AspNetCore.Hosting;
using NPOI.OpenXmlFormats.Dml;
using System.IO;

using iText.Html2pdf;
using iText.Layout.Font;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.IO.Font;

//// iText 1.7.18

namespace ERP6.Controllers
{
    public class AjaxsController : Controller
    {
        private readonly EEPEF01Context _context;
        private readonly IWebHostEnvironment _hostingEnviroment;
        public AjaxsController(EEPEF01Context context , IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        public async Task<byte[]> Export(Dictionary<string, string> Data, List<Dictionary<string, string>> DataList, string FilePath)
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                string HtmlTemp = ""; // html範本                
                string chMsjhFontPath = @"wwwroot/fonts/msjh.ttc,0"; // 微軟正黑體
                string arialFontPath = @"wwwroot/fonts/arial.ttf";
                string HtmlText = ""; // 每頁的html

                #region 讀取Html檔案

                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        HtmlTemp = sr.ReadToEnd();
                    }
                }

                #endregion 讀取Html檔案

                #region 範本帶入每張固定資料

                foreach (var it in Data)
                {
                    HtmlTemp = HtmlTemp.Replace(it.Key, it.Value);
                }

                #endregion 範本帶入每張固定資料

                using (MemoryStream ms = new MemoryStream())
                {
                    #region 宣告輸出PDF檔案和樣式

                    // 輸出的PDF
                    PdfDocument pdf = new PdfDocument(new PdfWriter(ms));
                    // 輸出的樣式
                    ConverterProperties converterProperties = new ConverterProperties();
                    FontProvider fontProvider = new DefaultFontProvider(false,false,false);
                    fontProvider.AddFont(FontProgramFactory.CreateFont(arialFontPath));
                    fontProvider.AddFont(FontProgramFactory.CreateFont(chMsjhFontPath));
                    converterProperties.SetFontProvider(fontProvider);

                    #endregion 宣告輸出PDF檔案和樣式

                    if (DataList != null && DataList.Count() > 0)
                    {
                        PdfMerger pdfMerger = new PdfMerger(pdf);

                        #region 迴圈資料(一圈一頁)

                        int page = 1;
                        foreach (var it in DataList)
                        {
                            HtmlText = HtmlTemp;

                            foreach (var item in it)
                            {
                                HtmlText = HtmlText.Replace(item.Key, item.Value);
                            }

                            HtmlText = HtmlText.Replace("$$C12$$", page.ToString());

                            MemoryStream baos = new MemoryStream();
                            PdfDocument temp = new PdfDocument(new PdfWriter(baos));
                            HtmlConverter.ConvertToPdf(HtmlText, temp, converterProperties);
                            ReaderProperties rp = new ReaderProperties();
                            baos = new MemoryStream(baos.ToArray());
                            temp = new PdfDocument(new PdfReader(baos, rp));
                            pdfMerger.Merge(temp, 1, temp.GetNumberOfPages());
                            temp.Close();
                            page++;
                        }

                        #endregion 迴圈資料(一圈一頁)

                        pdfMerger.Close();
                    }


                    return ms.ToArray();
                }
            }

            return null;
        }

        public async Task<byte[]> BatchExport(List<Dictionary<string, string>> DataLists,double sum)
        {
            string HtmlTemp = ""; // html範本                
            string chMsjhFontPath = @"wwwroot/fonts/msjh.ttc,0"; // 微軟正黑體
            string arialFontPath = @"wwwroot/fonts/arial.ttf";
            string HtmlText; // 每頁的html

            using (MemoryStream ms = new MemoryStream())
            {
                #region 宣告輸出PDF檔案和樣式

                // 輸出的PDF
                PdfDocument pdf = new PdfDocument(new PdfWriter(ms));

                // 輸出的樣式
                ConverterProperties converterProperties = new ConverterProperties();
                FontProvider fontProvider = new DefaultFontProvider(false, false, false);
                fontProvider.AddFont(FontProgramFactory.CreateFont(arialFontPath));
                fontProvider.AddFont(FontProgramFactory.CreateFont(chMsjhFontPath));
                converterProperties.SetFontProvider(fontProvider);

                #endregion 宣告輸出PDF檔案和樣式

                PdfMerger pdfMerger = new PdfMerger(pdf);

                int index = 0;
                int count = DataLists.Count;

                foreach (var dataList in DataLists)
                {
                    var filePath = dataList["filePath"];

                    #region 讀取Html檔案

                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            HtmlTemp = sr.ReadToEnd();
                        }
                    }

                    #endregion 讀取Html檔案

                    if (index == count - 1) // 檢查是否為最後一個元素
                    {
                        // 新增的 HTML 內容
                        string additionalHtml = $"<div class='item'><div>總計：</div><div>{string.Format("{0:N}", sum)}</div></div>";

                        // 使用特殊註解標識來確定插入位置
                        HtmlTemp = HtmlTemp.Replace("<!-- Insert Sum -->", additionalHtml);
                    }

                    int page = 1;
                    HtmlText = HtmlTemp;

                    #region 代入詳細資料

                    if (dataList != null && dataList.Count() > 0)
                    {
                        foreach (var item in dataList)
                        {
                            if (item.Key != "filePath")
                                HtmlText = HtmlText.Replace(item.Key, item.Value);
                        }
                    }

                    #endregion 代入詳細資料

                    HtmlText = HtmlText.Replace("$$C12$$", page.ToString());

                    MemoryStream baos = new MemoryStream();
                    PdfDocument temp = new PdfDocument(new PdfWriter(baos));
                    HtmlConverter.ConvertToPdf(HtmlText, temp, converterProperties);
                    ReaderProperties rp = new ReaderProperties();
                    baos = new MemoryStream(baos.ToArray());
                    temp = new PdfDocument(new PdfReader(baos, rp));
                    pdfMerger.Merge(temp, 1, temp.GetNumberOfPages());
                    temp.Close();
                    page++;
                    index++;
                }
                pdfMerger.Close();

                return ms.ToArray();

            }
        }

        public async Task<string> UploadFile(IFormFile file , string path)
        {
            if(file.Length > 0 && !String.IsNullOrEmpty(path))
            {
                path = $"{_hostingEnviroment.WebRootPath}/{path}";

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                string extension = System.IO.Path.GetExtension(file.FileName);
                string fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = System.IO.Path.Combine(path, fileName);

                while (System.IO.File.Exists(filePath))
                {
                    fileName = $"{Guid.NewGuid()}{extension}";
                    filePath = System.IO.Path.Combine(path, fileName);
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    return fileName;
                }
            }

            return "";
        }

        public void DeleteFile(string fileName , string path)
        {
            if(!String.IsNullOrEmpty(fileName) && !String.IsNullOrEmpty(path))
            {
                path = $"{_hostingEnviroment.WebRootPath}/{path}/{fileName}";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
        public JsonResult AjaxGetLastOut(string? CoNo , string? OutNo)
        {
            if (!String.IsNullOrEmpty(OutNo) && !String.IsNullOrEmpty(CoNo))
            {
                var result1 = _context.Out10.Where(x => x.OutNo == OutNo && x.CoNo == CoNo).Select(x => new {
                    OutNo = x.OutNo,
                    Memo = x.Memo,
                    PeNo = x.PeNo,
                    DriveNo = x.DriveNo,
                    Discount = x.Discount,
                    Dlien = _context.Cstm10.Find(CoNo) != null ? _context.Cstm10.Find(CoNo).Dlien : "",
                    Paymonth = !string.IsNullOrEmpty(x.Paymonth) ? DateTime.ParseExact(x.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : "",

                }).OrderByDescending(x => x.OutNo).FirstOrDefault();
                
                if (result1 != null)
                {
                    return Json(result1);
                }                
            }
            
            var CSTM10 = _context.Cstm10.Find(CoNo);
            var discount = CSTM10 != null ? CSTM10?.Discount : 0;

            var result = _context.Out10.Where(x => x.CoNo == CoNo && x.OutType == "1").Select(x => new {
                OutNo = x.OutNo,
                Memo = x.Memo,
                PeNo = x.PeNo ?? (CSTM10 != null ? CSTM10.PeNo : ""),
                DriveNo = x.DriveNo ?? (CSTM10 != null ? CSTM10.DriveNo : ""),
                Discount = CSTM10 != null ? CSTM10.Discount : x.Discount,
                Dlien = CSTM10 != null ? CSTM10.Dlien : "",
                Paymonth = "",

            }).OrderByDescending(x => x.OutNo).FirstOrDefault();

            return Json(result);


        }

        public JsonResult AjaxGetDiscount(string CoNo)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var cstm = _context.Cstm10.Find(CoNo);
            var lastOut = _context.Out10.Where(x => x.CoNo == CoNo).OrderByDescending(x => x.OutNo).FirstOrDefault();
            result.Add("discount", cstm != null ? cstm.Discount.ToString() : "");
            result.Add("memo", lastOut != null ? lastOut.Memo : "");
            return Json(result);
        }

        // GET: Ajaxs
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AjaxClientName(string id)
        {
            var data = _context.Cstm10.Where(x => x.AreaNo == id).ToList();
            return Json(data);
        }

        public ActionResult AjaxClientAddr(string id)
        {
            var data = _context.Cstm10.Where(x => x.CoNo == id).ToList();
            return Json(data);
        }

        public ActionResult AjaxOrderDet(string id, string outNo)
        {
            var stock10Data = _context.Stock10.Where(x => x.PartNo == id).FirstOrDefault();

            AjaxStock10ViewModel data = new AjaxStock10ViewModel
            {
                PartNo = stock10Data.PartNo,
                Spec = stock10Data.Spec,
                Unit = stock10Data.Unit,
                initQty2 = stock10Data.InitQty2,
                Atti = stock10Data.Atti,
                Price1 = stock10Data.Price1,
                SPrice1 = stock10Data.SPrice1,
            };

            //取出出貨單確定是否有促銷資料
            var out10Data = _context.Out10.Where(x => x.OutNo == outNo).FirstOrDefault();

            if (out10Data != null)
            {
                if (!string.IsNullOrEmpty(out10Data.Promotion_No))
                {
                    var stock20Data = _context.Stock21
                        .Where(x => x.SpNo == out10Data.Promotion_No && x.PartNo == id).FirstOrDefault();

                    if (stock20Data != null)
                    {
                        data.PPrice1 = stock20Data.Newprice;
                    }
                    else
                    {
                        data.PPrice1 = null;
                    }
                }
            }

            return Json(data);
        }

        //撈取業務人員資料
        public ActionResult AjaxBusiness(string id)
        {
            var data = _context.Pepo10.Where(x => x.PeNo == id).ToList();
            return Json(data);
        }

        //撈取司機人員資料
        public ActionResult AjaxDriver(string id)
        {
            var data = _context.Pepo10.Where(x => x.PeNo == id).FirstOrDefault();
            return Json(data);
        }

        public ActionResult AjaxVender(string vendorNo)
        {
            if (string.IsNullOrEmpty(vendorNo))
            {
                return Json(null);
            }

            var data = _context.Vendor10.Where(x => x.VendorNo == vendorNo).FirstOrDefault();

            if (data == null)
            {
                return Json(null);
            }

            return Json(data);
        }

        public ActionResult AjaxCustomer(string coNo)
        {
            if (string.IsNullOrEmpty(coNo))
            {
                return Json(null);
            }

            var data = _context.Cstm10.Where(x => x.CoNo == coNo).Select(x => new ERP6.ViewModels.Customer.CustomerIndexViewModel
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
            }).FirstOrDefault();

            if (data == null)
            {
                return Json(null);
            }

            return Json(data);
        }

        /// <summary>
        /// 撈取產品資料
        /// </summary>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public async Task<ActionResult> AjaxStock(string partNo)
        {
            var data = _context.Stock10.Where(x => x.PartNo == partNo).FirstOrDefault();

            //// 取得最後進貨資料
            var INData = await _context.In10.Join(_context.In20, x => x.InNo, y => y.InNo, (x, y) => new{
                InType = x.InType,
                PartNo = y.PartNo,
                InNo = x.InNo,
                LastIn = x.InDate,
                LastCost = y.Price
            }).Where(z => z.InType == "1" && z.PartNo == partNo).OrderByDescending(z => z.InNo).FirstOrDefaultAsync();

            //var INData = await _context.In10.Where(x => x.InType == "1").OrderByDescending(x => x.InNo).Select(x => x.InNo).Distinct().ToListAsync();
            //var IN20Data = await _context.In20.Where(x => INData.Contains(x.InNo) && x.PartNo == partNo).OrderByDescending(x => x.InNo).FirstOrDefaultAsync();
            //var In20List = _context.In20.Where(x => INData.Contains(x.InNo)).ToList().GroupBy(x => x.PartNo)
            //    .ToDictionary(y => y.Key, y => y.OrderByDescending(z => z.InNo).FirstOrDefault());

            if (data == null)
            {
                return Json(null);
            }

            data.Cost1 = data.Cost1 ?? 0;
            data.Cost2 = data.Cost2 ?? 0;
            data.Cost3 = data.Cost3 ?? 0;
            data.SalesCost = data.SalesCost ?? 0;
            data.Price1 = data.Price1 ?? 0;
            data.Price2 = data.Price2 ?? 0;
            data.Price3 = data.Price3 ?? 0;
            //data.LastCost = data.LastCost ?? 0;
            data.LastCost = INData != null ? (INData.LastCost ?? (data.LastCost ?? 0)) : (data.LastCost ?? 0);
            data.LastIn = INData != null ? (INData.LastIn ?? (data.LastIn ?? "")) : (data.LastIn ?? "");
            data.SafeQty = data.SafeQty ?? 0;
            data.StQty = data.StQty ?? 0;
            data.TranPara1 = data.TranPara1 ?? 0;
            data.TranPara2 = data.TranPara2 ?? 0;
            data.TranPara3 = data.TranPara3 ?? 0;
            data.SPrice1 = data.SPrice1 ?? 0;
            data.SPrice2 = data.SPrice2 ?? 0;
            data.SPrice3 = data.SPrice3 ?? 0;
            data.PackQty = data.PackQty ?? 0;
            data.InitQty1 = data.InitQty1 ?? 0;
            data.InitQty2 = data.InitQty2 ?? 0;
            data.InitCost1 = data.InitCost1 ?? 0;
            data.InitCost2 = data.InitCost2 ?? 0;
            data.InitCost3 = data.InitCost3 ?? 0;
            data.InitCost4 = data.InitCost4 ?? 0;
            data.InitCost5 = data.InitCost5 ?? 0;
            data.CompCost = data.CompCost ?? 0;
            data.L = data.L ?? 0;
            data.W = data.W ?? 0;
            data.H = data.H ?? 0;
            data.Cuft = data.Cuft ?? 0;
            data.SPrice4 = data.SPrice4 ?? 0;
            data.SPrice5 = data.SPrice5 ?? 0;
            data.SPrice6 = data.SPrice6 ?? 0;
            data.SPrice7 = data.SPrice7 ?? 0;
            data.SPrice8 = data.SPrice8 ?? 0;
            data.DefaultPrice1 = data.DefaultPrice1 ?? 0;
            data.DefaultPrice2 = data.DefaultPrice2 ?? 0;
            data.DefaultPrice3 = data.DefaultPrice3 ?? 0;
            data.DefaultPrice4 = data.DefaultPrice4 ?? 0;
            data.DefaultPrice5 = data.DefaultPrice5 ?? 0;

            data.IsReturn = data.IsReturn;

            data.ImageName = data.ImageName ?? "";
            data.ImagePath = !String.IsNullOrEmpty(data.ImagePath) ? $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/image/stock10/{data.ImagePath}"
                            : $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/image/imgfile.png";                

            if (!string.IsNullOrEmpty(data.LastOut))
            {
                data.LastOut = DateTime.ParseExact(data.LastOut, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(data.LastIn))
            {
                data.LastIn = DateTime.ParseExact(data.LastIn, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(data.LastModidate))
            {
                data.LastModidate = DateTime.ParseExact(data.LastModidate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            }

            //ViewBag.GPM1 = ((data.SPrice1 ?? 0 - data.Price1 ?? 0) - (data.Price1 ?? 0)) / data.SPrice1 ?? 0 * 100;
            //ViewBag.GPM2 = ((data.SPrice2 ?? 0 - data.Price2 ?? 0) - (data.Price2 ?? 0)) / data.SPrice2 ?? 0 * 100;
            //ViewBag.GPM3 = ((data.SPrice3 ?? 0 - data.Price3 ?? 0) - (data.Price3 ?? 0)) / data.SPrice3 ?? 0 * 100;

            return Json(data);
        }

        public ActionResult AjaxChangePassword(string userId, string pwd)
        {
            var checkData = false;

            if (string.IsNullOrEmpty(pwd))
            {
                return Json(checkData);
            }

            var userData = _context.Users.Where(x => x.Userid == userId).FirstOrDefault();

            if (userData == null)
            {
                return Json(checkData);
            }

            var encryptPwd = EncryptPwd(pwd);

            userData.NewPwd = encryptPwd;
            checkData = true;

            _context.SaveChanges();

            return Json(checkData);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string EncryptPwd(string pwd)
        {
            //SHA256
            var hashed = EncryptProvider.Sha256(pwd);

            return hashed;
        }

        /// <summary>
        /// 取得會計代碼資料
        /// </summary>
        /// <param name="AccNo">會計代碼</param>
        /// <returns></returns>
        public ActionResult AjaxAccNo(string AccNo)
        {
            try
            {
                var accNoData = _context.Accno10.Where(x => x.Accno == AccNo).FirstOrDefault();

                if (accNoData == null)
                {
                    return NotFound();
                }

                return Json(accNoData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 取得應收帳款資料檔資料
        /// </summary>
        /// <param name="PayMonthNo">帳款月份</param>
        /// <param name="CoNo">客戶編號</param>
        /// <returns></returns>
        public ActionResult AjaxOGet(string PayMonthNo, string CoNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(PayMonthNo))
                {
                    PayMonthNo = PayMonthNo.Replace("/", "");
                }

                //取得資料
                var oGetData = (from oGet10 in _context.OGet10
                                join cstm10 in _context.Cstm10 on oGet10.CoNo equals cstm10.CoNo
                                where oGet10.Paymonth == PayMonthNo && oGet10.CoNo == CoNo
                                select new OGetList
                                {
                                    Paymonth = DateTime.ParseExact(oGet10.Paymonth, "yyyyMM", null).ToString("yyyy-MM"),
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

                if (oGetData == null)
                {
                    return NotFound();
                }

                return Json(oGetData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AjaxBank10(string BankNo)
        {
            var data = (from bank10 in _context.Bank10
                        where bank10.BankNo == BankNo
                        select new
                        {
                            bankNo = bank10.BankNo,
                            bankName = bank10.BankName,
                            ntus = bank10.Ntus,
                            InitAmount = bank10.Initamount,
                            InitDate = string.IsNullOrEmpty(bank10.Initdate) ? string.Empty : DateTime.ParseExact(bank10.Initdate, "yyyyMMdd", null).ToString("yyyy-MM-dd"),
                            AccNo = bank10.Accno,
                            AccName = string.IsNullOrEmpty(bank10.Accno) ? string.Empty : _context.Accno10
                            .Where(x => x.Accno == bank10.Accno).FirstOrDefault().Accname,
                        }).FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Json(data);
        }

        #region EasyUI 相關

        public ActionResult AjaxCustomerForEasyUI()
        {
            var data = (from cstm10 in _context.Cstm10
                        select new
                        {
                            id = cstm10.CoNo,
                            text = cstm10.CoNo.ToLower() + cstm10.Coname.ToLower(),
                        }).ToList();

            if (data == null)
                return Json(null);

            //var aaa = data.Where(x => x.id == "91000").FirstOrDefault();

             return Json(data);
        }

        public ActionResult AjaxAccNoForEasyUI()
        {
            var data = (from accNo10 in _context.Accno10
                        select new
                        {
                            id = accNo10.Accno,
                            text = accNo10.Accno + string.Empty + accNo10.Accname,
                        }).ToList();

            if (data == null)
            {
                return Json(null);
            }

            return Json(data);
        }

        public ActionResult AjaxBank10ForEasyUI()
        {
            var data = (from bank10 in _context.Bank10
                        select new
                        {
                            id = bank10.BankNo,
                            text = bank10.BankNo + string.Empty + bank10.BankName,
                        }).ToList();

            if (data == null)
            {
                return Json(null);
            }

            return Json(data);
        }

        /// <summary>
        /// 廠商資料
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxVendorForEasyUI()
        {
            var data = (from vendor10 in _context.Vendor10
                        select new
                        {
                            id = vendor10.VendorNo,
                            text = vendor10.VendorNo + string.Empty + vendor10.Vename,
                        }).ToList();

            return Json(data);
        }

        /// <summary>
        /// 採購人員資料
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxPepoForEasyUI()
        {
            var data = (from peop10 in _context.Pepo10
                        select new
                        {
                            id = peop10.PeNo,
                            text = peop10.PeNo + string.Empty + peop10.Name + string.Empty + peop10.Dep,
                        }).ToList();

            return Json(data);
        }

        /// <summary>
        /// 表尾設定資料
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxMemoForEasyUI()
        {
            var data = (from memo10 in _context.Memo10
                        select new
                        {
                            id = memo10.Progno,
                            text = memo10.Progno + string.Empty + memo10.Whereuse,
                        }).ToList();

            return Json(data);
        }

        /// <summary>
        /// 備註資料
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxCustomerMemoForEasyUI()
        {
            var data = (from phase20 in _context.Phase20
                        where phase20.Whereuse == "明細備註"
                        select new
                        {
                            id = phase20.Phase,
                            text = phase20.Phase,
                        })
                        .ToList();

            return Json(data);
        }

        /// <summary>
        /// 產品設定資料
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxStockForSelected(string CONO , string PARTNO)
        {
            var aaa = new DateTime();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Start();

            var result = new Dictionary<string, dynamic>();

            if(!String.IsNullOrEmpty(CONO) && !String.IsNullOrEmpty(PARTNO))
            {
                // 找到客戶資訊
                var CSTM = _context.Cstm10.Find(CONO);
                // 今日日期
                var TODAY =DateTime.Now;
                // 找到產品資訊
                var STOCK = _context.Stock10.Find(PARTNO);
                double? RESULT_PRICE = 0;

                // 4. 牌價 和 建議售價
                #region 牌價 和 建議售價

                var CSTM_PRICETYPE = CSTM?.PriceType;
                var STOCKCOL = _context.PriceType.Where(x => x.PT_VALUE == CSTM_PRICETYPE).FirstOrDefault()?.PT_STOCKCOL;

                switch (STOCKCOL)
                {
                    case "Price1":
                        RESULT_PRICE = STOCK?.Price1;
                        result.Add("SPrice", STOCK?.SPrice1);
                        break;
                    case "Price2":
                        RESULT_PRICE = STOCK?.Price2;
                        result.Add("SPrice", STOCK?.SPrice2);
                        break;
                    case "Price3":
                        RESULT_PRICE = STOCK?.Price3;
                        result.Add("SPrice", STOCK?.SPrice3);
                        break;
                    case "DefaultPrice1":
                        RESULT_PRICE = STOCK?.DefaultPrice1;
                        result.Add("SPrice", STOCK?.SPrice4);
                        break;
                    case "DefaultPrice2":
                        RESULT_PRICE = STOCK?.DefaultPrice2;
                        result.Add("SPrice", STOCK?.SPrice5);
                        break;
                    case "DefaultPrice3":
                        RESULT_PRICE = STOCK?.DefaultPrice3;
                        result.Add("SPrice", STOCK?.SPrice6);
                        break;
                    case "DefaultPrice4":
                        RESULT_PRICE = STOCK?.DefaultPrice4;
                        result.Add("SPrice", STOCK?.SPrice7);
                        break;
                    case "DefaultPrice5":
                        RESULT_PRICE = STOCK?.DefaultPrice5;
                        result.Add("SPrice", STOCK?.SPrice8);
                        break;
                    default:
                        RESULT_PRICE = 0;
                        break;
                }

                #endregion 牌價 和 建議售價

                // 加入牌價
                result.Add("PPrice", RESULT_PRICE);

                // 1. 促銷價
                #region 促銷價

                // 找到在促銷期間並且有此產品的促銷資料
                var STOCK20 = _context.Stock20.ToList().Join(_context.Stock21.ToList(), x => x.SpNo, y => y.SpNo, (x, y) => new
                {
                    SPNO = x.SpNo,
                    CONO = x.CoNo,
                    BDATE = !String.IsNullOrEmpty(x.Bdate) ? DateTime.ParseExact(x.Bdate, "yyyyMMdd", null) : new DateTime(),
                    EDATE = !String.IsNullOrEmpty(x.Edate) ? DateTime.ParseExact(x.Edate, "yyyyMMdd", null) : new DateTime(),
                    PARTNO = y.PartNo,
                    PRICE = y.Newprice,
                }).Where(z => z.CONO == CONO && z.PARTNO == PARTNO 
                && TODAY.Date >= z.BDATE.Date
                && TODAY.Date <= z.EDATE.Date
                //&& TODAY.CompareTo(!string.IsNullOrEmpty(z.BDATE) ? DateTime.ParseExact(z.BDATE, "yyyyMMdd", null) : new DateTime()) >= 0
                //&& TODAY.CompareTo(!string.IsNullOrEmpty(z.EDATE) ? DateTime.ParseExact(z.EDATE, "yyyyMMdd", null) : new DateTime()) < 0
                )
                .OrderByDescending(z => z.SPNO).FirstOrDefault();
                //.Where(z => z.CONO == CONO && TODAY >= z.BDATE && TODAY <= z.EDATE && z.PARTNO == PARTNO)
                #endregion 促銷價

                if (STOCK20 != null) { result.Add("Price", STOCK20?.PRICE ?? 0);return Json(result); }

                sw.Start();

                // 2. 報價單 - 先獲得 不在促銷期間的歷史價格(約定價格)
                #region 不在促銷期間的歷史價格(約定價格)

                // out10
                var OUT10 = _context.Out10.Where(x => x.CoNo == CONO && x.OutType == "1").Select(x => new { CoNo = x.CoNo, OutNo = x.OutNo, OutDate = x.OutDate }).ToList();
                //找出在促銷期間單號
                var SalesHistory_OUTNO = OUT10.Join(_context.Stock20.ToList(), x => x.CoNo, y => y.CoNo, (x, y) => new
                {
                    OUTNO = x.OutNo,
                    OUTDATE = !string.IsNullOrEmpty(x.OutDate) ? DateTime.ParseExact(x.OutDate,"yyyyMMdd",null) : new DateTime(),
                    BDATE = !string.IsNullOrEmpty(y.Bdate) ? DateTime.ParseExact(y.Bdate, "yyyyMMdd", null) : new DateTime(),
                    EDATE = !string.IsNullOrEmpty(y.Edate) ? DateTime.ParseExact(y.Edate, "yyyyMMdd", null) : new DateTime(),
                }).Where(z=> z.BDATE.Date <= z.OUTDATE.Date && z.EDATE.Date >= z.OUTDATE.Date).Select(z => z.OUTNO).ToList();

                //找出不在促銷期間的並且有此產品的最新單號和價格
                var OUT20 = _context.Out20.Where(x => !SalesHistory_OUTNO.Contains(x.OutNo) && x.PartNo == PARTNO).ToList();
                
                var HISTORY_PART = OUT20.Join(OUT10, x => x.OutNo, y => y.OutNo, (x, y) => new { PRICE = x.Price, CONO = y.CoNo, OUTNO = x.OutNo }).OrderByDescending(x => x.OUTNO).FirstOrDefault();

                #endregion 不在促銷期間的歷史價格(約定價格)

                sw.Stop();
                sw.Reset();

                // 3. 報價單(如果建立時間較歷史新才優先於歷史)
                #region 報價單(如果建立時間較歷史新才優先於歷史)

                // 獲取歷史訂單的建立時間
                var HISTORY_OUTTIME = !String.IsNullOrEmpty(_context.Out10.Find(HISTORY_PART?.OUTNO)?.OutDate) ? DateTime.ParseExact(_context.Out10.Find(HISTORY_PART?.OUTNO)?.OutDate, "yyyyMMdd", null) : new DateTime();
                // 先獲取比歷史訂單新且有此產品的報價單
                var BOUDATA = _context.Bou10.ToList().Join(_context.Bou20.ToList(), x => x.QuNo, y => y.QuNo, (x, y) => new
                {
                    QUNO = x.QuNo,
                    PARTNO = y.PartNo,
                    CONO = x.CoNo,
                    QUDATE = !String.IsNullOrEmpty(x.QuDate) ? DateTime.ParseExact(x.QuDate, "yyyyMMdd", null) : new DateTime(),
                    SENDDATE = !String.IsNullOrEmpty(x.SendDate) ? DateTime.ParseExact(x.SendDate, "yyyyMMdd", null) : new DateTime(),
                    PRICE = y.Price
                }).Where(z => z.CONO == CONO && z.SENDDATE.Date <= TODAY.Date && z.PARTNO == PARTNO && z.QUDATE.Date >= HISTORY_OUTTIME.Date).OrderByDescending(x => x.QUNO).FirstOrDefault();

                #endregion 報價單(如果建立時間較歷史新才優先於歷史)

                // 加入價格 如果有新的報價單先加入
                if (BOUDATA != null) { result.Add("Price", Convert.ToDouble(BOUDATA.PRICE)); return Json(result); }
                if (HISTORY_PART != null) { result.Add("Price", Convert.ToDouble(HISTORY_PART.PRICE)); return Json(result); }

                // 都沒有就加入牌價
                result.Add("Price", RESULT_PRICE);

                return Json(result);
            }
            return null;
        }

        public ActionResult AjaxStockForEasyUI(string CoNo)
        {
            var result = new List<Dictionary<string, dynamic>>();

            var stockTypes = _context.StockTypes.Where(x => x.TYPE_ISOPEN).OrderBy(x => x.TYPE_ORDER).Select(x=>x.TYPE_NAME).ToList();

            var LASTOUT = _context.Out10.Where(x => x.CoNo == CoNo && x.OutType == "1").Select(x => x.OutNo).ToList();

            var OUT20 = _context.Out20.Where(z => LASTOUT.Contains(z.OutNo)).ToList();

            var lastout_detail = _context.Out20.Join(_context.Out10.Where(x => x.CoNo == CoNo && x.OutType == "1")
                    , x => x.OutNo, y => y.OutNo, (x, y) => new { PartNo = x.PartNo , Qty = x.Qty }).Where(x=>x.Qty > 0).Select(x => x.PartNo).Distinct().ToList();

            var STOCKS = _context.Stock10.Where(x => stockTypes.Contains(x.Type) && x.IsOpen == true && x.IsShow == true).Select(x=>new 
            {
                id = x.PartNo ,
                text = x.PartNo + x.Spec + x.Barcode ,
                spec = x.Spec ,
                unit = x.Unit ,
                taxType = x.TaxType,
                discount = x.LastDiscount,
                memo = "",
                stQty = x.StQty,
                kg = x.InitQty1,
                qty = x.PackQty,
            }).ToList();

            foreach (var STOCK in STOCKS)
            {
                var re = new Dictionary<string, dynamic>();

                var out20_detail = OUT20.Where(z => z.PartNo == STOCK.id).OrderByDescending(z => z.OutNo).FirstOrDefault();

                if (STOCK.id == "200591")
                {
                    var aaa = 0;
                };
                                
                re.Add("id", STOCK.id);
                re.Add("text", STOCK.text);
                re.Add("spec", STOCK.spec);
                re.Add("unit", STOCK.unit);
                re.Add("taxType", STOCK.taxType);
                re.Add("discount", out20_detail != null && out20_detail.Discount != null ? out20_detail.Discount : STOCK.discount);
                re.Add("memo", out20_detail != null ? out20_detail.Memo : STOCK.memo);
                re.Add("stQty", STOCK.stQty);
                re.Add("sales", out20_detail != null ? !String.IsNullOrEmpty(out20_detail.IsPromise) : false);
                re.Add("status", lastout_detail.Contains(STOCK.id));
                re.Add("kg", STOCK.kg);
                re.Add("qty", STOCK.qty ?? 0);
                result.Add(re);
            }
            return Json(result);

        }

        /// <summary>
        /// 業務人員
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxSalesForEasyUI()
        {
            var data = (from pepo10 in _context.Pepo10
                        select new
                        {
                            id = pepo10.PeNo,
                            text = pepo10.PeNo + string.Empty + pepo10.Name,
                        })
                        .ToList();

            return Json(data);
        }

        /// <summary>
        /// 司機人員
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxDriveNoForEasyUI()
        {
            var data = (from pepo10 in _context.Pepo10
                        where pepo10.Posi == "司機"
                        select new
                        {
                            id = pepo10.PeNo,
                            text = pepo10.PeNo + string.Empty + pepo10.Name,
                        })
                        .ToList();

            return Json(data);
        }

        #endregion EasyUI 相關
    }
}