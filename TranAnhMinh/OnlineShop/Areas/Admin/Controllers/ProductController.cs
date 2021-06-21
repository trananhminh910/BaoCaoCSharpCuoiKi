using Model.Dao;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.Model;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.adName = session.UserName;

            if(DateTime.Now.Month.ToString().Length == 1 )
            {
                ViewBag.monthURLImage = "0" + DateTime.Now.Month.ToString();
            } else
            {
                ViewBag.monthURLImage = DateTime.Now.Month.ToString();
            }
            ViewBag.yearURLImage = DateTime.Now.Year.ToString();

            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.search = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        public ActionResult ViewDetail(int id)
        {
            var entity = new ProductDao().ViewDetail(id);
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ViewBag.monthURLImage = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ViewBag.monthURLImage = DateTime.Now.Month.ToString();
            }
            ViewBag.yearURLImage = DateTime.Now.Year.ToString();
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            var entity = new ProductDao().ViewDetail(id);
            SetViewBag();
            return View(entity);
        }

        [HttpPost]
        public ActionResult Create(Product entity, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (
                    entity.Name == "" 
                    || entity.Image == "" 
                    || entity.Description == ""
                    || entity.Name == null 
                    || entity.Quantity == null
                    || entity.Price == null
                    || entity.Description == null
                    || entity.CategoryID == null
                    )
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Create", "Product");
                }
                string size = fc["size"];
                int sizeResize = 200;
                if (!string.IsNullOrEmpty(size))
                {
                    int.TryParse(size, out sizeResize);
                }

                List<string> fileNames = new List<string>();
                try
                {
                    // Duyệt qua các file được gởi lên phía client
                    foreach (string fileName in Request.Files)
                    {
                        //Lấy ra file trong list các file gởi lên
                        HttpPostedFileBase file = Request.Files[fileName];
                        //Save file content goes here
                        if (file != null && file.ContentLength > 0)
                        {
                            //Định nghĩa đường dẫn lưu file trên server
                            //ở đây mình lưu tại đường dẫn yourdomain.com/Uploads/
                            var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads\\products\\", Server.MapPath(@"\")));
                            //Lưu trữ hình ảnh theo từng tháng trong năm
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), DateTime.Now.ToString("yyyy-MM"));
                            bool isExists = System.IO.Directory.Exists(pathString);
                            if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                            var path = string.Format("{0}\\{1}", pathString, file.FileName);
                            string newFileName = file.FileName;
                            //lấy đường dẫn lưu file sau khi kiểm tra tên file trên server có tồn tại hay không
                            var newPath = GetNewPathForDupes(path, ref newFileName);
                            string serverPath = string.Format("/{0}/{1}/{2}", "Uploads", DateTime.Now.ToString("yyyy-MM"), newFileName);
                            //Lưu hình ảnh Resize từ file sử dụng file.InputStream
                            SaveResizeImage(Image.FromStream(file.InputStream), sizeResize, newPath);
                            fileNames.Add("LocalPath: " + newPath + "<br/>ServerPath: " + serverPath);

                            TempData["Image"] = fileNames;
                            entity.Image = newFileName;
                            long id = dao.Insert(entity);
                            if (id > 0)
                            {
                                SetAlert("Thêm mới sản phẩm thành công!", "success");
                                return RedirectToAction("Index", "Product");
                            }
                            else
                            {
                                SetAlert("Thêm mới sản phẩm không thành công!", "error");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["e"] = ex.Message;
                }
            }
            return View("Index");
        }
        // GET: /File/
        [HttpPost]
        public ActionResult Upload(FormCollection fc)
        {
            string size = fc["size"];
            int sizeResize = 200;
            if (!string.IsNullOrEmpty(size))
            {
                int.TryParse(size, out sizeResize);
            }

            List<string> fileNames = new List<string>();
            try
            {
                // Duyệt qua các file được gởi lên phía client
                foreach (string fileName in Request.Files)
                {
                    //Lấy ra file trong list các file gởi lên
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        //Định nghĩa đường dẫn lưu file trên server
                        //ở đây mình lưu tại đường dẫn yourdomain.com/Uploads/
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads\\", Server.MapPath(@"\")));
                        //Lưu trữ hình ảnh theo từng tháng trong năm
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), DateTime.Now.ToString("yyyy-MM"));
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        string newFileName = file.FileName;
                        //lấy đường dẫn lưu file sau khi kiểm tra tên file trên server có tồn tại hay không
                        var newPath = GetNewPathForDupes(path, ref newFileName);
                        string serverPath = string.Format("/{0}/{1}/{2}", "Uploads", DateTime.Now.ToString("yyyy-MM"), newFileName);
                        //Lưu hình ảnh Resize từ file sử dụng file.InputStream
                        SaveResizeImage(Image.FromStream(file.InputStream), sizeResize, newPath);
                        fileNames.Add("LocalPath: " + newPath + "<br/>ServerPath: " + serverPath);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
            }
            var dao = new ProductDao();
            TempData["Image"] = fileNames;
            return RedirectToAction("Index", "Home");

        }
        //Hàm resize hình ảnh
        public bool SaveResizeImage(Image img, int width, string path)
        {
            try
            {
                // lấy chiều rộng và chiều cao ban đầu của ảnh
                int originalW = img.Width;
                int originalH = img.Height;
                // lấy chiều rộng và chiều cao mới tương ứng với chiều rộng truyền vào của ảnh (nó sẽ giúp ảnh của chúng ta sau khi resize vần giứ được độ cân đối của tấm ảnh
                int resizedW = width;
                int resizedH = (originalH * resizedW) / originalW;
                Bitmap b = new Bitmap(resizedW, resizedH);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.Bicubic;    // Specify here
                g.DrawImage(img, 0, 0, resizedW, resizedH);
                g.Dispose();
                b.Save(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private string GetNewPathForDupes(string path, ref string fn)
        {
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int counter = 1;
            string newFullPath = path;
            string new_file_name = filename + extension;
            while (System.IO.File.Exists(newFullPath))
            {
                string newFilename = string.Format("{0}({1}){2}", filename, counter, extension);
                new_file_name = newFilename;
                newFullPath = Path.Combine(directory, newFilename);
                counter++;
            };
            fn = new_file_name;
            return newFullPath;
        }
        [HttpPost]
        public ActionResult Edit(Product entity)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                if (
                    entity.Name == ""
                    || entity.Image == ""
                    || entity.Description == ""
                    || entity.Name == null
                    || entity.Quantity == null
                    || entity.Price == null
                    || entity.Description == null
                    || entity.CategoryID == null
                    )
                {
                    SetAlert("Vui lòng nhập đầy đủ thông tin!", "warning");
                    return RedirectToAction("Edit", "Product");
                }
                var result = dao.Update(entity);
                if (result)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Product");
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
            var dao = new ProductDao();
            if (dao.Delete(id))
            {
                ModelState.AddModelError("", "Xóa thành công");
            }
            return RedirectToAction("Index");
        }

        public void SetViewBag(long? selectedid=null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedid);
        }
    }
}