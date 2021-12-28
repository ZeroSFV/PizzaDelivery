using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BLL.Interfaces;
using BLL.Models;
using PizzaDelivery.Interface;
using PizzaDelivery.View;
// using PizzaDelivery.Util;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PizzaDelivery.ViewModel
{
    public class MainWindowViewModel :INotifyPropertyChanged, IPizza
    {
        private IDbCrud _crud;

        public MainWindowViewModel(IDbCrud dbCrud)
        {
            _crud = dbCrud;
            CatalogVM = new CatalogViewModel(dbCrud, this);
            TypePage = new HomeViewModel(dbCrud, WentInUser, WentInWorker, WentInAdmin, WentInCourier);
            WorkerHaveOrders = true;
            WorkerHaveOrders1 = false;


        }
        public CatalogViewModel CatalogVM { get; set; }

        public void ClickPizza(PizzaModel pm)
        {
            TypePage = new OpenButPizzaViewModel(_crud, this, pm);
        }

        public void ClickAccept()
        {
            CheckWorkerForOrders(Userlog);
            TypePage = new WorkerAcceptedOrdersViewModel(_crud, this, Userlog.User_Id);

        }

        public void ClickNextStatus()
        {
            CheckWorkerForOrders(Userlog);
            TypePage = new WorkerAcceptsViewModel(_crud, this, Userlog.User_Id);
        }

        public void ClickOrder()
        {
            TypePage = new ActiveOrderViewModel(_crud, this, Userlog.User_Id);
        }

        public void ClickCatalog()
        {
            TypePage = new CatalogViewModel(_crud, this);
        }
        
        private bool wentIn;
        public bool WentIn
        {
            get { return wentIn; }
            set { wentIn = value; ; NotifyPropertyChanged("WentIn"); }
        }

        private OrderModel orders;
        public OrderModel Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                NotifyPropertyChanged("Orders");
            }
        }

        private bool wentInWorker;
        public bool WentInWorker
        {
            get { return wentInWorker; }
            set { wentInWorker = value; ; NotifyPropertyChanged("WentInWorker"); }
        }

        private bool workerHaveOrders;
        public bool WorkerHaveOrders
        {
            get { return workerHaveOrders; }
            set { workerHaveOrders = value; ; NotifyPropertyChanged("WorkerHaveOrders"); }
        }

        private bool workerHaveOrders1;
        public bool WorkerHaveOrders1
        {
            get { return workerHaveOrders1; }
            set { workerHaveOrders1 = value; ; NotifyPropertyChanged("WorkerHaveOrders1"); }
        }

        private bool wentInCourier;
        public bool WentInCourier
        {
            get { return wentInCourier; }
            set { wentInCourier = value; ; NotifyPropertyChanged("WentInCourier"); }
        }

        private bool wentInAdmin;
        public bool WentInAdmin
        {
            get { return wentInAdmin; }
            set { wentInAdmin = value; ; NotifyPropertyChanged("WentInAdmin"); }
        }

        private bool activeOrder;
        public bool ActiveOrder
        {
            get { return activeOrder; }
            set { activeOrder = value; ; NotifyPropertyChanged("ActiveOrder"); }
        }

        private bool wentInUser;
        public bool WentInUser
        {
            get { return wentInUser; }
            set { wentInUser = value; ; NotifyPropertyChanged("WentInUser"); }
        }

        private OrderModel order;
        public OrderModel Order
        {
            get { return order; }
            set
            {
                order = value;
                NotifyPropertyChanged("Order");
            }
        }

        private IWindowPage typePage;
        public IWindowPage TypePage
        {
            get
            {
                return typePage;
            }
            set
            {
                if (typePage != value)
                {
                    typePage = value;
                    NotifyPropertyChanged("TypePage");
                }
            }
        }

        private RelayCommand openCatalog;
        public RelayCommand OpenCatalog
        {
            get
            {
                return openCatalog ??
                    (openCatalog = new RelayCommand(obj =>
                    {
                        TypePage = new CatalogViewModel(_crud, this);
                    }
                ));
            }
        }

        private RelayCommand openBasket;
        public RelayCommand OpenBasket
        {
            get
            {
                return openBasket ??
                    (openBasket = new RelayCommand(obj =>
                    {
                        CheckOrders(Userlog);
                        
                    }
                ));
            }
        }

        private RelayCommand openAccepts;
        public RelayCommand OpenAccepts
        {
            get
            {
                return openAccepts ??
                    (openAccepts = new RelayCommand(obj =>
                    {
                        TypePage = new WorkerAcceptsViewModel(_crud, this, Userlog.User_Id);

                    }
                ));
            }
        }

        private RelayCommand openAcceptedOrders;
        public RelayCommand OpenAcceptedOrders
        {
            get
            {
                return openAcceptedOrders ??
                    (openAcceptedOrders = new RelayCommand(obj =>
                    {
                        TypePage = new WorkerAcceptedOrdersViewModel(_crud, this, Userlog.User_Id);

                    }
                ));
            }
        }

        




        private RelayCommand openProfile;
        public RelayCommand OpenProfile
        {
            get
            {
                return openProfile ??
                    (openProfile = new RelayCommand(obj =>
                    {
                        TypePage = new ProfileViewModel(_crud, this, Userlog.User_Id);
                    }
                ));
            }
        }


        private void CheckOrders(UserModel CurUser)
        {
            bool Orders = _crud.CheckActiveOrder(CurUser.User_Id);
            if (Orders == false)
                TypePage = new BasketViewModel(_crud, this, Userlog.User_Id);
            else TypePage = new ActiveOrderViewModel(_crud, this, Userlog.User_Id);
        }
        

        private RelayCommand openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {
                return openMainWindow ??
                    (openMainWindow = new RelayCommand(obj =>
                    {
                        TypePage = new HomeViewModel(_crud, WentInUser, WentInWorker, WentInAdmin, WentInCourier);
                    }
                ));
            }
        }

        private void CheckWorkerForOrders(UserModel User)
        {
            Order = _crud.GetActiveOrdersOfWorker(User);
            if (Order==null)
            {
                WorkerHaveOrders = false;
                WorkerHaveOrders1 = false;
            }
            else
            {
                WorkerHaveOrders = true;
                WorkerHaveOrders1 = true;
            }
        }

        private RelayCommand logIn;
        public RelayCommand LogIn
        {
            get
            {
                 return logIn ??
                    (logIn = new RelayCommand(obj =>
                    {
                        PageLogin login = new PageLogin(_crud);
                        bool? result = login.ShowDialog();
                        if (result == true)
                        {
                            WentIn = true;
                            Userlog = _crud.User(login.GetUserlog().User_Id);
                            if (Userlog.User_UserType == 1)
                            {
                                WentInUser = true;
                            }
                            if (Userlog.User_UserType == 2)
                            {
                                WentInWorker = true;
                                CheckWorkerForOrders(Userlog);
                            }
                            if (Userlog.User_UserType == 3)
                            {
                                wentInCourier = true;
                            }
                            if (Userlog.User_UserType == 4)
                            {
                                WentInAdmin = true;
                            }
                            TypePage = new HomeViewModel(_crud, WentInUser, WentInWorker, wentInAdmin, WentInCourier);
                            // ReminderLikeEventUser();
                        } 

                    }
                )); 
            }
        }

        private RelayCommand logOut;
        public RelayCommand LogOut
        {
            get
            {
                return logOut ??
                    (logOut = new RelayCommand(obj =>
                    {
                        WentIn = false;
                        WentInUser = false;
                        WentInWorker = false;
                        WentInAdmin = false;
                        WentInCourier = false;
                        WorkerHaveOrders = true;
                        WorkerHaveOrders1 = false;
                        Userlog = null;
                        TypePage = new HomeViewModel(_crud, WentInUser, wentInWorker, WentInAdmin, WentInCourier);
                    }
                ));
            }
        }


        private UserModel Userlog;

        public int GetUser()
        {
            return Userlog == null ? -1 : Userlog.User_Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
