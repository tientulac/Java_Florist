using Java_Florist.Models;
using Java_Florist.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class AccountController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        // GET: Account
        public ActionResult Index()
        {
            var listAccount = (from a in db.htUsers
                                select new AccountDTO
                                {
                                    UserId = a.UserId,
                                    UserName = a.UserName,
                                    Admin = a.Admin,
                                    Active = a.Active,
                                    Email = a.Email,
                                    StatusName = a.Active == true ? "Enable" : "Disable",
                                    FunctionName = a.Admin == true ? "ADMIN" : "USER"
                                }).ToList();
            ViewBag.ListAccount = listAccount;
            return View();
        }

        [HttpPost]
        public ActionResult Save(htUser req)
        {
            var _user = db.htUsers.Where(M => M.UserId == req.UserId).FirstOrDefault();
            _user.Admin = req.Admin;
            _user.Active = req.Active;
            _user.Email = req.Email;
            htUserFunction _userFunc = new htUserFunction();
            _userFunc.UserId = req.UserId;

            var _function = db.htUserFunctions.Where(M => M.UserId == req.UserId).FirstOrDefault();
            db.htUserFunctions.DeleteOnSubmit(_function);

            if (req.Admin == true)
            {
                _userFunc.FunctionId = 1;
                db.htUserFunctions.InsertOnSubmit(_userFunc);
            }
            else
            {
                _userFunc.FunctionId = 2;
                db.htUserFunctions.InsertOnSubmit(_userFunc);
            }
            db.SubmitChanges();
            
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int UserId)
        {
            var _user = db.htUsers.Where(M => M.UserId == UserId).FirstOrDefault();
            db.htUsers.DeleteOnSubmit(_user);
            db.SubmitChanges();
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int UserId)
        {
            var _user = db.htUsers.Where(M => M.UserId == UserId).FirstOrDefault();
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }
    }
}