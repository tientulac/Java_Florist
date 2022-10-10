using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class HomeController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UserLayout()
        {
            var listBouquetis = db.Bouquetis.ToList();
            var listMessage = db.Messages.ToList();
            var listFlower = db.Flowers.ToList();
            ViewBag.ListBouqueti = listBouquetis;
            ViewBag.ListMessage = listMessage;
            ViewBag.ListFlower = listFlower;
            return View();
        }
    }
}
