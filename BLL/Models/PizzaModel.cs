using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BLL.Models
{
    public class PizzaModel : INotifyPropertyChanged
    {
        public int Pizza_id { get; set; }
        
        public string Pizza_Name { get; set; }
    
        public decimal Pizza_Price { get; set; }
    
        public string Pizza_Description { get; set; }

        public bool Pizza_Prescence { get; set; }

        public string Pizza_Consistance { get; set; }

        public int Pizza_Size { get; set; }

        public string ViewPrice { get; set; }

        public string Pizza_Photo { get; set; }

        public List<int> OrderStringIds { get; set; }
        
        public PizzaModel() { }

        public PizzaModel(Pizza p)
        {
            Pizza_id = p.Pizza_id;
            Pizza_Name = p.Pizza_Name;
            Pizza_Price = p.Pizza_Price;
            Pizza_Description = p.Pizza_Description;
            Pizza_Prescence = p.Pizza_Prescence;
            Pizza_Consistance = p.Pizza_Consistance;
            Pizza_Size = p.Pizza_Size;
            Pizza_Photo = p.Pizza_Photo;
            OrderStringIds = p.OrderString.Select(i => i.OrderString_Id).ToList();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
