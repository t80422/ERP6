using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.AccNo
{
    public class IndexViewModel
    {
        public string Accno { get; set; }
        public string Accname { get; set; }
        public string Acctype { get; set; }
        public string Midtype { get; set; }
        public bool IsSearch { get; set; }
        public IList<AccNoList> accNoList { get; set; }
    }

    public class AccNoList
    {
        public string Accno { get; set; }
        public string Accname { get; set; }
        public string Acctype { get; set; }
        public string Midtype { get; set; }
    }
}
