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

namespace PizzaDelivery.ViewModel
{
    public class OpenButPizzaViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;



        public OpenButPizzaViewModel(IDbCrud _crud, IPizza _ipizz, PizzaModel pz)
        {
            OpenPizza = pz;

           // OpenPizza.Sessions = OpenEvent.Sessions.Where(i => i.IsDone == false).OrderBy(i => i.Date).ToList();

            crud = _crud;
            ipizz = _ipizz;
            Price = $"Стоимость: {OpenPizza.Pizza_Price:0.#} руб.";
            logUser = ipizz.GetUser();
           // WentIn = logUser == -1 ? false : true;
            //if (WentIn)
             //   CheckedForLike();
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


      /*  private RelayCommand clickLike;
        public RelayCommand ClickLike
        {
            get
            {
                return clickLike ??
                    (clickLike = new RelayCommand(obj =>
                    {

                        int sessionId = (int)obj;
                        if (WentIn == false)
                            return;

                        bool result = crud.Like(logUser, sessionId);
                        CheckedForLike();
                    }
                ));
            }
        }

        private void CheckedForLike()
        {
            EventModel ev = OpenEvent;
            foreach (SessionModel s in ev.Sessions)
            {
                if (crud.User(logUser).Sessions.Where(i => i.ID == s.ID).FirstOrDefault() != null)
                    s.IsFavourite = true;
                else
                    s.IsFavourite = false;
            }
            OpenEvent = ev;
            OnPropertyChanged("CurrentEvent");
        } */
    }
}

