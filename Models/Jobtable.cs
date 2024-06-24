using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class Jobtable
    {
        public string Jobid { get; set; }
        public string Packagename { get; set; }
        public string Methodname { get; set; }
        public string Owner { get; set; }
        public string Createdate { get; set; }
        public string Createtime { get; set; }
        public string Startdate { get; set; }
        public string Starttime { get; set; }
        public string Lasttimedate { get; set; }
        public string Lasttimetime { get; set; }
        public string Scheduletype { get; set; }
        public string Unittype { get; set; }
        public int? Timestep { get; set; }
        public string Selectedweekdays { get; set; }
        public int? Selectedmonthdays { get; set; }
        public string Execflag { get; set; }
        public string Scheduler { get; set; }
        public string Jobdescrip { get; set; }
        public string Notificationdata { get; set; }
    }
}
