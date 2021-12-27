using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PizzaDelivery.Interface;
using PizzaDelivery.ViewModel;
using BLL.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;
using BLL.Models;

namespace PizzaDelivery.View
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            DataContext = new PaymentWindowViewModel();
        }
        private void ButtPay_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
            var notyfy = new ToastContentBuilder();

            notyfy.AddText("Успех! \n Вы провели оплату! \n");

            notyfy.Show();
           
            this.Close();

        }
        
    }
}
