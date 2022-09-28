using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Java_Florist.Models.DTO
{
    public class AccountDTO : htUser
    {
        public string FunctionName { get; set; }
        public string StatusName { get; set; }
    }
}