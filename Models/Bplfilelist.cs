using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Bplfilelist
    {
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public int Filesize { get; set; }
        public int Filetime { get; set; }
        public string Owner { get; set; }
        public string Createtime { get; set; }
        public string Createdate { get; set; }
        public string Updatemode { get; set; }
        public byte[] Content { get; set; }
    }
}
