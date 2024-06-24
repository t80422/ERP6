using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Mailusergroups
    {
        public string Groupname { get; set; }
        public string Username { get; set; }
        public string Mailaddr { get; set; }
        public string Owner { get; set; }
    }
}
