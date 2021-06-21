using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        TranAnhMinhContext db = null;
        public OrderDetailDao()
        {
            db = new TranAnhMinhContext();
        }


        public bool Insert(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Add(entity);
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
