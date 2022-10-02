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

    public class BouquetiMessageDTO
    {
        public int CartOrderId { get; }
        public int CartItemId { get; set; }
        public int BouquetiId { get; set; }
        public string Message1 { get; set; }
    }

    public class OrderModel
    {
        public int UserId { get; set; }
        public int Status { get; set; }
        public string Address_From { get; set; }
        public string Address_To { get; set; }
        public int TypePayment { get; set; }
        public string TimeDelivery { get; set; }
        public int CartId { get; set; }
        public List<BouquetiMessageDTO> ListMessageCartItem { get; set; }
    }
}