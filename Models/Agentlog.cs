using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Agentlog
    {
        public string Agentid { get; set; }
        public string Userid { get; set; }
        public string Menuid { get; set; }
        public string Agentuserid { get; set; }
        public string Execdate { get; set; }
        public string Exitdate { get; set; }
    }
}
