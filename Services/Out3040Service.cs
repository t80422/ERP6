using ERP6.Models;
using ERP6.Repositories;
using ERP6.Repositories.Customer;
using ERP6.Repositories.Stock10Repository;
using ERP6.ViewModels;
using ERP6.ViewModels.Out30VMs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Services
{
    public class Out3040Service : IOut3040Service
    {
        private IOut30Rep _out30Rep;
        private IOut40Rep _out40Rep;
        private IStock10Rep _stock10Rep;
        private ICustomerRep _cusRep;

        public Out3040Service(IOut30Rep out30Rep, IOut40Rep out40Rep, IStock10Rep stock10Rep, ICustomerRep customerRep)
        {
            _out30Rep = out30Rep;
            _out40Rep = out40Rep;
            _stock10Rep = stock10Rep;
            _cusRep = customerRep;
        }

        public async Task AddAsync(Out30VM vm)
        {
            using (var transaction = await _out30Rep.BeginTransactionAsync())
            {
                try
                {
                    await _out30Rep.AddAsync(vm.Out30);
                    await _out30Rep.SaveAsync();

                    foreach (var detail in vm.Out40List)
                    {
                        detail.Out40.CoNo = vm.Out30.CoNo;
                        detail.Out40.Paymonth = vm.Out30.Paymonth;
                        await _out40Rep.AddAsync(detail.Out40);
                    }

                    await _out40Rep.SaveAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(string coNo, string payMonth)
        {
            using (var transaction = await _out30Rep.BeginTransactionAsync())
            {
                try
                {
                    var out40List = await _out40Rep.GetByOut30(coNo, payMonth);

                    if (out40List.Any())
                        _out40Rep.DeleteRange(out40List);

                    await _out30Rep.DeleteAsync(coNo, payMonth);
                    await _out30Rep.SaveAsync();
                    await _out40Rep.SaveAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Cstm10>> GetAllCustomersAsync()
        {
            return await _cusRep.GetAllAsync();
        }

        public async Task<IEnumerable<Stock10>> GetAllProductsAsync()
        {
            return await _stock10Rep.GetAllAsync();
        }

        public async Task<IEnumerable<ConsignInventoryCheck>> GetConsignInventoryChecksDataAsync(ExportVM vm)
        {
            var rawData = await _out30Rep.GetOut30ReportFilter(vm);
            return rawData.Select(x => new ConsignInventoryCheck()
            {
                BillingMonth = x.Out30.Paymonth,
                CurrentInventory = x.Out40.StQty ?? 0,
                CusName = x.Cstm10.Coname,
                CusNo = x.Cstm10.CoNo,
                In = x.Out40.InQty ?? 0,
                PreInventory = x.Out40.LQty ?? 0,
                ProductName = x.Stock10.Spec,
                ProductNo = x.Stock10.PartNo,
                Return = x.Out40.InretQty ?? 0,
                Sale = x.Out40.OutQty ?? 0
            });
        }

        public async Task<IEnumerable<ConsignReconciliation>> GetConsignReconciliationsAsync(ExportVM vm)
        {
            var rawData = await _out30Rep.GetOut30ReportFilter(vm);
            return rawData.Select(x => new ConsignReconciliation()
            {
                BillingMonth = x.Out30.Paymonth,
                CurrentInventory = x.Out40.StQty ?? 0,
                CusName = x.Cstm10.Coname,
                CusNo = x.Cstm10.CoNo,
                In = x.Out40.InQty ?? 0,
                PreInventory = x.Out40.LQty ?? 0,
                Price = x.Out40.Amount ?? 0,
                ProductName = x.Stock10.Spec,
                ProductNo = x.Stock10.PartNo,
                Return = x.Out40.InretQty ?? 0,
                Sale = x.Out40.OutQty ?? 0,
                UnitPrice = x.Out40.Price,
                AmountNotCollected = x.Out30.NotGet ?? 0,
                CashDiscount = x.Out30.CashDis ?? 0,
                Discount = x.Out30.Discount ?? 0,
                Tax = x.Out30.Tax ?? 0,
                TaxAmount = x.Out30.Total1 ?? 0,
                TaxFreeAmount = x.Out30.Total0 ?? 0,
                Total = x.Out30.Total2 ?? 0
            });
        }

        public async Task<Out30> GetOut30ByIdAsync(string coNo, string payMonth)
        {
            return await _out30Rep.GetByIdAsync(coNo, payMonth);
        }

        public async Task<IEnumerable<Out30Detail>> GetOut40ListsByCoNoPaymonthAsync(string coNo, string payMonth)
        {
            return await _out40Rep.GetOut40DetailsByCoNoPaymonth(coNo, payMonth);
        }

        public async Task<Out40Stock10Dto> GetOut40Stock10Async(string coNo, string partNo, string paymonth)
        {
            try
            {
                var stock10 = await _stock10Rep.GetByIdAsync(partNo);

                if (stock10 == null)
                    return null;

                var data = new Out40Stock10Dto()
                {
                    Barcode = stock10.Barcode,
                    PartNo = stock10.PartNo,
                    Price = stock10.Price1,
                    Spec = stock10.Spec,
                    TaxType = stock10.TaxType,
                    Unit = stock10.Unit
                };

                var previousOut40 = await _out40Rep.GetLastPayMonthByCoNoPartNo(coNo, partNo, paymonth);
                double qty = 0;

                if (previousOut40 != null)
                    qty = previousOut40.StQty.Value;

                data.LQty = qty;
                data.StQty = qty;

                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Out30VM vm)
        {
            using (var transaction = await _out30Rep.BeginTransactionAsync())
            {
                try
                {
                    _out30Rep.Update(vm.Out30);

                    var existingOut40s = await _out40Rep.GetByOut30(vm.Out30.CoNo, vm.Out30.Paymonth);

                    if (existingOut40s != null && existingOut40s.Any())
                    {
                        _out40Rep.DeleteRange(existingOut40s);
                    }

                    foreach (var detail in vm.Out40List)
                    {
                        detail.Out40.CoNo = vm.Out30.CoNo;
                        detail.Out40.Paymonth = vm.Out30.Paymonth;
                        await _out40Rep.AddAsync(detail.Out40);
                    }
                    await _out30Rep.SaveAsync();
                    await _out40Rep.SaveAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateStockPass(Out30 out30)
        {
            _out30Rep.Update(out30);
            _out30Rep.SaveAsync();
        }
    }
}