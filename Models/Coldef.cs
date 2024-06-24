using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Coldef
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public decimal? Seq { get; set; }
        public string FieldType { get; set; }
        public string IsKey { get; set; }
        public decimal? FieldLength { get; set; }
        public string Caption { get; set; }
        public string Editmask { get; set; }
        public string Needbox { get; set; }
        public string Canreport { get; set; }
        public string ExtMenuid { get; set; }
        public decimal? FieldScale { get; set; }
        public string DdName { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateTime { get; set; }
        public string Sysflag { get; set; }
        public string Lastupdatedatetime { get; set; }
    }
}
