using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=PizzaDeliveryContext")
        {
        }

        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderString> OrderString { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>()
                .Property(e => e.Basket_Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderString)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.OrderString_Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Pizza_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Pizza_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Pizza_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Pizza_Consistance)
                .IsUnicode(false);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Pizza_Photo)
                .IsUnicode(false);

            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.Basket)
                .WithRequired(e => e.Pizza)
                .HasForeignKey(e => e.Basket_Pizza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.OrderString)
                .WithRequired(e => e.Pizza)
                .HasForeignKey(e => e.OrderString_Pizza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Size>()
                .Property(e => e.Size_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.Pizza)
                .WithRequired(e => e.Size)
                .HasForeignKey(e => e.Pizza_Size)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Status_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.Order_Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.Type_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.User_UserType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_PhoneNumer)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_Passport)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Basket)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Basket_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Order_Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.Order_Courier);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order2)
                .WithOptional(e => e.User2)
                .HasForeignKey(e => e.Order_Worker);
        }
    }
}
