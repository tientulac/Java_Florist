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
            var listOrder =(from a in db.Orders
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
                                    ToTal = 1000000
                                }).ToList();
            ViewBag.ListOrder = listOrder;
            return View();
        }

        public ActionResult Delete(int OrderId)
        {
            var _order = db.Orders.Where(M => M.OrderId == OrderId).FirstOrDefault();
            db.Orders.DeleteOnSubmit(_order);
            db.SubmitChanges();
            return Json(new { success = true, data = _order }, JsonRequestBehavior.AllowGet);
        }
    }
}