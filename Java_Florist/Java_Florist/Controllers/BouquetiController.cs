using Java_Florist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class BouquetiController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Bouqueti req)
        {
            if (req.BouquetiId > 0)
            {
                var _bouqueti = db.Bouquetis.Where(M => M.BouquetiId == req.BouquetiId).FirstOrDefault();
                _bouqueti.BouquetiName = req.BouquetiName;
                _bouqueti.Price = req.Price;
                _bouqueti.Image = req.Image;
                _bouqueti.Status = req.Status;
                db.SubmitChanges();
                return Json(new { success = true, data = _bouqueti }, JsonRequestBehavior.AllowGet);
            }

            db.Bouquetis.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int BouquetiId)
        {
            var _bouqueti = db.Bouquetis.Where(M => M.BouquetiId == BouquetiId).FirstOrDefault();
            db.Bouquetis.DeleteOnSubmit(_bouqueti);
            db.SubmitChanges();
            return Json(new { success = true, data = _bouqueti }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Index()
        {
            var listBouquetis = db.Bouquetis.ToList();
            ViewBag.ListBouqueti = listBouquetis;
            return View();
        }
    }
}