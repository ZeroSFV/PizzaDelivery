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
        public HomeViewModel(IDbCrud dbCrud)
        {
            _crud = dbCrud;
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
