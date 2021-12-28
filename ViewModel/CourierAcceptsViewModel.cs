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
    public class CourierAcceptsViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public CourierAcceptsViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Orders = new ObservableCollection<OrderModel>();
            GetAllCourierOrders();

        }


        private ICommand _AcceptOrder;
        public ICommand AcceptOrder
        {
            get
            {
                if (_AcceptOrder == null)
                    _AcceptOrder = new RelayCommand(args => ChangeToNextStatus(args));
                return _AcceptOrder;
            }
        }

        private void ChangeToNextStatus(object args)
        {
            if ((int)args != -1)
            {
                crud.AcceptOrderCourier(UserId, Orders[(int)args]);
                crud.ChangeToNextStatus(Orders[(int)args]);
                OrderCourier = new ObservableCollection<OrderModel>(crud.GetActiveOrdersOfCourier(UserId));
                if (OrderCourier.Count == 1)
                {
                    var notyfy = new ToastContentBuilder();
                    notyfy.AddText("Вы успешно приняли заказ на доставку!. \n Вы можете взять ещё один заказ на доставку.\n Если вам хватит 1 заказа на доставку - можете переходить в окно активных заказов" );
                    notyfy.Show();
                    GetAllCourierOrders();
                }
                if (OrderCourier.Count == 2)
                {
                    var notyfy = new ToastContentBuilder();
                    notyfy.AddText("Вы успешно приняли заказ на доставку!. \n Вы достигли лимита принятых заказов.\n Переносим вас в окно ваших активных заказов");
                    notyfy.Show();              
                }
                ipizz.ClickAcceptCourier();
            }
        }

        private ObservableCollection<OrderModel> _OrderCourier;
        public ObservableCollection<OrderModel> OrderCourier
        {
            get
            {
                return _OrderCourier;
            }
            set
            {
                _OrderCourier = value;
                OnPropertyChanged("OrderCourier");
            }
        }

        private bool noOrders;
        public bool NoOrders
        {
            get { return noOrders; }
            set { noOrders = value; ; OnPropertyChanged("NoOrders"); }
        }

        private void GetAllCourierOrders()
        {
            
            var orders = crud.GetAllComplectOrders();
            if (orders.Count > 0)
            {
                
                    Orders.Clear();
                
                NoOrders = false;
                foreach (var i in orders)
                {
                    Orders.Add(i);
                }
            }
            else { NoOrders = true; }
        }

    

        private ObservableCollection<OrderModel> _Orders;
        public ObservableCollection<OrderModel> Orders
        {
            get
            {
                return _Orders;
            }
            set
            {
                _Orders = value;
                OnPropertyChanged("Orders");
            }
        }

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.CourierAcceptsUserControl;
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
