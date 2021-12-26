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
            if (Baskets.Count > 0)
                NotEmptyBasket = true;
            else NotEmptyBasket = false;


        }

        public void GetBaskets(int UsId)
        {
            Baskets = new ObservableCollection<BasketModel>(crud.GetAllBasketsByUserId(UsId));

            //Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzas());




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

        private RelayCommand _deleteBasket;
        public RelayCommand DeleteBasket
        {
            get
            {

                return _deleteBasket ??
                    (_deleteBasket = new RelayCommand(obj =>
                    {
                        //   string t = obj.ToString();
                        BasketModel bs = Baskets[0];
                        // PizzaModel pz = Pizzas.ToList().Find(p => p.Pizza_Name == t);
                       // if (pz != null) ipizz.ClickPizza(pz);

                    }
                ));


               
            }
        }

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
