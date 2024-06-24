using ERP6.Models;
using ERP6.ViewModels.Out30VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Repositories
{
    public interface IOut40Rep : IRepository<Out40>
    {
        void DeleteRange(IEnumerable<Out40> entities);
        Task<Out40> GetLastPayMonthByCoNoPartNo(string coNo,string partNo,string paymonth);
        Task<IEnumerable<Out40>> GetByOut30(string coNo, string payMonth);
        Task<IEnumerable<Out30Detail>> GetOut40DetailsByCoNoPaymonth(string coNo, string paymonth);
    }
}