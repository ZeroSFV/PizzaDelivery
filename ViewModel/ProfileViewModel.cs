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
    
    public class ProfileViewModel : INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;

       
        public ProfileViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId)
        {

            ipizz = _ipizz;
            crud = dbCrud;
            Userlog = userId;

           
            ProfileVM = new ProfileAccountViewModel(dbCrud, ipizz, userId);
            OrdersVM = new ProfileOrdersViewModel(dbCrud, ipizz,userId);

        }

        private int Userlog;

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.ProfileUserControl;
        }

        public ProfileAccountViewModel ProfileVM { get; set; }
        
        public ProfileOrdersViewModel OrdersVM { get; set; }

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
