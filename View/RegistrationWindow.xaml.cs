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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window, ILogin
    {
        public RegistrationWindow(IDbCrud db)
        {
            InitializeComponent();
            DataContext = new RegistrationWindowViewModel(db, this);
        }

        private void ButtCloseWind_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtCollapse_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

       

        public string GetPassword()
        {

            return Password.Password;
        }

        public void CloseLogin(bool? resultLog, UserModel user)
        {
            if (resultLog == true)
            {
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("ПОЗДРАВЛЯЕМ! \nРегистрация прошла успешно.");
               
                notyfy.Show();
                this.Close();
            }
            else
            {
                var notyfy = new ToastContentBuilder();
                notyfy.AddText("НЕУДАЧА! \n Не вышло создать аккаунт \n Возможно, вы не ввели какие-либо данные \n Также возможно, что вы ввели телефон и паспорт неправильно \n Попробуйте снова");
                
                notyfy.Show();
                this.Close();
            }

        }
    }
}
