using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class DashBoardController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        // GET: DashBoard
        public ActionResult Index()
        {
            ViewBag.CountAccount = db.htUsers.ToList()?.Count() ?? 0;
            ViewBag.CountCustomer = db.Customers.ToList()?.Count() ?? 0;
            ViewBag.CountBouqueti = db.Bouquetis.ToList()?.Count() ?? 0;
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
    }
}