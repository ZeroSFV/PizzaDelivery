using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Interface
{
    public enum TypeWindow { HomeUserControl, CatalogUserControl, OpenButPizza, BasketUserControl, ProfileUserControl, LoginPage, ActiveOrderUserControl }
    public interface IWindowPage
    {
        TypeWindow GetWindowType();
    }
}
