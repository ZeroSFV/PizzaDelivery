using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Models;
using DAL.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections.ObjectModel;

namespace BLL
{
    public class DBDataOperations : IDbCrud
    {
        IDbRepository dataBase;

        public DBDataOperations(IDbRepository repos)
        {
            dataBase = repos;
        }

        public List<PizzaModel> GetAllPizzasBySizeDescription(int size, string Description)
        {
            return dataBase.Pizzas.GetList().Select(i => new PizzaModel(i)).Where(i => i.Pizza_Size == size).Where(i => i.Pizza_Prescence == true).Where(i => i.Pizza_Description.Contains(Description) == true).ToList();
        }

        public UserModel LoginTrue(UserModel user)
        {
            var notyfy = new ToastContentBuilder();
            User u = dataBase.Users.GetList().Where(i => i.User_Login == user.User_Login).Where(i => i.User_Password == user.User_Password).FirstOrDefault();
            if (u == null)
            {
                {
                    notyfy.AddText("НЕУДАЧА! \nНе получилось войти в аккаунт \nНеверный логин или пароль");
                    //notyfy.AddAppLogoOverride(new Uri
                    //    (@"C:\Users\Frortate\Desktop\КУРСОВАЯ\Курсовая WPF SE\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\Image\notpage.png"));
                    notyfy.Show();
                }
                return null;
            }
            return new UserModel(u);
        }

        public bool RegTrue(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.User_Login) || string.IsNullOrWhiteSpace(user.User_Password) || string.IsNullOrWhiteSpace(user.User_FullName) || string.IsNullOrWhiteSpace(user.User_PhoneNumer) || string.IsNullOrWhiteSpace(user.User_Passport))
                return false;
            if (user.User_PhoneNumer.Contains("_") == true)
                return false;
            if (user.User_Passport.Contains("_") == true)
                return false;

            bool isExist;
            isExist = dataBase.Users.GetList()
                .Where(i => i.User_Login == user.User_Login)
                .Where(i => i.User_Password == user.User_Password)
                .FirstOrDefault() == null ? false : true;
            if (isExist)
                return false;

            dataBase.Users.Create(new User { User_Login = user.User_Login, User_Password = user.User_Password, User_FullName = user.User_FullName, User_Passport = user.User_Passport, User_PhoneNumer = user.User_PhoneNumer, User_UserType = 1 });
            Save();
            return true;
        }

        public List<OrderModel> GetOrdersOfUser(int UserId)
        {
            return dataBase.Orders.GetList().Select(i => new OrderModel(i)).Where(i => i.Order_Client == UserId).ToList();
        }
        public List<BasketModel> GetAllBasketsByUserId(int UserId)
        {
            return dataBase.Baskets.GetList().Select(i => new BasketModel(i)).Where(i => i.Basket_User == UserId).ToList();
        }

        public int MakeOrder(int UserId, ObservableCollection<BasketModel> Baskets, string Address)
        {
            User us = dataBase.Users.GetItem(UserId);
            List<Basket> Basks = dataBase.Baskets.GetList().Where(i => i.Basket_User == UserId).ToList();
            Order order = new Order();
            order.Order_Client = UserId;
            order.Order_CreationTime = DateTime.Now;
            order.Order_Address = Address;
            order.Order_Price = 0;
            for (int i = 0; i<Basks.Count;i++)
            {
                order.Order_Price += Basks[i].Basket_Price;
            }
           
            order.Order_PhoneNumber = us.User_PhoneNumer;
            order.Order_Status = 1;
            order.Status = dataBase.Statuses.GetItem(order.Order_Status);
            order.User = us;
            dataBase.Orders.Create(order);

            if (dataBase.Save() <= 0)
                return 0;
            var orders = dataBase.Orders.GetList();
            int key = orders[orders.Count - 1].Order_Id;

            foreach (var i in Basks)
            {
                OrderString line = new OrderString();
                line.OrderString_Count = i.Basket_Amount;
                line.OrderString_Order = key;
                line.OrderString_Pizza = i.Basket_Pizza;
                line.Order = dataBase.Orders.GetItem(key);
                line.Pizza = i.Pizza;
                order = dataBase.Orders.GetItem(key);
                order.OrderString.Add(line);
                dataBase.Orders.Update(order);
                dataBase.OrderStrings.Create(line);
            }

            

            if (dataBase.Save() <= 0)
                return 0;


            var carts = dataBase.Baskets.GetList();
            for (int j = 0; j < carts.Count; j++)
            {
                if (carts[j].Basket_User == UserId)
                {
                    dataBase.Baskets.Delete(carts[j].Basket_Id);
                }
            }
            Save();
            return 1;

        }

        public List<OrderModel> GetAllWorkInOrders()
        {
            var orders = dataBase.Orders.GetList();
            var orderLines = dataBase.OrderStrings.GetList();

            List<OrderModel> orderModels = orders.Select(i =>new OrderModel(i)).Where(i => i.Order_Status == 1).Where(i=>i.Order_Worker==null).ToList();

            foreach (var i in orderModels)
            {
                foreach (var j in orderLines)
                {
                    if (j.OrderString_Order == i.Order_Id)
                    {
                        var prod = dataBase.Pizzas.GetItem(j.OrderString_Pizza);
                        i.OrderLines.Add(new OrderStringModel { OrderString_Count = j.OrderString_Count, Pizza = new PizzaModel(j.Pizza), OrderString_Order = j.OrderString_Order, OrderString_Pizza = j.OrderString_Pizza, ViewCount = $"{j.OrderString_Count:0.#} шт.", ViewPrice = $"{j.OrderString_Count * j.Pizza.Pizza_Price:0.#} руб." }); 
                    }
                }
                
            }

            return orderModels;

        }

        public List<OrderModel> GetAcceptedOrdersOfWorker(int UserId)
        {
            var orders = dataBase.Orders.GetList();
            var orderLines = dataBase.OrderStrings.GetList();
           // User us = dataBase.Users.GetItem(UserId);

            List<OrderModel> orderModels = orders.Select(i => new OrderModel(i)).Where(i => i.Order_Status == 2).Where(i => i.Order_Worker == UserId).ToList();

            foreach (var i in orderModels)
            {
                foreach (var j in orderLines)
                {
                    if (j.OrderString_Order == i.Order_Id)
                    {
                        var prod = dataBase.Pizzas.GetItem(j.OrderString_Pizza);
                        i.OrderLines.Add(new OrderStringModel { OrderString_Count = j.OrderString_Count, Pizza = new PizzaModel(j.Pizza), OrderString_Order = j.OrderString_Order, OrderString_Pizza = j.OrderString_Pizza, ViewCount = $"{j.OrderString_Count:0.#} шт.", ViewPrice = $"{j.OrderString_Count * j.Pizza.Pizza_Price:0.#} руб." });
                    }
                }

            }

            return orderModels;
        }

        public List<PizzaModel> GetAllPizzasBySizeDescriptionAdmin(int size, string description)
        {
            return dataBase.Pizzas.GetList().Select(i => new PizzaModel(i)).Where(i => i.Pizza_Size == size).Where(i => i.Pizza_Description.Contains(description) == true).ToList();
        }

        public List<OrderModel> GetAcceptedOrdersOfCourier(int UserId)
        {
            var orders = dataBase.Orders.GetList();
            var orderLines = dataBase.OrderStrings.GetList();
            // User us = dataBase.Users.GetItem(UserId);

            List<OrderModel> orderModels = orders.Select(i => new OrderModel(i)).Where(i => i.Order_Status == 4).Where(i => i.Order_Courier == UserId).ToList();

            foreach (var i in orderModels)
            {
                foreach (var j in orderLines)
                {
                    if (j.OrderString_Order == i.Order_Id)
                    {
                        var prod = dataBase.Pizzas.GetItem(j.OrderString_Pizza);
                        i.OrderLines.Add(new OrderStringModel { OrderString_Count = j.OrderString_Count, Pizza = new PizzaModel(j.Pizza), OrderString_Order = j.OrderString_Order, OrderString_Pizza = j.OrderString_Pizza, ViewCount = $"{j.OrderString_Count:0.#} шт.", ViewPrice = $"{j.OrderString_Count * j.Pizza.Pizza_Price:0.#} руб." });
                    }
                }

            }

            return orderModels;
        }

        public void ChangePizza(PizzaModel OpenPizza, bool Vision, decimal Pricer)
        {
            Pizza p = dataBase.Pizzas.GetItem(OpenPizza.Pizza_id);
            p.Pizza_Prescence = Vision;
            p.Pizza_Price = Pricer;
            Save();
        }

        public bool CheckIfOrderCanBeCancelled(int order_Id)
        {
            Order curOrder = dataBase.Orders.GetItem(order_Id);
            if (curOrder.Order_Status < 3)
                return true;
            else return false;
        }

        public void CancelThisOrder(int order_Id)
        {
            Order curOrder = dataBase.Orders.GetItem(order_Id);
            curOrder.Order_Status = 6;
            Save();
        }

        public OrderModel GetActiveOrderByUserId(int userId)
        {
            return dataBase.Orders.GetList().Select(i => new OrderModel(i)).Where(i => i.Order_Client == userId).Where(i => i.Order_Status < 5).FirstOrDefault();
        }

        public List<OrderStringModel> GetAllActiveStringsOfOrder(int orderId)
        {
            return dataBase.OrderStrings.GetList().Select(i => new OrderStringModel(i)).Where(i => i.OrderString_Order == orderId).ToList();
        }

        public void AcceptOrderWorker(int UserId, OrderModel Orders)
        {
            Order order = dataBase.Orders.GetItem(Orders.Order_Id);
            User wk = dataBase.Users.GetItem(UserId);
            order.User2 = wk;
            order.Order_Worker = wk.User_Id;
            Save();
        }

        public void AcceptOrderCourier(int UserId, OrderModel Orders)
        {
            Order order = dataBase.Orders.GetItem(Orders.Order_Id);
            User wk = dataBase.Users.GetItem(UserId);
            order.User1 = wk;
            order.Order_Courier = wk.User_Id;
            Save();
        }

        public void ChangeToNextStatus(OrderModel Orders)
        {
            Order order = dataBase.Orders.GetItem(Orders.Order_Id);
            order.Order_Status++;
            Save();
        }

        public bool CheckActiveOrder(int CurUser)
        {
            Order ord = dataBase.Orders.GetList().Where(i => i.Order_Client == CurUser).Where(i=>i.Order_Status<5).FirstOrDefault();
            if (ord == null)
                return false;
            else return true;
        }

        public OrderModel GetActiveOrdersOfWorker(UserModel User)
        {
            return dataBase.Orders.GetList().Select(i => new OrderModel(i)).Where(i => i.Order_Worker == User.User_Id).Where(i => i.Order_Status < 3).FirstOrDefault();
        }

        public void DeleteBasket(int id)
        {
            Basket basket = dataBase.Baskets.GetItem(id);
            if (basket != null)
            {
                dataBase.Baskets.Delete(basket.Basket_Id);
                Save();
            }
        }
        public List<SizeModel> GetAllSizes()
        {
            return dataBase.Sizes.GetList().Select(i => new SizeModel(i)).ToList();
        }

        public List<StatusModel> GetAllStatuses()
        {
            return dataBase.Statuses.GetList().Select(i => new StatusModel(i)).ToList();
        }

        public List<OrderModel> GetOrdersByStatusAnddAte(int SelectedStatus, DateTime DateStart, DateTime DateEnd)
        {
            var orders = dataBase.Orders.GetList();
            var orderLines = dataBase.OrderStrings.GetList();

            List<OrderModel> orderModels = orders.Where(i => DateStart <= i.Order_CreationTime && DateEnd >= i.Order_CreationTime && ((SelectedStatus == -1 && (i.Order_Status == 1 || i.Order_Status == 2 || i.Order_Status == 3 || i.Order_Status == 4 || i.Order_Status == 5 || i.Order_Status == 6)) | SelectedStatus == 0 | SelectedStatus == i.Order_Status)).Select(i => new OrderModel { OrderLines = new ObservableCollection<OrderStringModel>(), Order_Id = i.Order_Id, Order_CreationTime = (DateTime)i.Order_CreationTime, Order_Status = i.Order_Status,  Order_Price = i.Order_Price, Order_Address = i.Order_Address,Order_PhoneNumber =i.Order_PhoneNumber, Status = new StatusModel(i.Status), ViewPrice = $"{i.Order_Price:0.#} руб."}).ToList();

            foreach (var i in orderModels)
            {
                foreach (var j in orderLines)
                {
                    if (j.OrderString_Order == i.Order_Id)
                    {
                        var prod = dataBase.Pizzas.GetItem(j.OrderString_Pizza);
                        i.OrderLines.Add(new OrderStringModel { OrderString_Count = j.OrderString_Count, Pizza = new PizzaModel(j.Pizza), OrderString_Order = j.OrderString_Order, OrderString_Pizza = j.OrderString_Pizza, ViewCount = $"{j.OrderString_Count:0.#} шт.", ViewPrice = $"{j.OrderString_Count * j.Pizza.Pizza_Price:0.#} руб." });
                    }
                }
                //i.OrderStatus = dataBase.OrderStatuses.GetItem((int)i.Order_Status_Id).Name;
               // i.PickPoint = dataBase.PickPoints.GetItem((int)i.Pick_Point_Id).Name;
            }

            return orderModels;
        }

        public void UpdateBasket(BasketModel basket)
        {
            Basket bs = dataBase.Baskets.GetItem(basket.Basket_Id);
            bs.Basket_Amount = basket.Basket_Amount;
            bs.Basket_Price = basket.Pizza.Pizza_Price * basket.Basket_Amount;
            basket.ViewPrice = $"{basket.Basket_Price:0.#} руб.";
            Save();
        }

        public List<OrderModel> GetActiveOrdersOfCourier(int UserId)
        {
            return dataBase.Orders.GetList().Select(i => new OrderModel(i)).Where(i => i.Order_Courier == UserId).Where(i => i.Order_Status == 4).ToList();
        }

        public void CreateBasket(int logUser, PizzaModel OpenPizza)
        {
            User us = dataBase.Users.GetItem(logUser);
            Pizza pz = dataBase.Pizzas.GetItem(OpenPizza.Pizza_id);
            Basket Basks = dataBase.Baskets.GetList().Where(i => i.Basket_User == logUser).Where(i => i.Basket_Pizza == OpenPizza.Pizza_id).FirstOrDefault();
            if (Basks != null)
            {
                Basks.Basket_Amount++;
                Basks.Basket_Price += pz.Pizza_Price;
            }
            else { dataBase.Baskets.Create(new Basket{ Basket_Amount = 1, Basket_Price = pz.Pizza_Price, Basket_Pizza = pz.Pizza_id, Basket_User = us.User_Id, Pizza = pz, User = us}); }
            Save();
        }

        public UserModel User(int id)
        {
            return new UserModel(dataBase.Users.GetItem(id));
        }

        public List<OrderModel> GetAllComplectOrders()
        {
            var orders = dataBase.Orders.GetList();
            var orderLines = dataBase.OrderStrings.GetList();

            List<OrderModel> orderModels = orders.Select(i => new OrderModel(i)).Where(i => i.Order_Status == 3).Where(i => i.Order_Courier == null).ToList();

            foreach (var i in orderModels)
            {
                foreach (var j in orderLines)
                {
                    if (j.OrderString_Order == i.Order_Id)
                    {
                        var prod = dataBase.Pizzas.GetItem(j.OrderString_Pizza);
                        i.OrderLines.Add(new OrderStringModel { OrderString_Count = j.OrderString_Count, Pizza = new PizzaModel(j.Pizza), OrderString_Order = j.OrderString_Order, OrderString_Pizza = j.OrderString_Pizza, ViewCount = $"{j.OrderString_Count:0.#} шт.", ViewPrice = $"{j.OrderString_Count * j.Pizza.Pizza_Price:0.#} руб." });
                    }
                }

            }

            return orderModels;
        }

        public bool Save()
        {
            if (dataBase.Save() > 0) return true;
            return false;
        }
    }
}
