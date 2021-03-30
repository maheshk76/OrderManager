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
    public class AgentController : Controller
    {
        private OrderManagerDBContext _context;
        public AgentController()
        {
            _context = new OrderManagerDBContext();
        }
        
        // GET: Agent
        public ActionResult Index()
        {
            var parameters = new List<object>() {
                new SqlParameter("@AgentCode",-1)
            };
            IEnumerable<AgentViewModel> agents = _context.Database.SqlQuery<AgentViewModel>("exec GetAgent @AgentCode",
                parameters.ToArray()).ToList();
            return View(agents);
        }
        public ActionResult AgentForm(int? id)
        {
            AgentViewModel agent = new AgentViewModel();
            IEnumerable<SelectListItem> cities = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = false

            }).ToList();
            if (id == null)
            {
                ViewBag.VTitle = "Create";

            }
            else
            {
                ViewBag.VTitle = "Edit";
                agent = _context.Database.SqlQuery<AgentViewModel>("exec GetAgent @AgentCode", new SqlParameter("@AgentCode", id)).FirstOrDefault();

            }
            agent.Countrylist = cities;
            return View(agent);
        }
        public ActionResult Save(AgentViewModel agent)
        {
            var param = new List<object>(){
                new SqlParameter("@AgentCode",agent.AgentCode),
                new SqlParameter("@AgentName", agent.AgentName),
                new SqlParameter("@WorkingArea", agent.WorkingArea),
                new SqlParameter("@Commission", agent.Commission),
                new SqlParameter("@PhoneNo",agent.PhoneNo),
                new SqlParameter("@Country", agent.CountryId)
            };
            _context.Database.ExecuteSqlCommand("exec InsertUpdateAgent @AgentCode,@AgentName,@WorkingArea,@Commission,@PhoneNo,@Country",
                param.ToArray());
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _context.Database.ExecuteSqlCommand("exec DeleteAgent @AgentCode",
                new SqlParameter("@AgentCode", id));

            return RedirectToAction("Index");
        }
    }
}