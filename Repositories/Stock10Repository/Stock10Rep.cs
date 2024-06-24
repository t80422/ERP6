using ERP6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Repositories.Stock10Repository
{
    public class Stock10Rep:Repository<Stock10>,IStock10Rep
    {
        public Stock10Rep(EEPEF01Context context) : base(context)
        {

        }
    }
}
