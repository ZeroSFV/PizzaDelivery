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
    public class SizeRepositorySQL : IRepository<Size>
    {
        private Model1 dataBase;

        public SizeRepositorySQL(Model1 dbcontext)
        {
            this.dataBase = dbcontext;
        }
        public List<Size> GetList()
        {
            return dataBase.Size.ToList();
        }

        public Size GetItem(int id)
        {
            return dataBase.Size.Find(id);
        }

        public void Create(Size item)
        {
            dataBase.Size.Add(item);
        }

        public void Update(Size item)
        {
            dataBase.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Size item = dataBase.Size.Find(id);
            if (item != null)
            {
                dataBase.Size.Remove(item);
            }
        }

        public bool Save()
        {
            return dataBase.SaveChanges() > 0;
        }
    }
}
