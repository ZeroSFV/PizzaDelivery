using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace PizzaDelivery.Interface
{
    public interface IPizza
    {
        void ClickPizza(PizzaModel pm);

        void ClickCatalog();
        int GetUser();
    }
}
