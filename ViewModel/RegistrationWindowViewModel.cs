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

namespace PizzaDelivery.ViewModel
{
    public class RegistrationWindowViewModel
    {
        IDbCrud crud;
        ILogin login;


        public RegistrationWindowViewModel(IDbCrud crud, ILogin login)
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
        public string SecName { get; set; }

        public string FirName { get; set; }

        public string LasName { get; set; }

        public string PhoneNum { get; set; }

        public string Pass { get; set; }

        public string PasswordCont { get { return login.GetPassword(); } }

        private RelayCommand registrationWindow;
        public RelayCommand RegistrationWindow
        {
            get
            {
                return registrationWindow ??
                    (registrationWindow = new RelayCommand(obj =>
                    {
                        bool result = crud.RegTrue(new UserModel(Logined, PasswordCont, SecName + " " + FirName + " " + LasName, PhoneNum, Pass));

                        if (result)
                            login.CloseLogin(result);
                        else login.CloseLogin(result);

                    }
                ));
            }
        }
    }
}
