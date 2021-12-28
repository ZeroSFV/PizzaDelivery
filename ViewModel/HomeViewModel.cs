using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BLL.Interfaces;
using BLL.Models;
//using PizzaDelivery.Util;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using PizzaDelivery.Interface;

namespace PizzaDelivery
{
    public class HomeViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud _crud;
       
        public HomeViewModel(IDbCrud dbCrud, bool wentInClient, bool wentInWorker, bool wentInAdmin, bool wentInCourier)
        {
            _crud = dbCrud;
            WentInClient = wentInClient;
            WentInWorker = wentInWorker;
            WentInAdmin = wentInAdmin;
            WentInCourier = wentInCourier;
            if (WentInClient == true || WentInAdmin == true || WentInWorker == true|| WentInCourier==true)
                WentIn = true;
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

        private bool wentInClient;
        public bool WentInClient
        {
            get { return wentInClient; }
            set { wentInClient = value; ; NotifyPropertyChanged("WentInClient"); }
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

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.HomeUserControl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
