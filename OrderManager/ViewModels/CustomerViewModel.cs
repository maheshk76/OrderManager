using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManager.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerCode { get; set; }
        public string FullName { get; set; }
        public string WorkingArea { get; set; }
        public string Grade { get; set; }
        public Nullable<decimal> OpeningAmount { get; set; }
        public Nullable<decimal> ReceivingAmount { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<decimal> OutstandingAmount { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> AgentCode { get; set; }
        public int CustomerCity { get; set; }
        public int CustomerCountry { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AgentName { get; set; }
    }
    public class EditCustomerViewModel {
        public int CustomerCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string WorkingArea { get; set; }
        [Required]
        public string Grade { get; set; }
        [Required]
        public Nullable<decimal> OpeningAmount { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public Nullable<int> AgentCode { get; set; }
        [Display(Name ="City")]
        public int CustomerCity { get; set; }
        [Display(Name ="Country")]
        public int CustomerCountry { get; set; }

        public virtual IEnumerable<SelectListItem> Agentlist { get; set; }
        public virtual IEnumerable<SelectListItem> Citylist { get; set; }
        public virtual IEnumerable<SelectListItem> Countrylist { get; set; }
    }

}