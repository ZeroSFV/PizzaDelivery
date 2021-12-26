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
    public class PageLoginViewModel: INotifyPropertyChanged
    {
        IDbCrud crud;
        ILogin login;


        public PageLoginViewModel(IDbCrud crud, ILogin login)
        {
            this.crud = crud;
            this.login = login;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string Logined { get; set; }

        public string PasswordCont { get { return login.GetPassword(); } }

        private RelayCommand loginTrue;
        public RelayCommand LoginTrue
        {
            get
            {
                return loginTrue ??
                    (loginTrue = new RelayCommand(obj =>
                    {
                        UserModel result = crud.LoginTrue(new UserModel(Logined, PasswordCont));

                        if (result != null)
                            login.CloseLogin(true, result);
                        else
                        {

                        }



                    }
                ));
            }
        }


        private RelayCommand registrationWindow;
        public RelayCommand RegistrationWindow
        {
            get
            {
                return registrationWindow ??
                    (registrationWindow = new RelayCommand(obj =>
                    {
                        RegistrationWindow reg = new RegistrationWindow(crud);
                        reg.Show();
                    }
                ));
            }
        }

    }
}
