using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        List<PizzaModel> GetAllPizzasBySizeDescription(int size, string description);

        UserModel LoginTrue(UserModel user);
        List<SizeModel> GetAllSizes();

        UserModel User(int id);

        bool RegTrue(UserModel user);

        List<BasketModel> GetAllBasketsByUserId(int UserId);


    }
}
