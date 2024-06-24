using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Bank10
    {
        public string BankNo { get; set; }
        public string BankName { get; set; }
        public double Initamount { get; set; }
        public string Accno { get; set; }
        public string Initdate { get; set; }
        public string Ntus { get; set; }
    }
}
