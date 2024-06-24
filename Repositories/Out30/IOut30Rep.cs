using ERP6.Models;
using ERP6.ViewModels.Out30VMs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP6.Repositories
{
    public interface IOut30Rep : IRepository<Out30>
    {
        Task DeleteAsync(string coNo, string payMonth);
        Task<IEnumerable<Out30ReportFilter>> GetOut30ReportFilter(ExportVM vm);
    }
}