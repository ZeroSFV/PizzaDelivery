using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using BLL.Interfaces;
using View.Util;
using Ninject;

namespace PizzaDelivery
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule());

              IDbCrud crudServ = kernel.Get<IDbCrud>();
              IFileService fileServ = kernel.Get<IFileService>();
            

            MainWindow mainWindow = new MainWindow(crudServ, fileServ);

            mainWindow.Show();
        }
    }
}
