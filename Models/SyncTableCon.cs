using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class SyncTableCon
    {
        public string TableName { get; set; }
        public string ApsrvId { get; set; }
        public decimal OrderNo { get; set; }
        public decimal FieldsList { get; set; }
        public string KeyField { get; set; }
        public string Condition { get; set; }
    }
}
