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
    public class PizzaRepositorySQL : IRepository<Pizza>
    {
        private Model1 dataBase;

        public PizzaRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<Pizza> GetList()
        {
            return dataBase.Pizza.ToList();
        }

        public Pizza GetItem(int id)
        {
            return dataBase.Pizza.Find(id);
        }

        public void Create(Pizza item)
        {
            dataBase.Pizza.Add(item);
        }

        public void Update(Pizza item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Pizza item = dataBase.Pizza.Find(id);
            if (item != null)
            {
                dataBase.Pizza.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
