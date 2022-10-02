using Java_Florist.Models;
using Java_Florist.Models.DTO;
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
            var listOrder = (from a in db.Orders
                             select new OrderDTO
                             {
                                 OrderId = a.OrderId,
                                 CustomerId = a.CustomerId,
                                 CustomerName = (db.Customers.Where(x => x.CustomerId == a.CustomerId).FirstOrDefault().F_Name + " " + db.Customers.Where(x => x.CustomerId == a.CustomerId).FirstOrDefault().L_Name) ?? "___",
                                 StatusName = (a.Status == 1) ? "Delivering" : (a.Status == 2) ? "Canceled" : "Done",
                                 Address_From = a.Address_From,
                                 Address_To = a.Address_To,
                                 TypePayment = a.TypePayment,
                                 TypePaymentName = a.TypePayment == 1 ? "PayCard" : "Money",
                                 TimeDelivery = a.TimeDelivery,
                                 Status = a.Status,
                                 CartId = a.CartId,
                                 ToTal = (double)(db.CartItems.Where(x => x.CartId == a.CartId).Sum(c => c.TotalCount * db.Bouquetis.Where(x => x.BouquetiId == c.BouquetiId).FirstOrDefault().Price) ?? 0)
                             }).ToList();
            ViewBag.ListOrder = listOrder;
            return View();
        }

        public ActionResult GetByUserId(int userId)
        {
            var customerId = db.Customers.Where(x => x.UserId == userId)?.FirstOrDefault()?.CustomerId ?? 0;
            if (customerId > 0)
            {
                var listOrder = (from a in db.Orders.Where(o => o.CustomerId == customerId)
                                 select new OrderDTO
                                 {
                                     OrderId = a.OrderId,
                                     CustomerId = a.CustomerId,
                                     CustomerName = (db.Customers.Where(x => x.CustomerId == a.CustomerId).FirstOrDefault().F_Name + " " + db.Customers.Where(x => x.CustomerId == a.CustomerId).FirstOrDefault().L_Name) ?? "___",
                                     StatusName = (a.Status == 1) ? "Delivering" : (a.Status == 2) ? "Canceled" : "Done",
                                     Address_From = a.Address_From,
                                     Address_To = a.Address_To,
                                     TypePayment = a.TypePayment,
                                     TypePaymentName = a.TypePayment == 1 ? "PayCard" : "Money",
                                     TimeDelivery = a.TimeDelivery,
                                     Status = a.Status,
                                     CartId = a.CartId,
                                     ToTal = (double)(db.CartItems.Where(x => x.CartId == a.CartId).Sum(c => c.TotalCount * db.Bouquetis.Where(x => x.BouquetiId == c.BouquetiId).FirstOrDefault().Price) ?? 0)
                                 }).ToList();
                return Json(new { success = true, data = listOrder }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderModel req)
        {
            var customer = db.Customers.Where(x => x.UserId == req.UserId);
            req.CartId = db.CartItems.Where(x => x.CartItemId == req.ListMessageCartItem.FirstOrDefault().CartItemId).FirstOrDefault().CartId ?? 0;
            if (req.CartId > 0)
            {
                Order _order = new Order();
                _order.CustomerId = customer.FirstOrDefault().CustomerId;
                _order.Status = req.Status;
                _order.Address_From = req.Address_From;
                _order.Address_To = req.Address_To;
                _order.TypePayment = req.TypePayment;
                _order.TimeDelivery = req.TimeDelivery;
                _order.CartId = req.CartId;

                req.ListMessageCartItem.ForEach(x =>
                {
                    var _cartItem = db.CartItems.Where(c => c.CartItemId == x.CartItemId && c.BouquetiId == x.BouquetiId);
                    if (_cartItem.Any())
                    {
                        _cartItem.FirstOrDefault().Message = x.Message1;
                        db.SubmitChanges();
                    }
                });
                var _cart = db.Carts.Where(x => x.CartId == req.CartId);
                _cart.FirstOrDefault().Status = 2;

                db.Orders.InsertOnSubmit(_order);
                db.SubmitChanges();
                return Json(new { success = true, data = _order }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, data = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int OrderId)
        {
            var _order = db.Orders.Where(M => M.OrderId == OrderId).FirstOrDefault();
            var _cart = db.Carts.Where(x => x.Status == 2 && x.CartId == _order.CartId);
            if (_cart.Any())
            {
                var _userId = _order.CustomerId;
                db.Orders.DeleteOnSubmit(_order);
                db.Carts.Where(x => x.CartId == _order.CartId && x.Status == 2).ToList().ForEach(c =>
                {
                    db.Carts.DeleteOnSubmit(c);
                    db.SubmitChanges();
                });
                db.CartItems.Where(x => x.CartId == _order.CartId).ToList().ForEach(c =>
                {
                    db.CartItems.DeleteOnSubmit(c);
                    db.SubmitChanges();
                });
                db.SubmitChanges();

                return Json(new { success = true, data = _order }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, data = "" }, JsonRequestBehavior.AllowGet);
        }

        public int GetCountOrder(int userId)
        {
            var countOrder = 0;
            var customerId = db.Customers.Where(x => x.UserId == userId)?.FirstOrDefault()?.CustomerId ?? 0;
            var order = db.Orders.Where(x => x.CustomerId == customerId);
            if (order.Any())
            {
                countOrder = db.Orders.Where(x => x.CustomerId == customerId).Count();
            }
            return countOrder;
        }
    }
}