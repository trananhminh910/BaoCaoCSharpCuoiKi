using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.Model;

namespace Model.Dao
{
    public class ProductDao
    {
        TranAnhMinhContext db = null;
        public ProductDao()
        {
            db = new TranAnhMinhContext();
        }

       
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public void updateImage(Product entity, string fileName)
        {
            var Product = db.Products.Find(entity.ID);
            Product.Image = fileName;
            db.SaveChanges();
        }

        public bool Update(Product entity)
        {
            try
            {
                var Product = db.Products.Find(entity.ID);
                Product.Name = entity.Name;
                Product.Quantity = entity.Quantity;
                Product.Price = entity.Price;
                Product.Description = entity.Description;
                Product.Status = entity.Status;
                Product.CategoryID = entity.CategoryID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var Product = db.Products.Find(id);
                db.Products.Remove(Product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Quantity).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);
        }

        public IEnumerable<Product> ListAllPaging(int cateID, string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products.Where(x => x.CategoryID == cateID);

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Quantity).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);
        }
    }
}
