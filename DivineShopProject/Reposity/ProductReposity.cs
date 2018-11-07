using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class ProductReposity : IProduct
    {
        private DbConnection _connection;

        public ProductReposity(DbConnection Connection)
        {
            _connection = Connection;
          
        }

        public IEnumerable<Product> Products { get => _connection.Products; set => throw new NotImplementedException(); }

        public void Create(Product product)
        {
            _connection.Add(product);
            _connection.SaveChanges();
        }

        public Product GetById(int id)
        {
           return _connection.Products.Find(id);
        }

        public IEnumerable<Product> GetProductsByCategory(int Category)
        {
            return _connection.Products.Where(p => p.Category == Category);
        }

        public void Remove(int id)
        {
            var product = _connection.Products.Find(id);
            _connection.Products.Remove(product);
            _connection.SaveChanges();
        }

        public IEnumerable<Product> SearchUser(string value)
        {
            return _connection.Products.Where(p => p.Name.Contains(value));
           
        }

        public IEnumerable<Product> SortByAsc()
        {
            return _connection.Products.OrderBy(p => p.Name).ThenBy(p => p.Id);
        }

        public IEnumerable<Product> SortByDes()
        {
            return _connection.Products.OrderByDescending(p => p.Name).ThenBy(p=>p.Id);
        }

        public void Update(Product product)
        {
            _connection.Entry(product).State = EntityState.Modified;
            _connection.SaveChanges();
          //  _connection.SaveChanges();
        }
    }
}
