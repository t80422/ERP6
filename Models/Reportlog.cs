using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Reportlog
    {
        public string Jobid { get; set; }
        public string Owner { get; set; }
        public string Createdate { get; set; }
        public string Createtime { get; set; }
        public string Printername { get; set; }
        public string Needshowdialog { get; set; }
        public byte[] Condition { get; set; }
        public string Pagecount { get; set; }
        public string Module { get; set; }
        public string Report { get; set; }
        public string Sysflag { get; set; }
        public string Sysver { get; set; }
    }
}
