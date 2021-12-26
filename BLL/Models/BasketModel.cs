using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BasketModel
    {
        public int Basket_Id { get; set; }

        public int Basket_Amount { get; set; }

        public decimal Basket_Price { get; set; }

        public int Basket_User { get; set; }

        public int Basket_Pizza { get; set; }

        public PizzaModel Pizza { get; set; }

        public BasketModel() { }
        
        public BasketModel(Basket b)
        {
            Basket_Id = b.Basket_Id;
            Basket_Amount = b.Basket_Amount;
            Basket_Price = b.Basket_Price;
            Basket_User = b.Basket_User;
            Basket_Pizza = b.Basket_Pizza;
            Pizza = new PizzaModel(b.Pizza);
        }
    }
}
