using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using DivineShopProject.View_Model;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProduct _product;
        private readonly IOrder _order;
        private readonly ICategory _category;
        // public List<Cart> ListCart;
        public AdminController(IProduct product, IOrder order, ICategory category)
        {
         
            _product = product;
            //  _cart = cart;
            _order = order;
            _category = category;
        }
        public IActionResult Product()
        {
            HttpContext.Session.Remove("ERRORSALE");
            HttpContext.Session.Remove("ERRORPRICE");
            HttpContext.Session.Remove("ERRORID");
            if (HttpContext.Session.GetString("Admin") != null)
            {
                var product = _product.Products;
                ViewBag.Title = "LIST GAME IN AVAILABLE";
                return View(product);
            }
           else
            {
                return RedirectToAction("Login", "Home");
            }
          
        }
        [HttpPost]
        public IActionResult Product(String search)
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                var product = _product.Products.Where(p => p.Name.Contains(search));
                ViewBag.Title = "LIST GAME IN AVAILABLE";
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public IActionResult OrderAccept()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                var order = _order.GetOrderDetails;
                ViewBag.Title = "ALL ORDER FOR ACCEEPTING";
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Accept(int id)
        {
            String dateTime = DateTime.UtcNow.ToString("dddd MMMM dd hh:mm:ss yyyy");
            var order = _order.GetOrderById(id);
            order.Accept = true;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DivineShop", "lamthanhphat98@gmail.com"));
            message.To.Add(new MailboxAddress("DivineShop", order.Email));
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
                 "<table width='470' border='0' align='center' cellspacing='0' cellpadding='0' style='padding-left:10px;padding-bottom:10px'>" +
                 "<tbody>" +
                 "<tr bgcolor='#17212e'>" +
                 "<td style = 'padding-top:32px;padding-bottom:16px;margin-left:20px'>" +

                    "<span style = 'font-size:24px;color:#66c0f4;font-family:Arial,Helvetica,sans-serif;font-weight:bold'>" +
                        "Your Order ID : " + order.OrderId  +

                    "</span></br>" +

                    "</td>" +
                 "</tr>" +
                 "<tr>" +
                 "<td style = 'padding-top:12px;padding-bottom:24px;margin-left:20px'>" +

                    "<span style = 'font-size:14px;color:silver;font-family:Arial,Helvetica,sans-serif;font-weight:bold'>" +
                        "Tổng thanh toán giỏ hàng của bạn cần thanh toán là " + order.Price.ToString("0,0") + " VNĐ . Chúc bạn chơi game vui vẻ" +

                    "</span></br>" +
                    "<span style = 'padding-top:12px;font-size:14px;color:#c6d4df;font-family:Arial,Helvetica,sans-serif;font-weight:normal'>" +

                        "<p> This email message will serve as your receipt."+ "</p>" +

                    "</span>" +

                    "</td>" +
                 "</tr>" +
                 "<tr bgcolor='#121a25'>" +
                 "<td style='padding-top:20px;font-size:12px;color:#c6d4df;line-height:17px;font-family:Arial,Helvetica,sans-serif'>"  +
                 "<table>" +
                 "<tbody>" +
                 "<tr><td width = '200'><div align = 'right' ><b>" + order.CustomerId + " | Abyss &nbsp;&nbsp;</b></div></td><td width = '202'>" + order.Price.ToString("0,0") +" VNĐ </td></tr>" +
                 "<tr>" +
                              "<td width = '200' ><div align = 'right' ><b > &nbsp;</b></div></td >" +
           
                                         "<td width = '202'><hr width = '180' color = '#cccccc' align = 'left' size = '1' noshade></td>" +
                        
                                                    "</tr>" +
                                                    "<tr>"+
                              "<td width = '200'><div align = 'right'><b> Date Confirmed &nbsp; &nbsp;</b></div></td>"+
              
                                            "<td width = '202' >"+dateTime +"</td>" +
                   
                                               "</tr>" +
                                                                                       "</tbody>" +
                 "</table>" +
                 "</td>" +
                 "</tr>" +
                 "</tbody>" +
                 "</table>"+
                  "</td>" +

                 "</tr>"+
                 "<tr>"+

            "<td bgcolor = '#000000'>"+


                    "<table width = '460' height = '55' border = '0' align = 'center' cellpadding = '0' cellspacing = '0' >"+


                        "<tbody> <tr valign = 'center' >" +

                            "<td width = '110'>" +



                                "<img src = 'https://ci6.googleusercontent.com/proxy/11em3UmhXvSZ7mK0qOJZpSjCYTSNd8l1sWqc60_GvSJyLcXe_FmliC6TBX3036UREe2YclBWkSJT_So-WFHfBICrlBnbbpCqh8YA-NA=s0-d-e1-ft#http://storefront.steampowered.com/v/img/gift/VALVe.gif' alt = 'VALVE' width = '92' height = '26' hspace = '0' vspace = '0' border = '0' align = 'top' class='CToWUd'></a>"+
                            "</td>" +
                            "<td width = '350' valign='center'>" +
                                "<span style = 'color:#999999;font-size:9px;font-family:Verdana,Arial,Helvetica,sans-serif'>© Valve Corporation.All rights reserved. All trademarks are property of their respective owners in the US and other countries.</span>"+
                            "</td>"+
                        "</tr>" +

                    "</tbody></table>"+
            "</td>"+
    "</tr>"+
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
            _order.Update(order);
            return RedirectToAction("OrderAccept");
        }
        public IActionResult RemoveProduct(int id)
        {
            _product.Remove(id);
            return RedirectToAction("Product");
        }
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Title = "EDIT GAME";
            HttpContext.Session.SetInt32("IDGAME", id);
            var product = _product.GetById(id);
            var category = _category.GetCategories;
            //List<SelectListItem> selectedList = new List<SelectListItem>(category.ToList());
            List<SelectListItem> selectedList = category.Select(x => {
                return new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString(),
                    Selected = false
                };

            }).ToList();

            ViewBag.SelectedList = selectedList;
            //  _product.Remove(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product Product)
        {
            int id = int.Parse(HttpContext.Session.GetInt32("IDGAME").ToString());
          if(Product.Sale<0 || Product.Sale>100)
            {
                HttpContext.Session.SetString("ERRORSALE", "Sale game is between 0% and 100%");
                // ViewBag.Error = "Sale game is between 0% and 100%";
                return RedirectToAction("UpdateProduct/" + id);
            }
            if (Product.Price < 0)
            {
                HttpContext.Session.SetString("ERRORPRICE", "Price must be positive number");
                //ViewBag.ErrorPrice = "Price must be positive number";
                return RedirectToAction("UpdateProduct/"+id);
            }                      
                _product.Update(Product);
                return RedirectToAction("Product");
           
          
        }
        public IActionResult AddNew()
        {
            ViewBag.Title = "ADD NEW GAME";
            var category = _category.GetCategories;
            //List<SelectListItem> selectedList = new List<SelectListItem>(category.ToList());
        List<SelectListItem> selectedList = category.Select(x => {
               return new SelectListItem()
              {
                Text = x.CategoryName,
              Value = x.CategoryId.ToString(),
                 Selected = false
              };

          }).ToList();

            ViewBag.SelectedList = selectedList;
            
            return View();
        }

        [HttpPost]
        public IActionResult AddNew(ProductViewModel Product)
        {
            ViewBag.Title = "ADD NEW GAME";

            HttpContext.Session.Remove("ERRORSALE");
            HttpContext.Session.Remove("ERRORPRICE");
            HttpContext.Session.Remove("ERRORID");
            try
            {
                if (Product.Sale < 0 || Product.Sale > 100)
                {
                    HttpContext.Session.SetString("ERRORSALE", "Sale game is between 0% and 100%");
                    return RedirectToAction("AddNew");
                }
                if(Product.Price<0)
                {
                    HttpContext.Session.SetString("ERRORPRICE", "Price must be positive number");
                    return RedirectToAction("AddNew");
                }
                if(Product.Id<0)
                {
                    HttpContext.Session.SetString("ERRORID", "Game's Id must be positive number");

                    //ViewBag.ErrorId = "Game's Id must be positive number";
                    return RedirectToAction("AddNew");
                }
                Product NewProduct = new Product();
                // int count = _product.Products.Count();
                NewProduct.Id = Product.Id;
                NewProduct.Name = Product.Name;
                NewProduct.Picture = Product.Picture;
                NewProduct.Description = Product.Description;
                NewProduct.Category = Int32.Parse(Product.Category);
                NewProduct.Price = Product.Price;
                NewProduct.Sale = Product.Sale;
                NewProduct.Available = true;
                _product.Create(NewProduct);
                HttpContext.Session.Remove("ERRORPRODUCT");
                return RedirectToAction("Product");
            }
            catch(Exception e)
            {
                if (e.InnerException.Message.Contains("duplicate key"))
                {
                    HttpContext.Session.SetString("ERRORPRODUCT", "Product is already registered");
                }
                //return View();
                return RedirectToAction("AddNew");
            }                   
        }
        public IActionResult AddNewCategory()
        {
            ViewBag.Title = "Add New Category";
            return View();
        }
        [HttpPost]
        public IActionResult AddNewCategory(Category NewCategory)
        {
            //ViewBag.Title = "Add New Category";
                ViewBag.Title = "Add New Category";           
                _category.Create(NewCategory);
                HttpContext.Session.Remove("ERRORCATEGORY");
                return RedirectToAction("Product", "Admin");
       
          

        }

        public IActionResult DeleteAccept(int id)
        {
            var removeOrder = _order.GetOrderById(id);
            _order.Remove(removeOrder);
            return RedirectToAction("OrderAccept");
        }
    }
}
