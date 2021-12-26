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
using Microsoft.Toolkit.Uwp.Notifications;

namespace PizzaDelivery.View
{
    /// <summary>
    /// Логика взаимодействия для OpenButPizza.xaml
    /// </summary>
    public partial class OpenButPizza : UserControl
    {
        public OpenButPizza()
        {
            InitializeComponent();
        }

        private void ButtonAddBasket_Click(object sender, RoutedEventArgs e)
        {
            var notyfy = new ToastContentBuilder();
            notyfy.AddText("Пицца добавлена в корзину. \n");
            //notyfy.AddAppLogoOverride(new Uri
            //    (@"C:\Users\Frortate\Desktop\КУРСОВАЯ\Курсовая WPF SE\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\Image\notpage.png"));
            notyfy.Show();
        }
    }
}
