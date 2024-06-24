using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Paperno10
    {
        public string PaperNo { get; set; }
        public string PaperType { get; set; }
        public string Prefix { get; set; }
        public string Bno { get; set; }
        public string Eno { get; set; }
        public double? Counter { get; set; }
        public int? NoLength { get; set; }
        public string NowActive { get; set; }
        public string Bdate { get; set; }
        public string Edate { get; set; }
    }
}
