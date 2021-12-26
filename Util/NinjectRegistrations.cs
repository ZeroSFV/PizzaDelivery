using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using BLL;
using BLL.Interfaces;

namespace View.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbCrud>().To<DBDataOperations>();
           // Bind<ICategoryService>().To<CategoryService>();
           // Bind<ICatalogService>().To<ProductCatalogService>();
           // Bind<IOrderService>().To<OrderService>();
        }
    }
}
