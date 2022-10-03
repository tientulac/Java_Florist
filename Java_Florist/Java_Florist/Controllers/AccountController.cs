using Java_Florist.Models;
using Java_Florist.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace Java_Florist.Controllers
{
    public class AccountController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] tgData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < tgData.Length; i++)
            {
                hashString += tgData[i].ToString("x2");
            }
            return hashString;
        }

        [HttpPost]
        public ActionResult Login(htUser req)
        {
            req.PassWord = GetMD5(req.PassWord);
            try
            {
                var _user = db.htUsers.Where(x => x.UserName == req.UserName && x.PassWord == req.PassWord);
                if (_user.Any())
                {
                    var token = createToken(_user.FirstOrDefault().UserName);
                    return Json(new { success = true, data = _user.FirstOrDefault(), token = token }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = "", token = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, data = "", token = "" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Register(AccountDTO req)
        {
            req.PassWord = GetMD5(req.PassWord);
            try
            {
                var _user = db.htUsers.Where(x => x.UserName == req.UserName);
                var _userEmail = db.htUsers.Where(x => x.Email == req.Email);
                if (_user.Any())
                {
                    return Json(new { success = false, data = "The user name was exist !" }, JsonRequestBehavior.AllowGet);
                }
                if (_userEmail.Any())
                {
                    return Json(new { success = false, data = "The email was exist !" }, JsonRequestBehavior.AllowGet);
                }
                var _userInsert = new htUser();
                _userInsert.UserName = req.UserName;
                _userInsert.Email = req.Email;
                _userInsert.PassWord = req.PassWord;
                _userInsert.Admin = false;
                _userInsert.Active = true;
                _userInsert.FullName = req.F_Name + ' ' + req.L_Name;

                db.htUsers.InsertOnSubmit(_userInsert);
                db.SubmitChanges();
                var _customer = new Customer();
                _customer.F_Name = req.F_Name;
                _customer.L_Name = req.L_Name;
                _customer.Dob = req.Dob;
                _customer.Gender = req.Gender;
                _customer.Phone = req.Phone;
                _customer.Address = req.Address;
                _customer.UserId = _userInsert.UserId;
                db.Customers.InsertOnSubmit(_customer);
                db.SubmitChanges();

                return Json(new { success = true, data = "Register successfully !"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ChangePass(AccountDTO req)
        {
            req.NewPassword = GetMD5(req.NewPassword);
            req.OldPassword = GetMD5(req.OldPassword);
            try
            {
                var _user = db.htUsers.Where(x => x.UserName == req.UserName && x.Email == req.Email && x.PassWord == req.OldPassword && x.Email == req.Email);
                var _userEmail = db.htUsers.Where(x => x.Email == req.Email);
                var _userPassword = db.htUsers.Where(x => x.PassWord == req.OldPassword);
                if (!_userEmail.Any())
                {
                    return Json(new { success = false, data = "The email does not exist !" }, JsonRequestBehavior.AllowGet);
                }

                if (!_userPassword.Any())
                {
                    return Json(new { success = false, data = "The password does not match !" }, JsonRequestBehavior.AllowGet);
                }

                if (_user.Any())
                {
                    _user.FirstOrDefault().PassWord = req.NewPassword;
                    db.SubmitChanges();
                    return Json(new { success = true, data = "Successfully !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, data = "ERROR !" }, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult Register(AccountDTO req)
        //{
        //    req.PassWord = GetMD5(req.PassWord);
        //    req.Admin = false;
        //    req.Active = true;
        //    try
        //    {
        //        var _user = new htUser();
        //        _user.UserName = req.UserName;
        //        _user.PassWord = req.PassWord;
        //        _user.Admin = req.Admin;
        //        _user.Active = req.Active;
        //        _user.FullName = req.F_Name + req.L_Name;
        //        _user.Email = req.Email;
        //        _user.UserCategory = 1;
        //        db.htUsers.InsertOnSubmit(_user);
        //        db.SubmitChanges();
        //        var _customer = new Customer();
        //        _customer.F_Name = req.F_Name;
        //        _customer.L_Name = req.L_Name;
        //        _customer.Dob = req.Dob;
        //        _customer.Gender = req.Gender;
        //        _customer.Phone = req.Phone;
        //        _customer.Address = req.Address;
        //        _customer.UserId = _user.UserId;
        //        db.Customers.InsertOnSubmit(_customer);
        //        db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, data = "", token = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { success = false, data = "", token = "" }, JsonRequestBehavior.AllowGet);
        //}

        // GET: Account
        public ActionResult Index()
        {
            var listAccount = (from a in db.htUsers
                                select new AccountDTO
                                {
                                    UserId = a.UserId,
                                    UserName = a.UserName,
                                    Admin = a.Admin,
                                    Active = a.Active,
                                    Email = a.Email,
                                    StatusName = a.Active == true ? "Enable" : "Disable",
                                    FunctionName = a.Admin == true ? "ADMIN" : "USER"
                                }).ToList();
            ViewBag.ListAccount = listAccount;
            return View();
        }

        [HttpPost]
        public ActionResult Save(htUser req)
        {
            var _user = db.htUsers.Where(M => M.UserId == req.UserId).FirstOrDefault();
            _user.Admin = req.Admin;
            _user.Active = req.Active;
            _user.Email = req.Email;
            htUserFunction _userFunc = new htUserFunction();
            _userFunc.UserId = req.UserId;

            var _function = db.htUserFunctions.Where(M => M.UserId == req.UserId).FirstOrDefault();
            db.htUserFunctions.DeleteOnSubmit(_function);

            if (req.Admin == true)
            {
                _userFunc.FunctionId = 1;
                db.htUserFunctions.InsertOnSubmit(_userFunc);
            }
            else
            {
                _userFunc.FunctionId = 2;
                db.htUserFunctions.InsertOnSubmit(_userFunc);
            }
            db.SubmitChanges();
            
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int UserId)
        {
            var _user = db.htUsers.Where(M => M.UserId == UserId).FirstOrDefault();
            db.htUsers.DeleteOnSubmit(_user);
            db.SubmitChanges();
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int UserId)
        {
            var _user = db.htUsers.Where(M => M.UserId == UserId).FirstOrDefault();
            return Json(new { success = true, data = _user }, JsonRequestBehavior.AllowGet);
        }

        public static string createToken(string Username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //đặt thời gian hết hạn token
            DateTime expires = DateTime.UtcNow.AddDays(10);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in

            var userIdentity = new ClaimsIdentity("Identity");
            userIdentity.Label = "Identity";
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, Username));
            userIdentity.AddClaim(new Claim("Username", Username));
            //userIdentity.AddClaim(new Claim("Category", Category));
            //userIdentity.HasClaim(ClaimTypes.Role, Category);
            var claims = new List<Claim>();

            var identity = new ClaimsPrincipal(userIdentity);
            Thread.CurrentPrincipal = identity;
            //string sec = EncryptCode;
            string sec = "088881139703564148785";
            //string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1" + Category;
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://unisoft.edu.vn/", audience: "http://unisoft.edu.vn/",
                        subject: userIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}