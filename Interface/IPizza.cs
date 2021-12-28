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
        void ClickOrder();
        void ClickAccept();
        int GetUser();

        void ClickNextStatus();

        void ClickAcceptCourier();
        void ClickNextStatusCourier();

        void ClickPizzaAdmin(PizzaModel pz);

        void ClickAdminCatalog();
    }
}
