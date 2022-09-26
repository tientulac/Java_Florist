using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class OrdersController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}