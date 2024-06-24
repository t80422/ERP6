using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Autonum
    {
        public string Numbername { get; set; }
        public string Currentprefix { get; set; }
        public int Digitswidth { get; set; }
        public int Currentdigits { get; set; }
        public int Valueinterval { get; set; }
        public int Minvalue { get; set; }
        public int Maxvalue { get; set; }
    }
}
