using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Java_Florist.Models.DTO
{
    public class OrderDTO : Order
    {
        public string CustomerName { get; set; }
        public double ToTal { get; set; } 
        public string StatusName { get; set; }
        public string TypePaymentName { get; set; }
    }
}