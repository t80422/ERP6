using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Trstbl
    {
        public string TrsId { get; set; }
        public string ParamId { get; set; }
        public string TrsOwner { get; set; }
        public string TrsType { get; set; }
        public string TrsFlag { get; set; }
        public string ErrorMsg { get; set; }
    }
}
