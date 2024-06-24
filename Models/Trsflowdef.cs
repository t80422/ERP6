using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Trsflowdef
    {
        public string TfId { get; set; }
        public string TfName { get; set; }
        public string SPackageName { get; set; }
        public string STableName { get; set; }
        public string PackageName { get; set; }
        public string TfFlag { get; set; }
        public string AuditorId { get; set; }
        public string CanExec { get; set; }
        public string SyncType { get; set; }
    }
}
