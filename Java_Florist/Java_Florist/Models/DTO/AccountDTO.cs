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
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}