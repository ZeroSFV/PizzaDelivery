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

namespace PizzaDelivery.ViewModel
{
    public class CatalogViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud _crud;
        IPizza ipizz;

        public CatalogViewModel(IDbCrud dbCrud, IPizza _ipizz)
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
            _crud = dbCrud;
            Pizzas = new ObservableCollection<PizzaModel>();
            Sizes = new ObservableCollection<SizeModel>(_crud.GetAllSizes());
            Descriptions = new List<string>();
            Descriptions.Add("Мясная");
            Descriptions.Add("Острая");
            Descriptions.Add("Грибная");
            Descriptions.Add("Рыбная");
            InitContent();
            GetPizzas(filterBySizes.Size_Id,filterByDescription);
            


        }

        public ObservableCollection<SizeModel> Sizes { get; set; }
        public List<string> Descriptions { get; set; }

        private ObservableCollection<PizzaModel> pizzas;
        public ObservableCollection<PizzaModel> Pizzas
        {
            get { return pizzas; }
            set
            {
                pizzas = value;
                NotifyPropertyChanged("Pizzas");
            }
        }

        private string filterByDescription;
        public string FilterByDescription
        {
            get { return filterByDescription; }
            set
            {
                filterByDescription = value;

                if (filterByDescription == null)
                {
                    filterByDescription = Descriptions[0];
                    NotifyPropertyChanged("FilterByDescription");
                }

                GetPizzas(filterBySizes.Size_Id, filterByDescription);

                NotifyPropertyChanged("FilterByDescription");

               
            }
        }

        private void InitContent()
        {
            filterByDescription = Descriptions[0];
            filterBySizes = Sizes[0];
            NotifyPropertyChanged("FilterByDescription");
            NotifyPropertyChanged("FilterBySizes");
          
        }

        private SizeModel filterBySizes;
        public SizeModel FilterBySizes
        {
            get { return filterBySizes; }
            set
            {
                filterBySizes = value;


                GetPizzas(filterBySizes.Size_Id, filterByDescription);
                NotifyPropertyChanged("FilterBySizes");



            }
        }

        public void GetPizzas(int size, string description)
        {
            switch (description)
            {
                case "Мясная":
                    Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzasBySizeDescription(size,description));
                    break;
                case "Острая":
                    Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzasBySizeDescription(size, description));
                    break;
                case "Грибная":
                    Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzasBySizeDescription(size, description));
                    break;
                case "Рыбная":
                    Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzasBySizeDescription(size, description));
                    break;

            }

            //Pizzas = new ObservableCollection<PizzaModel>(_crud.GetAllPizzas());




        }

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.CatalogUserControl;
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
                    NotifyPropertyChanged("TypePage");
                }
            }
        }

        private RelayCommand openPizza;
        public RelayCommand OpenPizza
        {
            get
            {
                return openPizza ??
                    (openPizza = new RelayCommand(obj =>
                    {
                        string t = obj.ToString();
                        
                        PizzaModel pz = Pizzas.ToList().Find(p => p.Pizza_Name == t);
                        if (pz != null) ipizz.ClickPizza(pz);

                    }
                ));
            }
        }

        /*
                private ICommand _selectedIndexCommand;
                public ICommand SelectedIndexChangedCommand
                {
                    get
                    {
                        if (_selectedIndexCommand == null)
                            _selectedIndexCommand = new RelayCommand(args => SelectedIndexChanged(args));
                        return _selectedIndexCommand;
                    }
                }

                private void SelectedIndexChanged(object args)
                {
                    if ((int)args != -1)
                    {
                        Pizza_Id = Pizzas[(int)args].Pizza_id;

                        var Pizza = _crud.GetAllPizzas().Where(i => i.Pizza_id == Pizza_Id).ToList();

                        Description = "Описание: \n" + Pizza[0].Pizza_Description;
                        Pizza_Name = Pizza[0].Pizza_Name;
                        Pizza_Photo = Pizza[0].Pizza_Photo;
                        Pizza_Price = $"Стоимость: {Pizza[0].Pizza_Price:0.#} руб.";
                        //CanAddToCart = Product[0].Availability;



                        if (Pizza[0].Pizza_Prescence == true)
                        {
                            Pizza_Prescence = "Есть в наличии";
                        }
                        else
                        {
                            Pizza_Prescence = " Нет в наличии";
                        }
                    }
                }

                private int Pizza_Id;

                public string Guarantee
                {
                    get
                    {
                        return _Guarantee;
                    }
                    set
                    {
                        _Guarantee = value;
                        NotifyPropertyChanged("Guarantee");
                    }
                }
                private string _Guarantee;
                public string Description
                {
                    get
                    {
                        return _Description;
                    }
                    set
                    {
                        _Description = value;
                        NotifyPropertyChanged("Description");
                    }
                }
                private string _Description;
                public string Pizza_Name
                {
                    get
                    {
                        return _Pizza_Name;
                    }
                    set
                    {
                        _Pizza_Name = value;
                        NotifyPropertyChanged("Pizza_Name");
                    }
                }
                private string _Pizza_Name;
                public string Pizza_Photo
                {
                    get
                    {
                        return _Pizza_Photo;
                    }
                    set
                    {
                        _Pizza_Photo = value;
                        NotifyPropertyChanged("Pizza_Photo");
                    }
                }
                private string _Pizza_Photo;
                public string Pizza_Price
                {
                    get
                    {
                        return _Pizza_Price;
                    }
                    set
                    {
                        _Pizza_Price = value;
                        NotifyPropertyChanged("Pizza_Price");
                    }
                }
                private string _Pizza_Price;

                public string Pizza_Prescence
                {
                    get
                    {
                        return _Pizza_Prescence;
                    }
                    set
                    {
                        _Pizza_Prescence = value;
                        NotifyPropertyChanged("Pizza_Prescence");
                    }
                }
                private string _Pizza_Prescence;

                */




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
