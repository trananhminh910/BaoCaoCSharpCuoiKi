using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.Model;

namespace Model.Dao
{
    public class UserAccountDao
    {
        TranAnhMinhContext db = null;
        public UserAccountDao()
        {
            db = new TranAnhMinhContext();
        }

        public long Insert(UserAccount entity)
        {
            db.UserAccounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(UserAccount entity)
        {
            try
            {
                var user = db.UserAccounts.Find(entity.ID);
                user.Password = entity.Password;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public UserAccount ViewDetail(int id)
        {
            return db.UserAccounts.Find(id);
        }

        public UserAccount getById(string username)
        {
            return db.UserAccounts.SingleOrDefault(x=> x.UserName == username);
        }

        public bool Login(string username, string password)
        {
            var result = db.UserAccounts.Count(x => x.UserName == username && x.Password == password);
            if(result > 0 )
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccounts.Find(id);
                db.UserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBlockedAccount()
        {
            try
            {
                IQueryable<UserAccount> users = db.UserAccounts.Where(x => x.Status == false);
                db.UserAccounts.RemoveRange(users);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int BlockedAccountCount()
        {
            try
            {
                var userBlocked = db.UserAccounts.Where(x => x.Status == false);
                return userBlocked.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IEnumerable<UserAccount> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserAccount> model = db.UserAccounts;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}
