using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Java_Florist.Models.DTO
{
    public class CustomerDTO : Customer
    {
        public string UserName { get; set; }
        public string GenderName { get; set; }
        public string DobString { get; set; }
    }
}