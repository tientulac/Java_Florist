using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Java_Florist.Models.DTO
{
    public class CartDTO : Cart
    {
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public int? TotalItem { get; set; }
    }
}