using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.ViewModels.Customer
{
    public class CustomerEditViewModel
    {
        public bool Back { get; set; }
        public string CoNo { get; set; }
        public string Coname { get; set; }
        public string Company { get; set; }
        public string Invocomp { get; set; }
        public string Dlien { get; set; }
        public string Fax { get; set; }
        public string PrintPrice { get; set; }
        public string Boss { get; set; }
        public string Sales { get; set; }
        public string Memo { get; set; }
        public string TaxType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Uniform { get; set; }
        public string Compaddr { get; set; }
        public string Sendaddr { get; set; }
        public string Payaccount { get; set; }
        public string Paybank { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public double? Prenotget { get; set; }
        public double? Total1 { get; set; }
        public double? Tax { get; set; }
        public double? Total2 { get; set; }
        public double? YesGet { get; set; }
        public double? SubTot { get; set; }
        public double? NotGet { get; set; }
        public string PeNo { get; set; }
        public string Product { get; set; }
        public string AreaNo { get; set; }
        public string Payment { get; set; }
        public int? ChehkDay { get; set; }
        public string Ntus { get; set; }
        public double? Taxrate { get; set; }
        public string Mobile { get; set; }
        public double? Discount { get; set; }
        public string CusType { get; set; }
        public string PriceType { get; set; }
        public string DriveNo { get; set; }
        public bool IsTrue { get; set; }
        public string ErrorMessage { get; set; }
        public string ParentId { get; set; }
        public List<CustomerList> customerList { get; set; }
    }
}
