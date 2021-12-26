using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepository
    {
        IRepository<Order> Orders { get; }
        IRepository<OrderString> OrderStrings { get; }
        IRepository<Pizza> Pizzas { get; }
        IRepository<Size> Sizes { get; }
        IRepository<Status> Statuses { get; }
        IRepository<Basket> Baskets { get; }
        IRepository<DAL.Entities.Type> Types { get; }
        IRepository<User> Users { get; }
       

        int Save();
    }
}
