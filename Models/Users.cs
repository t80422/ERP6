using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Users
    {
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Agent { get; set; }
        public string Pwd { get; set; }
        public string Createdate { get; set; }
        public string Creater { get; set; }
        public string Modifydate { get; set; }
        public string Modifier { get; set; }
        public string Description { get; set; }
        public string Sysflag { get; set; }
        public string NewPwd { get; set; }
        public string PERMISSIONS { get; set; }
    }
}
