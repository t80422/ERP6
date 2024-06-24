using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Maillog
    {
        public string Createdate { get; set; }
        public string Createtime { get; set; }
        public string Owner { get; set; }
        public string Seqno { get; set; }
        public string Mailfrom { get; set; }
        public string Mailto { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Result { get; set; }
        public string Executive { get; set; }
    }
}
