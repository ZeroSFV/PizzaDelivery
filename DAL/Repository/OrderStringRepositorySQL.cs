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
    public class OrderStringRepositorySQL : IRepository<OrderString>
    {
        private Model1 dataBase;

        public OrderStringRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<OrderString> GetList()
        {
            return dataBase.OrderString.ToList();
        }

        public OrderString GetItem(int id)
        {
            return dataBase.OrderString.Find(id);
        }

        public void Create(OrderString item)
        {
            dataBase.OrderString.Add(item);
        }

        public void Update(OrderString item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            OrderString item = dataBase.OrderString.Find(id);
            if (item != null)
            {
                dataBase.OrderString.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
