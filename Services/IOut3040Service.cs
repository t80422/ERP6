using ERP6.Models;
using ERP6.ViewModels;
using ERP6.ViewModels.Out30VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Services
{
    public interface IOut3040Service
    {
        Task AddAsync(Out30VM vm);
        Task UpdateAsync(Out30VM vm);
        Task DeleteAsync(string coNo, string payMonth);
        Task<Out40Stock10Dto> GetOut40Stock10Async(string coNo, string partNo,string paymonth);
        Task<Out30> GetOut30ByIdAsync(string coNo, string payMonth);
        Task<IEnumerable<Out30Detail>> GetOut40ListsByCoNoPaymonthAsync(string coNo, string payMonth);
        Task<IEnumerable<Cstm10>> GetAllCustomersAsync();
        Task<IEnumerable<Stock10>> GetAllProductsAsync();

        /// <summary>
        /// 取得寄賣客戶庫存盤點
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<IEnumerable<ConsignInventoryCheck>> GetConsignInventoryChecksDataAsync(ExportVM vm);

        /// <summary>
        /// 取得寄賣客戶庫存對帳
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<IEnumerable<ConsignReconciliation>> GetConsignReconciliationsAsync(ExportVM vm);

        void UpdateStockPass(Out30 out30);
    }
}