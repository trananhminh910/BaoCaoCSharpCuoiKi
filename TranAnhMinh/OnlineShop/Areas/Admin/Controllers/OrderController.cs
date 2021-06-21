using Model.Dao;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.adName = session.UserName;

            var dao = new OrderDao();
            var model = dao.ListAllPagingApproved(searchString, page, pageSize);
            ViewBag.search = searchString;
            return View(model);
        }

        public ActionResult unapproved(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.adName = session.UserName;

            var dao = new OrderDao();
            var model = dao.ListAllPagingUnapproved(searchString, page, pageSize);
            
            ViewBag.search = searchString;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new OrderDao();
            if (dao.Delete(id))
            {
                SetAlert("Xóa thành công", "success");
            }
            return RedirectToAction("Index");
        }


        public ActionResult approved(int id)
        {
            var dao = new OrderDao();
            if (dao.Approved(id))
            {
                SetAlert("Duyệt thành công", "success");
            }
            return RedirectToAction("Index");
        }

    }
}