using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
   public interface IOrder
    {
        IEnumerable<Order> GetOrderDetails { get; }
        void Add(Order Order);
        void Update(Order Order);
        Order GetOrderById(int id);
        void Remove(Order Order);
    }
}
