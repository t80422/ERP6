using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Menutable
    {
        public string Menuid { get; set; }
        public string Caption { get; set; }
        public string Parent { get; set; }
        public string Package { get; set; }
        public string Packagecla { get; set; }
        public string Itemparam { get; set; }
        public string Form { get; set; }
        public string Isshowmodal { get; set; }
        public string Functions { get; set; }
        public string Itemtype { get; set; }
        public string SeqNo { get; set; }
    }
}
