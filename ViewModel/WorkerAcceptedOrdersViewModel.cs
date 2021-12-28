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
    public class WorkerAcceptedOrdersViewModel: INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public WorkerAcceptedOrdersViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Orders = new ObservableCollection<OrderModel>();
            GetAllWorkerOrders();


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
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("Заказ переведён в комплектование!. \n Вы можете снова выбрать заказ для выполнения.\n");

                notyfy.Show();
                ipizz.ClickNextStatus();
            }
        }

       

        private void GetAllWorkerOrders()
        {

            var orders = crud.GetAcceptedOrdersOfWorker(UserId);
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
            return TypeWindow.WorkerAcceptedOrdersUserControl;
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
