using Model.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        TranAnhMinhContext db = null;
        public OrderDao()
        {
            db = new TranAnhMinhContext();
        }


        public int Insert(Order entity)
        {
                db.Orders.Add(entity);
                db.SaveChanges();
                return entity.ID;
        }

        public bool Approved(int id)
        {
            var entity = db.Orders.Find(id);
            entity.Status = true;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var Order = db.Orders.Find(id);
                db.Orders.Remove(Order);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Order> ListAllPagingApproved(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders.Where(x => x.Status == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TotalMoney.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public IEnumerable<Order> ListAllPagingUnapproved(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders.Where(x => x.Status == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TotalMoney.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}
