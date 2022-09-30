using Java_Florist.Models;
using Java_Florist.Models.DTO;
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
            var _cartItem = db.CartItems.Where(x => x.CartId == CartId);
            _cartItem.ToList().ForEach(c => {
                db.CartItems.DeleteOnSubmit(c);
                db.SubmitChanges();
            });

            db.SubmitChanges();
            return Json(new { success = true, data = _cart }, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart
        public ActionResult Index()
        {
            var listCart = (from a in db.Carts
                            select new CartDTO { 
                                CartId = a.CartId,
                                Status = a.Status,
                                StatusName = a.Status == 1 ? "New" : "Deleted",
                                UserId = a.UserId,
                                UserName = db.htUsers.Where(x => x.UserId == a.UserId).FirstOrDefault().UserName ?? "__",
                                TotalItem = db.CartItems.Where(x => x.CartId == a.CartId).Count()
                            });
            ViewBag.ListCart = listCart;
            return View();
        }
    }
}