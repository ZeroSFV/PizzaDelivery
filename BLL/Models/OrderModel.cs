using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class OrderModel
    {
        public int Order_Id { get; set; }

        public DateTime Order_CreationTime { get; set; }

        
        public decimal Order_Price { get; set; }

       
        public string Order_Address { get; set; }

        
        public string Order_PhoneNumber { get; set; }

        public int Order_Client { get; set; }

        public int Order_Status { get; set; }

        public StatusModel Status { get; set; }

        public int? Order_Courier { get; set; }
        public int? Order_Worker { get; set; }
        // public ObservableCollection<OrderStringModel> OrderStrings { get; set; }

        public string ViewPrice { get; set; }

        public List<int> OrderStringIds  { get; set; }
       
        public List<int> UserWorkerIds { get; set; }

        public OrderModel() { }

        public OrderModel(Order o)
        {
            Order_Id = o.Order_Id;
            Order_CreationTime = o.Order_CreationTime;
            Order_Price = o.Order_Price;
            Order_Status = o.Order_Status;
            Order_Address = o.Order_Address;
            Order_PhoneNumber = o.Order_PhoneNumber;
            Order_Client = o.Order_Client;
            Order_Courier = o.Order_Courier;
            Order_Worker = o.Order_Worker;
            OrderStringIds = o.OrderString.Select(i => i.OrderString_Id).ToList();
            Status = new StatusModel(o.Status);
            ViewPrice = $"{o.Order_Price:0.#} руб.";

        }

    }
}
