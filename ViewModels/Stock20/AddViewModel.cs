using System.Collections.Generic;
using X.PagedList;

namespace ERP6.ViewModels.Stock20
{
    public class AddViewModel
    {
        public string SpNo { get; set; }
        public string Bdate { get; set; }
        public string Edate { get; set; }
        public string CoNo { get; set; }
        public string Memo { get; set; }
        public string Userid { get; set; }
        public List<Stock20List> stock20List { get; set; }
        public List<Stock21List> stock21List { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
