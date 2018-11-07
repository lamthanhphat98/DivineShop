using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DivineShopProject.Models;
using DivineShopProject.Interfaces;
using Microsoft.AspNetCore.Http;
using DivineShopProject.View_Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using MimeKit;
using MailKit.Net.Smtp;

namespace DivineShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IUser _user;
        public HomeController(IUser user, IProduct product, ICart cart)
        {
            _user = user;
            _product = product;
            _cart = cart;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Infor()
        {
            ViewData["Name"] = "Tài khoản";
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            ////ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            ////HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            //HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            HttpContext.Session.SetString("CART", CountAll + "");
            HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            //ViewBag.Cart = CountAll;
            return View();
        }

        public IActionResult Password()
        {
            //    int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //    var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //    foreach (var item in totalCart)
            //    {
            ////        item.Product = _product.GetById(item.Id);
            //    }
            //    ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            //    ViewBag.Cart = CountAll;
            //    HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            //  products = products.or
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            HttpContext.Session.SetString("CART", CountAll + "");
            HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            ViewData["Name"] = "Tài khoản / Thay đổi mật khẩu";
            //var account = _user.GetById(HttpContext.Session.GetString("Username"));
            return View();
        }
        [HttpPost]
        public IActionResult Password(RegisterForm user)
        {
            ViewData["Name"] = "Tài khoản / Thay đổi mật khẩu";
            var products = _product.Products;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            HttpContext.Session.SetString("CART", CountAll + "");
            HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            //  products = products.or
            //Console.WriteLine(user.Password);
            //Console.WriteLine(user.NewPassword);
            //Console.WriteLine(user.ConfirmNewPassword);
            try
            {
                  
                   var account = _user.GetById(HttpContext.Session.GetString("Username"));
                    if (account.Password == user.Password)
                    {
                        if(!account.Password.Equals(user.NewPassword))
                        {
                            account.Password = user.NewPassword;
                        if(user.NewPassword.Equals(user.ConfirmNewPassword))
                        {
                            _user.Update(account);
                            return RedirectToAction("Infor");
                        }
                        else
                        {
                            ViewBag.ConfirmNewPassword = "Confirm must match with New Password";
                            return View();
                        }
                           
                        }
                        else
                        {
                            ViewBag.newPassword = "Old password must be different with new password";
                            return View();

                        }
                        
                    }
                    else
                    {
                        ViewBag.Error = "Old password maybe wrong";

                        return View();
                    }               
            }
            catch (Exception e)
            {
                return View();
            }


          
        }
        public IActionResult Account()
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            //ViewBag.Cart = CountAll;
            //HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            //  products = products.or
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            HttpContext.Session.SetString("CART", CountAll + "");
            HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            ViewData["Name"] = "Tài khoản / Thay đổi tài khoản";
            var account = _user.GetById(HttpContext.Session.GetString("Username"));
            return View(account);
        }
        [HttpPost]
        public IActionResult Account(User user)
        {
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            ViewBag.Cart = CountAll;
            HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            //  products = products.or
            ViewData["Name"] = "Tài khoản / Thay đổi tài khoản";
            var account = _user.GetById(HttpContext.Session.GetString("Username"));
            account.Email = user.Email;
            account.Fullname = user.Fullname;
            _user.Update(account);
            return RedirectToAction("Infor");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterForm register)
        {
            var user = new User();
            //   var duplicate = GetSingleByUniqueConstraint();
            try
            {
                if (!register.Password.Equals(register.ConfirmPassword))
                {

                    ViewBag.Error = "Confirm must match with password";
                    return View();
                }
                else
                {
                    // throw new Exception();
                    user.Username = register.Username;
                    user.Password = register.Password;
                    user.Email = register.Email;
                    user.Fullname = register.Fullname;
                    user.Admin = false;
                    user.Cash = 0;
                    _user.AddUser(user);
                    HttpContext.Session.Remove("ERROR");
                    return RedirectToAction("Login");
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);   
                if (e.InnerException.Message.Contains("duplicate key"))
                {
                    HttpContext.Session.SetString("ERROR", "Username is already registered");
                }
                return View();
            }

       
        }

        [HttpPost]
        public IActionResult Login(User user)
        {

            var account = _user.GetUsers.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (account != null)
            {
                if (account.Admin == true)
                {
                    HttpContext.Session.SetString("Admin", account.Username);
                    return RedirectToAction("Product", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("Username", account.Username);
                    HttpContext.Session.SetString("CASH", account.Cash.ToString("0,0"));
                    return RedirectToAction("List", "Product");
                }

            }
            else
            {
                HttpContext.Session.SetString("ERRORLOGIN", "Username or password maybe wrong");
                return View();
            }
      
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult RetakePassword()
        {
            HttpContext.Session.Remove("ERRORLOGIN");
            return View();
        }

        public IActionResult AddMoreCash()
        {
            ViewBag.Name = "Nạp tiền";
            return View();
        }
        [HttpPost]
        public IActionResult AddMoreCash(Cash cash)
        {
            ViewBag.Name = "Nạp tiền";
            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {


                var account = _user.GetById(HttpContext.Session.GetString("Username"));
                if (cash.Option.ToString().Equals("20.000"))
                {
                    account.Cash += 20000;
                }
                else if (cash.Option.ToString().Equals("50.000"))
                {
                    account.Cash += 50000;
                }
                else if (cash.Option.ToString().Equals("100.000"))
                {
                    account.Cash += 100000;
                }
                else if (cash.Option.ToString().Equals("200.000"))
                {
                    account.Cash += 200000;
                }
                else if (cash.Option.ToString().Equals("500.000"))
                {
                    account.Cash += 500000;
                }
                _user.Update(account);
                HttpContext.Session.SetString("CASH", account.Cash.ToString("0,0"));

            }
            return RedirectToAction("List", "Product");
        }
        [HttpPost]
        public IActionResult RetakePassword(User user)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {
                var User = _user.GetById(user.Username);
                if(User == null)
                {
                    HttpContext.Session.SetString("ERRORUSER", "User doesn't exist in system");
                    return View();
                }
                else
                {
                if (!User.Email.Equals(user.Email))
                {
                    HttpContext.Session.SetString("ERRORMAIL", "Mail must be match with your mail");
                    return View();
                }
                else
                {
                    //order.Accept = true;
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("DivineShop", "lamthanhphat98@gmail.com"));
                    message.To.Add(new MailboxAddress("DivineShop", User.Email));
                    message.Subject = "DivineShop";
                    message.Body = new TextPart("html")
                    {
                        //Text = "<b>Password hiện tại của bạn là " + User.Password + " .Xin hãy cẩn thận trong việc xử dụng password lần sau.</b>"
                        //+ "</br>." + "<img style='width=100px;height=100px' src='https://divineshop.vn/image/catalog/home/Logo.png'/>"
                        Text = "<table style='width:538px;background-color:#393836' align='center' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" + "<tr>" +

                        "<td style='height:65px; background - color:#171a21;border-bottom:1px solid #4d4b48;padding:0px'>" +
                   "<img src = 'https://ci6.googleusercontent.com/proxy/nHpeCJleoSDKGx3XuQvgtff86GHWEj6jd8b64uc0xE4BraE4IHAG_7T7aEKiM7pNwBZlb5YWK-6jL_YWVocVpzRXedVvyrREHkNoEYjituULMNWGBJphXykQmNWYztdZOnABJlE=s0-d-e1-ft#https://store.steampowered.com/public/shared/images/email/email_header_logo.png' width = '538' height = '65' alt = 'Steam' class='CToWUd'>" +
                     "</td>" + "</tr>" +
                     "<tr>" +
                     "<td bgcolor='#17212e'>" +
                     "<table width='470' border='0' align='center' cellspacing='0' cellpadding='0' style='padding-left:5px;padding-right:5px;padding-bottom:10px'>" +
                     "<tbody>" +
                     "<tr bgcolor='#17212e'>" +
                     "<td style = 'padding-top:32px;padding-bottom:16px'>" +

                        "<span style = 'font-size:24px;color:#66c0f4;font-family:Arial,Helvetica,sans-serif;font-weight:bold'>" +
                            "Dear " + User.Username + "," +

                        "</span></br>" +

                        "</td>" +
                     "</tr>" +
                     "<tr>" +
                     "<td style = 'padding-top:12px;padding-bottom:24px'>" +

                        "<span style = 'font-size:14px;color:silver;font-family:Arial,Helvetica,sans-serif;font-weight:bold'>" +
                            "Mật khẩu hiện tại của bạn là " + User.Password + ". Hãy sử dụng cẩn thận nó sau này." +

                        "</span></br>" +

                        "</td>" +
                     "</tr>" +
                     "</tbody>" +
                     "</table>" +
                      "</td>" +

                     "</tr>" +
                     "<tr>" +

                "<td bgcolor = '#000000'>" +


                        "<table width = '460' height = '55' border = '0' align = 'center' cellpadding = '0' cellspacing = '0' >" +


                            "<tbody> <tr valign = 'center' >" +

                                "<td width = '110'>" +



                                    "<img src = 'https://ci6.googleusercontent.com/proxy/11em3UmhXvSZ7mK0qOJZpSjCYTSNd8l1sWqc60_GvSJyLcXe_FmliC6TBX3036UREe2YclBWkSJT_So-WFHfBICrlBnbbpCqh8YA-NA=s0-d-e1-ft#http://storefront.steampowered.com/v/img/gift/VALVe.gif' alt = 'VALVE' width = '92' height = '26' hspace = '0' vspace = '0' border = '0' align = 'top' class='CToWUd'></a>" +
                                "</td>" +
                                "<td width = '350' valign='center'>" +
                                    "<span style = 'color:#999999;font-size:9px;font-family:Verdana,Arial,Helvetica,sans-serif'>© Valve Corporation.All rights reserved. All trademarks are property of their respective owners in the US and other countries.</span>" +
                                "</td>" +
                            "</tr>" +

                        "</tbody></table>" +
                "</td>" +
        "</tr>" +
                        "</tbody>" +
                        " </table>"
                    };
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("lamthanhphat98@gmail.com", "Phatfaker1");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    HttpContext.Session.Remove("ERRORMAIL");
                        HttpContext.Session.Remove("ERRORUSER");
                    return RedirectToAction("Login");
                }
                }
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    
    }
}
