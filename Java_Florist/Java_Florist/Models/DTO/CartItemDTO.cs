using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Java_Florist.Models.DTO
{
    public class CartItemDTO : Bouqueti
    {
        public int CartId { get; set; }
        public int CartItemId { get; set; }
        public double ToTal { get; set; }
    }
}