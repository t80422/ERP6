using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Userworksheet
    {
        public string Userid { get; set; }
        public string Worksheetname { get; set; }
        public byte[] Worksheetdefs { get; set; }
        public string Worksheetdescription { get; set; }
        public string Worksheetbplname { get; set; }
        public string Worksheetreportname { get; set; }
        public string Worksheetserverend { get; set; }
    }
}
