using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Reportlogdetail
    {
        public string Jobid { get; set; }
        public string Jobpageid { get; set; }
        public byte[] Pagecontent { get; set; }
        public string Sysflag { get; set; }
        public string Sysver { get; set; }
    }
}
