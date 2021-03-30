using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManager.ViewModels
{
    public class AgentViewModel
    {
        public int AgentCode { get; set; }
        public string AgentName { get; set; }
        public string WorkingArea { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public string PhoneNo { get; set; }
        [Display(Name ="Country")]
        public Nullable<int> CountryId { get; set; }
        public string AgentCountry { get; set; }
        public virtual IEnumerable<SelectListItem> Countrylist { get; set; }
        [Display(Name ="Customers")]
        public int CustCount { get; set; }
    }
}