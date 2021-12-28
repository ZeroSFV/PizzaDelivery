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
using System.Windows.Input;

namespace PizzaDelivery.ViewModel
{
    public class OpenButPizzaViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;



        public OpenButPizzaViewModel(IDbCrud _crud, IPizza _ipizz, PizzaModel pz)
        {
            OpenPizza = pz;
            
            crud = _crud;
            ipizz = _ipizz;
            Price = $"Стоимость: {OpenPizza.Pizza_Price:0.#} руб.";
            logUser = ipizz.GetUser();
            CheckOrders(logUser);
        }

        private bool butAddVis;
        public bool ButAddVis
        {
            get { return butAddVis; }
            set { butAddVis = value; ; OnPropertyChanged("ButAddVis"); }
        }

        private void CheckOrders(int CurUser)
        {
            bool Orders = crud.CheckActiveOrder(CurUser);
            if (Orders == false)
                ButAddVis = true;
            else ButAddVis = false;
        }

        private PizzaModel openPizza;

        public PizzaModel OpenPizza
        {
            get { return openPizza; }
            set
            {
                openPizza = value;
                OnPropertyChanged("OpenPizza");
            }
        }

        public ICommand AddPizzaToBasket
        {
            get
            {
                if (_addPizzaToBasket == null)
                    _addPizzaToBasket = new RelayCommand(args => AddBasket(args));
                return _addPizzaToBasket;
            }
        }
        private ICommand _addPizzaToBasket;
        private void AddBasket(object args)
        {
            crud.CreateBasket(logUser, OpenPizza);
        }

       

        public ICommand EndMakeOrder
        {
            get
            {
                if (_endMakeOrder == null)
                    _endMakeOrder = new RelayCommand(args => CloseMakeOrder(args));
                return _endMakeOrder;
            }
        }
        private ICommand _endMakeOrder;
        private void CloseMakeOrder(object args)
        {
            ipizz.ClickCatalog();
        }

     

        public string Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                OnPropertyChanged("Price");
            }
        }
        private string _Price;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.OpenButPizza;
        }

        private int logUser;
        public bool WentIn { get; set; }


      
    }
}

