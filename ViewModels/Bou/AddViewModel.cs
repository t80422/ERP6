using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Bou
{
    public class AddViewModel
    {
        public string QuNo { get; set; }
        public string QuDate { get; set; }
        public string CoNo { get; set; }
        public string Corp { get; set; }
        public string Fax { get; set; }
        public string Atte { get; set; }
        public string Tel { get; set; }
        public string SendAddr { get; set; }
        public string Payment { get; set; }
        public string SendDate { get; set; }
        public string Memo { get; set; }
        public string Sales { get; set; }
        public string Ntus { get; set; }
        public double? Total { get; set; }
        public string Userid { get; set; }
        public string PaperTail { get; set; }
        public double? Total1 { get; set; }
        public double? Total0 { get; set; }
        public string YnPass { get; set; }
        public List<Bou10List> bou10List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
