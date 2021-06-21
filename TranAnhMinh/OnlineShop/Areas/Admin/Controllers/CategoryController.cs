using Model.Dao;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.Model;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.adName = session.UserName;

            var dao = new CategoryDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.search = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var entity = new CategoryDao().ViewDetail(id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult Create(Category entity)
        {

            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                if (entity.Name == "" || entity.Description == "" || entity.Name == null || entity.Description == null)
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Create", "Category");
                }
                long id = dao.Insert(entity);
                if (id > 0)
                {
                    SetAlert("Thêm mới danh mục sản phẩm thành công!", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Thêm mới danh mục sản phẩm không thành công!", "error");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Category entity)
        {

            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();


                if (entity.Name == "" || entity.Description == "" || entity.Name == null || entity.Description == null)
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Edit", "Category");
                }
                var result = dao.Update(entity);
                if (result)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Cập nhật không thành công!", "error");
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new CategoryDao();
            if (dao.Delete(id))
            {
                ModelState.AddModelError("", "Xóa thành công");
            }
            return RedirectToAction("Index");
        }

    }
}