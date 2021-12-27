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

    }
}
