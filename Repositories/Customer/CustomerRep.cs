using ERP6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Repositories.Customer
{
    public class CustomerRep:Repository<Cstm10>,ICustomerRep
    {
        public CustomerRep(EEPEF01Context context) : base(context)
        {

        }
    }
}
