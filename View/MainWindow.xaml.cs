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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes;
using BLL.Interfaces;
using PizzaDelivery.ViewModel;
using Microsoft.Toolkit.Uwp.Notifications;

namespace PizzaDelivery
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IDbCrud crudService)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(crudService);
        }

        private void ButtLogout_Click(object sender, RoutedEventArgs e)
        {
            var notyfy = new ToastContentBuilder();
            notyfy.AddText("Жаль что вы ушли. \n Будем ждать вас снова");
            
            notyfy.Show();
        }
    }
}
