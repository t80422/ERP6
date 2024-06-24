using ERP6.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Out30VMs
{
    public class Out30VM
    {
        public Out30 Out30 { get; set; }
        public List<Out30Detail> Out40List { get; set; }
        public List<SelectListItem> TaxTypeList { get; set; }

        public Out30VM()
        {
            TaxTypeList = new List<SelectListItem>()
            {
                new SelectListItem{Text="免稅",Value="0"},
                new SelectListItem{Text="外加",Value="1"},
                new SelectListItem{Text="內含",Value="2"},
            };
        }

        [NotMapped]
        public string FromattedPaymont
        {
            get => !string.IsNullOrEmpty(Out30?.Paymonth) ? DateTime.ParseExact(Out30.Paymonth, "yyyyMM", null).ToString("yyyy-MM") : null;
            set => Out30.Paymonth = value.Replace("-", "");
        }
    }

    public class Out30Detail
    {
        public Out40 Out40 { get; set; }
        public Stock10 Stock10 { get; set; }
    }
}
