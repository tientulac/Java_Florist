using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class MessageController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Message req)
        {
            if (req.OccasionId > 0)
            {
                var _message = db.Messages.Where(M => M.OccasionId == req.OccasionId).FirstOrDefault();
                _message.Message1 = req.Message1;
                db.SubmitChanges();
                return Json(new { success = true, data = _message }, JsonRequestBehavior.AllowGet);
            }

            db.Messages.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int OccasionId)
        {
            var _message = db.Messages.Where(M => M.OccasionId == OccasionId).FirstOrDefault();
            db.Messages.DeleteOnSubmit(_message);
            db.SubmitChanges();
            return Json(new { success = true, data = _message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int OccasionId)
        {
            var _message = db.Messages.Where(M => M.OccasionId == OccasionId).FirstOrDefault();
            return Json(new { success = true, data = _message }, JsonRequestBehavior.AllowGet);
        }

        // GET: Message
        public ActionResult Index()
        {
            var listMessage = db.Messages.ToList();
            ViewBag.ListMessage = listMessage;
            return View();
        }
    }
}