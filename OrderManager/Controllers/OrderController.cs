using EmployeeWorkManager.ViewModels;
using OrderManager.Models;
using OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManager.Controllers
{
    public class OrderController : Controller
    {
        private OrderManagerDBContext _context;
        public OrderController()
        {
            _context = new OrderManagerDBContext();
        }

        // GET: Order
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> customers = _context.Customers.Select(c => new SelectListItem
            {
                Value = c.CustomerCode.ToString(),
                Text = c.FirstName+" "+c.LastName,
                Selected = false

            });
            IEnumerable<SelectListItem> cities = _context.Cities.Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.CityName,
                Selected = false

            });
            FilterModel filtermodel = new FilterModel
            {
                Cities = cities,
                Customers=customers
            };
            return View(filtermodel);
        }
        public ActionResult Book(int custId)
        {
            OrderViewModel model = new OrderViewModel();
            model.CustomerCode = custId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Book(OrderViewModel model)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerCode == model.CustomerCode);
            if (ModelState.IsValid)
            {

                model.OrderDate = DateTime.Now.Date;
                var parameters = new List<object>() {
                new SqlParameter("@OrderAmount", model.OrderAmount),
                new SqlParameter("@AdvanceAmount", model.AdvanceAmount),
                new SqlParameter("@OrderDate", model.OrderDate),
                new SqlParameter("@CustomerCode", model.CustomerCode),
                new SqlParameter("@OrderDesc", model.OrderDescription)
            };
                if (model.OrderAmount<=0 || model.AdvanceAmount <= 0)
                {
                    ModelState.AddModelError("", "Enter valid amount");
                    return View(model);
                }
                if (model.OrderAmount > customer.OpeningAmount)
                {
                    ModelState.AddModelError("", "Not Enough funds");
                    return View(model);
                }
                customer.PaymentAmount += model.AdvanceAmount;
                decimal remainingamount = Convert.ToDecimal(model.OrderAmount - model.AdvanceAmount);
                customer.OutstandingAmount += remainingamount;
               // customer.OpeningAmount -= model.AdvanceAmount;
                _context.Database.ExecuteSqlCommand("exec AddOrder @OrderAmount,@AdvanceAmount,@OrderDate,@CustomerCode,@OrderDesc", parameters.ToArray());
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Orderlist(string search = "", int? custId = 0, int? cityId = 0, string sdate = "", string edate = "", int cPage = 1, int Pagesize = 5,string sortBy="fname",string sortOrder="ASC")
        {
            /*switch (sortBy)
            {
                case "fname":
                    if (sortOrder == "ASC")
                        ViewBag.NameOrder = "DESC";
                    else
                        ViewBag.NameOrder = "ASC";
                    break;
                case "oamt":
                    if (sortOrder == "ASC")
                        ViewBag.OAmtOrder = "DESC";
                    else
                        ViewBag.OAmtOrder = "ASC";
                    break;
                case "aamt":
                    if (sortOrder == "ASC")
                        ViewBag.AAmtOrder = "DESC";
                    else
                        ViewBag.AAmtOrder = "ASC";
                    break;
                default:
                    break;
            }*/
            //cPage => current Page
            var parameters = new List<object>() {
                new SqlParameter("@search", search),
                new SqlParameter("@custId", custId),
                new SqlParameter("@cityId", cityId),
                new SqlParameter("@sdate", sdate),
                new SqlParameter("@edate", edate),
                new SqlParameter("@pagesize", Pagesize),
                new SqlParameter("@pagenum", cPage),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortOrder", sortOrder),
            };
            var outparam = new SqlParameter
            {
                ParameterName = "possiblerows",
                DbType = System.Data.DbType.Int32,
                Size = Int32.MaxValue,
                Direction = System.Data.ParameterDirection.Output
            };
            parameters.Add(outparam);

            List<OrderViewModel> OrderList = _context.Database.SqlQuery<OrderViewModel>
                ("exec GetOrders @search,@custId,@cityId,@sdate,@edate,@pagesize,@pagenum,@sortBy,@sortOrder,@possiblerows OUTPUT", parameters.ToArray()).ToList<OrderViewModel>();
            int possiblerows;
            Debug.WriteLine(outparam.Value);
            if (outparam.Value ==DBNull.Value )
                possiblerows = 0;
            else
                possiblerows = Convert.ToInt32(outparam.Value);

            if (possiblerows > Pagesize)
            {
                var TotalPages = (int)Math.Ceiling((double)((decimal)possiblerows / Pagesize));
                ViewBag.TotalPages = TotalPages;
                ViewBag.CurrPage = cPage;
            }
            return PartialView(OrderList);
        }
        public ActionResult Delete(int id)
        {
            _context.Database.ExecuteSqlCommand("exec DeleteOrder @OrderId", new SqlParameter("@OrderId", id));
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            List<OrderViewModel> OrderList = _context.Database.SqlQuery<OrderViewModel>
                ("exec GetAllOrders @custId", new SqlParameter("@custId", id)).ToList<OrderViewModel>();
            return View("AllOrders",OrderList);
        }
    }
}