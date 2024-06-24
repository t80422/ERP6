using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Tablesynclog
    {
        public string Tablename { get; set; }
        public string Lastupdatetime { get; set; }
        public string Descr { get; set; }
    }
}
