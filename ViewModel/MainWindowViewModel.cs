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
            TypePage = new HomeViewModel(dbCrud, wentInUser, wentInWorker, wentInAdmin);
          
        }
        public CatalogViewModel CatalogVM { get; set; }

        public void ClickPizza(PizzaModel pm)
        {
            TypePage = new OpenButPizzaViewModel(_crud, this, pm);
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

        private bool wentInWorker;
        public bool WentInWorker
        {
            get { return wentInWorker; }
            set { wentInWorker = value; ; NotifyPropertyChanged("WentInWorker"); }
        }

        private bool wentInAdmin;
        public bool WentInAdmin
        {
            get { return wentInAdmin; }
            set { wentInAdmin = value; ; NotifyPropertyChanged("WentInAdmin"); }
        }

        private bool wentInUser;
        public bool WentInUser
        {
            get { return wentInUser; }
            set { wentInUser = value; ; NotifyPropertyChanged("WentInUser"); }
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
                        TypePage = new BasketViewModel(_crud, this, Userlog.User_Id);
                    }
                ));
            }
        }

        private RelayCommand openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {
                return openMainWindow ??
                    (openMainWindow = new RelayCommand(obj =>
                    {
                        TypePage = new HomeViewModel(_crud, WentInUser, WentInWorker, wentInAdmin);
                    }
                ));
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
                            if (Userlog.User_Id == 1)
                            {
                                WentInUser = true;
                            }
                            if (Userlog.User_Id == 2 || Userlog.User_Id == 3)
                            {
                                WentInWorker = true;
                            }
                            if (Userlog.User_Id == 4)
                            {
                                WentInAdmin = true;
                            }
                            TypePage = new HomeViewModel(_crud, WentInUser, WentInWorker, wentInAdmin);
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
                        Userlog = null;
                        if (TypePage.GetWindowType() != TypeWindow.HomeUserControl)
                            TypePage = new HomeViewModel(_crud, WentInUser, wentInWorker, WentInAdmin);
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
