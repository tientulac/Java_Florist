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
        public ActionResult GetCartItem(int userId)
        {
            var cartByUser = db.Carts.Where(x => x.UserId == userId);
            if (cartByUser.Any())
            {
                var listCartItem = (from a in db.CartItems.Where(x => x.CartId == cartByUser.FirstOrDefault().CartId)
                                    select new CartItemDTO
                                    {
                                        UserId = cartByUser.FirstOrDefault().UserId.GetValueOrDefault(),
                                        CartItemId = a.CartItemId,
                                        CartId = a.CartId.GetValueOrDefault(),
                                        BouquetiId = a.BouquetiId.GetValueOrDefault(),
                                        BouquetiName = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().BouquetiName ?? "__",
                                        Price = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Price ?? 0,
                                        Image = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Image ?? "",
                                        Status = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Status ?? 0,
                                        TotalCount = a.TotalCount ?? 0,
                                        Desc = db.Bouquetis.Where(x => x.BouquetiId == a.BouquetiId).FirstOrDefault().Desc ?? "__",
                                        Message = db.Messages.Where(x => x.OccasionId == (db.BouquetiMessages.Where(m => m.BouquetiId == a.BouquetiId).FirstOrDefault().OccasionId) && (x.Message1.Length > 0)).FirstOrDefault().Message1 ?? "__"
                                    });
                return Json(new { success = true, data = listCartItem }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, data = "" }, JsonRequestBehavior.AllowGet);
        }

        public int GetCountCart(int userId)
        {
            var countCart = 0;
            var cart = db.Carts.Where(x => x.UserId == userId);
            if (cart.Any())
            {
                countCart = db.CartItems.Where(x => x.CartId == cart.FirstOrDefault().CartId).Sum(c => c.TotalCount) ?? 0;
            }
            return countCart;
        }

        public ActionResult InsertCartItem(int userId, int bouquetiId)
        {
            var cart = db.Carts.Where(x => x.UserId == userId);
            if (cart.Any())
            {
                var cartId = cart.FirstOrDefault().CartId;
                var checkItem = db.CartItems.Where(x => x.CartId == cartId && x.BouquetiId == bouquetiId);
                if (!checkItem.Any())
                {
                    var cartItem = new CartItem();
                    cartItem.BouquetiId = bouquetiId;
                    cartItem.CartId = cartId;
                    cartItem.TotalCount = 1;
                    db.CartItems.InsertOnSubmit(cartItem);
                    db.SubmitChanges();
                }
                else
                {
                    var item = checkItem.FirstOrDefault();
                    item.CartId = cartId;
                    item.BouquetiId = bouquetiId;
                    item.TotalCount++;
                    db.SubmitChanges();
                }
            }
            else
            {
                var cart_insert = new Cart();
                cart_insert.Status = 1;
                cart_insert.UserId = userId;
                db.Carts.InsertOnSubmit(cart_insert);
                var cartItem = new CartItem();
                cartItem.BouquetiId = bouquetiId;
                cartItem.CartId = cart_insert.CartId;
                cartItem.TotalCount = 1;
                db.CartItems.InsertOnSubmit(cartItem);
                db.SubmitChanges();
            }
            return Json(new { success = true, data = cart }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int cartItemId)
        {
            var _cartItem = db.CartItems.Where(M => M.CartItemId == cartItemId).FirstOrDefault();
            db.CartItems.DeleteOnSubmit(_cartItem);
            db.SubmitChanges();
            return Json(new { success = true, data = _cartItem }, JsonRequestBehavior.AllowGet);
        }
    }
}