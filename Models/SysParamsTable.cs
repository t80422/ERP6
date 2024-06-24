using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class SysParamsTable
    {
        public string ParamKind { get; set; }
        public string UnitId { get; set; }
        public string Itemtype { get; set; }
        public string Menuid { get; set; }
        public string ParamId { get; set; }
        public string ValueId { get; set; }
        public string ValueStr { get; set; }
    }
}
