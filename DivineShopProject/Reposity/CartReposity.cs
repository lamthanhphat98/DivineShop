using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class CartReposity : ICart
    {
        private DbConnection _connection;

        public CartReposity(DbConnection Connection)
        {
            _connection = Connection;
        }
        public IEnumerable<Cart> GetCarts => _connection.Carts;



        public Cart GetById(int id)
        {
            return _connection.Carts.Find(id);
        }

        public void AddCart(Cart Cart)
        {
            _connection.Carts.Add(Cart);
            _connection.SaveChanges();
        }
        public void UpdateCart(Cart Cart)
        {         
            _connection.Entry(Cart).State = EntityState.Modified;
            _connection.SaveChanges();

        }

        public IEnumerable<Cart> GetByUSerId(string userId)
        {
            return _connection.Carts.Where(c => c.UserId == userId);
        }

        public int GetAllItems(string userId)
        {
            return _connection.Carts.Where(c => c.UserId == userId).Count();
        }

        public void Remove(Cart Cart)
        {
            _connection.Carts.Remove(Cart);

            _connection.SaveChanges();
        }

        public void RemoveAll(String username)
        {
            _connection.Carts.RemoveRange(_connection.Carts.Where(c=>c.UserId==username));
            _connection.SaveChanges();
        }

        public Cart GetCartByUser(string user, int id)
        {
            return _connection.Carts.Where(c => c.UserId == user && c.Id == id).FirstOrDefault();
            
        }
    }
}
