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
    public class WorkerAcceptsViewModel:INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public WorkerAcceptsViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Orders = new ObservableCollection<OrderModel>();
            GetAllWorkerOrders();
            


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
                crud.AcceptOrderWorker(UserId, Orders[(int)args]);
                crud.ChangeToNextStatus(Orders[(int)args]);
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("Вы успешно приняли заказ на выполнение!. \n Переносим вас в окно ваших активных заказов.\n");

                notyfy.Show();
                ipizz.ClickAccept();
            }
        }

        private bool noOrders;
        public bool NoOrders
        {
            get { return noOrders; }
            set { noOrders = value; ; OnPropertyChanged("NoOrders"); }
        }

        private void GetAllWorkerOrders()
        {
            
            var orders = crud.GetAllWorkInOrders();
            if (orders.Count > 0)
            {
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
            return TypeWindow.WorkerAcceptsUserControl;
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
