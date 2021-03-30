using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeWorkManager.ViewModels
{
    public class FilterModel
    {
        public string Search { get; set; }

        [Display(Name = "Customer")]
        public int? CustId { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public string Sdate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public string Edate { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Customers{ get; set; }

        public int Pagesize { get; set; }
        public IEnumerable<SelectListItem> RecordserPage = new List<SelectListItem>()
        {
           new SelectListItem
           {
               Value="5",
               Text="5",
               Selected=true
           },
           new SelectListItem
           {
               Value="10",
               Text="10",
               Selected=false
           }, new SelectListItem
           {
               Value="15",
               Text="15",
               Selected=false
           }
        };




    }
}