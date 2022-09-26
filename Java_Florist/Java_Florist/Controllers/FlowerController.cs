using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class FlowerController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Flower req)
        {
            if (req.FlowerId > 0)
            {
                var _flower = db.Flowers.Where(M => M.FlowerId == req.FlowerId).FirstOrDefault();
                _flower.FlowerName = req.FlowerName;
                _flower.Color = req.Color;
                _flower.Image = req.Image;
                db.SubmitChanges();
                return Json(new { success = true, data = _flower }, JsonRequestBehavior.AllowGet);
            }

            db.Flowers.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int FLowerId)
        {
            var _flower = db.Flowers.Where(M => M.FlowerId == FLowerId).FirstOrDefault();
            db.Flowers.DeleteOnSubmit(_flower);
            db.SubmitChanges();
            return Json(new { success = true, data = _flower }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int FLowerId)
        {
            var _flower = db.Flowers.Where(M => M.FlowerId == FLowerId).FirstOrDefault();
            return Json(new { success = true, data = _flower }, JsonRequestBehavior.AllowGet);
        }

        // GET: Flower
        public ActionResult Index()
        {
            var listFLowers = db.Flowers.ToList();
            ViewBag.ListFlower = listFLowers;
            return View();
        }
    }
}