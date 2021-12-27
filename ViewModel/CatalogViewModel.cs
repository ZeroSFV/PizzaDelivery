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

        



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
