using Model.Dao;
using Model.Model;
using OnlineShop.Areas.Customer.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Customer/Cart
        public ActionResult Index()
        {
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ViewBag.monthURLImage = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ViewBag.monthURLImage = DateTime.Now.Month.ToString();
            }
            ViewBag.yearURLImage = DateTime.Now.Year.ToString();

            var listCart = (List<CartItem>)Session[CartSession];
            var totalMoney = 0;
            foreach (var item in listCart)
            {
                var totalMoneyItem = Convert.ToInt32(item.Product.Price * item.Quantity);
                totalMoney = totalMoney + totalMoneyItem;
            }
            ViewBag.totalMoney = totalMoney;

            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
                return View(list);
        }

        public ActionResult AddItem(int productid, int quantity)
        {
            var product = new ProductDao().ViewDetail(productid);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x => x.Product.ID == productid))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productid)
                        {
                            item.Quantity += quantity;
                        }
                    }
                } else {
                    //Add new object cart item

                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;

                    list.Add(item);
                    //gan vao session
                    Session[CartSession] = list;
                }
                
            } else
            {
                //Add new object cart item

                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");   
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });

        }

        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null )
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Payment()
        {
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ViewBag.monthURLImage = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ViewBag.monthURLImage = DateTime.Now.Month.ToString();
            }
            ViewBag.yearURLImage = DateTime.Now.Year.ToString();

            var ListCart = (List<CartItem>)Session[CartSession];
            var totalMoney = 0;
            foreach (var item in ListCart)
            {
                var totalMoneyItem = Convert.ToInt32(item.Product.Price * item.Quantity);
                totalMoney = totalMoney + totalMoneyItem;
            }
            ViewBag.totalMoney = totalMoney;

            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                ViewBag.UserName = session.UserName;
                ViewBag.UserID = session.UserID;
            }
            else
            {
                ViewBag.Alert = "Hello World";
                return RedirectToAction("Index");
            }
            
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult PaymentAction()
        {
            var order = new Order();
            var cart = (List<CartItem>)Session[CartSession];
            var totalMoney = 0;
            foreach (var item in cart)
            {
                var totalMoneyItem = Convert.ToInt32(item.Product.Price * item.Quantity);
                totalMoney = totalMoney + totalMoneyItem;
            }
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session != null)
            {
                order.CreateDate = DateTime.Now;
                order.CustomerID = session.UserID;
                order.TotalMoney = totalMoney;
                order.Status = false;
            } else
            {
                ViewBag.Alert = "Hello World";
                return RedirectToAction("Payment","Cart");
            }
            try
            {
                var id = new OrderDao().Insert(order);
                var detailDao = new OrderDetailDao();
                foreach(var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                }
            } catch(Exception ex)
            {
                throw;
            }
            return RedirectToAction("Completed", "Cart");
        }

        public ActionResult Completed()
        {
            var dao = new ProductDao();
            var categoryDao = new CategoryDao();

            ViewBag.ListCategory = categoryDao.ListAll();
            return View();
        }
    }   
}