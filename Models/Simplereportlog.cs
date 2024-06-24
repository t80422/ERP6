using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Simplereportlog
    {
        public string Bplname { get; set; }
        public string Reportname { get; set; }
        public string Createdate { get; set; }
        public string Createtime { get; set; }
        public string Owner { get; set; }
        public string Machine { get; set; }
        public string Type { get; set; }
        public string Sysflag { get; set; }
        public string Sysver { get; set; }
    }
}
