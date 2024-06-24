using System.Collections.Generic;

namespace ERP6.ViewModels.Pur
{
    public class EditViewModel
    {
        public string PurNo { get; set; }
        public string PurDate { get; set; }
        public string SendDate { get; set; }
        public string VendorNo { get; set; }
        public string Ntus { get; set; }
        public string PeNo { get; set; }
        public double? Total { get; set; }
        public string Memo { get; set; }
        public string YnPur { get; set; }
        public string Userid { get; set; }
        public string PaperTail { get; set; }
        public double? Total1 { get; set; }
        public double? Total0 { get; set; }
        public string InNo { get; set; }
        public List<Pur10List> pur10List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
