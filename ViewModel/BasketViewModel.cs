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

namespace PizzaDelivery.ViewModel
{
    public class BasketViewModel : INotifyPropertyChanged, IWindowPage
    {

        IDbCrud crud;
        IPizza ipizz;
        int UserId;
        public BasketViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {
            // _crud = dbCrud;
            // _categoryService = categoryService;
            //  _catalogService = productCatalogService;
            // _dialogService = dialogService;

            //  var tempTypes = _categoryService.GetTypeModels();
            // Types = new ObservableCollection<TypeModel>();
            //  foreach (var i in tempTypes)
            //  {
            //       Types.Add(i);
            //   }

            /*  var tempProd = _crud.GetAllPizzas();
              _Pizzas = new ObservableCollection<PizzaModel>();
              foreach (var i in tempProd)
             {
                  i.ViewPrice = $"{i.Pizza_Price:0.#} руб.";
                  _Pizzas.Add(i);
             } */
            ipizz = _ipizz;
            crud = dbCrud;
            UserId = userId;
            Baskets = new ObservableCollection<BasketModel>();
            GetBaskets(UserId);
            


        }

        public void GetBaskets(int UsId)
        {
            Baskets = new ObservableCollection<BasketModel>(crud.GetAllBasketsByUserId(UsId));
            if (Baskets.Count > 0)
                NotEmptyBasket = true;
            else NotEmptyBasket = false;
            //Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzas());




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

        public ICommand AddOrder
        {
            get
            {
                if (_addOrder == null)
                    _addOrder = new RelayCommand(args => AddNewOrder(args));
                return _addOrder;
            }
        }
        private ICommand _addOrder;
        private void AddNewOrder(object args)
        {
            if (string.IsNullOrWhiteSpace(Adress) || string.IsNullOrWhiteSpace(Flat) || string.IsNullOrWhiteSpace(Entrance) || string.IsNullOrWhiteSpace(Floor))
            {

            }
            else 
            {
                PaymentWindow pay = new PaymentWindow();
                bool? result = pay.ShowDialog();
                if (result==true)
                {
                    crud.MakeOrder(UserId, Baskets, Adress + "кв: " + Flat + "Под: " + Entrance + "Этаж: " + Floor);
                    GetBaskets(UserId);
                }
            }
        }

        private bool notEmptyBasket;
        public bool NotEmptyBasket
        {
            get { return notEmptyBasket; }
            set { notEmptyBasket = value; ; OnPropertyChanged("NotEmptyBasket"); }
        }

        private ObservableCollection<BasketModel> baskets;
        public ObservableCollection<BasketModel> Baskets
        {
            get { return baskets; }
            set
            {
                baskets = value;
                OnPropertyChanged("Baskets");
            }
        }


        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(args => BinClicked(args));
                return _deleteCommand;
            }
        }

        private void BinClicked(object args)
        {
            if ((int)args != -1)
            {
                crud.DeleteBasket(Baskets[(int)args].Basket_Id);
                GetBaskets(UserId);
            }
        }

        private ICommand _plusAmountCommand;
        public ICommand PlusAmountCommand
        {
            get
            {
                if (_plusAmountCommand == null)
                    _plusAmountCommand = new RelayCommand(args => PlusClicked(args));
                return _plusAmountCommand;
            }
        }
        private void PlusClicked(object args)
        {
            if ((int)args != -1)
            {
                Baskets[(int)args].Basket_Amount++;
                crud.UpdateBasket(Baskets[(int)args]);
                GetBaskets(UserId);
            }
        }


        private ICommand _minusAmountCommand;
        public ICommand MinusAmountCommand
        {
            get
            {
                if (_minusAmountCommand == null)
                    _minusAmountCommand = new RelayCommand(args => MinusClicked(args));
                return _minusAmountCommand;
            }
        }
        private void MinusClicked(object args)
        {
            if ((int)args != -1)
            {
                Baskets[(int)args].Basket_Amount--;
                if (Baskets[(int)args].Basket_Amount == 0)
                    crud.DeleteBasket(Baskets[(int)args].Basket_Id);
                else crud.UpdateBasket(Baskets[(int)args]);
                GetBaskets(UserId);
                
            }
        }

        public string Adress { get; set; }
        public string Flat { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.BasketUserControl;
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
