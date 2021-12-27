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
    public class ProfileOrdersViewModel : INotifyPropertyChanged
    {
        IDbCrud crud;
        IPizza ipizz;


        public ProfileOrdersViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            Userlog = userId;
            Orders = new ObservableCollection<OrderModel>();
            GetOrders(Userlog);
        }

        private void GetOrders(int UserId)
        {
            Orders = new ObservableCollection<OrderModel>(crud.GetOrdersOfUser(UserId));
            if (Orders.Count > 0)
                NoOrders = false;
            else NoOrders = true;
        }

        private bool noOrders;
        public bool NoOrders
        {
            get { return noOrders; }
            set { noOrders = value; ; OnPropertyChanged("NoOrders"); }
        }

        private ObservableCollection<OrderModel> orders;
        public ObservableCollection<OrderModel> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        private int Userlog;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
