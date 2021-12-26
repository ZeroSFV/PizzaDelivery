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
    public class TypeRepositorySQL : IRepository<DAL.Entities.Type>
    {
        private Model1 dataBase;

        public TypeRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<DAL.Entities.Type> GetList()
        {
            return dataBase.Type.ToList();
        }

        public DAL.Entities.Type GetItem(int id)
        {
            return dataBase.Type.Find(id);
        }

        public void Create(DAL.Entities.Type item)
        {
            dataBase.Type.Add(item);
        }

        public void Update(DAL.Entities.Type item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DAL.Entities.Type item = dataBase.Type.Find(id);
            if (item != null)
            {
                dataBase.Type.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
