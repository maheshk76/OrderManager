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
        [Required]
        public string AgentName { get; set; }
        [Required]
        public string WorkingArea { get; set; }
        [Required]
        [Range(0,15)]
        [Display(Name ="Commission(In %)")]
        public Nullable<decimal> Commission { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Display(Name ="Country")]
        public Nullable<int> CountryId { get; set; }
        [Required]
        public string AgentCountry { get; set; }
        public virtual IEnumerable<SelectListItem> Countrylist { get; set; }
        [Display(Name ="Customers")]
        public int CustCount { get; set; }
    }
}