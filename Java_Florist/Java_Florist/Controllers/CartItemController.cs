using Java_Florist.Models;
using Java_Florist.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class CartItemController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        // GET: CartItem
        public ActionResult GetCartItem(int CartId)
        {
            var listCartItem = (from a in db.CartItems.Where(x => x.CartId == CartId)
                            select new CartItemDTO
                            {
                                CartItemId = a.CartItemId,
                                CartId = a.CartId.GetValueOrDefault(),
                                BouquetiId = a.BouquetiId.GetValueOrDefault(),
                                BouquetiName = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().BouquetiName ?? "__",
                                Price = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Price ?? 0,
                                Image = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Image ?? "__",
                                Status = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Status ?? 0
                            });
            return Json(new { success = true, data = listCartItem }, JsonRequestBehavior.AllowGet);
        }
    }
}