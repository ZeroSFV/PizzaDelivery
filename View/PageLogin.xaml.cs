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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Window, ILogin
    {

        UserModel Userlog;

        public PageLogin(IDbCrud db)
        {
            InitializeComponent();
            DataContext = new PageLoginViewModel(db, this);
        }

        public string GetPassword()
        {

            return Password.Password;
        }

        private void ButtCloseWind_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       

        private void ButtCollapse_Click(object sender, RoutedEventArgs e)
        {
           
                WindowState = WindowState.Minimized;
            
        }

        public void CloseLogin(bool? resultLog, UserModel user)
        {
            DialogResult = resultLog;
            Userlog = user;
            var notyfy = new ToastContentBuilder();

            if (Userlog == null)
            {
                notyfy.AddText("НЕУДАЧА! \nНе вышло аойти в аккаунт \nПопробуйте снова");
                //notyfy.AddAppLogoOverride(new Uri
                //    (@"C:\Users\Frortate\Desktop\КУРСОВАЯ\Курсовая WPF SE\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\Image\notpage.png"));
                notyfy.Show();
            }
            else
            if (Userlog != null)
            {
                notyfy.AddText("ПОЗДРАВЛЯЕМ! \nВы успешно зашли в свой аккуант. \nУдачного времяприпровождения :)");
                //notyfy.AddAppLogoOverride(new Uri
                //    (@"C:\Users\Frortate\Desktop\КУРСОВАЯ\Курсовая WPF SE\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\Image\notpage.png"));
                notyfy.Show();
            }
            this.Close();

        }

        public UserModel GetUserlog()
        {
            return Userlog;
        }
    }
}
