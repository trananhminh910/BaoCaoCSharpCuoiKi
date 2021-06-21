using Model.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)

        {
            if (ModelState.IsValid)
            {
                var dao = new UserAccountDao();
                var result = false;
                if (model.Username == "" || model.Password == "" || model.Username == null || model.Password == null)
                {
                            ModelState.AddModelError("", "Cần Nhập đủ thông tin đăng nhập!");
                }
                else
                {
                    result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                    if (result)
                    {
                        var user = dao.getById(model.Username);
                        var userSession = new UserLogin();

                        userSession.UserName = user.UserName;
                        userSession.UserID = user.ID;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "home", new { area = "Admin" });
                    }
                    else
                    {
                        if (model.Username == "" || model.Password == "" || model.Username == null || model.Password == null)
                        {
                            ModelState.AddModelError("", "Cần Nhập đủ thông tin đăng nhập!");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng nhập không hợp lệ");
                        }
                    }
                }
            }
            return View("Index");
        }

    }

    
}