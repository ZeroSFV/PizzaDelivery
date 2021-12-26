using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel
    {
        public int User_Id { get; set; }

       
        public string User_Login { get; set; }

       
        public string User_Password { get; set; }

       
        public string User_FullName { get; set; }

        public string User_PhoneNumer { get; set; }

      
        public string User_Passport { get; set; }

        public int User_UserType { get; set; }

        

        public UserModel() { }

        public UserModel(string login, string password)
        {
            User_Login = login;

            User_Password = password;

        }

        public UserModel(string login, string password, string FIO, string PhonNum, string Passport)
        {
            User_Login = login;
            User_Password = password;
            User_FullName = FIO;
            User_PhoneNumer = PhonNum;
            User_Passport = Passport;

        }

        public UserModel(User u)
        {
            User_Id = u.User_Id;
            User_Login = u.User_Login;
            User_Password = u.User_Password;
            User_FullName = u.User_FullName;
            User_PhoneNumer = u.User_PhoneNumer;
            User_Passport = u.User_Passport;
            User_UserType = u.User_UserType;
           
        }
    }
}
