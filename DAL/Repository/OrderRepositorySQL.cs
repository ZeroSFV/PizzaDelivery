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
    public class OrderRepositorySQL : IRepository<Order>
    {
        private Model1 dataBase;

        public OrderRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<Order> GetList()
        {
            return dataBase.Order.ToList();
        }

        public Order GetItem(int id)
        {
            return dataBase.Order.Find(id);
        }

        public void Create(Order item)
        {
            dataBase.Order.Add(item);
        }

        public void Update(Order item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order item = dataBase.Order.Find(id);
            if (item != null)
            {
                dataBase.Order.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
