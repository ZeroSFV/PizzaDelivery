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
    public class StatusRepositorySQL : IRepository<Status>
    {
        private Model1 dataBase;

        public StatusRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<Status> GetList()
        {
            return dataBase.Status.ToList();
        }

        public Status GetItem(int id)
        {
            return dataBase.Status.Find(id);
        }

        public void Create(Status item)
        {
            dataBase.Status.Add(item);
        }

        public void Update(Status item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Status item = dataBase.Status.Find(id);
            if (item != null)
            {
                dataBase.Status.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
