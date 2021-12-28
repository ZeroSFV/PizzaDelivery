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
    public class CourierAcceptedOrdersViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public CourierAcceptedOrdersViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Orders = new ObservableCollection<OrderModel>();
            GetAllCourierOrders();


        }


        private ICommand _NextStatus;
        public ICommand NextStatus
        {
            get
            {
                if (_NextStatus == null)
                    _NextStatus = new RelayCommand(args => ChangeToNextStatus(args));
                return _NextStatus;
            }
        }

        private void ChangeToNextStatus(object args)
        {
            if ((int)args != -1)
            {
                
                crud.ChangeToNextStatus(Orders[(int)args]);
                GetAllCourierOrders();
                if (Orders.Count > 0)
                {
                    var notyfy = new ToastContentBuilder();
                    notyfy.AddText("Заказ доставлен! \n У вас есть ещё один заказ на выполнение.\n Но вы можете выбрать ещё один заказ на доставку");
                    notyfy.Show();
                }
                else
                {
                    var notyfy = new ToastContentBuilder();
                    notyfy.AddText("Заказ доставлен! \n У вас пока что больше нет заказа для выполнения.\n Вы снова можете выбрать 2 заказа на доставку");
                    notyfy.Show();
                }
                ipizz.ClickNextStatusCourier();
            }
        }



        private void GetAllCourierOrders()
        {

            var orders = crud.GetAcceptedOrdersOfCourier(UserId);
            if (Orders.Count>0)
            {
                Orders.Clear();
            }
            foreach (var i in orders)
            {
                Orders.Add(i);
            }

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
            return TypeWindow.CourierAcceptedOrdersUserControl;
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
