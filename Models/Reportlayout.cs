using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Reportlayout
    {
        public string LayoutType { get; set; }
        public string Owner { get; set; }
        public string GroupNa { get; set; }
        public string LayoutNa { get; set; }
        public byte[] Infocomp { get; set; }
        public string DefaultFlag { get; set; }
    }
}
