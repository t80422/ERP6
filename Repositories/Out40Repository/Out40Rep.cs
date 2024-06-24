using ERP6.Models;
using ERP6.ViewModels.Out30VMs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Repositories
{
    public class Out40Rep : Repository<Out40>, IOut40Rep
    {
        public Out40Rep(EEPEF01Context context) : base(context)
        {
        }

        public void DeleteRange(IEnumerable<Out40> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<Out40>> GetByOut30(string coNo, string payMonth)
        {
            return await _dbSet.Where(x => x.CoNo == coNo && x.Paymonth == payMonth).ToListAsync();
        }

        public async Task<Out40> GetLastPayMonthByCoNoPartNo(string coNo, string partNo, string paymonth)
        {
            var data = _dbSet.Where(x => x.CoNo == coNo && x.PartNo == partNo && x.Paymonth.CompareTo(paymonth) < 0);

            return await data.OrderByDescending(x => x.Paymonth).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Out30Detail>> GetOut40DetailsByCoNoPaymonth(string coNo, string paymonth)
        {
            return await (from Out40 in _dbSet
                          join Stock10 in _context.Stock10 on Out40.PartNo equals Stock10.PartNo
                          where Out40.CoNo == coNo && Out40.Paymonth == paymonth
                          select new Out30Detail
                          {
                              Out40 = Out40,
                              Stock10 = Stock10
                          }).ToListAsync();
        }
    }
}