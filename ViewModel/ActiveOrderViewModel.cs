using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using PizzaDelivery.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PizzaDelivery.View;
using Microsoft.Toolkit.Uwp.Notifications;

namespace PizzaDelivery.ViewModel
{
    public class ActiveOrderViewModel : INotifyPropertyChanged, IWindowPage
    {

        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public ActiveOrderViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Order = new OrderModel();
            OrderStrings = new ObservableCollection<OrderStringModel>();
            GetActiveOrder(UserId);
            GetActiveOrderStrings(Order.Order_Id);


        }

        private void GetActiveOrder(int userId)
        {
            Order = crud.GetActiveOrderByUserId(userId);
        }

        private void GetActiveOrderStrings(int orderId)
        {
            OrderStrings = new ObservableCollection<OrderStringModel>(crud.GetAllActiveStringsOfOrder(orderId));
        }

        public ICommand EndCheckOrder
        {
            get
            {
                if (_endCheckOrder == null)
                    _endCheckOrder = new RelayCommand(args => CloseCheckOrder(args));
                return _endCheckOrder;
            }
        }
        private ICommand _endCheckOrder;
        private void CloseCheckOrder(object args)
        {
            ipizz.ClickCatalog();
        }

        public ICommand CancelOrder
        {
            get
            {
                if (_cancelOrder == null)
                    _cancelOrder = new RelayCommand(args => CancelCurrentOrder(args));
                return _cancelOrder;
            }
        }
        private ICommand _cancelOrder;
        private void CancelCurrentOrder(object args)
        {
            bool result = crud.CheckIfOrderCanBeCancelled(Order.Order_Id);
            if (result == true)
            {
                crud.CancelThisOrder(Order.Order_Id);
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("Ваш заказ был успешно отменён. \n Переносим вас в каталог.\n Не бойтесь заказывать у нас снова! \n");

                notyfy.Show();
                ipizz.ClickCatalog();
            }
            else
            {
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("Ваш заказ уже был приготовлен на кухне. \n Поэтому мы не можем отменить ваш заказ. \n Дождитесь доставки этого заказа! \n");

                notyfy.Show();
            }
        }

        private ObservableCollection<OrderStringModel> orderStrings;
        public ObservableCollection<OrderStringModel> OrderStrings
        {
            get { return orderStrings; }
            set
            {
                orderStrings = value;
                OnPropertyChanged("OrderStrings");
            }
        }

        private OrderModel order;
        public OrderModel Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.ActiveOrderUserControl;
        }


        private IWindowPage typePage;

        public IWindowPage TypePage
        {
            get { return typePage; }

            set
            {
                if (typePage != value)
                {
                    typePage = value;
                    OnPropertyChanged("TypePage");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
