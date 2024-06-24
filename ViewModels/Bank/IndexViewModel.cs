using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Bank
{
    public class IndexViewModel
    {
        public string BankNo { get; set; }
        public string BankName { get; set; }
        public double Initamount { get; set; }
        public string Accno { get; set; }
        public string Initdate { get; set; }
        public string Ntus { get; set; }
        public List<Bank10List> bank10List { get; set; }
        public bool IsSearch { get; set; }
        public string AccnoDDL { get; set; }
    }

    public class Bank10List
    {
        public string BankNo { get; set; }
        public string BankName { get; set; }
        public double Initamount { get; set; }
        public string Accno { get; set; }
        public string Initdate { get; set; }
        public string Ntus { get; set; }
    }
}
