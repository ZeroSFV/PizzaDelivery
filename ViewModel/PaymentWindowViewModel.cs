using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using PizzaDelivery.Interface;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PizzaDelivery.View;

namespace PizzaDelivery.ViewModel
{
    public class PaymentWindowViewModel : INotifyPropertyChanged
    {
        public PaymentWindowViewModel()
        {
          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
