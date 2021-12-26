﻿using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    class OrderStringModel
    {
        public int OrderString_Id { get; set; }

        public int OrderString_Count { get; set; }

        public int OrderString_Order { get; set; }

        public int OrderString_Pizza { get; set; }

        public OrderStringModel() {}

        public OrderStringModel(OrderString os)
        {
            OrderString_Id = os.OrderString_Id;
            OrderString_Count = os.OrderString_Count;
            OrderString_Order = os.OrderString_Order;
            OrderString_Pizza = os.OrderString_Pizza;
        }
    }
}