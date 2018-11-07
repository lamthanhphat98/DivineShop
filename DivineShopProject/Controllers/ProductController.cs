using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using DivineShopProject.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DivineShopProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly IUser _user;
        private readonly ICart _cart;
        private readonly IOrder _order;
        private readonly ICategory _category;
        private readonly ILike _like;
        //Dictionary<Product, List<String>> listCode = new Dictionary<Product, List<String>>();
        public List<Cart> ListCart;
        public ProductController(IProduct product, ICart cart, IOrder order, ICategory category, ILike Like, IUser user)
        {
            _product = product;
            _cart = cart;
            _order = order;
            _category = category;
            _like = Like;
            _user = user;
        }

        public IActionResult List(int page = 1, int pageSize = 6)
        {
            HttpContext.Session.Remove("ERRORLOGIN");
            if (HttpContext.Session.GetString("Username") != null)
            {
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
                //HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
                //  products = products.or
                PagedList<Product> modelList = new PagedList<Product>(products, page, pageSize);
                return View("List", modelList);
               
            }
            else
            {
                var products = _product.Products;
                ViewBag.Username = HttpContext.Session.GetString("Username");
                int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
                var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
                foreach (var item in totalCart)
                {
                    item.Product = _product.GetById(item.Id);
                }
                HttpContext.Session.SetString("TOTAL", "00");
                HttpContext.Session.SetString("CART", "0");

                ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
                ViewBag.Cart = CountAll;
                PagedList<Product> modelList = new PagedList<Product>(products, page, pageSize);
                return View("List", modelList);
            }

        }

        public IActionResult ShowDetail(int id)
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            // ViewBag.Cart = CountAll;

            var product = _product.GetById(id);
            ViewBag.Name = product.Name;
            return View(product);
        }
        public IActionResult Search(string search)
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            //ViewBag.Cart = CountAll;
            var product = _product.SearchUser(search);
            return View(product);
        }

        public IActionResult Menu(string search)
        {
            var product = _product.SearchUser(search);
            HttpContext.Session.SetString("Search", search);
            //  String option = HttpContext.Session.GetString("Option");
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            //ViewBag.Cart = CountAll;

            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"}
            };

            // HttpContext.Session.SetObject("SelectedList", selectedList);

            ViewBag.SelectedList = selectedList;
            ViewBag.Name = search;
            // ViewBag.SelectedList = selectedList;
            ViewBag.Product = product;
            return View();
        }
        public IActionResult Sort(String option)
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            //ViewBag.Cart = CountAll;

            //HttpContext.Session.SetString("Option",option);
            //option = ViewBag.Option;

            var product = _product.SearchUser(HttpContext.Session.GetString("Search"));
            if (option != null)
            {
                if (option.Equals("A-Z"))
                {
                    product = product.OrderBy(p => p.Name);
                }
                else if (option.Equals("Z-A"))
                {
                    product = product.OrderByDescending(p => p.Name);
                }
            }
            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"}
            };

            // HttpContext.Session.SetObject("SelectedList", selectedList);

            ViewBag.SelectedList = selectedList;
            //List<SelectListItem> selectedList = new List<SelectListItem>
            //{
            //     new SelectListItem {Text = "Mặc định", Value = "Default"},
            //     new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
            //     new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"}
            //};

            ViewBag.Name = HttpContext.Session.GetString("Search");
            // ViewBag.SelectedList = selectedList;

            return View(product);
        }
        public IActionResult Cart(String search)
        {
            String userId = HttpContext.Session.GetString("Username");
            if(userId==null)
            {
                return RedirectToAction("Login", "Home");               
            }
            else
            {     
            int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            foreach (var item in totalCart)
            {
                item.Product = _product.GetById(item.Id);
            }
            HttpContext.Session.SetString("CART", CountAll + "");
            HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            //ViewBag.Cart = CountAll;

            if (HttpContext.Session.GetString("SEARCH") == null)
            {
                HttpContext.Session.SetString("SEARCH", search);
            }
            //  HttpContext.Session.SetString("SEARCH", search);
            ViewData["Name"] = HttpContext.Session.GetString("SEARCH");
            var cart = _cart.GetByUSerId(userId);
            foreach (var item in cart)
            {
                item.Product = _product.GetById(item.Id);
            }

            return View(cart);
            }
        }
        public IActionResult Store(int id, int quantity)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var currentProduct = _product.GetById(id);
                if (currentProduct.Available==false)
                {
                    HttpContext.Session.SetString("ERRORPRODUCT", "Sản phảm "+currentProduct.Name +" bạn muốn mua hiện đã hết hàng");
                    return RedirectToAction("List");

                }
                else
                {
                    HttpContext.Session.Remove("ERRORPRODUCT");

                    if (quantity != 0 || quantity != 1)
                    {
                        String userId = HttpContext.Session.GetString("Username");
                        var cart = _cart.GetCartByUser(userId, id);
                        if (cart != null)
                        {
                            cart.Quantity += quantity;
                            _cart.UpdateCart(cart);
                        }
                        else
                        {
                            cart = new Cart();
                            cart.Product = _product.GetById(id);
                            cart.Quantity = quantity;
                            // cart.Id = id;
                            cart.UserId = userId;
                            DateTime date = Convert.ToDateTime(DateTime.Now);
                            cart.DateBuy = date;
                            _cart.AddCart(cart);

                        }
                    }
                    else
                    {
                        // ListCart = new List<Cart>();
                        String userId = HttpContext.Session.GetString("Username");
                        var cart = _cart.GetCartByUser(userId, id);
                        if (cart != null)
                        {
                            cart.Quantity++;
                            _cart.UpdateCart(cart);
                        }
                        else
                        {
                            cart = new Cart();
                            cart.Product = _product.GetById(id);
                            cart.Quantity = 1;
                            // cart.Id = id;
                            cart.UserId = userId;
                            DateTime date = Convert.ToDateTime(DateTime.Now);
                            cart.DateBuy = date;
                            _cart.AddCart(cart);

                        }
                    }
                    return RedirectToAction("List");
                }
            }
        }
        public IActionResult Update(int id, int quantity)
        {
            String userId = HttpContext.Session.GetString("Username");
            var cart = _cart.GetCartByUser(userId, id);
            if (cart != null)
            {
                cart.Quantity = quantity;
                _cart.UpdateCart(cart);
            }

            return RedirectToAction("Cart");
        }
        public IActionResult Remove(int id)
        {
            String userId = HttpContext.Session.GetString("Username");
            var cart = _cart.GetCartByUser(userId, id);
            if (cart != null)
            {
                _cart.Remove(cart);
            }

            return RedirectToAction("Cart");
        }
        public IActionResult Payment()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
                //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
                //foreach (var item in totalCart)
                //{
                //    item.Product = _product.GetById(item.Id);
                //}
                //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
                //HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));

                //ViewBag.Cart = CountAll;
                ViewData["Name"] = "Thanh toán / Payment";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Category()
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

            ViewData["Name"] = "Thể loại";
            var category = _category.GetCategories;
            return View(category);
        }
        [HttpPost]
        public IActionResult Payment(Order Order)
        {       
            if (HttpContext.Session.GetString("Username") != null)
            {
                var currentUser = _user.GetById(HttpContext.Session.GetString("Username"));
                if (!ModelState.IsValid)
                {
                    ViewData["Name"] = "Thanh toán / Payment";
                    return View();
                }
                else
                {
                  Double total = Double.Parse(HttpContext.Session.GetString("TOTAL"));
                    if (total == 0)
                    {
                        ViewData["Name"] = "Thanh toán / Payment";
                        ViewBag.ERROR = "Giỏ hàng của bạn không tồn tại";
                        return View();
                    }
                    else
                    {
                        if (currentUser.Email.Equals(Order.Email))
                        {
                            Order.CustomerId = HttpContext.Session.GetString("Username");
                            Order.Price = Double.Parse(HttpContext.Session.GetString("TOTAL"));
                            //Console.WriteLine(Order.Fullname);
                            Order.Accept = false;
                            var cart = _cart.GetByUSerId(Order.CustomerId);
                            _cart.RemoveAll(HttpContext.Session.GetString("Username"));
                            _order.Add(Order);
                            //HttpContext.Session.Remove("ERROREMAIL");
                            return RedirectToAction("List");
                        }
                        else
                        {
                            ViewData["Name"] = "Thanh toán / Payment";
                            ViewBag.ERROR = "Email bạn nhập phải khớp với email của bạn";
                            //HttpContext.Session.SetString("ERROREMAIL", "Your email must match with current email");
                            return View();
                        }
                    }
                    
                }

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }
        public IActionResult ViewCategory(String search)
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

            ViewData["Name"] = search;
            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"}
            };
            ViewBag.SelectedList = selectedList;
            HttpContext.Session.SetString("CATEGORYSORT", search);
            var category = _category.GetCategoryByName(search);
            var product = _product.GetProductsByCategory(category.CategoryId);
            return View(product);
        }

        public IActionResult ShowAll(int page = 1, int pageSize = 12)
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            //ViewBag.Cart = CountAll;
            // HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            ViewData["Name"] = "All";
            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"},
                  new SelectListItem {Text = "Giá cao -> thấp", Value = "highest"},
                  new SelectListItem {Text = "Giá thấp -> cao", Value = "lowest"},
            };
            ViewBag.SelectedList = selectedList;
            // var category = _category.GetCategoryByName(search);
            var product = _product.Products;
            PagedList<Product> modelList = new PagedList<Product>(product, page, pageSize);
            return View("ShowAll",modelList);
            //return View(product);
        }

        public IActionResult SortAll(String option)
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

            //HttpContext.Session.SetString("Option",option);
            //option = ViewBag.Option;

            var product = _product.Products;
            if (option != null)
            {
                if (option.Equals("A-Z"))
                {
                    product = product.OrderBy(p => p.Name);
                }
                else if (option.Equals("Z-A"))
                {
                    product = product.OrderByDescending(p => p.Name);
                }
                else if (option.Equals("highest"))
                {
                    product = product.OrderByDescending(p => p.Price);
                }
                else if (option.Equals("lowest"))
                {
                    product = product.OrderBy(p => p.Price);
                }
            }
            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"},
                  new SelectListItem {Text = "Giá cao -> thấp", Value = "highest"},
                  new SelectListItem {Text = "Giá thấp -> cao", Value = "lowest"},
            };
            ViewBag.SelectedList = selectedList;
            ViewBag.Name = "All";
            return View(product);
          
        }
        public IActionResult Like(int id)
        {
            String username = HttpContext.Session.GetString("Username");
            if (username != null)
            {
                var like = _like.GetLike(username, id);
                if (like == null)
                {
                    like = new Like();
                    like.ProductId = id;
                    like.UserId = username;
                    like.product = _product.GetById(id);
                    _like.AddLike(like);

                    return Json(new { success = true, msg = "You have liked " + like.product.Name });
                    // HttpContext.Session.Remove("ERRORLIKE");
                }
                else
                {
                    return Json(new { success = true, msg = "You have liked this game" });
                    // ViewBag.ERROR = "YOU HAVE LIKED THIS GAME!!!";
                }
            }
            else
            {
                return Json(new { success = true, msg = "You must login before liking this game" });
                // return RedirectToAction("Login", "Home");
            }

            return RedirectToAction("List");
        }
        public IActionResult ListLike()
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
            var listlike = _like.GetAllLike(HttpContext.Session.GetString("Username"));
            ViewData["Name"] = "Thay đổi danh sách yêu thích";
            //  LikeModel like = new LikeModel();
            if(listlike!=null)
            {
                foreach (var item in listlike)
                {
                    item.product = _product.GetById(item.ProductId);
                }
            }
          
            return View(listlike);
        }

        public IActionResult Dislike(int id)
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
            ViewData["Name"] = "Thay đổi danh sách tui yêu thích";
            String username = HttpContext.Session.GetString("Username");
            //  LikeModel like = new LikeModel();
            _like.DisLike(username, id);
            // var listlike = _like.GetAllLike(HttpContext.Session.GetString("Username"));
            return RedirectToAction("ListLike");
        }

        public IActionResult ViewCategorySort(String option)
        {
            //    int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //    var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //    foreach (var item in totalCart)
            //    {
            //        item.Product = _product.GetById(item.Id);
            //    }
            //    ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));

            //    ViewBag.Cart = CountAll;
            ViewData["Name"] = HttpContext.Session.GetString("CATEGORYSORT");
            List<SelectListItem> selectedList = new List<SelectListItem>
            {
                 new SelectListItem {Text = "Mặc định", Value = "Default"},
                 new SelectListItem {Text = "Tên (A-Z)", Value = "A-Z"},
                 new SelectListItem {Text = "Tên (Z-A)", Value = "Z-A"}
            };


            ViewBag.SelectedList = selectedList;
            var category = _category.GetCategoryByName(HttpContext.Session.GetString("CATEGORYSORT"));
            var product = _product.GetProductsByCategory(category.CategoryId);
            // var product = _product.SearchUser(HttpContext.Session.GetString("Search"));
            if (option != null)
            {
                if (option.Equals("A-Z"))
                {
                    product = product.OrderBy(p => p.Name);
                }
                else if (option.Equals("Z-A"))
                {
                    product = product.OrderByDescending(p => p.Name);
                }
            }
            return View(product);
        }
        public IActionResult InforAboutUs()
        {
            ViewData["Name"] = "Thông tin";
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            //HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));

            //ViewBag.Cart = CountAll;
            return View();
        }
        public IActionResult PaymentByCash()
        {
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0");
            //HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));

            //ViewBag.Cart = CountAll;
            ViewBag.Name = "Thanh toán qua số dư";
            return View();
        }
        [HttpPost]
        public IActionResult PaymentByCash(String total)
        {
            
            //int CountAll = _cart.GetAllItems(HttpContext.Session.GetString("Username"));
            //var totalCart = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
            //foreach (var item in totalCart)
            //{
            //    item.Product = _product.GetById(item.Id);
            //}
            //ViewBag.Total = totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100)));
            ////HttpContext.Session.SetObject("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))));
            //HttpContext.Session.SetString("TOTAL", totalCart.Sum(c => c.Quantity * ((c.Product.Price - (c.Product.Price * c.Product.Sale) / 100))).ToString("0,0"));

            //ViewBag.Cart = CountAll;

            ViewBag.Name = "Thanh toán qua số dư";
            Double cash = Double.Parse(HttpContext.Session.GetString("CASH"));
            

            if (Double.Parse(total) >= cash)
            {
                ViewBag.Name = "Thanh toán qua số dư";
                ViewBag.Error = "Bạn không đủ tiền để trả cho sản phẩm này";
                return View();
            }
            else if(Double.Parse(total) == 0)
            {
                ViewBag.Name = "Thanh toán qua số dư";
                ViewBag.Error = "Giỏ hàng của bạn không tồn tại";
                return View();
            }
            else
            {              
                 Dictionary<Product, List<String>> listCode = new Dictionary<Product, List<String>>();
                var Product = _cart.GetByUSerId(HttpContext.Session.GetString("Username"));
                foreach (var item in Product)
                {
                    List<String> listCodeString = new List<String>();
                    item.Product = _product.GetById(item.Id);
                    Random r = new Random();
                    for (int i = 0; i < item.Quantity; i++)
                    {
                      
                        listCodeString.Add(GenerateName(10,r));
                    }
                    
                    listCode.Add(item.Product, listCodeString);          
                }

                var str = JsonConvert.SerializeObject(listCode.ToArray(), Formatting.Indented);
                //HttpContext.Session.SetObject("LISTCODE", listCode);
                HttpContext.Session.SetString("LISTCODE", str);

                _cart.RemoveAll(HttpContext.Session.GetString("Username"));
                var account = _user.GetById(HttpContext.Session.GetString("Username"));
                account.Cash = account.Cash - Double.Parse(total);
                _user.Update(account);
                HttpContext.Session.SetString("CASH", account.Cash.ToString("0,0"));
                //Response.Redirect("")
                return RedirectToAction("Success", "Home");
            }
            // return View();
        }
        public static string GenerateName(int len,Random r)
        {
         
            string[] consonants = { "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "L", "N", "P", "Q", "R", "S", "SH", "ZH", "T", "V", "W", "X","1","2","3","4", "5", "6", "7", "8", "9" };
            string[] vowels = { "A", "E", "I", "O", "U", "AE", "Y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
        public IActionResult GetCode()
        {
            var str = HttpContext.Session.GetString("LISTCODE");
            if(str==null)
            {
                Dictionary<Product, List<String>> listCode = new Dictionary<Product, List<String>>();
                ViewBag.Name = "Nhận code";
                return View(listCode);
            }
            else
            {

        
            var list = JsonConvert.DeserializeObject<KeyValuePair<Product, List<String>>[]>(str).ToDictionary(
                keys => keys.Key,
                values => values.Value
                );
          
            ViewBag.Name = "Nhận code";
            return View(list);
            }
            // Dictionary<Product, List<String>> listCode = new Dictionary<Product, List<String>>();
            //var codeGame = HttpContext.Session.GetObject<Dictionary<Product, List<String>>>("LISTCODE");
            //return View(codeGame);
        }
    }

}
