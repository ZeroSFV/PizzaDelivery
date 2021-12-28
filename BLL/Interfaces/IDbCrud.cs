using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        List<PizzaModel> GetAllPizzasBySizeDescription(int size, string description);

        UserModel LoginTrue(UserModel user);
        List<SizeModel> GetAllSizes();

        UserModel User(int id);

        bool RegTrue(UserModel user);

        List<BasketModel> GetAllBasketsByUserId(int UserId);

        void DeleteBasket(int id);

        void UpdateBasket(BasketModel basket);

        void CreateBasket(int logUser, PizzaModel OpenPizza);

        int MakeOrder(int UserId,ObservableCollection<BasketModel> Baskets, string Address);

        bool CheckActiveOrder(int CurUser);

        OrderModel GetActiveOrderByUserId(int userId);

        List<OrderStringModel> GetAllActiveStringsOfOrder(int orderId);

        bool CheckIfOrderCanBeCancelled(int order_Id);

        void CancelThisOrder(int order_Id);

        List<OrderModel> GetOrdersOfUser(int UserId);

        List<OrderModel> GetAllWorkInOrders();

        List<OrderModel> GetAllComplectOrders();

        void AcceptOrderWorker(int UserId, OrderModel Orders);

        void AcceptOrderCourier(int UserId, OrderModel Orders);

        List<OrderModel> GetAcceptedOrdersOfCourier(int UserId);

        void ChangeToNextStatus(OrderModel Orders);

        OrderModel GetActiveOrdersOfWorker(UserModel User);

        List<OrderModel> GetAcceptedOrdersOfWorker(int UserId);

        List<OrderModel> GetActiveOrdersOfCourier(int UserId);

        void ChangePizza(PizzaModel OpenPizza, bool Vision, decimal Pricer);

        List<PizzaModel> GetAllPizzasBySizeDescriptionAdmin(int size, string description);

        List<StatusModel> GetAllStatuses();

        List<OrderModel> GetOrdersByStatusAnddAte(int SelectedStatus, DateTime DateStart, DateTime DateEnd);

    }
}
