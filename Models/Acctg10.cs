using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Acctg10
    {
        public string Userid { get; set; }
        public string Accdate { get; set; }
        public string Accid { get; set; }
        public double Ctotal { get; set; }
        public double Dtotal { get; set; }
        public string Papertype { get; set; }
        public string Passpaper { get; set; }
        public string Passid { get; set; }
        public string Company { get; set; }
        public string YnPrint { get; set; }
        public string Ntus { get; set; }
    }
}
