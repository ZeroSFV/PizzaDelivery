using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Models;
using DAL.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;

namespace BLL
{
    public class DBDataOperations : IDbCrud
    {
        IDbRepository dataBase;
        
        public DBDataOperations(IDbRepository repos)
        {
            dataBase = repos;
        }

        public List<PizzaModel> GetAllPizzasBySizeDescription(int size, string Description)
        {
            return dataBase.Pizzas.GetList().Select(i => new PizzaModel(i)).Where(i=>i.Pizza_Size==size).Where(i=>i.Pizza_Prescence==true).Where(i => i.Pizza_Description == Description).ToList();
        }

        public UserModel LoginTrue(UserModel user)
        {
            var notyfy = new ToastContentBuilder();
            User u = dataBase.Users.GetList().Where(i => i.User_Login == user.User_Login).Where(i => i.User_Password == user.User_Password).FirstOrDefault();
            if (u == null)
            {
                {
                    notyfy.AddText("НЕУДАЧА! \nНе получилось войти в аккаунт \nНеверный логин или пароль");
                    //notyfy.AddAppLogoOverride(new Uri
                    //    (@"C:\Users\Frortate\Desktop\КУРСОВАЯ\Курсовая WPF SE\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\SE Селезнёв Д.А. 3-41xx  (Курсовое приложение)\Image\notpage.png"));
                    notyfy.Show();
                }
                return null;
            }
            return new UserModel(u);
        }

        public bool RegTrue(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.User_Login) || string.IsNullOrWhiteSpace(user.User_Password) || string.IsNullOrWhiteSpace(user.User_FullName) || string.IsNullOrWhiteSpace(user.User_PhoneNumer) || string.IsNullOrWhiteSpace(user.User_Passport))
                return false;
            if (user.User_PhoneNumer.Contains("_") ==true)
                return false;
            if (user.User_Passport.Contains("_") == true)
                return false;

            bool isExist;
            isExist = dataBase.Users.GetList()
                .Where(i => i.User_Login == user.User_Login)
                .Where(i => i.User_Password == user.User_Password)
                .FirstOrDefault() == null ? false : true;
            if (isExist)
                return false;

            dataBase.Users.Create(new User { User_Login = user.User_Login, User_Password = user.User_Password, User_FullName = user.User_FullName, User_Passport = user.User_Passport, User_PhoneNumer = user.User_PhoneNumer, User_UserType = 1});
            Save();
            return true;
        }

        public List<BasketModel> GetAllBasketsByUserId(int UserId)
        {
            return dataBase.Baskets.GetList().Select(i => new BasketModel(i)).Where(i => i.Basket_User == UserId).ToList();
        }

        public List<SizeModel> GetAllSizes()
        {
            return dataBase.Sizes.GetList().Select(i => new SizeModel(i)).ToList();
        }

        public UserModel User(int id)
        {
            return new UserModel(dataBase.Users.GetItem(id));
        }

        public bool Save()
        {
            if (dataBase.Save() > 0) return true;
            return false;
        }
    }
}
