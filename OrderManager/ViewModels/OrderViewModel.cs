using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderManager.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name ="CustomerName")]
        public string FullName { get; set; }
        public int OrderNum { get; set; }
        [Required]
        public Nullable<decimal> OrderAmount { get; set; }
        [Required]
        public Nullable<decimal> AdvanceAmount { get; set; }
        public Nullable<decimal> OpeningAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerCode { get; set; }
        public string OrderDescription { get; set; }
        
        public string City { get; set; }
        public string AgentName { get; set; }

        public int OrderPerDate { get; set; }
    }
}