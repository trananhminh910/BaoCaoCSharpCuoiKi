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
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.adName = session.UserName;

            var dao = new UserAccountDao();
            ViewBag.BlockedAccountCount = dao.BlockedAccountCount();

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
            var user = new UserAccountDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(UserAccount user)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserAccountDao();
                if (dao.getById(user.UserName) != null)
                {
                    SetAlert("Tên người dùng đã tồn tại, vui lòng nhập tên khác!", "warning");
                    return RedirectToAction("Create", "User");
                }
                if (user.UserName == "" || user.Password == "" || user.UserName == null || user.Password == null)
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Create", "User");
                }
                var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pass;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm mới người dùng thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm mới người dùng không thành công!", "error");
                }
            }



            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(UserAccount user)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserAccountDao();


                if (user.Password == "" || user.Password == null)
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Edit", "User");
                }
                var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pass;
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "User");
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
            var dao = new UserAccountDao();
            if (dao.Delete(id))
            {
                ModelState.AddModelError("", "Xóa thành công");
            }
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult DeleteBlockedAccount()
        {
            var dao = new UserAccountDao();
            dao.DeleteBlockedAccount();
            ModelState.AddModelError("", "Xóa thành công");
            return RedirectToAction("Index");
        }
    }
}