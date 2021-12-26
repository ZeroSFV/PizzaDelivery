using System;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepositorySQL : IRepository<User>
    {
        private Model1 dataBase;

        public UserRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<User> GetList()
        {
            return dataBase.User.ToList();
        }

        public User GetItem(int id)
        {
            return dataBase.User.Find(id);
        }

        public void Create(User item)
        {
            dataBase.User.Add(item);
        }

        public void Update(User item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User item = dataBase.User.Find(id);
            if (item != null)
            {
                dataBase.User.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
