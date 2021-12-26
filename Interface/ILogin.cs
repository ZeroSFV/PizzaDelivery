using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace PizzaDelivery.Interface
{
    public interface ILogin
    {
        string GetPassword();
        void CloseLogin(bool? resultLog = false, UserModel user = null);
    }
}
