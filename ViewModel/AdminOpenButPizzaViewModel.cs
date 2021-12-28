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
    public class AdminOpenButPizzaViewModel : INotifyPropertyChanged, IWindowPage
    {

        IDbCrud crud;
        IPizza ipizz;

        public AdminOpenButPizzaViewModel(IDbCrud _crud, IPizza _ipizz, PizzaModel pz)
        {
            OpenPizza = pz;
            Pricer = pz.Pizza_Price;
            Vision = pz.Pizza_Prescence;
            crud = _crud;
            ipizz = _ipizz;
            Price = $"Стоимость: {OpenPizza.Pizza_Price:0.#} руб.";
            logUser = ipizz.GetUser();
           
        }

        private bool vision;
        public bool Vision
        {
            get { return vision; }
            set { vision = value; ; OnPropertyChanged("Vision"); }
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


        public ICommand ChangePrice
        {
            get
            {
                if (_ChangePrice == null)
                    _ChangePrice = new RelayCommand(args => PriceChange(args));
                return _ChangePrice;
            }
        }
        private ICommand _ChangePrice;
        private void PriceChange(object args)
        {
            crud.ChangePizza(OpenPizza,Vision,Pricer);
            var notyfy = new ToastContentBuilder();
            notyfy.AddText("Цена пиццы изменена. \n");

            notyfy.Show();
        }

        public ICommand Vis
        {
            get
            {
                if (_Vis == null)
                    _Vis = new RelayCommand(args => VisChange(args));
                return _Vis;
            }
        }
        private ICommand _Vis;

        private void VisChange(object args)
        {
            Vision = true;
            crud.ChangePizza(OpenPizza, Vision, Pricer);
            var notyfy = new ToastContentBuilder();
            notyfy.AddText("Пицца возвращена в каталог. \n");

            notyfy.Show();
        }

        public ICommand NoVis
        {
            get
            {
                if (_NoVis == null)
                    _NoVis = new RelayCommand(args => NoVisChange(args));
                return _NoVis;
            }
        }
        private ICommand _NoVis;

        private void NoVisChange(object args)
        {
            Vision = false;
            crud.ChangePizza(OpenPizza, Vision, Pricer);
            var notyfy = new ToastContentBuilder();
            notyfy.AddText("Пицца убрана из каталога. \n");

            notyfy.Show();
        }





        public ICommand EndChangePizza
        {
            get
            {
                if (_endChangePizza == null)
                    _endChangePizza = new RelayCommand(args => ClosePizza(args));
                return _endChangePizza;
            }
        }
        private ICommand _endChangePizza;
        private void ClosePizza(object args)
        {
            ipizz.ClickAdminCatalog();
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
            return TypeWindow.AdminOpenButPizzaUserControl;
        }

        private int logUser;
        public bool WentIn { get; set; }


        public decimal Pricer
        {
            get
            {
                return _Pricer;
            }
            set
            {
                _Pricer = value;
                OnPropertyChanged("Pricer");
            }
        }
        private decimal _Pricer;
    }
}
