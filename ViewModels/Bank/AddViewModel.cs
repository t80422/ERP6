using System.Collections.Generic;

namespace ERP6.ViewModels.Bank
{
    public class AddViewModel
    {
        public string BankNo { get; set; }
        public string BankName { get; set; }
        public double Initamount { get; set; }
        public string Accno { get; set; }
        public string Initdate { get; set; }
        public string Ntus { get; set; }
        public List<Bank10List> bank10List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
