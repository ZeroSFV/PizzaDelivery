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
    public class BasketRepositorySQL : IRepository<Basket>
    {
        private Model1 dataBase;

        public BasketRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<Basket> GetList()
        {
            return dataBase.Basket.ToList();
        }

        public Basket GetItem(int id)
        {
            return dataBase.Basket.Find(id);
        }

        public void Create(Basket item)
        {
            dataBase.Basket.Add(item);
        }

        public void Update(Basket item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Basket item = dataBase.Basket.Find(id);
            if (item != null)
            {
                dataBase.Basket.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
