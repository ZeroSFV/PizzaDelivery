using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Repository
{
    public class DbRepositorySQL : IDbRepository
    {
        private Model1 dataBase;

        private OrderRepositorySQL OrderRepository;
        private OrderStringRepositorySQL OrderStringRepository;
        private PizzaRepositorySQL PizzaRepository;
        private SizeRepositorySQL SizeRepository;
        private StatusRepositorySQL StatusRepository;
        private BasketRepositorySQL BasketRepository;
        private TypeRepositorySQL TypeRepository;
        private UserRepositorySQL UserRepository;
       
        public DbRepositorySQL()
        {
            dataBase = new Model1();
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (OrderRepository == null)
                    OrderRepository = new OrderRepositorySQL(dataBase);
                return OrderRepository;
            }
        }
        public IRepository<OrderString> OrderStrings
        {
            get
            {
                if (OrderStringRepository == null)
                    OrderStringRepository = new OrderStringRepositorySQL(dataBase);
                return OrderStringRepository;
            }
        }
        public IRepository<Pizza> Pizzas
        {
            get
            {
                if (PizzaRepository == null)
                    PizzaRepository = new PizzaRepositorySQL(dataBase);
                return PizzaRepository;
            }
        }
        public IRepository<Size> Sizes
        {
            get
            {
                if (SizeRepository == null)
                    SizeRepository = new SizeRepositorySQL(dataBase);
                return SizeRepository;
            }
        }
        public IRepository<Status> Statuses
        {
            get
            {
                if (StatusRepository == null)
                    StatusRepository = new StatusRepositorySQL(dataBase);
                return StatusRepository;
            }
        }
        public IRepository<Basket> Baskets
        {
            get
            {
                if (BasketRepository == null)
                    BasketRepository = new BasketRepositorySQL(dataBase);
                return BasketRepository;
            }
        }
        public IRepository<Type> Types
        {
            get
            {
                if (TypeRepository == null)
                    TypeRepository = new TypeRepositorySQL(dataBase);
                return TypeRepository;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (UserRepository == null)
                    UserRepository = new UserRepositorySQL(dataBase);
                return UserRepository;
            }
        }
       
        public int Save()
        {
            return dataBase.SaveChanges();
        }
    }
}
