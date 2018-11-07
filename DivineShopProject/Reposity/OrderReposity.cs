using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class OrderReposity : IOrder
    {
        private DbConnection _connection;

        public OrderReposity(DbConnection Connection)
        {
            _connection = Connection;
        }

        public IEnumerable<Order> GetOrderDetails => _connection.OrderDetail;

        

        public void Add(Order Order)
        {
            _connection.Add(Order);
            _connection.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return _connection.OrderDetail.Find(id);
        }

        public void Remove(Order Order)
        {
            
            _connection.Remove(Order);
            _connection.SaveChanges();
        }

        public void Update(Order Order)
        {

            _connection.Entry(Order).State = EntityState.Modified;
            _connection.SaveChanges();
        }
    }
}
