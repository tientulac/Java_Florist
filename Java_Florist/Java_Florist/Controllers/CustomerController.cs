using Java_Florist.Models;
using Java_Florist.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class CustomerController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Customer req)
        {
            if (req.CustomerId > 0)
            {
                var _customer = db.Customers.Where(M => M.CustomerId == req.CustomerId).FirstOrDefault();
                _customer.F_Name = req.F_Name;
                _customer.L_Name = req.L_Name;
                _customer.Dob = req.Dob;
                _customer.Gender = req.Gender;
                _customer.Phone = req.Phone;
                _customer.Address = req.Address;
                _customer.UserId = req.UserId;
                db.SubmitChanges();
                return Json(new { success = true, data = _customer }, JsonRequestBehavior.AllowGet);
            }

            db.Customers.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int CustomerId)
        {
            var _customer = db.Customers.Where(M => M.CustomerId == CustomerId).FirstOrDefault();
            db.Customers.DeleteOnSubmit(_customer);
            db.SubmitChanges();
            return Json(new { success = true, data = _customer }, JsonRequestBehavior.AllowGet);
        }

        // GET: Customer
        public ActionResult Index()
        {
            var listCustomer = (from a in db.Customers
                                select new CustomerDTO {
                                    CustomerId = a.CustomerId,
                                    F_Name = a.F_Name,
                                    L_Name = a.L_Name,
                                    Dob = a.Dob,
                                    Gender = a.Gender,
                                    Phone = a.Phone,
                                    Address = a.Address,
                                    UserId = a.UserId,
                                    UserName = db.htUsers.Where(x => x.UserId == a.UserId).Any() ? db.htUsers.Where(x => x.UserId == a.UserId).FirstOrDefault().UserName : "___",
                                    GenderName = a.Gender == true ? "Female" : "Male",
                                    DobString = a.Dob.ToString().Substring(0, 10)
                                }).ToList();
            ViewBag.ListCustomer = listCustomer;
            return View();
        }
    }
}