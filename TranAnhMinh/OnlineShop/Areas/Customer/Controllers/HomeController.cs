using Model.Dao;
using OnlineShop.Areas.Customer.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Customer/Home
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new ProductDao();
            var categoryDao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            if (Request["cateID"] == null)
            {
                model = dao.ListAllPaging(searchString, page, pageSize);
            } else
            {
                var cateID = int.Parse(Request["cateID"]);
                model = dao.ListAllPaging(cateID, searchString, page, pageSize);
            }

            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ViewBag.monthURLImage = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ViewBag.monthURLImage = DateTime.Now.Month.ToString();
            }
            ViewBag.yearURLImage = DateTime.Now.Year.ToString();
            ViewBag.search = searchString;
            ViewBag.ListCategory = categoryDao.ListAll();
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}