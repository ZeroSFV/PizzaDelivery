using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery.Interface
{
    public enum TypeWindow { HomeUserControl, CatalogUserControl, OpenButPizza, BasketUserControl, ProfileUserControl, LoginPage, ActiveOrderUserControl, WorkerAcceptsUserControl, WorkerAcceptedOrdersUserControl, CourierAcceptsUserControl, CourierAcceptedOrdersUserControl, AdminCatalogUserControl, AdminOpenButPizzaUserControl, AdminReportUserControl, AdminUsercUserControl }
    public interface IWindowPage
    {
        TypeWindow GetWindowType();
    }
}
