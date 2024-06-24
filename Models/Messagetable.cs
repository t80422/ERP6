using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Messagetable
    {
        public string Msgdate { get; set; }
        public string Msgtime { get; set; }
        public int Seqno { get; set; }
        public string Userid { get; set; }
        public string Ownerid { get; set; }
        public int Messageid { get; set; }
        public int? Methodid { get; set; }
        public byte[] Data { get; set; }
        public byte[] Optdata { get; set; }
        public int? Flag { get; set; }
        public int? Trycount { get; set; }
    }
}
