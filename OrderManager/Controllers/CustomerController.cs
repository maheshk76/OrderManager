using OrderManager.Models;
using OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManager.Controllers
{
    public class CustomerController : Controller
    {
        private OrderManagerDBContext _context;
        IEnumerable<SelectListItem> cities;
        IEnumerable<SelectListItem> countries;
        public CustomerController()
        {
            _context = new OrderManagerDBContext();

            cities = _context.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.CityId.ToString(),
                    Text = c.CityName,
                    Selected = false

                }).ToList();
           countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = false

            }).ToList();
        }
        // GET: Customer
        public ActionResult Index(int? agentid)
        {
            IEnumerable<CustomerViewModel> Customers = _context.Database.SqlQuery<CustomerViewModel>("exec GetCustomer @CustCode",
               new SqlParameter("@CustCode", -1));
            if (agentid == null)
                return View(Customers.ToList());
            else
                Customers = Customers.Where(c => c.AgentCode == agentid);
                return View(Customers.ToList());

        }
        public ActionResult CustForm(int? id,int? agentid)
        {
            EditCustomerViewModel customer = new EditCustomerViewModel();
            if (id == null)
                ViewBag.VTitle = "Create";
            else
            {
                ViewBag.VTitle = "Edit";
                customer = _context.Database.SqlQuery<EditCustomerViewModel>("exec GetCustomer @CustCode"
                    ,new SqlParameter("@CustCode", id)).FirstOrDefault();

            }
            customer.Countrylist = countries;
            customer.Citylist = cities;
            customer.AgentCode = agentid;
            return View(customer);
        }
        [HttpPost]
        public ActionResult Save(EditCustomerViewModel cust)
        {
            var agent=_context.Agents.FirstOrDefault(a => a.AgentCode == cust.AgentCode);
            if (agent == null)
            {
                ModelState.AddModelError("", "Agent doesn't exists");
                cust.Citylist = cities;
                cust.Countrylist = countries;
                return View("CustForm", cust);
            }
            var param = new List<object>(){
               new SqlParameter("@CustCode",cust.CustomerCode),
               new SqlParameter("@FirstName",cust.FirstName),
               new SqlParameter("@LastName",cust.LastName),
               new SqlParameter("@CustomerCity",cust.CustomerCity),
               new SqlParameter("@WorkingArea",cust.WorkingArea),
               new SqlParameter("@CustomerCountry",cust.CustomerCountry),
               new SqlParameter("@Grade",cust.Grade),
               new SqlParameter("@OpeningAmount",cust.OpeningAmount),
               new SqlParameter("@PhoneNo",cust.PhoneNo),
               new SqlParameter("@AgentCode",cust.AgentCode),

            };
            _context.Database.ExecuteSqlCommand(
                "exec InsertUpdateCustomer @CustCode,@FirstName,@LastName,@CustomerCity,@WorkingArea,@CustomerCountry,@Grade,@OpeningAmount,@PhoneNo,@AgentCode",
                param.ToArray());
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _context.Database.ExecuteSqlCommand("exec DeleteCustomer @custId"
                , new SqlParameter("@custId", id));
            return RedirectToAction("Index");
        }
    }
    
}