using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class CartController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Cart req)
        {
            db.Carts.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int CartId)
        {
            var _cart = db.Carts.Where(M => M.CartId == CartId).FirstOrDefault();
            db.Carts.DeleteOnSubmit(_cart);
            db.SubmitChanges();
            return Json(new { success = true, data = _cart }, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart
        public ActionResult Index()
        {
            var listCart = db.Carts.ToList();
            ViewBag.ListCart = listCart;
            return View();
        }
    }
}