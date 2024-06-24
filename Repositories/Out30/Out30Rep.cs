using ERP6.Models;
using ERP6.ViewModels.Out30VMs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Repositories
{
    public class Out30Rep:Repository<Out30>, IOut30Rep
    {

        public Out30Rep(EEPEF01Context context):base(context)
        {
        }

        public async Task DeleteAsync(string coNo, string payMonth)
        {
            var out30 = await GetByIdAsync(coNo, payMonth);
            if (out30 != null)
                _dbSet.Remove(out30);
        }

        public async Task<IEnumerable<Out30ReportFilter>> GetOut30ReportFilter(ExportVM vm)
        {
            // 提前過濾數據
            var out30Query = _context.Out30.AsQueryable();
            var out40Query = _context.Out40.AsQueryable();
            var stock10Query = _context.Stock10.AsQueryable();
            var cstm10Query = _context.Cstm10.AsQueryable();

            // 篩選客戶
            if (!string.IsNullOrEmpty(vm.CusNo_Start))
            {
                out30Query = out30Query.Where(x => x.CoNo.CompareTo(vm.CusNo_Start) >= 0);
                out40Query = out40Query.Where(x => x.CoNo.CompareTo(vm.CusNo_Start) >= 0);
                cstm10Query = cstm10Query.Where(x => x.CoNo.CompareTo(vm.CusNo_Start) >= 0);
            }

            if (!string.IsNullOrEmpty(vm.CusNo_End))
            {
                out30Query = out30Query.Where(x => x.CoNo.CompareTo(vm.CusNo_End) <= 0);
                out40Query = out40Query.Where(x => x.CoNo.CompareTo(vm.CusNo_End) <= 0);
                cstm10Query = cstm10Query.Where(x => x.CoNo.CompareTo(vm.CusNo_End) <= 0);
            }

            // 篩選商品
            if (!string.IsNullOrEmpty(vm.ProductNo_Start))
            {
                out40Query = out40Query.Where(x => x.PartNo.CompareTo(vm.ProductNo_Start) >= 0);
                stock10Query = stock10Query.Where(x => x.PartNo.CompareTo(vm.ProductNo_Start) >= 0);
            }

            if (!string.IsNullOrEmpty(vm.ProductNo_End))
            {
                out40Query = out40Query.Where(x => x.PartNo.CompareTo(vm.ProductNo_End) <= 0);
                stock10Query = stock10Query.Where(x => x.PartNo.CompareTo(vm.ProductNo_End) <= 0);
            }

            // 篩選低於庫存量
            if (vm.StockThreshold.HasValue)
            {
                out40Query = out40Query.Where(x => x.StQty <= vm.StockThreshold.Value);
            }

            // 篩選結帳月份
            if (!string.IsNullOrEmpty(vm.BillingMonthStart))
            {
                out30Query = out30Query.Where(x => x.Paymonth.CompareTo(vm.BillingMonthStart) >= 0);
                out40Query = out40Query.Where(x => x.Paymonth.CompareTo(vm.BillingMonthStart) >= 0);
            }

            if (!string.IsNullOrEmpty(vm.BillingMonthEnd))
            {
                out30Query = out30Query.Where(x => x.Paymonth.CompareTo(vm.BillingMonthEnd) <= 0);
                out40Query = out40Query.Where(x => x.Paymonth.CompareTo(vm.BillingMonthEnd) <= 0);
            }

            // 連接查詢
            var result = await (from out30 in out30Query
                                join out40 in out40Query on new { out30.CoNo, out30.Paymonth } equals new { out40.CoNo, out40.Paymonth }
                                join stock10 in stock10Query on out40.PartNo equals stock10.PartNo
                                join cstm10 in cstm10Query on out30.CoNo equals cstm10.CoNo
                                select new Out30ReportFilter
                                {
                                    Out30 = out30,
                                    Out40 = out40,
                                    Stock10 = stock10,
                                    Cstm10 = cstm10
                                })
                               .Distinct()
                               .ToListAsync();

            return result;
        }
    }
}